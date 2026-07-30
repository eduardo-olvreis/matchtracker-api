using MatchTracker.Api.Repositories;

namespace MatchTracker.Api.Services
{
    public class PartidaService : IPartidaService
    {
        private readonly IPartidaRepository _repository;
        public PartidaService(IPartidaRepository repository)
        {
            _repository = repository;
        }
    }
}
