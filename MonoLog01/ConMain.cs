using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    class ConMain
    {

        public delegate bool LogOption(string opt, string[] opts);

        public static void usage()
        {
            Console.WriteLine("MonoLog Version 0.0.1 - Basic Logging");
            Console.WriteLine("Usage:");
            Console.WriteLine("TODO: MonoLog --config");
            Console.WriteLine("                 Interactively create a new configuration.");
            Console.WriteLine("TODO: MonoLog --config cfg-name");
            Console.WriteLine("                 Show a predefined configuration.");
            Console.WriteLine("TODO: MonoLog --config cfg-name ... ");
            Console.WriteLine("                 Log via a predefined configuration.");
            Console.WriteLine("MonoLog ... ");
            Console.WriteLine("                 Log to ./mono.log file.");
        }

        public static bool config(string sFlag, string[] opts)
        {
            if (sFlag.Equals("--config") && opts.Length == 1)
            {
                // create a config
            }
            if (sFlag.Equals("--config") && opts.Length == 2)
            {
                // show a config
            } 
            if (sFlag.Equals("--config") && opts.Length >= 2)
            {
                // use a config
            }
            return false;
        }


        static LogOption GetOption(string sFlag)
        {
            if (sFlag.Equals("--config"))
            {
                return config;
            }
            return null;
        }

        static LogConfig getDefaultFile()
        {
            return LogConfig.Load();
        }

        static bool MonoLog(string message)
        {
            bool br = false;
            LogConfig file = getDefaultFile();
            using (StreamWriter ofi = new StreamWriter(file.getFilePath(), true))
            {
                ofi.Write(file.getDefaultTime());
                ofi.Write(":\t");
                ofi.WriteLine(message);
                ofi.Flush();
                ofi.Close();
                br = true;
            }           
            return br;
        }

        static bool MonoLog(string[] args)
        {
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
            return MonoLog(sb.ToString());
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                usage();
                return;
            }
            foreach (string str in args)
            {
                LogOption opt = GetOption(str);
                if (opt != null)
                {
                    if (opt(str, args) == false)
                    {
                        Console.Error.WriteLine("Error: MonoLog Option");
                        Environment.Exit(1);
                    }
                    Environment.Exit(0);
                }

                if (MonoLog(args) == false)
                {
                    Console.Error.WriteLine("Error: MonoLog Operation");
                    Environment.Exit(1);
                }
                Environment.Exit(0);

            }
        }
    }
}
