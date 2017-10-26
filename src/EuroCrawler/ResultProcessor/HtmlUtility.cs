using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JustAgile.Html.Linq;
using System.Web;

namespace ResultProcessor
{
    public static class HtmlUtility
    {
        public static string GetPlainText(string html)
        {
            try
            {
                HDocument document = HDocument.Parse(html);

                StringBuilder builder = new StringBuilder();
                GetPlainText(builder, document.Nodes());

                return CleanUpWhiteSpace(HttpUtility.HtmlDecode(builder.ToString()));
            }
            catch { return ""; }
        }

        private static string CleanUpWhiteSpace(string value)
        {
            StringBuilder builder = new StringBuilder();

            bool isWhiteSpace = true;

            foreach (char c in value)
            {
                if (c == '\r')
                {
                    builder.Append("\r\n");
                }
                else if (Char.IsWhiteSpace(c))
                {
                    if (!isWhiteSpace)
                    {
                        builder.Append(" ");
                    }


                    isWhiteSpace = true;
                }
                else
                {
                    builder.Append(c);

                    isWhiteSpace = false;
                }
            }

            return builder.ToString();
        }

        private static void GetPlainText(StringBuilder builder, IEnumerable<HNode> nodes)
        {
            foreach (HNode node in nodes)
            {
                HElement element = node as HElement;

                if (element != null)
                {
                    if (element.Name != "script" && element.Name != "style")
                    {
                        GetPlainText(builder, element.Nodes());
                    }
                }
                else
                {
                    HText text = node as HText;

                    if (text != null)
                    {
                        builder.Append(text + "\r\n");
                    }
                    else
                    {
                        HEntity entity = node as HEntity;

                        if (entity != null)
                        {
                            builder.Append(entity.Value + "\r\n");
                        }
                    }
                }
            }
        }
    }
}
