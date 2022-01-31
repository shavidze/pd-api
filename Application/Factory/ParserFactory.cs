using Application.AbstractParser;
using Application.RequestModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factory
{
    public class ParserFactory : IParserFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ParserFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IParser GetParser(IFormFile file)
        {
            if (file == null)
                throw new ArgumentNullException("File is null");

            var fileExtension = Path.GetExtension(file.FileName);

            if (fileExtension.Contains("pdf"))
            {
                return PdfParser.PdfParser.Create(PdfModel.Create(file));
            }
            else if (fileExtension.Contains("ppt"))
            {
                return PPTParser.PPTParser.Create(PPtModel.Create(file));
            }

            return null;
        }
    }
}
