using System;
using System.Text;
using System.Threading;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Enums;
using System.Net;

namespace Radio_Player
{
    class Song
    {
        public delegate void MethodContainer(string music);
        public event MethodContainer NewSong;

        private string[] m_title;
        private string m_Singer = "";
        private string m_Song = "";
        private string m_Address;
        private string[] m_Url;
        private int m_Number = 0;
        private VkApi vk;
        private Thread download;
        private Lyrics[] m_lyric;
        public int m_counter = 0;
        public bool m_activ = false;
        public Tab downloadTab;

        public int Number
        {
            get { return m_Number; }
            set { m_Number = value; }
        }

        public void SetAddress(string Address)
        {
            m_Address = Address;
        }

        public Song(string addres)
        {
            m_lyric = new Lyrics[10];
            m_title = new string[10];
            m_Url = new string[10];
            m_Address = addres;
            download = new Thread(Go);
            download.Start();
        }

        public void Go()
        {
            VkAuthorize();
            while (true)
            {
                bool ok = false;
                if (m_Address == "http://lux.fm/player/onAir.do")
                    ok = LuxFM();
                else
                    ok = Tavrmedia();

                if (ok)
                {
                    if (NewSong != null)
                        NewSong(m_Singer + " - " + m_Song);
                    try { SearchSong(); }
                    catch (Exception) { m_Singer = "none"; }
                }
                Thread.Sleep(500);
            }
        }

        public string GET_http(string url)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.WebRequest reqGET = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = reqGET.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string html = sr.ReadToEnd();
            return html;
        }

        private void VkAuthorize()
        {
            int appId = 12345; // указываем id приложения
            string email = "+380931670003"; // email для авторизации
            string password = "radioPlayer"; // пароль
            Settings settings = Settings.Audio; // уровень доступа к данным
            vk = new VkApi();
            vk.Authorize(appId, email, password, settings); // авторизуемся
        }

        public void SearchSong()
        {
            int totalCount;
            string seachString = m_Singer + " " + m_Song;
            var audios = vk.Audio.Search(seachString, out totalCount, true, AudioSort.Popularity, true, 10);
            m_counter = audios.Count;
            for (int i = 0; i < audios.Count; i++)
            {             
                m_lyric[i] = vk.Audio.GetLyrics((long)audios[i].LyricsId);
                m_title[i] = audios[i].Artist + " - " + audios[i].Title;
                m_Url[i] = audios[i].Url + ""; //ссылка для скачивания
            }
            for (int i = 0; i < m_lyric.Length; i++)
            {
                if (m_lyric[i].Text.Length > 300)
                {
                    m_Number = i;
                    break;
                }
            }
            Print();
        }

        public void Print()
        {
            if (m_counter > 0 && m_activ && m_Number < m_counter)
            {
                Clear();              
                int space = (64 - m_title[m_Number].Length) / 2;
                if (m_title[m_Number].Length > 64)
                    space = 0;
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.SetCursorPosition(8 + space, 3);
                if (m_title[m_Number].Length > 64)
                    Console.Write(m_title[m_Number].Substring(0, 61) + "...");
                else
                    Console.Write(m_title[m_Number]);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(0, 6);
                Console.Write(m_lyric[m_Number].Text);
                Console.SetCursorPosition(0, 0);
            }
        }
        private void Clear()
        {
            GlobalMutex.GetMutex.WaitOne();
            Console.SetCursorPosition(8, 3);
            Console.Write("                                                                ");
            Console.SetCursorPosition(0, 6);
            for (int i = 0; i < 300; i++)
                Console.Write("                                                            ");
            GlobalMutex.GetMutex.ReleaseMutex();
        }

