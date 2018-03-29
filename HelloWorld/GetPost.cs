using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    /*
     Http请求
     get post put delete 查 改 增 删

     get：用户获取信息
     参数通过URL传输，在URL后用？分割，参数间用&分割
     http://url?para1=value1&para2=value2

     post 更新数据

         */
    public class GetPost
    {
        public static HttpWebResponse HttpGet(string url, IDictionary<string, string> paras, int timeout, string agent, CookieCollection cookies)
        {
            bool first = true;
            string data = "";
            if (paras != null && paras.Count > 0)
            {
                var buffer = new StringBuilder();
                foreach (var key in paras.Keys)
                {
                    if (!first)
                    {
                        buffer.AppendFormat("&{0}={1}", key, paras[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, paras[key]);
                        first = false;
                    }
                }
                data = buffer.ToString();
            }
            url += data == "" ? "" : ("?" + data);
            return HttpGet(url, timeout, agent, cookies);
        }
        
        public static HttpWebResponse HttpGet(string url,int timeout,string agnet,CookieCollection cookies)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            if (url.StartsWith("https://",StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
                {
                    if (sslPolicyErrors == SslPolicyErrors.None)
                        return true;
                    Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
                    // Do not allow this client to communicate with unauthenticated servers.
                    return false;
                });
                request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10; 
            }
            else
            {
                request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            }
            request.Method = "GET";
            request.UserAgent = agnet;//设置代理
            request.Timeout = timeout;//设置超时
            request.Headers.Add("name","value");//添加头
            if (cookies!=null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var result = reader.ReadToEnd();
            stream.Close();
            reader.Close();
            Debug.WriteLine(result);
            return response;            
        }

        public static HttpWebResponse HttpPost(string url,IDictionary<string ,string> paras,int timeout,string agent,CookieCollection cookies)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
                {
                    if (sslPolicyErrors == SslPolicyErrors.None)
                        return true;
                    Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
                    // Do not allow this client to communicate with unauthenticated servers.
                    return false;
                });
                request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "text/html";
            request.UserAgent = agent;//设置代理
            request.Timeout = timeout;//设置超时
            request.Headers.Add("name", "value");//添加头
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //发送post数据
            if (paras!=null || paras.Count>0)
            {
                StringBuilder buffer = new StringBuilder();
                bool first = true;
                foreach (var k in paras.Keys)
                {
                    if (!first)
                    {
                        buffer.AppendFormat("&{0}={1}", k, paras[k]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", k, paras[k]);
                        first = false;
                    }
                }
                byte[] data = Encoding.ASCII.GetBytes(buffer.ToString());
                using (var s = request.GetRequestStream())
                {
                    s.Write(data, 0, data.Length);
                }
            }
            string[] values = request.Headers.GetValues("Content-Type");
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var result = reader.ReadToEnd();
            stream.Close();
            reader.Close();
            Debug.WriteLine(result);
            return response;
        }

    }
}
