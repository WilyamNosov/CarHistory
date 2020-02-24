using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarHistory.Models;
using HtmlAgilityPack;

namespace CarHistory.Services
{
    public class ParserService : IParserService
    {
        public string BaseUrl { get; set; }
        public string InfoUrl { get; set; }
        public IEnumerable<CarParameter> GetInformationByVIN(string url)
        {
            var carParameters = new List<CarParameter>();
            var htmlDocument = GetDocument(url);
            GetExpandedUrl(htmlDocument);
            var nodes = GetHtmlNodes();
            
            foreach(var node in nodes)
            {
                var tdTags = node.ChildNodes;
                if (tdTags.Count == 5)
                {
                    carParameters.Add(new CarParameter() { Property = tdTags[1].InnerText, Value = tdTags[3].InnerText });
                }
            }

            return carParameters;
        }

        public HtmlDocument GetDocument(string url)
        {
            GiveBaseUrl(url);
            var htmlWeb = new HtmlWeb();
            var htmlDocument = htmlWeb.Load(url);
            
            return htmlDocument;
        }

        public void GetExpandedUrl(HtmlDocument htmlDocument)
        {
            var infoUrl = htmlDocument.DocumentNode.SelectSingleNode("//ul[@class='list-unstyled']").FirstChild.LastChild.Attributes["href"].Value;
            InfoUrl = infoUrl;
        }

        public HtmlNodeCollection GetHtmlNodes()
        {
            var url = BaseUrl + InfoUrl;
            var htmlDocument = GetDocument(url);
            var nodes = htmlDocument.DocumentNode.SelectNodes("//table/tr");
            
            return nodes;
        }

        private void GiveBaseUrl(string url)
        {
            BaseUrl = url.Split('/')[0] + "//" + url.Split('/')[2];
        }
    }
}
