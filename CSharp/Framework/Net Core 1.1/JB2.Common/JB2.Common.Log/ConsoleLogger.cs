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

            LogAsync(entry).Wait();
            
        }

        public override async Task LogErrorAsync(Exception ex, string message = "", string logcode = "")
        {
            var entry = new LogEntry(JB2.Common.NewID.ShortGuid(), Enum.LogServerityType.Error, message, ex, DateTime.Now, logcode);

            LogAsync(entry).Wait();
        }

        public override async Task LogMessageAsync(string message, string logcode = "")
        {
            var entry = new LogEntry(JB2.Common.NewID.ShortGuid(), Enum.LogServerityType.Informational, message, null, DateTime.Now, logcode);

            LogAsync(entry).Wait();
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
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;          
                Console.WriteLine("<log id:" + e.ID + ">");
            Console.ResetColor();


            //print the Log Type
            Console.ForegroundColor = typeColor;
            Console.Write(e.Serverity.ToString());
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": ");
            Console.ResetColor();

            //If code exists, print it out
            if (!string.IsNullOrEmpty(e.LogCode))
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[" + e.LogCode + "] => ");
                Console.ResetColor();
            }

            //print the message
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(e.Message);
            Console.ResetColor();

            if(e.Serverity == Enum.LogServerityType.Error)
            {
                Console.WriteLine();
                Console.Write("   Stack Trace => " + e.Exception.StackTrace);
            }



            // end the log
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("</log id:" + e.ID + ">");
            Console.ResetColor();


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
                        c.SetAllServerity(true);
                        _instance = c;
                    }
                    return _instance;
                }

            }
        }

        public static ConsoleLogger OnlyLogErrors()
        {
            var result = new ConsoleLogger();
            result.SetAllServerity(false);
            result.SetServerityType(Enum.LogServerityType.Error, true);

            return result;
        }
    }
}
