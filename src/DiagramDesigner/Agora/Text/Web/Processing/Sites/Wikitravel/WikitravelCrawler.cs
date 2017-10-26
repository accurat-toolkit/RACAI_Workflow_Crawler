using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Agora.Text.Web.Processing.Sites.Wikitravel {
    public class WikitravelCrawler : IDisposable {


        public delegate void DataCollectedEnd();
        public event DataCollectedEnd NoMoreData;
        public delegate void DataCollectedEvent(DataCollectedEventArgs e);
        public event DataCollectedEvent DataCollected;
        /// <summary>
        /// Creates a new instance of the crawler. The baseURL and ServerLocation must point to the romanian version of the site
        /// </summary>
        /// <param name="BaseURL">Base url path e.g. http://www.wikitravel.org </param>
        /// <param name="ServerLocation">This represents the location on the server, added to the base path. E.g. /ro/Bucureşti </param>
        /// <param name="pagesNeeded">Indicates when to stop crawling (it uses BF search algorithm). The number of pager crawled will be pagesNeeded*3 (RO, EN, DE) </param>
        private string baseURL;
        private string serverLocation;
        private int needed;
        public WikitravelCrawler(string BaseURL, string ServerLocation, int pagesNeeded) {
            this.baseURL = BaseURL;
            this.serverLocation = ServerLocation;
            this.needed = pagesNeeded;
        }
        private void WorkingThread() {
            //progresul curent
            int currentFileProgress = 0;
            //stiva de procesare pe site-uri
            List<string> crawlingStack = new List<string>();
            //primul url este cel de baza
            crawlingStack.Add(baseURL + serverLocation);
            //cata vreme mai putem procesa si nu am atins limita
            while ((currentFileProgress < crawlingStack.Count)&&(currentFileProgress<needed)) {
                string roUrl = crawlingStack[currentFileProgress];
                string enUrl = "";
                string deUrl = "";
                WikitravelDataExtraction wde = new WikitravelDataExtraction(roUrl);
                //cautam echivalentele in celelalte limbi si linkuri pe care ar trebui sa le urmam
                foreach (string tmp in wde.FollowLinks) {
                    if (tmp.Contains("wikitravel.org/de/"))
                        deUrl = tmp;
                    else if (tmp.Contains("wikitravel.org/en/"))
                        enUrl = tmp;
                    else if (tmp.Contains("href=\"/ro")) {
                        //trebuie sa procesam url-ul pentru al adauga in lista
                        string temp = tmp.Substring(6);
                        temp = temp.Substring(0, temp.Length - 1);
                        crawlingStack.Add(this.baseURL + temp);
                    }
                }
                try{
                    enUrl = enUrl.Substring(6);
                    enUrl = enUrl.Substring(0, enUrl.Length - 1);
                }catch{
                }
                try {
                    deUrl = deUrl.Substring(6);
                    deUrl = deUrl.Substring(0, deUrl.Length - 1);
                } catch {
                }
                //lansam evenimentul pentru site-ul ro
                LaunchAllEventsAndClearMemory(currentFileProgress,roUrl, enUrl, deUrl, wde);
                wde = null;
                GC.Collect();
                currentFileProgress++;
            }
            if (NoMoreData != null) {
                NoMoreData();
            }
        }

        private void LaunchAllEventsAndClearMemory(int cid, string roUrl, string enUrl, string deUrl, WikitravelDataExtraction wde) {
            DataCollectedEventArgs e = new DataCollectedEventArgs();
            e.CrawlerID = cid;
            e.Text = wde.TextContents;
            e.URL = roUrl;
            e.Language = LanguageType.RO;
            if (this.DataCollected != null) {
                DataCollected(e);
            }

            wde = new WikitravelDataExtraction(enUrl);
            e.CrawlerID = cid;
            e.Text = wde.TextContents;
            e.URL = enUrl;
            e.Language = LanguageType.EN;
            if (this.DataCollected != null) {
                DataCollected(e);
            }

            wde = new WikitravelDataExtraction(deUrl);
            e.CrawlerID = cid;
            e.Text = wde.TextContents;
            e.URL = deUrl;
            e.Language = LanguageType.DE;
            if (this.DataCollected != null) {
                DataCollected(e);
            }

        }
        Thread t = null;

        /// <summary>
        /// Start crawling on the baseURL/serverLocation
        /// </summary>
        public void StartCrawling() {
            t = new Thread(WorkingThread);
            t.Start();
        }
        /// <summary>
        /// Stops the current process. All progress will be lost
        /// </summary>
        public void StopCrawling() {
            t.Abort();
        }

        #region IDisposable Members

        void IDisposable.Dispose() {
            if (t.ThreadState == ThreadState.Running)
                t.Abort();
        }

        #endregion
    }
    public class DataCollectedEventArgs {
        public int CrawlerID;
        public string URL;
        public string Text;
        public LanguageType Language;
    }
    public enum LanguageType {
        RO,
        EN,
        DE
    }
}
