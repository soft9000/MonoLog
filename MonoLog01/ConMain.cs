using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    /// <summary>
    /// Managing the console user interface. Destined to be a TUIGUI.
    /// </summary>
    public class ConMain
    {
        /// <summary>
        /// The "pointer to a function" way to manage any growing set of command-line options.
        /// </summary>
        /// <param name="which">The location of the 'opt' in the 'ops'</param>
        /// <param name="opt">The 'opt' from the 'ops' - convenience.</param>
        /// <param name="opts">The full set of parameters from the command-line.</param>
        /// <returns>Troolean is the best way to manage any stdout, stderror reporting.</returns>
        public delegate TROOL LogOption(int which, string opt, string[] opts);

        public static void usage()
        {
            Console.WriteLine("MonoLog Version 0.0.2 - Basic Logging");
            Console.WriteLine("Usage:");
            Console.WriteLine("MonoLog --config");
            Console.WriteLine("                 Interactively create a new configuration.");
            Console.WriteLine("MonoLog --config cfg-name");
            Console.WriteLine("                 Show a predefined configuration.");
            Console.WriteLine("MonoLog --config cfg-name ... ");
            Console.WriteLine("                 Log via a predefined configuration.");
            Console.WriteLine("MonoLog ... ");
            Console.WriteLine("                 Log to ./mono.log file.");
        }

        /// <summary>
        /// Configuration management is our first delegate to manage.
        /// </summary>
        /// <param name="which">The location of the 'opt' in the 'ops'</param>
        /// <param name="opt">The 'opt' from the 'ops' - convenience.</param>
        /// <param name="opts">The full set of parameters from the command-line.</param>
        /// <returns>Troolean is the best way to manage any stdout, stderror reporting.</returns>
        public static TROOL config(int where, string sFlag, string[] opts)
        {
            if (sFlag.Equals("--config") && opts.Length == 1)
            {
                // create a new config
                LogConfig cfg = LogConfigDlg.Create(Console.Out, Console.In);
                if (cfg == null)
                {
                    Console.Error.WriteLine("Error: Configuration aborted.");
                    return TROOL.ERROR;
                }
                if (!LogConfig.Save(cfg))
                {
                    Console.Error.WriteLine("Error: Unable to write [" + cfg.ConfigName + "].");
                    return TROOL.ERROR;
                }
                Console.Out.WriteLine("Success: Created [" + cfg.ConfigName + "]");
                return TROOL.TRUE;
            }
            if (sFlag.Equals("--config") && opts.Length == 2)
            {
                // display and / or update a config
                LogConfig cfg = LogConfig.LoadConfig(opts[1]);
                if (cfg == null)
                {
                    Console.Error.WriteLine("Error: Unable to configure [" + opts[1] + "]: Please create it?");
                    return TROOL.ERROR;
                }
                TROOL br = LogConfigDlg.DisplayOrUpdate(cfg, Console.Out, Console.In);
                if (br == TROOL.TRUE)
                {
                    if (!LogConfig.Save(cfg))
                    {
                        Console.Error.WriteLine("Error: Unable to update [" + opts[1] + "].");
                        return TROOL.ERROR;
                    }
                    else
                    {
                        Console.Out.WriteLine("Updated [" + opts[1] + "].");
                    }
                }
                return TROOL.TRUE;
            }
            if (sFlag.Equals("--config") && opts.Length >= 2)
            {
                // use a config
                LogConfig cfg = LogConfig.LoadConfig(opts[1]);
                if (cfg == null)
                {
                    Console.Error.WriteLine("Error: Unable to configure [" + opts[1] + "]: Please create it?");
                    return TROOL.ERROR;
                }
                Console.Out.WriteLine("Loaded [" + cfg.ConfigName + "] ...");
                List<string> alist = new List<string>();
                for (int ss = where + 2; ss < opts.Length; ss++)
                {
                    alist.Add(opts[ss]);
                }
                if (!MonoLog.Log(cfg, alist.ToArray())) { return TROOL.ERROR; }
            }
            return TROOL.TRUE;
        }

        /// <summary>
        /// Each command-line option is parsed to check for a delegate.
        /// </summary>
        /// <param name="sFlag">Single CLI string, as present, in any passed parameters.</param>
        /// <returns>Return null if no LogOption is present.</returns>
        static LogOption GetOption(string sFlag)
        {
            if (sFlag.Equals("--config"))
            {
                return config;
            }
            return null;
        }

        /// <summary>
        /// The default, fallback, in-memory log configuration. Always the starting point for any
        /// new configurations (etc.)
        /// </summary>
        /// <returns></returns>
        static LogConfig getDefaultFile()
        {
            return LogConfig.Load();
        }

        /// <summary>
        /// The minimalist's way to test any CLI, methinks.
        /// </summary>
        /// <param name="args">Instrumentation params.</param>
        /// <returns></returns>
        public static int DoMain(string[] args)
        {
            if (args.Length == 0)
            {
                usage();
                return 1;
            }
            for (int ss = 0; ss < args.Length; ss++)
            {
                string str = args[ss];
                LogOption opt = GetOption(str);
                if (opt != null)
                {
                    if (opt(ss, str, args) == TROOL.ERROR)
                    {
                        Console.Error.WriteLine("Error: MonoLog Option");
                        return 1;
                    }
                    return 0;
                }

            }

            if (MonoLog.Log(MonoLog.Flatten(args)) == false)
            {
                Console.Error.WriteLine("Error: MonoLog Operation");
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// The run-time interface to the testable interface.
        /// </summary>
        /// <param name="args">Obviously passed-in.</param>
        public static void Main(string[] args)
        {
            Environment.Exit(DoMain(args));
        }
    }
}
