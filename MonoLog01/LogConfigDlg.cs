using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    /// <summary>
    /// Whe we "go gui" we'll want to minimuze the ripple effect of change.
    /// </summary>
    public class LogConfigDlg
    {
        /// <summary>
        /// Encapsulate the user interface.
        /// </summary>
        /// <param name="outp">Where to write to.</param>
        /// <param name="inp">Where to read from.</param>
        /// <returns>Null on timeout / error.</returns>
        public static LogConfig Create(System.IO.TextWriter outp, System.IO.TextReader inp)
        {
            LogConfig result = LogConfig.Load();
            WriteTitle(outp, "Create");
            string zname;
            int times = 0;
            while (true)
            {
                outp.Write("Configuration Name [a-Z, 0-9]: ");
                zname = inp.ReadLine();
                times++;
                if (times > 3) return null;
                string zerror = LogConfig.GetConfigNameError(zname);
                if (zerror != null)
                {
                    outp.WriteLine(zerror);
                    continue;
                }
                break;

            }
            result.ConfigName = zname;
            TROOL trool = Edit(result, outp, inp);
            if (trool == TROOL.TRUE)
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Encapsulate the user interface.
        /// </summary>
        /// <param name="cfg">An existing LogConfig.</param>
        /// <param name="outp">Where to write to.</param>
        /// <param name="inp">Where to read from.</param>
        /// /// <returns>False if error / timeout.</returns>
        public static TROOL Edit(LogConfig cfg, TextWriter outp, TextReader inp)
        {
            if (cfg == null)
            {
                return TROOL.ERROR;
            }
            WriteTitle(outp, "Editing [" + cfg.ConfigName + "]");
            int times = 0;
            string zpath;
            bool bChanged = false;
            while (true)
            {
                outp.Write("Log file path: ");
                zpath = inp.ReadLine();
                times++;
                if (times > 3)
                {
                    if (bChanged)
                    {
                        return TROOL.TRUE;
                    }
                    return TROOL.FALSE;
                }
                try
                {
                    StreamWriter ofi = new StreamWriter(zpath, false);
                    ofi.WriteLine("test");
                    ofi.Close();
                    File.Delete(zpath);
                    cfg.FilePath = zpath;
                    bChanged = true;
                    break;
                }
                catch (Exception ex)
                {
                    outp.WriteLine("Error: " + ex.Message);
                    continue;
                }

            }
            return TROOL.TRUE;
        }

        /// <summary>
        /// Encapsulate the user interface.
        /// </summary>
        /// <param name="cfg">An existing LogConfig.</param>
        /// <param name="outp">Where to write to.</param>
        /// <param name="inp">Where to read from.</param>
        /// /// <returns>False if error / timeout.</returns>
        public static TROOL DisplayOrUpdate(LogConfig cfg, TextWriter outp, TextReader inp)
        {
            if (cfg == null)
            {
                return TROOL.ERROR;
            }
            WriteTitle(outp, "Review");
            int times = 0;
            bool bChanged = false;
            while (true)
            {
                outp.WriteLine(cfg.ToString());
                outp.WriteLine("Edit? [y/n]: ");
                string edit = inp.ReadLine().ToLower().Trim();
                times++;
                if (times > 3 || edit.Length < 1)
                {
                    if (bChanged)
                    {
                        return TROOL.TRUE;
                    }
                    return TROOL.FALSE;
                }
                if (edit[0] == 'n')
                {
                    if (bChanged)
                    {
                        return TROOL.TRUE;
                    } 
                    return TROOL.FALSE;
                }
                if (edit[0] == 'y')
                {
                    TROOL trool = Edit(cfg, outp, inp);
                    if (trool == TROOL.TRUE)
                    {
                        bChanged = true;
                        times = 0;
                    }
                    continue;
                }
            }
        }

        private static void WriteTitle(System.IO.TextWriter outp, string p)
        {
            outp.WriteLine("Log Configurator: " + p);
            outp.WriteLine("~~~~~~~~~~~~~~~~");
        }

    }
}
