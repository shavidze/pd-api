using Model;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.PitchDeckRepository
{
    public interface IPitchDeckRepository
    {
        Task SaveAsync(PitchDeck pitchDeck);
    }
}
