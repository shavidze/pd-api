using Application.AbstractParser;
using Application.RequestModel;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.PdfParser
{
    public class PdfParser : IParser<PdfModel>
    {
        public async Task<List<Image>> ParseAsync(PdfModel model)
        {
            if (model.File == null)
                throw new ArgumentNullException("File doesn't exist");

            var file = model.File;

            var fileContent = default(List<byte>);

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileContent = ms.ToArray().ToList();
            }

            var images = new List<Image>();

            using (var document = new PdfDocument(fileContent.ToArray()))
            {
                for (var i = 0; i < document.Pages.Count; i++)
                {
                    var image = document.SaveAsImage(i);

                    images.Add(image);
                }
            }

            return images;
        }
    }
}
