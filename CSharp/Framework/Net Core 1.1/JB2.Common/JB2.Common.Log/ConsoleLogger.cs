using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Log
{
    public class ConsoleLogger : Logger
    {
        public override Logit GetLogMethod()
        {
            return new Logit(ConsoleLogit);
        }

        public override async Task LogDebugMessageAsync(string message, string logcode = "")
        {
            var entry = new LogEntry(JB2.Common.NewID.ShortGuid(),Enum.LogServerityType.Debug, message, null, DateTime.Now, logcode);

            await LogAsync(entry);
            
        }

        public override async Task LogErrorAsync(Exception ex, string message = "", string logcode = "")
        {
            var entry = new LogEntry(JB2.Common.NewID.ShortGuid(), Enum.LogServerityType.Error, message, ex, DateTime.Now, logcode);

            await LogAsync(entry);
        }

        public override async Task LogMessageAsync(string message, string logcode = "")
        {
            var entry = new LogEntry(JB2.Common.NewID.ShortGuid(), Enum.LogServerityType.Informational, message, ex, DateTime.Now, logcode);

            await LogAsync(entry);
        }

        public static ServiceResult ConsoleLogit(ILogEntry e)
        {
            Console.WriteLine(e.Message);

            return true;
        }
    }
}
