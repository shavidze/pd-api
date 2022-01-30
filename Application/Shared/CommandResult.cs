using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Shared
{
    public class CommandResult
    {
        public object Data { get; set; }

        public bool IsSucceeded { get; set; }

        public string Message { get; set; }

        public static CommandResult Succeeded() => new CommandResult { IsSucceeded = true };

        public static CommandResult Succeeded(object data) => new CommandResult { IsSucceeded = true, Data = data };

        public static CommandResult Error() => new CommandResult();

        public static CommandResult Error(string message) => new CommandResult { Message = message };
    }
}
