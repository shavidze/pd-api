using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ImageToMemorySaver
{
    public class ImageToMemorySaver : IImageToMemorySaver
    {
        private readonly string _apiAddress;

        public ImageToMemorySaver(IConfiguration confifguration)
        {
            _apiAddress = confifguration["ApiAddress"];
        }
        public async Task<List<SavedImageToMemory>> SaveAsync(List<Image> images)
        {
            var savedImages = new List<SavedImageToMemory>();

            using (MemoryStream imageStream = new MemoryStream())
            {
                foreach (var image in images)
                {
                    image.Save(imageStream, ImageFormat.Png);

                    imageStream.Position = 0;

                    var imageId = Guid.NewGuid();
                    var imagePath = @$"Images/keeped-" + imageId + "-image" + ".png";
                    var imageName = @$"keeped-" + imageId + "-image" + ".png";
                    var fullPath = $@"{_apiAddress}/{imagePath}";

                    var savedImage = SavedImageToMemory.Create(imageName, fullPath, ImageFormat.Png, imageId);

                    FileStream outStream = System.IO.File.OpenWrite(imagePath);
                    imageStream.WriteTo(outStream);

                    await outStream.FlushAsync();

                    outStream.Close();

                    await outStream.DisposeAsync();

                    savedImages.Add(savedImage);
                }

                imageStream.Close();
                await imageStream.DisposeAsync();
            }

            return savedImages;
        }
    }

    public class SavedImageToMemory
    {
        public string ImageName { get; set; }

        public ImageFormat Format { get; set; }

        public Guid ImageId { get; set; }

        public string Path { get; set; }

        public static SavedImageToMemory Create(string imageName, string path, ImageFormat format, Guid imageId) => new SavedImageToMemory
        {
            ImageName = imageName,
            Format = format,
            ImageId = imageId,
            Path = path
        };
    }
}
