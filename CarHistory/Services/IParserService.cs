using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace CarHistory.Services
{
    interface IParserService
    {
        public HtmlDocument GetDocument(string url);
    }
}
