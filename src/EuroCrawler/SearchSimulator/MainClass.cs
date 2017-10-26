using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agora.Builder.Interfaces;
using Agora.Text.Web.Extraction;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SearchSimulator {
    public class MainClass : ProcessingBlock {
        #region ProcessingBlock Members
        object ProcessingBlock.ProcessData(object data, Agora.Builder.System.BaseApplication MyApplication) {
            frmConfiguration fc = new frmConfiguration(MyApplication);
            fc.StartPosition = FormStartPosition.CenterScreen;
            fc.ShowDialog();
            return fc.ArticleList;
            ////throw new NotImplementedException();
            //wb = new TextWebBrowser();
            //string response = wb.Navigate("http://www.europarl.europa.eu/news/archive/search.do?language=RO");
            //string[] topics = ExtractTopics(response);

            //string searchUrl = ExtractSearchUrl(response);
            ////activam search-ul
            //string[] urls = ExtractSearchResults(topics, searchUrl);
            //return urls;
        }

        #endregion
    }
}
