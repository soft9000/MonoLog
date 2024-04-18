﻿using System;
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
        #region REGION 1: LogOptions
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
            TUI.Title("MonoLog Version 0.0.3: ", "Basic Logging", Console.Out);
            TUI.Message("Usage:", Console.Out);
            TUI.Message("mlog.exe", Console.Out);
            TUI.Message("                 Display this help information.", Console.Out);
            TUI.Message("mlog.exe ... ", Console.Out);
            TUI.Message("                 Log to ./mono.log file.", Console.Out);
            TUI.Message("mlog.exe --config", Console.Out);
            TUI.Message("                 Interactively create a new configuration.", Console.Out);
            TUI.Message("mlog.exe --config cfg-name", Console.Out);
            TUI.Message("                 Show a predefined configuration.", Console.Out);
            TUI.Message("mlog.exe --config cfg-name ... ", Console.Out);
            TUI.Message("                 Log via a predefined configuration.", Console.Out);
            /*
            TUI.Message("mlog.exe --config.list", Console.Out);
            TUI.Message("                 Display configuration names.", Console.Out);
            TUI.Message("mlog.exe --exe config.[l|e|u|d].number ", Console.Out);
            TUI.Message("                 Perform operation on item number in config log. (*)", Console.Out);
            TUI.Message("                 l = [l]ist from item number.", Console.Out);
            TUI.Message("                 e = interactively [e]dit item number.", Console.Out);
            TUI.Message("                 u = [u]pdate item number.", Console.Out);
            TUI.Message("                 d = [d]elete item number.", Console.Out);
            TUI.Message("mlog.exe --pipe config", Console.Out);
            TUI.Message("                 Pipe fitting: Logs standard input to `config`. (*)", Console.Out);
            TUI.Message("                 Example:  `ls | mono mlog.exe -p mono`", Console.Out);
            TUI.Message("                 Example:  `dir | mlog -p mono`", Console.Out);
            TUI.Message("(*) NOTE: Use `mono` for `./mono.log` for `--exe` and `--pipe`.", Console.Out);
            */
        }

        /// <summary>
        /// List configuration names.
        /// </summary>
        /// <param name="which">The location of the 'opt' in the 'ops'</param>
        /// <param name="opt">The 'opt' from the 'ops' - convenience.</param>
        /// <param name="opts">The full set of parameters from the command-line.</param>
        /// <returns>Troolean is the best way to manage any stdout, stderror reporting.</returns>
        public static TROOL config_list(int where, string sFlag, string[] opts)
        {
            string[] names = MonoHome.GetConfigNames();
            TUI.Message("User-Defined Configurations:", Console.Out);
            for (int ss = 0; ss < names.Length;ss++ )
            {
                TUI.Message(String.Format("{0}.) {1}", ss+1, names[ss]), Console.Out);
            }
            return TROOL.TRUE;
        }

        /// <summary>
        /// Execute the operation against a configuration's log file.
        /// </summary>
        /// <param name="which">The location of the 'opt' in the 'ops'</param>
        /// <param name="opt">The 'opt' from the 'ops' - convenience.</param>
        /// <param name="opts">The full set of parameters from the command-line.</param>
        /// <returns>Troolean is the best way to manage any stdout, stderror reporting.</returns>
        public static TROOL exe(int where, string sFlag, string[] opts)
        {
            if (sFlag.Equals("--exe"))
            {
            }
            return TROOL.TRUE;
        }

        /// <summary>
        /// Pipe a series of message to a configuration's log file.
        /// </summary>
        /// <param name="which">The location of the 'opt' in the 'ops'</param>
        /// <param name="opt">The 'opt' from the 'ops' - convenience.</param>
        /// <param name="opts">The full set of parameters from the command-line.</param>
        /// <returns>Troolean is the best way to manage any stdout, stderror reporting.</returns>
        public static TROOL pipe(int where, string sFlag, string[] opts)
        {
            if (sFlag.Equals("--pipe"))
            {
            }
            return TROOL.TRUE;
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
                    TUI.Message("Error: Configuration aborted.", Console.Error);
                    return TROOL.ERROR;
                }
                if (!LogConfig.Save(cfg))
                {
                    TUI.Message("Error: Unable to write [" + cfg.ConfigName + "].", Console.Error); 
                    return TROOL.ERROR;
                }
                TUI.Message("Success: Created [" + cfg.ConfigName + "]", Console.Out); 
                return TROOL.TRUE;
            }
            if (sFlag.Equals("--config") && opts.Length == 2)
            {
                // display and / or update a config
                LogConfig cfg = LogConfig.LoadConfig(opts[1]);
                if (cfg == null)
                {
                    TUI.Message("Error: Unable to configure [" + opts[1] + "]: Please create it?", Console.Error); 
                    return TROOL.ERROR;
                }
                TROOL br = LogConfigDlg.DisplayOrUpdate(cfg, Console.Out, Console.In);
                if (br == TROOL.TRUE)
                {
                    if (!LogConfig.Save(cfg))
                    {
                        TUI.Message("Error: Unable to write [" + cfg.ConfigName + "].", Console.Error); 
                        return TROOL.ERROR;
                    }
                    else
                    {
                        TUI.Message("Updated [" + opts[1] + "].", Console.Out); 
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
                    TUI.Message("Error: Unable to configure [" + opts[1] + "]: Please create it?", Console.Error); 
                    return TROOL.ERROR;
                }
                TUI.Message("Loaded [" + cfg.ConfigName + "] ...", Console.Out); 
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
        #endregion

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
                        TUI.Message("Error: MonoLog Option", Console.Error);
                        return 1;
                    }
                    return 0;
                }

            }

            if (MonoLog.Log(MonoLog.Flatten(args)) == false)
            {
                TUI.Message("Error: MonoLog Operation", Console.Error); 
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
