using Application.AbstractParser;
using Application.RequestModel;
using Aspose.Slides;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Application.PPTParser
{
    public class PPTParser : IParser<PPtModel>
    {
        public async Task<List<Image>> ParseAsync(PPtModel model)
        {
            if (model.File == null)
                throw new ArgumentNullException("File doesn't exist");

            var images = new List<Image>();

            var file = model.File;

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);

                using (var pres = new Presentation(ms))
                {
                    foreach (ISlide sld in pres.Slides)
                    {
                        using (var tempStr = new MemoryStream())
                        {
                            Bitmap bmp = sld.GetThumbnail(1f, 1f);
                            bmp.Save(tempStr, System.Drawing.Imaging.ImageFormat.Png);

                            images.Add(Image.FromStream(tempStr));

                        }
                    }
                }
            }

            return images;
        }
    }
}
