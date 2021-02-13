using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using WinFormsApp1;
using System.IO;
using System.Net.Http.Headers;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var email = textBox1.Text;
            var password = textBox2.Text;
            var baseAddress = new Uri("https://github.com");
            var credentials = Encoding.ASCII.GetBytes(email + ":" + password);
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler();
            textBox3.Clear();

            handler.UseDefaultCredentials = true;
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            handler.UseCookies = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://github.com/settings/security-log");

                //var logindata = new System.Collections.Specialized.NameValueCollection();
                //logindata.Add("username", email);
                //logindata.Add("password", password);
                //string lols = HttpGet("https://api.github.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("product", "1"));
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36");
                client.DefaultRequestHeaders.Add("Accept", "application/xhtml+xml");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                //webBrowser1.Document.GetElementById("login_field").InnerText = textBox1.Text.ToString();
                //webBrowser1.Document.GetElementById("password").InnerText = textBox2.Text.ToString();
                //webBrowser1.Document.GetElementById("login_submit").InvokeMember("click");

                var response = await client.GetAsync("https://github.com/settings/security-log");

                if (response.IsSuccessStatusCode)
                {
                    //var data = response.Content.ReadAsStreamAsync<GitHubCommunication>();
                    var htmls = response.Content.ReadAsStringAsync().Result;
                    textBox3.Text = htmls;
                    Task task1 = new Task(new Action(scroll));
                    task1.Start();
                    //htmk.LoadDocument
                }
            }
        }

        void scroll()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                textBox3.SelectionStart = textBox3.Text.Length;
                textBox1.ScrollToCaret();
            }));
        }

        public static string HttpGet(string URI)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            req.Proxy = new System.Net.WebProxy("", true);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();

        }

        //public static string HttpPost(string URI, string parameters)
        //{

        //    System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
        //    req.Proxy = new System.Net.WebProxy(ProxyString, true);
        //    //Add these, as we're doing a POST
        //    req.ContentType = "application/json";
        //    req.Method = "POST";
        //    //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
        //    byte[] bytes = System.Text.Encoding.ASCII.GetBytes(parameters);
        //    req.ContentLength = bytes.Length;
        //    System.IO.Stream os = req.GetRequestStream();
        //    os.Write(bytes, 0, bytes.Length); //Push it out there
        //    os.Close();
        //    System.Net.WebResponse resp = req.GetResponse();
        //    if (resp == null) return null;
        //    System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
        //    return sr.ReadToEnd().Trim();
        //}

        //public static void HttpPost(string URI, string email, string password)
        //{
        //    string formUrl = "https://github.com/login/session"; // NOTE: This is the URL the form POSTs to, not the URL of the form (you can find this in the "action" attribute of the HTML's form tag
        //    string formParams = string.Format("login_field={0}&password={1}", email, password);
        //    string cookieHeader;
        //    WebRequest req = WebRequest.Create(formUrl);
        //    req.ContentType = "application/json";
        //    req.Method = "POST";
        //    byte[] bytes = Encoding.ASCII.GetBytes(formParams);
        //    req.ContentLength = bytes.Length;
        //    using (Stream os = req.GetRequestStream())
        //    {
        //        os.Write(bytes, 0, bytes.Length);
        //        os.Close();
        //    }
        //    try
        //    {
        //        using (WebResponse resp = req.GetResponse());
        //        {
        //            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
        //            {
        //            }
        //        }
        //        cookieHeader = resp.Headers["Set-cookie"];
        //    }
        //    catch
        //    {

        //    }           

        //}
    }
}
