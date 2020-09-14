using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public string filial { get; set; }

        public string web_fili;
        public string web_place;
        public string web_tvk_1plx;
        public string web_tvk_2plx;
        public string er_string;
        public int GetPlexByVal(int i)
        {
            if (i == id_trans1)
                return 0;
            if (i == id_trans2)
                return 1;
            else
                return 999;
        }
        public static async Task<LoadItem> GetTransmitterAsync(int id)
        {
            string q = $"https://xn--80aa2azak.xn--p1aadc.xn--p1ai/rtrs/ajax/digital?multiplex=1&node={id.ToString()}";
            //
            LoadItem li = new LoadItem();
            string html = q;
            HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8,
            };
            CancellationTokenSource cts = new CancellationTokenSource(5000);
            HD = await web.LoadFromWebAsync(html, cts.Token);
            if (cts.IsCancellationRequested)
            {
                li.er_string = "Время ожидания истекло";
                return li;
            }
            if (HD.DocumentNode.InnerHtml.Contains("error"))
            {
                li.er_string = "Узел не найден";
                return li;
            }
            var qqwe = HD.DocumentNode.Descendants("label");
            //2 или 1 в зависимости от количества плексов
            int count = 0;
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
                    li.id_trans1 = buf;
                }
                else if (count == 2)
                {
                    int.TryParse(helpers, out buf);
                    li.id_trans2 = buf;
                }
                var span = itemq.Descendants("span");
                var hz = span.Where(has => has.InnerText.Contains("ТВК")).ToList();
                if (hz.Count > 0)
                {
                    var hz_select = hz.Select(x => x.InnerText).SingleOrDefault();
                    if (count == 1)
                        li.web_tvk_1plx = hz_select;
                    else if (count == 2)
                        li.web_tvk_2plx = hz_select;
                }
            }
            if (HD.DocumentNode.Descendants("h4").Any())
            {
                var span_h = HD.DocumentNode.Descendants("h4").SingleOrDefault().InnerText;
                li.web_fili = span_h;
                var span_sp = HD.DocumentNode.Descendants("span");
                if (span_sp.Count() > 0)
                    li.web_place = span_sp.First().InnerText;
            }
            return li;
        }
        public static async Task GetTransmImgAsync(LoadItem li)
        {
            string getimg = "https://xn--80aa2azak.xn--p1aadc.xn--p1ai/rtrs/ajax/broadcast?type=digital&id=";


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
                            if(t.Exception.InnerException.Message.Contains("407"))
                            {
                                li.er_string = "Ошибка доступа к сайту из за прокси сервера на вашей машине";
                                return;
                            }
                        }
                    }
                    else
                    {
                        byte[] pageContent = t.Result;
                        UTF8Encoding utf = new UTF8Encoding();
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



            //HtmlAgilityPack.HtmlDocument HD = new HtmlAgilityPack.HtmlDocument();
            //var web = new HtmlWeb
            //{
            //    AutoDetectEncoding = false,
            //    OverrideEncoding = Encoding.UTF8,
            //};
            //CancellationTokenSource cts = new CancellationTokenSource(5000);
            //HD = await web.LoadFromWebAsync(getimg + li.id_trans1, cts.Token);
            //if (cts.IsCancellationRequested)
            //{
            //    li.er_string = "Время ожидания истекло";
            //    return;
            //}
            //if (HD.DocumentNode.InnerHtml.Contains("404"))
            //{
            //    li.er_string = "Узел не найден";
            //    return;
            //}
            //li.map_trans1 = JsonConvert.DeserializeObject<mapobj>(HD.DocumentNode.InnerText);


            //using (var wb = new WebClient())
            //{
            //    var response = wb.DownloadString(getimg + li.id_trans1);
            //    li.map_trans1 = JsonConvert.DeserializeObject<mapobj>(response);
            //}
            //if (li.id_trans2 != 0)
            //{
            //    using (var wb = new WebClient())
            //    {
            //        var response = wb.DownloadString(getimg + li.id_trans2);
            //        li.map_trans2 = JsonConvert.DeserializeObject<mapobj>(response);
            //    }
            //}
        }
        public static async Task<LoadItem> GetMapInfoAsync(int id)
        {
            LoadItem li = new LoadItem();
            //li = await GetTransmitterAsync(id);
            li.id_trans1 = id;
            await GetTransmImgAsync(li);
            return li;
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
                string outs = $"L.imageOverlay('{map}', {coord});";
                return outs;
            }
            else
                return $"L.imageOverlay('{map}', [[0,0],[0,0]]);";

        }
    }
}
