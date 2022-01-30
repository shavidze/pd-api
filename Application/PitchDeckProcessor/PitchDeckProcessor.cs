using Application.AbstractParser;
using Application.ImageToMemorySaver;
using Application.PitchDeckExecutors.Commands;
using Application.RequestModel;
using Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.PitchDeckProcessor
{
    public class PitchDeckProccessor : IPitchDeckProccessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IImageToMemorySaver _imageToMemorySaver;
        private readonly IDispatcher _dispatcher;
        private readonly List<string> _allowedExtensions;

        public PitchDeckProccessor(IServiceProvider serviceProvider,
                                   IImageToMemorySaver imageToMemorySaver,
                                   IDispatcher dispatcher,
                                   IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _imageToMemorySaver = imageToMemorySaver;
            _dispatcher = dispatcher;
            _allowedExtensions = configuration.GetSection("AllowedFileExtensions").AsEnumerable().Select(x => x.Value).ToList();
        }

        public async Task<CommandResult> ProccessAsync(IFormFile file)
        {
            if (file == null)
                throw new ArgumentException("File does not exist");

            var extension = Path.GetExtension(file.FileName);

            if (!_allowedExtensions.Contains(extension))
                throw new Exception("Not Allowed File Extension");

            var images = default(List<Image>);

            if (extension.Contains("pdf"))
            {
                var pdfParser = (IParser<PdfModel>)_serviceProvider.GetService(typeof(IParser<PdfModel>));               
                images = await pdfParser.ParseAsync(PdfModel.Create(file));
            }
            else if (extension.Contains("ppt"))
            {
                var pdfParser = (IParser<PPtModel>)_serviceProvider.GetService(typeof(IParser<PPtModel>));
                images = await pdfParser.ParseAsync(PPtModel.Create(file));
            }

            if (images != null && images.Any())
            {
                var savedImages = await _imageToMemorySaver.SaveAsync(images);

                var createPitchDeckCommand = CreatePitchDeckCommand.Create(savedImages);

                return await _dispatcher.DispatcheCommandAsync(createPitchDeckCommand);
            }

            return CommandResult.Error("Coulnd't generated images");
        }
    }
}
