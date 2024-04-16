using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    public class MonoLog
    {

        public static bool Log(LogConfig config, string message)
        {
            if (config == null)
            {
                Console.Error.WriteLine("Error: Configuration not found.");
                return false;
            }
            if (message == null || message.Length == 0)
            {
                Console.Error.WriteLine("Error: I've nothing to log?");
                return false;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(config.GetTime());
            sb.Append(": ");
            sb.Append(message);
            message = sb.ToString();
            try
            {
                using (StreamWriter ofi = new StreamWriter(config.GetLogFile(), true))
                {
                    ofi.Write(message);
                    ofi.Flush();
                    Console.WriteLine(message);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Log(LogConfig config, string[] args)
        {
            if (config == null)
            {
                Console.Error.WriteLine("Error: Configuration not found.");
                return false;
            }
            if (args == null || args.Length == 0)
            {
                Console.Error.WriteLine("Error: I've nothing to log?");
                return false;
            } 

            return Log(config, Flatten(args));

        }

        public static bool Log(string message)
        {
            LogConfig config = LogConfig.Load();
            return Log(config, message);
        }

        public static bool Log(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.Error.WriteLine("Error: I've nothing to log?");
                return false;
            } 
            string message = Flatten(args);
            return Log(message);
        }

        public static string Flatten(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            bool bFirst = true;
            foreach (string str in args)
            {
                if (!bFirst)
                {
                    sb.Append(" ");
                }
                sb.Append(str);
                bFirst = false;
            }
            return sb.ToString();
        }

    }
}