        public void DownloadSong()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileAsync(new Uri(m_Url[m_Number]), "Музыка/" + m_Singer + " - " + m_Song + ".mp3");
            webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
        }

        public void webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (downloadTab != null)
            {
                downloadTab.Status = 0;
                downloadTab.Update(0);
            }
        }

        public bool LuxFM()
        {
            string htmlpage;
            try
            {
                htmlpage = GET_http(m_Address);
            }
            catch (Exception)
            {
                return false;
            }
            string[] separator = { "id=\"song-name\">", "</div>", "</a>" };
           // string[] separator = { "\"singer\":\"", "\",\"song_id\"", "\",\"song\":\"", "\r\n" };
            int indexof = htmlpage.IndexOf(separator[0]);
            int lastof = htmlpage.IndexOf(separator[1], indexof);
            htmlpage = htmlpage.Substring(indexof, lastof - indexof); 
            htmlpage = System.Text.RegularExpressions.Regex.Replace(htmlpage, @"\s+", " ");
            htmlpage = Coding(htmlpage);
            string[] namesong = htmlpage.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (m_Singer != namesong[1] || m_Song != namesong[0])
            {
                m_Singer = namesong[1];
                m_Song = namesong[0];
                return true;
            } 
            return false;
        }

        public bool Tavrmedia()
        {
            string htmlpage;
            try
            {
                htmlpage = GET_http(m_Address);
            }
            catch (Exception)
            {
                return false;
            }
            string[] separators = { "\"singer\":\"", "\",\"song\":\"", "\",\"", "\r\n" };
            int indexof = htmlpage.IndexOf(separators[0]);
            int lastof = htmlpage.IndexOf(separators[2], indexof);
            if (lastof - indexof < 1)
                return false;
            string singer = htmlpage.Substring(indexof, lastof - indexof);
            singer = singer.Replace(separators[0], "");
            singer = singer.Replace("\r\n", "");
            singer = Coding(singer);
            indexof = htmlpage.IndexOf(separators[1]);
            lastof = htmlpage.IndexOf(separators[2], indexof + 3);
            if (lastof - indexof < 1)
                return false;
            string song = htmlpage.Substring(indexof, lastof - indexof);
            song = song.Replace(separators[1], "");
            song = song.Replace("\r\n", "");
            song = Coding(song);

            if(m_Singer != singer || m_Song != song)
            {
                m_Singer = singer;
                m_Song = song;
                return true;
            }        
            return false;
        }
        public string Coding(string str)
        {
            string[] Ar1 = {
            @"\u0430",@"\u0431",@"\u0432",@"\u0433",@"\u0434",
            @"\u0435",@"\u0436",@"\u0437",@"\u0438",@"\u0439",
            @"\u043A",@"\u043B",@"\u043C",@"\u043D",@"\u043E",
            @"\u043F",@"\u0440",@"\u0441",@"\u0442",@"\u0443",
            @"\u0444",@"\u0445",@"\u0446",@"\u0447",@"\u0448",
            @"\u0449",@"\u044A",@"\u044B",@"\u044C",@"\u044D",
            @"\u044E",@"\u044F",@"\u0451",@"\u0410",@"\u0411",
            @"\u0412",@"\u0413",@"\u0414",@"\u0415",@"\u0416",
            @"\u0417",@"\u0418",@"\u0419",@"\u041A",@"\u041B",
            @"\u041C",@"\u041D",@"\u041E",@"\u041F",@"\u0420",
            @"\u0421",@"\u0422",@"\u0423",@"\u0424",@"\u0425",
            @"\u0426",@"\u0427",@"\u0428",@"\u0429",@"\u042A",
            @"\u042B",@"\u042C",@"\u042D",@"\u042E",@"\u042F",
            @"\u0401",@"\u041d",@"\u043d",@"\u044a",@"\u043a",
            @"\u044c",@"\u043b",@"\u043c",@"\u044f",@"\u043e",
            @"\u044b",@"\u041a",@"\u043f",@"\u044e",@"\u0456",
            @"\u041c",@"\u0454",@"\u041b",@"\u041f",@"\u041e",
            @"\u042e",@"\u042f",@"\u042d",@"\u044d" };

            string[] Ar2 = {
            "а","б","в","г","д","е","ж","з","и","й","к",
            "л","м","н","о","п","р","с","т","у","ф","х",
            "ц","ч","ш","щ","ъ","ы","ь","э","ю","я","ё",
            "А","Б","В","Г","Д","Е","Ж","З","И","Й","К",
            "Л","М","Н","О","П","Р","С","Т","У","Ф","Х",
            "Ц","Ч","Ш","Щ","Ъ","Ы","Ь","Э","Ю","Я","Ё",
            "Н", "н", "ы", "к", "ь", "л", "м", "я", "о",
            "ы", "К", "п", "ю", "i", "М", "е", "Л", "П",
            "О", "Ю", "Я", "Э", "э"};

            for (int i = 0; i < Ar1.Length; i++)
                str = str.Replace(Ar1[i], Ar2[i]);
            return str;
        }

    }
}
