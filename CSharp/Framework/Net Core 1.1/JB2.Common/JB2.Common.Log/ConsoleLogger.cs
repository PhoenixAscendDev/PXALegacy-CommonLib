using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common.Log
{
    public class ConsoleLogger : Logger
    {
        #region Fields

        internal static ConsoleLogger _instance = null;
        internal static readonly object padlock = new object();
        #endregion Fields

        #region Constructors

        public ConsoleLogger() : base()
        {

        }

        #endregion Constructors


        public override Logit GetLogMethod()
        {
            return new Logit(consoleLogit);
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
            var entry = new LogEntry(JB2.Common.NewID.ShortGuid(), Enum.LogServerityType.Informational, message, null, DateTime.Now, logcode);

            await LogAsync(entry);
        }

        internal static ServiceResult consoleLogit(ILogEntry e)
        {

            ConsoleColor typeColor = ConsoleColor.White;

            switch(e.Serverity)
            {
                case Enum.LogServerityType.Debug:
                    typeColor = ConsoleColor.Green;
                    break;
                case Enum.LogServerityType.Error:
                    typeColor = ConsoleColor.Red;
                    break;
                case Enum.LogServerityType.Informational:
                    typeColor = ConsoleColor.Yellow;
                    break;
                default:
                    typeColor = ConsoleColor.White;
                    break;
            }
            Console.ResetColor();
            Console.ForegroundColor = typeColor;
            Console.Write(e.Serverity.ToString().PadLeft(20,' ') );
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": " + e.Message);
            Console.WriteLine();

            return true;
        }

        public static ConsoleLogger Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        ConsoleLogger c = new ConsoleLogger();
                        _instance = c;
                    }
                    return _instance;
                }

            }
        }
    }
}
