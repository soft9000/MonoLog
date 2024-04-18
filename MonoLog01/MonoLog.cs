using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    /// <summary>
    /// "He starts Monologging ..."  ;-)
    /// </summary>
    public class MonoLog
    {

        /// <summary>
        /// Use a configuration file.
        /// </summary>
        /// <param name="config">Active / testable LogConfig.</param>
        /// <param name="message">The bare message - no time string.</param>
        /// <returns>True when the message has been saved.</returns>
        public static bool Log(LogConfig config, string message)
        {
            if (config == null)
            {
                TUI.Message("Error: Configuration not found.", null);
                return false;
            }
            if (message == null || message.Length == 0)
            {
                TUI.Message("Error: I've nothing to log?", null);
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
                    ofi.WriteLine(message);
                    ofi.Flush();
                    TUI.Message(message, Console.Out);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Create a message from a series of words. Spaces will be inserted.
        /// </summary>
        /// <param name="config">Active / testable LogConfig.</param>
        /// /// <param name="args">A series of words to be made into a space-delimited message.</param>
        /// <returns>True when the message has been saved.</returns>
        public static bool Log(LogConfig config, string[] args)
        {
            if (config == null)
            {
                TUI.Message("Error: Configuration not found.", null);
                return false;
            }
            if (args == null || args.Length == 0)
            {
                TUI.Message("Error: I've nothing to log?", null);
                return false;
            } 

            return Log(config, Flatten(args));

        }

        /// <summary>
        /// Send a message to the log file. 
        /// </summary>
        /// <param name="message">Undated message to append to the default (`pwd`) log file.</param>
        /// <returns>True when the message has been saved.</returns>
        public static bool Log(string message)
        {
            LogConfig config = LogConfig.Load();
            return Log(config, message);
        }

        public static bool Log(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                TUI.Message("Error: I've nothing to log?", null);
                return false;
            } 
            string message = Flatten(args);
            return Log(message);
        }

        /// <summary>
        /// Merge words into a space-delimited message.
        /// </summary>
        /// <param name="args">Words to merge.</param>
        /// <returns>True when the message has been saved.</returns>
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
