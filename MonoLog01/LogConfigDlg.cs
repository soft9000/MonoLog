﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    /// <summary>
    /// When we "go gui" we'll want to minimuze the ripple effect of change.
    /// </summary>
    public class LogConfigDlg
    {
        /// <summary>
        /// Encapsulate the user interface.
        /// </summary>
        /// <param name="outp">Where to write to.</param>
        /// <param name="inp">Where to read from.</param>
        /// <returns>Null on timeout / error.</returns>
        public static LogConfig Create(IoSet p)
        {
            LogConfig result = LogConfig.Load();
            TUI.Title("Configuration: ", "Create", p.Out);
            string zname;
            int times = 0;
            while (true)
            {
                TUI.Message("Configuration Name [a-Z, 0-9]: ", p.Out);
                zname = p.In.ReadLine();
                times++;
                if (times > 3) return null;
                string zerror = LogConfig.GetConfigNameError(zname);
                if (zerror != null)
                {
                    TUI.Message(zerror, p.Out);
                    continue;
                }
                break;

            }
            result.ConfigName = zname;
            TROOL trool = Edit(result, p);
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
        public static TROOL Edit(LogConfig cfg, IoSet p)
        {
            if (cfg == null)
            {
                return TROOL.ERROR;
            }
            TUI.Title("Configuration: ", "Editing", p.Out);
            int times = 0;
            string zpath;
            bool bChanged = false;
            while (true)
            {
                p.Out.Write("Log file path: ");
                zpath = p.In.ReadLine();
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
                    TUI.Message("test", ofi);
                    ofi.Close();
                    File.Delete(zpath);
                    cfg.FilePath = zpath;
                    bChanged = true;
                    break;
                }
                catch (Exception ex)
                {
                    TUI.Message("Error: " + ex.Message, p.Out);
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
        public static TROOL DisplayOrUpdate(LogConfig cfg, IoSet p)
        {
            if (cfg == null)
            {
                return TROOL.ERROR;
            }
            TUI.Title("Configuration: ", "Review", p.Out);
            int times = 0;
            bool bChanged = false;
            while (true)
            {
                TUI.Message(cfg.ToString(), p.Out);
                TUI.Message("Edit? [y/n]: ", p.Out);
                string edit = p.In.ReadLine().ToLower().Trim();
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
                    TROOL trool = Edit(cfg, p);
                    if (trool == TROOL.TRUE)
                    {
                        bChanged = true;
                        times = 0;
                    }
                    continue;
                }
            }
        }


    }
}
