using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agora.Text.Web.Extraction;
using System.Text.RegularExpressions;

namespace SearchSimulator {
    public class Searcher {
        #region other processing region
        public Topic[] ExtractTopics(string primary_response) {
            Regex r = new Regex(@"<input type=\""checkbox\"" name=\""topicBeanCount.topicString\"" value=\""(.*?)\""", RegexOptions.IgnoreCase);
            string[] tmp = ExtractDataUsingRegex(r, primary_response);
            for (int i = 0; i < tmp.Length; i++) {
                tmp[i] = tmp[i].Substring(tmp[i].Length - 4, 3);
            }
            Regex r2 = new Regex(@"<span class=\""EI_lnk\"">(.*?)</span>", RegexOptions.IgnoreCase| RegexOptions.Singleline);
            string[] text = ExtractDataUsingRegex(r2, primary_response);
            Topic[] t = new Topic[tmp.Length];
            for (int i = 0; i < text.Length; i++) {
                text[i] = text[i].Substring(21, text[i].IndexOf('\n')-21);
                t[i] = new Topic();
                t[i].id = tmp[i];
                t[i].name = text[i];
            }
            return t;
        }
        public string[] ExtractDataUsingRegex(Regex r, string text) {
            Match m = r.Match(text);
            List<string> tmp = new List<string>();
            while (m.Success) {
                tmp.Add(m.Value);
                m = m.NextMatch();
            }
            return tmp.ToArray();
        }
        public string ExtractSearchUrl(string primary_response) {
            return "http://www.europarl.europa.eu/news/archive/search/topicSearch.do?language=RO";
            //Regex r = new Regex(@"<input type=\""checkbox\"" name=\""topicBeanCount.topicString\"" value=\""(.*?)\""",RegexOptions.IgnoreCase);
            //string[] tmp = ExtractDataUsingRegex(r, primary_response);
            //if (tmp.Length == 1) {
            //    return tmp[0];
            //} else {
            //    return string.Empty;
            //}
        }
        public string[] ExtractSearchResults(string[] topics, string searchURL) {
            PostData pd = new PostData();
            foreach (string topic in topics) {
                pd.AddElement("topicBeanCount.topicString", topic);
            }
            string response = wb.Navigate(pd, searchURL);
            //Log("Parsez rezultatele cautarii: ");
            //aflam numarul maxim de articole gasite
            Regex r = new Regex(@"startValue=(.*?)\""");
            string[] pagini_cautare = ExtractDataUsingRegex(r, response);
            int maxPage = 0;
            List<string> url_cautare = new List<string>();
            List<string> verificate = new List<string>();
            foreach (string tmp in pagini_cautare) {
                string ps = tmp.Substring(tmp.LastIndexOf('=') + 1);
                ps = ps.Substring(0, ps.Length - 1);
                int pag = int.Parse(ps);
                if (pag > maxPage) {
                    maxPage = pag;
                }
                if (!verificate.Contains(pag.ToString())) {
                    verificate.Add(pag.ToString());
                    url_cautare.Add("http://www.europarl.europa.eu/news/archive/search/topicSearch.do?language=RO&startValue=" + pag);
                }
            }
            //Log("\tPagina maxima de cautare este :" + maxPage.ToString() + " => 30*" + maxPage.ToString() + "=" + 30 * maxPage + " articole");
            //Log("\tGenerez lista de cautare:");
            //foreach (string s in url_cautare) {
            //    Log("\t\t" + s);
            //}
            List<string> articole = new List<string>();
            //Log("\tGenerez lista de articole pentru fiecare pagina:");
            r = new Regex(@"pubRef=(.*?)&amp;", RegexOptions.IgnoreCase);
            string[] tmpList = ExtractDataUsingRegex(r, response);
            //articole.AddRange(tmpList);
            //Log("\t\tPagina 1:");
            foreach (string s in tmpList) {
                if (!articole.Contains(s)) {
                    //Log("\t\t\t" + s);
                    articole.Add(s);
                }
            }
            int i = 1;
            foreach (string s in url_cautare) {
                response = wb.Navigate(pd, s);
                r = new Regex(@"pubRef=(.*?)&amp;", RegexOptions.IgnoreCase);
                tmpList = ExtractDataUsingRegex(r, response);
                //articole.AddRange(tmpList);
                i++;
                //Log("\t\tPagina " + i + ":");
                foreach (string surl in tmpList) {
                    if (!articole.Contains(surl)) {
                        //Log("\t\t\t" + surl);
                        articole.Add(surl);
                    }
                }
            }
            //postprocesare linkuri
            for (int idx = 0; idx < articole.Count; idx++) {
                string s = "http://www.europarl.europa.eu/sides/getDoc.do?pubRef=" + articole[idx].Substring(7, articole[idx].Length - 14);
                articole[idx] = s;
            }
            return articole.ToArray();
        }
        public TextWebBrowser wb;
        #endregion
    }
    public class Topic {
        public string id;
        public string name;
        public override string ToString() {
            return name;
        }
    }
}
