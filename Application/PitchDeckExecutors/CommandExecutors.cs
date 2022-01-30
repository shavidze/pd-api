using Application.PitchDeckExecutors.Commands;
using Application.Shared;
using Infrastructure.Repositories.PitchDeckRepository;
using Model;
using System.Linq;
using System.Threading.Tasks;

namespace Application.PitchDeckExecutors
{
    public class CreatePitchDeckCommandExecutor : ICommandExecutor<CreatePitchDeckCommand>
    {
        private readonly IPitchDeckRepository _repository;

        public CreatePitchDeckCommandExecutor(IPitchDeckRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> ExecuteAsync(CreatePitchDeckCommand model)
        {
            var images = model.Images.Select(x => new PitchDeckImage
            {
                ImageId = x.ImageId,
                ImageName = x.ImageName,
                Path = x.Path,
                ImageType = x.Format.ToString(),
            }).ToList();

            var pitchDeck = new PitchDeck
            {
                Images = images
            };

            await _repository.SaveAsync(pitchDeck);

            return CommandResult.Succeeded(pitchDeck.Id);
        }
    }

    public interface ICommandExecutor<T> where T : class
    {
        Task<CommandResult> ExecuteAsync(T model);
    }
}
