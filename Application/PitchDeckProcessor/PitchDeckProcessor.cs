using Application.AbstractParser;
using Application.Factory;
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
        private readonly IParserFactory _parserFactory;
        private readonly List<string> _allowedExtensions;

        public PitchDeckProccessor(IImageToMemorySaver imageToMemorySaver,
                                   IDispatcher dispatcher,
                                   IConfiguration configuration,
                                   IParserFactory parserFactory)
        {
            _imageToMemorySaver = imageToMemorySaver;
            _dispatcher = dispatcher;
            _parserFactory = parserFactory;
            _allowedExtensions = configuration.GetSection("AllowedFileExtensions")
                                              .AsEnumerable()
                                              .Select(x => x.Value)
                                              .ToList();
        }

        public async Task<CommandResult> ProccessAsync(IFormFile file)
        {
            if (file == null)
                throw new ArgumentException("File does not exist");

            var extension = Path.GetExtension(file.FileName);

            if (!_allowedExtensions.Contains(extension))
                throw new Exception("Not Allowed File Extension");

            var parser = _parserFactory.GetParser(file);

            if (parser == null)
                throw new Exception("Parser is not provided");

            var images = await parser.ParseAsync();

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
