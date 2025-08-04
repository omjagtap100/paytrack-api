using paytrack_api.Models;
using paytrack_api.Services.Interfaces;
using paytrack_api.Repository.Interfaces;

namespace paytrack_api.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Task<IEnumerable<Client>> GetAll()
        {
            return Task.FromResult(_clientRepository.GetAll());
        }

        public Task<Client> GetById(int id)
        {
            return Task.FromResult(_clientRepository.GetById(id));
        }

        public Task<bool> Add(Client client)
        {
            return Task.FromResult(_clientRepository.Add(client));
        }

        public Task<bool> Update(Client client)
        {
            return Task.FromResult(_clientRepository.Update(client));
        }

        public Task<bool> Delete(Client client)
        {
            return Task.FromResult(_clientRepository.Delete(client));
        }
    }
}
