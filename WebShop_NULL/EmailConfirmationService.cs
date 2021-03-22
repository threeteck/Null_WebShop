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

        public EmailConfirmationService(TimeSpan expirationTime)
        {
            _expirationTime = expirationTime;
            _tokens = new ConcurrentDictionary<string, EmailConfirmationToken>();
            _expirationTimerCancellationSource = new CancellationTokenSource();
            ExpirationTimer(_expirationTimerCancellationSource.Token);
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