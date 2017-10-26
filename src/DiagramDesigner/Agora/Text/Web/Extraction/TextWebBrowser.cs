using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;
using System.IO;

namespace Agora.Text.Web.Extraction {
    public class TextWebBrowser {
        CookieContainer myCC;

        public TextWebBrowser() {
            myCC = new CookieContainer();
        }
        public string DoNavigation(PostData param, string url) {
            string s = string.Empty;
            if (param != null) {
                s = param.GetEncodedPostData();
            }
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            if (s != String.Empty) {
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.CookieContainer = myCC;
                req.ContentLength = s.Length;
                TextWriter sw = new StreamWriter(req.GetRequestStream());
                sw.Write(s);
                sw.Close();
            }
            WebResponse ws = req.GetResponse();
            StreamReader answer = new StreamReader(ws.GetResponseStream(), System.Text.Encoding.UTF8);
            return answer.ReadToEnd();
        }

        public string Navigate(string url) {
            return DoNavigation(null, url);
        }
        public string Navigate(PostData parameters, string url) {
            return DoNavigation(parameters, url);
        }
    }
    public class PostData {
        List<string> keys = new List<string>();
        List<string> values = new List<string>();
        public void AddElement(string key, string value) {
            this.keys.Add(key);
            this.values.Add(value);
        }
        public string GetEncodedPostData() {

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < keys.Count; i++) {
                if (sb.Length != 0) {
                    sb.Append("&");
                }
                sb.Append(keys[i]);
                sb.Append("=");
                sb.Append(System.Web.HttpUtility.UrlEncode(values[i]));
            }
            return sb.ToString();
        }

    }
}
