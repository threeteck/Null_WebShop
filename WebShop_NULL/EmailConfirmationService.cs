using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace WebShop_NULL
{
    public class EmailConfirmationService : IDisposable
    {
        private class EmailConfirmationToken
        {
            public int Id;
            public DateTime CreationTime;

            public EmailConfirmationToken(int id, DateTime creationTime)
            {
                Id = id;
                CreationTime = creationTime;
            }
        }

        private ConcurrentDictionary<string, EmailConfirmationToken> _tokens;
        private ConcurrentDictionary<int, string> _keys;
        private readonly TimeSpan _expirationTime;

        private CancellationTokenSource _expirationTimerCancellationSource;
        private bool _isDisposed = false;

        private CommandService _commandService;

        public EmailConfirmationService(TimeSpan expirationTime, CommandService commandService)
        {
            _expirationTime = expirationTime;
            _commandService = commandService;
            _tokens = new ConcurrentDictionary<string, EmailConfirmationToken>();
            _keys = new ConcurrentDictionary<int, string>();

            _commandService.AddCommand("TokenClear", ClearTokensCommand);
            
            _expirationTimerCancellationSource = new CancellationTokenSource();
            ExpirationTimer(_expirationTimerCancellationSource.Token); //Done to get rid of build warning
        }

        public string GenerateEmailConfirmationToken(int userId)
        {
            var key = Guid.NewGuid().ToString();
            var token = new EmailConfirmationToken(userId, DateTime.Now);

            if (_keys.ContainsKey(userId) && _keys.TryRemove(userId, out var oldKey))
                _tokens.TryRemove(oldKey, out var oldToken);

            if (_tokens.TryAdd(key, token))
                _keys.TryAdd(userId, key);

            return key;
        }

        public int ConfirmEmail(string key)
        {
            var id = -1;
            if (_tokens.TryRemove(key, out var token))
            {
                id = token.Id;
                _keys.TryRemove(id, out var oldKey);
            }

            return id;
        }

        private void ExpireToken(string key)
        {
            if (_tokens.TryRemove(key, out var token))
            {
                _keys.TryRemove(token.Id, out key);
            }
        }

        private async Task ExpirationTimer(CancellationToken cancellationToken)
        {
            await Task.Yield();
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var (key, token) in _tokens)
                {
                    if (DateTime.Now - token.CreationTime > _expirationTime)
                        ExpireToken(key);
                }

                try
                {
                    await Task.Delay(60000, cancellationToken);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void ClearTokens()
        {
            foreach (var (key, token) in _tokens)
            {
                ExpireToken(key);
            }
        }

        private bool ClearTokensCommand(out string message, params string[] args)
        {
            ClearTokens();
            message = "Все токены авторизации удалены";
            return true;
        }
        
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _expirationTimerCancellationSource.Cancel();
                _expirationTimerCancellationSource.Dispose();
            }
        }
    }
}