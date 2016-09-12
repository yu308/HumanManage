using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace HumanManage.Helpers
{
    public class HtmlToPdfHelper
    {
        public class ConvertHtmlToPDF
        {

            //private static ILog _log = LogManager.GetLogger(typeof(HtmlToPDF));

            /**/

            /// <summary>
            /// HTML Convert to PDF
            /// </summary>
            /// <param name="url">URL</param>
            /// <param name="path">PDF File Path</param>
            public static bool HtmlToPdf(string url, string path)
            {
                try
                {
                    if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(path))
                    {
                        return false;
                    }

                    using (Process p = new Process())
                    {

                        string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\topdf\wkhtmltopdf.exe");

                        if (!File.Exists(str))
                        {
                            return false;
                        }

                        KillWKHtmltoPDF();
                        p.StartInfo.FileName = str;
                        p.StartInfo.Arguments = String.Format("\"{0}\" \"{1}\"", url, path);
                        p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardInput = true;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.StartInfo.CreateNoWindow = true;
                        p.Start();
                        return ConfirmConvertSuccess(path);
                    }

                }

                catch (Exception ex)
                {
                    
                }

                return false;

            }

            private static bool ConfirmConvertSuccess(string path)
            {

                int count = 0;

                bool isSuccessful = true;

                while (true)
                {

                    if (File.Exists(path))
                    {
                        WaitWKHtmltoPDFClose();
                        break;
                    }

                    Thread.Sleep(1000);

                    count++;

                    if (count >= 300)
                    {
                        isSuccessful = false;
                        break;
                    }

                }

                return isSuccessful;

            }

            private static void WaitWKHtmltoPDFClose()
            {
                while (true)
                {
                    Process[] procs = Process.GetProcessesByName("wkhtmltopdf");
                    if (procs.Length > 0)
                    {
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        break;
                    }
                }

            }

            /**/

            /// <summary>

            /// Kill WKHTMLTOPDF exe

            /// </summary>

            private static void KillWKHtmltoPDF()
            {

                try
                {
                    Process[] procs = Process.GetProcessesByName("wkhtmltopdf");
                    Array.ForEach(procs,
                    delegate(Process proc)
                    {
                        proc.Kill();
                    });

                }

                catch (Exception ex)
                {

                }

            }

        }

    }
}