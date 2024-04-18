using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    /// <summary>
    /// Visual segregation of TUI 'Ops.
    /// </summary>
    public class TUI
    {       
        /// <summary>
        /// The "major header" style.
        /// </summary>
        /// <param name="title">Theme</param>
        /// <param name="subject">Operation</param>
        /// <param name="outp">Stream</param>
        public static void Title(string title, string subject, System.IO.TextWriter outp)
        {
            if (outp == null)
            {
                outp = Console.Error;
            } 
            if (title == null)
            {
                title = "Info: ";
            }
            if (subject == null)
            {
                subject = "";
            }

            outp.WriteLine(title + subject);
            outp.WriteLine(new String('=', title.Length));
        }

        /// <summary>
        /// The "minor header" style.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="outp">Stream</param>
        public static void Subtitle(string message, TextWriter outp)
        {
            if (outp == null)
            {
                outp = Console.Error;
            }
            if (message == null)
            {
                message = "";
            }
            outp.WriteLine(message);
            outp.WriteLine(new String('~', message.Length));
        }

        /// <summary>
        /// The "message" style.
        /// </summary>
        /// <param name="message">Message to display.</param>
        /// <param name="outp">Stream</param>
        public static void Message(string message, TextWriter outp)
        {
            if (outp == null)
            {
                outp = Console.Error;
            }
            if (message == null)
            {
                message = "";
            }
            outp.WriteLine(message);
        }


    }
}
