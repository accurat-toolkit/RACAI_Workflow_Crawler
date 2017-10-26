using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Agora.Text.Web.Extraction;

namespace Agora.Text.Web.Processing.Sites.Wikitravel {
    public class WikitravelDataExtraction {
        BasicHTMLTextExtraction bte = null; 
        public WikitravelDataExtraction(string url) {
            bte = new BasicHTMLTextExtraction(url);
            bte.UpdateContents();
        }

        public string []FollowLinks {
            get {
                Regex r = new Regex(@"href=\""(.*?)\""", RegexOptions.IgnoreCase);
                List<string> forbidden = new List<string>();
                forbidden.Add("#");
                forbidden.Add("mailto");
                forbidden.Add("special");
                forbidden.Add("action=");
                forbidden.Add("Special:");
                forbidden.Add("doubleclick");
                forbidden.Add("USer:");
                forbidden.Add("Wikitravel:");
                forbidden.Add("Utilizator:");
                forbidden.Add("Image:");
                forbidden.Add("http:");
                forbidden.Add("www.");
                List<string> priorityAllow = new List<string>();
                priorityAllow.Add("wikitravel.org/de/");
                priorityAllow.Add("wikitravel.org/en/");
                priorityAllow.Add("wikitravel.org/ro/");
                return bte.getDataUsingRegex(new Regex[] { r }, priorityAllow.ToArray(), forbidden.ToArray());
            }
        }

        public string TextContents{
            get {
                string response = "";
                List<string> textChior = new List<string>();
                Regex r2 = new Regex(@"(<p>|<li>)(.*?)(</p>|</li>)", RegexOptions.Singleline);
                textChior.AddRange(bte.getDataUsingRegex(new Regex[] { r2 }, new string[] { }, new string[] { }));
                foreach (string tmp in textChior) {
                    //postprocesare pentru eliminarea de taguri html
                    string tmpProc = tmp;

                    Regex filtrare1 = new Regex(@"<(.*?)>");
                    tmpProc = filtrare1.Replace(tmpProc, "");
                    //inlocuire taguri html
                    tmpProc = tmpProc.Replace("&lt;", "<");
                    tmpProc = tmpProc.Replace("&quot;", "\"");
                    tmpProc = tmpProc.Replace("&nbsp;", " ");
                    tmpProc = tmpProc.Replace("&iexcl;", "!");
                    tmpProc = tmpProc.Replace("\r", "");
                    tmpProc = tmpProc.Replace("\n", "");
                    //TODO: de completat ce se mai poate reprezenta
                    //ce ne scapa distrugem
                    Regex filtrare2 = new Regex(@"&(.*?);");
                    tmpProc = filtrare2.Replace(tmpProc, "");
                    tmpProc = tmpProc.Replace("&amp;", "&");

                    response = response + tmpProc + "\r\n";
                }
                return response;
            }
        }
    }
}
