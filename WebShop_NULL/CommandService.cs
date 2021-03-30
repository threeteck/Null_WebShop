using System.Collections.Concurrent;
using System.Linq;

namespace WebShop_NULL
{
    public delegate bool CommandAction(params string[] args);
    public class CommandService
    {
        private ConcurrentDictionary<string, CommandAction> _commands;

        public CommandService()
        {
            _commands = new ConcurrentDictionary<string, CommandAction>();
        }

        public void AddCommand(string keyword, CommandAction action)
        {
            _commands.TryAdd(keyword, action);
        }

        public bool ExecuteCommand(string command)
        {
            var splited = command.Split(' ');

            if (splited.Length <= 0 || 
                !_commands.TryGetValue(splited[0], out var action))
                return false;
            
            return action(splited.Skip(1).ToArray());
        }
    }
}