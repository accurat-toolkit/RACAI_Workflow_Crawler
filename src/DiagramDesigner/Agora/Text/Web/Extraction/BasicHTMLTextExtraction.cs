using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace Agora.Text.Web.Extraction {
    public class BasicHTMLTextExtraction {
        string BaseURL = null;
        public BasicHTMLTextExtraction(string url) {
            BaseURL = url;
        }
        bool isLoaded;
        string cachedText;
        private string getContents(bool forceUpdate) {
            if ((!isLoaded) || (forceUpdate)) {
                try {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BaseURL);
                    request.Method = "GET";
                    WebResponse response = request.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
                    cachedText = sr.ReadToEnd();
                    sr.Close();
                    response.Close();
                    isLoaded = true;
                } catch {
                    isLoaded = true;
                    cachedText = "<p>Page not found</p>";
                }
            }
            return cachedText;
        }

        public void UpdateContents() {
            getContents(true);
        }

        public string[] getDataUsingRegex(Regex[] lstRegex, String[] allowedSequences, String[] forbiddenSequences) {
            List<string> followURLS = new List<string>();
            string rez = getContents(false);
            foreach (Regex r in lstRegex) {
                Match m = r.Match(rez);
                while (m.Success) {
                    string tmp = m.Value;
                    bool ok = true;
                    bool doNotCheck = false;
                    foreach (string test in allowedSequences) {
                        if (tmp.Contains(test)) {
                            doNotCheck = true;
                            break;
                        }
                    }
                    if (!doNotCheck)
                        foreach (string test in forbiddenSequences) {
                            if (tmp.Contains(test)) {
                                ok = false;
                                break;
                            }
                        }
                    if ((ok) && (!followURLS.Contains(tmp))) {
                        followURLS.Add(tmp);
                    }
                    m = m.NextMatch();
                }
            }
            return followURLS.ToArray();
        }
    }
}
