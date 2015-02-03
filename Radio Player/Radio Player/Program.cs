using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Radio_Player
{
    class Program
    {
        [DllImport(@"MouseInConsole.dll")]
        static extern int ReCord(ref int X, ref int Y);
        static void Main(string[] args)
        {
            RadioList r = new RadioList("RadioList.txt");
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            ConsoleMediaPlayer CMP = new ConsoleMediaPlayer(10, 2);
            //Radio temp = new Radio(10, 10);
            //temp.Show();
            CMP.Show();
            int X = 0;
            int Y = 0;
            while (true)
            {
                int click = ReCord(ref X, ref Y);
                CMP.Event(X, Y, click);
                CMP.mut.WaitOne();
                int index = r.Event(X, Y, click);
                if (index != -1)
                {
                    CMP.m_WMP.controls.stop();
                    CMP.URL = r.GetRadio(index).UrlStream;
                    CMP.Show();
                }
                CMP.mut.ReleaseMutex();
            }
        }

        static public string GET_http(string url)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.WebRequest reqGET = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = reqGET.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string html = sr.ReadToEnd();
            return html;
        }

        static string LuxFM()
        {
            string htmlpage = GET_http(@"http://lux.fm/player/onAir.do");
            int indexof = htmlpage.IndexOf("id=\"song-name\">");
            int lastof = htmlpage.IndexOf("</div>", indexof);
            indexof += 16;
            string namesong = htmlpage.Substring(indexof, lastof - indexof);
            namesong = namesong.Replace(@"</a>", " ");
            namesong = System.Text.RegularExpressions.Regex.Replace(namesong, @"\s+", " ");
            return namesong;
        }
    }
}
