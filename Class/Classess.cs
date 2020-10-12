using HtmlAgilityPack;
using Newtonsoft.Json;
using RtrsMapService_User.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RtrsMapService_User
{
    public static class StaticInfo
    {
        public delegate void ExceptServer();
        public static event ExceptServer ThrowServerError;
        public static void ThrowServerErr()
        {
            ThrowServerError?.Invoke();
        }

        public delegate void CloseMainWindow();
        public static event CloseMainWindow Ev_CloseMainWindow;
        public static void DoCloseMainWindow()
        {
            Ev_CloseMainWindow?.Invoke();
        }


        public delegate void CloseChildWindow();
        public static event CloseChildWindow Ev_CloseChildWindow;
        public static void DoCloseChildWindow()
        {
            Ev_CloseChildWindow?.Invoke();
        }

        public delegate void TransferDataToMap(mapobj mo, Image img, int id);
        public static event TransferDataToMap Ev_TransferDataToMap;
        public static void DoTransferDataToMap(mapobj mo, Image img, int id)
        {
            Ev_TransferDataToMap?.Invoke(mo, img, id);
        }


        public delegate void GetServerState();
        public static event GetServerState Ev_GetServerState;
        public static void DoGetServerState()
        {
            Ev_GetServerState?.Invoke();
        }
        public delegate void ResponseServerState(SimpleHTTPServer state);
        public static event ResponseServerState Ev_ResponseServerState;
        public static void DoResponseServerState(SimpleHTTPServer state)
        {
            Ev_ResponseServerState?.Invoke(state);
        }


        public enum MultiSelect
        {
            both = 0,
            first = 1,
            second = 2
        }
        public static IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }
    }

    public class ComboboxItem
    {
        public ComboboxItem(LoadItem li, string text)
        {
            Value = li;
            Text = text;
        }
        public string Text { get; set; }
        public LoadItem Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
    public class ImgItemInfo
    {
        public string name { get; set; }
        public DateTime dt { get; set; }
        public double size_byte { get; set; }
        public double size_mb { get { return size_byte / 1024 / 1024; } }
        public ImgItemInfo(string _nm, DateTime _dt, double _size_byte)
        {
            name = _nm;
            dt = _dt;
            size_byte = _size_byte;
        }
        public override string ToString()
        {
            return $"name is {name}. dt is {dt.ToString()}";
        }
        public void comparre(string img_nm, DateTime dt)
        {

        }
    }
    public class LoadItem
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("lat")]
        public double lat { get; set; }
        [JsonProperty("lng")]
        public double lon { get; set; }
        [JsonProperty("id_trans1")]
        public int id_trans1 { get; set; }
        [JsonProperty("id_trans2")]
        public int id_trans2 { get; set; }
        [JsonProperty("map_trans1")]
        public mapobj map_trans1 { get; set; }
        [JsonProperty("map_trans2")]
        public mapobj map_trans2 { get; set; }

        public string web_fili { get; set; }
        public string web_place { get; set; }
        public string er_string { get; set; }

        public string filial { get; set; }



        //public int GetPlexByVal(int i)
        //{
        //    if (i == id_trans1)
        //        return 0;
        //    if (i == id_trans2)
        //        return 1;
        //    else
        //        return 999;
        //}
        //public static async Task<LoadItem> GetTransmitterAsync(int id)
        //{
        //    string q = $"https://xn--80aa2azak.xn--p1aadc.xn--p1ai/rtrs/ajax/digital?multiplex=1&node={id.ToString()}";
        //    //
        //    LoadItem li = new LoadItem();
        //    string html = q;
        //    HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
        //    var web = new HtmlWeb
        //    {
        //        AutoDetectEncoding = false,
        //        OverrideEncoding = Encoding.UTF8,
        //    };
        //    CancellationTokenSource cts = new CancellationTokenSource(5000);
        //    HD = await web.LoadFromWebAsync(html, cts.Token);
        //    if (cts.IsCancellationRequested)
        //    {
        //        li.er_string = "Время ожидания истекло";
        //        return li;
        //    }
        //    if (HD.DocumentNode.InnerHtml.Contains("error"))
        //    {
        //        li.er_string = "Узел не найден";
        //        return li;
        //    }
        //    var qqwe = HD.DocumentNode.Descendants("label");
        //    //2 или 1 в зависимости от количества плексов
        //    int count = 0;
        //    foreach (var itemq in qqwe)
        //    {
        //        count++;
        //        var t = itemq.Descendants("input");
        //        var ttt = t.Where(qqq => qqq.Attributes.Contains("data-transmitter-id")).ToList();
        //        string helpers = string.Empty;
        //        if (ttt.Count > 0)
        //        {
        //            helpers = t.Select(x => x.Attributes["data-transmitter-id"].Value).SingleOrDefault();
        //        }
        //        else
        //        {
        //            count = 999;
        //        }
        //        int buf = 0;
        //        if (count == 1)
        //        {
        //            int.TryParse(helpers, out buf);
        //            li.id_trans1 = buf;
        //        }
        //        else if (count == 2)
        //        {
        //            int.TryParse(helpers, out buf);
        //            li.id_trans2 = buf;
        //        }
        //        var span = itemq.Descendants("span");
        //        var hz = span.Where(has => has.InnerText.Contains("ТВК")).ToList();
        //        if (hz.Count > 0)
        //        {
        //            var hz_select = hz.Select(x => x.InnerText).SingleOrDefault();
        //            if (count == 1)
        //                li.map_trans1.web_tvk = hz_select;
        //            else if (count == 2)
        //                li.map_trans2.web_tvk = hz_select;
        //        }
        //    }
        //    if (HD.DocumentNode.Descendants("h4").Any())
        //    {
        //        var span_h = HD.DocumentNode.Descendants("h4").SingleOrDefault().InnerText;
        //        li.web_fili = span_h;
        //        var span_sp = HD.DocumentNode.Descendants("span");
        //        if (span_sp.Count() > 0)
        //            li.web_place = span_sp.First().InnerText;
        //    }
        //    return li;
        //}
        public static async Task GetTransmImgAsync(LoadItem li)
        {
            string getimg = "https://xn--80aa2azak.xn--p1aadc.xn--p1ai/rtrs/ajax/broadcast?type=digital&id=";

            UTF8Encoding utf = new UTF8Encoding();
            WebClient client = new WebClient();
            client.Proxy = null;
            client.UseDefaultCredentials = true;

            string baseHtml = "";

            var t = client.DownloadDataTaskAsync(getimg + li.id_trans1);

            try
            {
                if (await Task.WhenAny(t, Task.Delay(5000)) == t)
                {
                    if (t.Exception != null)
                    {
                        if (t.Exception.InnerExceptions.Count <= 1)
                        {
                            if (t.Exception.InnerException.Message.Contains("404"))
                            {
                                li.er_string = "Мульт не найден";
                                return;
                            }
                            if (t.Exception.InnerException.Message.Contains("407"))
                            {
                                li.er_string = "Ошибка доступа к сайту из за прокси сервера на вашем компьютере";
                                return;
                            }
                        }
                    }
                    else
                    {
                        byte[] pageContent = t.Result;
                        baseHtml = utf.GetString(pageContent);
                        li.er_string = string.Empty;
                    }
                }
                else
                {
                    li.er_string = "Время ожидания истекло";
                    return;
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Загрузка завершена с ошибкой\n {ex.Message}", "Ошибка");
                return;
            }
            li.map_trans1 = JsonConvert.DeserializeObject<mapobj>(baseHtml);
            t.Dispose();
            client.Dispose();
        }
        public static async Task<LoadItem> GetMapInfoAsync(int id)
        {
            LoadItem li = new LoadItem();
            li.id_trans1 = id;
            await GetTransmImgAsync(li);
            return li;
        }
        public static string ExPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string ServerPath = ExPath + "\\server";
        public static string ImgMapPath = ExPath + "\\server\\mapimg\\";
        public void GetInfoLoadItem(List<ImgItemInfo> list)
        {
            string q = $"https://xn--80aa2azak.xn--p1aadc.xn--p1ai/rtrs/ajax/digital?multiplex=1&node={id.ToString()}";
            string getimg = "https://xn--80aa2azak.xn--p1aadc.xn--p1ai/rtrs/ajax/broadcast?type=digital&id=";
            string tvk = string.Empty;
            string html = q;
            HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8,
            };
            HD = web.Load(html);
            var qqwe = HD.DocumentNode.Descendants("label");
            //2 или 1 в зависимости от количества плексов
            int count = 0;
            try
            {
                foreach (var itemq in qqwe)
                {
                    count++;
                    var t = itemq.Descendants("input");
                    var ttt = t.Where(qqq => qqq.Attributes.Contains("data-transmitter-id")).ToList();
                    string helpers = string.Empty;
                    if (ttt.Count > 0)
                    {
                        helpers = t.Select(x => x.Attributes["data-transmitter-id"].Value).SingleOrDefault();
                    }
                    else
                    {
                        count = 999;
                    }
                    int buf = 0;
                    if (count == 1)
                    {
                        int.TryParse(helpers, out buf);
                        id_trans1 = buf;
                    }
                    else if (count == 2)
                    {
                        int.TryParse(helpers, out buf);
                        id_trans2 = buf;
                    }
                    var span = itemq.Descendants("span");
                    var hz = span.Where(has => has.InnerText.Contains("ТВК")).ToList();
                    if (hz.Count > 0)
                    {
                        var hz_select = hz.Select(x => x.InnerText).SingleOrDefault();
                        if (count == 1)
                            tvk = hz_select;
                        else if (count == 2)
                            tvk = hz_select;
                    }
                }
                if (HD.DocumentNode.Descendants("h4").Any())
                {
                    var span_h = HD.DocumentNode.Descendants("h4").SingleOrDefault().InnerText;
                    web_fili = span_h;
                    var span_sp = HD.DocumentNode.Descendants("span");
                    if (span_sp.Count() > 0)
                        web_place = span_sp.First().InnerText;
                }
                if (id_trans1 != 0)
                {
                    using (var wb = new WebClient())
                    {
                        var response = wb.DownloadString(getimg + id_trans1);
                        map_trans1 = JsonConvert.DeserializeObject<mapobj>(response);
                        ImgChecker(list, map_trans1.map);
                        map_trans1.web_tvk = tvk;
                    }
                }
                if (id_trans2 != 0)
                {
                    using (var wb = new WebClient())
                    {
                        var response = wb.DownloadString(getimg + id_trans2);
                        map_trans2 = JsonConvert.DeserializeObject<mapobj>(response);
                        ImgChecker(list, map_trans2.map);
                        map_trans2.web_tvk = tvk;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ImgChecker(List<ImgItemInfo> list, string path)
        {
            string withIMG = ImgMapPath + Path.GetFileName(path);
            WebClient wb = new WebClient();

            if (!File.Exists(withIMG))
            {
                wb.DownloadFile(path, withIMG);
            }
            else
            {
                DateTime dt = File.GetLastWriteTime(withIMG);
                if (list.Where(l => l.name == Path.GetFileName(path)).Single().dt > dt)
                {
                    wb.DownloadFile(path, withIMG);
                }
            }
        }
        public void ClearMapObj()
        {
            id_trans1 = 0;
            id_trans2 = 0;
            map_trans1 = null;
            map_trans2 = null;
        }
        public string PrintVar()
        {
            return $"a{id}";
        }
    }
    public class mapobj
    {
        public mapobj()
        {
            map = string.Empty;
            swx = swy = nex = ney = 0.0;
        }
        [JsonProperty("map")]
        public string map { get; set; }
        [JsonProperty("swx")]
        public double swx { get; set; }
        [JsonProperty("swy")]
        public double swy { get; set; }
        [JsonProperty("nex")]
        public double nex { get; set; }
        [JsonProperty("ney")]
        public double ney { get; set; }

        public string web_name { get { return Path.GetFileNameWithoutExtension(map); } }

        public string web_tvk { get; set; }
        [JsonIgnore]
        private string png_name { get { return Path.GetFileName(map); } }

        public PointF GetCenter()
        {
            PointF pf = new PointF();
            pf.X = (float)((nex + swx) / 2);
            pf.Y = (float)((ney + swy) / 2);
            return pf;
        }
        public override string ToString()
        {
            if (map != "")
            {
                string coord = "[[" + swx.ToString(CultureInfo.InvariantCulture) + "," + swy.ToString(CultureInfo.InvariantCulture) + "],[" +
    nex.ToString(CultureInfo.InvariantCulture) + "," + ney.ToString(CultureInfo.InvariantCulture) + "]]";
                string outs = $"L.imageOverlay('/mapimg/{png_name}', {coord});";
                return outs;
            }
            else
                return $"L.imageOverlay('', [[0,0],[0,0]]);";///mapimg/{png_name}', [[0,0],[0,0]]);";

        }
    }
    public class Data
    {
        [JsonProperty("data")]
        public List<LoadItem> li { get; set; }
        //500+- мб картинок

        [JsonProperty("updated")]
        public int DtTime { get; set; }
        //public DateTime MultiTime { get; set; }
        [JsonProperty("plex_updated")]
        public DateTime PlexLoad { get; set; }

        [JsonIgnore]
        public DateTime Time
        {
            get
            {
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(DtTime).ToLocalTime();
                return dtDateTime;
            }
        }
        public LoadItem GetMapInfo_plex(int id_plex)
        {
            int num = 0;
            if (this.li.Any(x => x.id_trans1 == id_plex))
                num = 1;
            else if (this.li.Any(x => x.id_trans2 == id_plex))
                num = 2;
            if (num == 0) return null;
            LoadItem liq = new LoadItem();
            if (num == 1)
                liq = this.li.Where(x => x.id_trans1 == id_plex).SingleOrDefault();
            else
                liq = this.li.Where(x => x.id_trans2 == id_plex).SingleOrDefault();
            return liq;
        }
        public LoadItem GetMapInfo_server(int id_serv)
        {
            int num = 0;
            if (this.li.Any(x => x.id == id_serv))
                num = 1;
            if (num == 0) return null;
            LoadItem liq = new LoadItem();
            liq = this.li.Where(x => x.id == id_serv).SingleOrDefault();
            return liq;
        }
        public Data()
        {
            li = new List<LoadItem>();
        }
    }
}
