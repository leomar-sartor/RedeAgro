using RedeAgro.Entidade;
using RedeAgro.Inputs;
using RedeAgro.Intefaces;
using RedeAgro.Models;
using RedeAgro.Object;

namespace RedeAgro.Services
{
    public class CredenciadoService : ICredenciadoService
    {
        private readonly ICredenciadoRepository _credenciadoRepository;

        public CredenciadoService(ICredenciadoRepository credencaidoRepository)
        {
            _credenciadoRepository = credencaidoRepository;
        }

        public async Task<Credenciado> AddAsync(CredenciadoInput input)
        {
            var obj = new Credenciado()
            {
                Id = input.Id,
                Nome = input.Nome,
                Email = input.Email,
                Telefone = input.Telefone,
                Formacoes = input.Formacoes,
                Especialidades = input.Especialidades,
                Atuacoes = input.Atuacoes,
                Servicos = input.Servicos,
                Location = input.Location
            };

            await _credenciadoRepository.AddAsync(obj);

            return obj;
        }

        public async Task<Credenciado> GetByIdAsync(Guid id)
        {
            return await _credenciadoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Credenciado>> GetAllAsync()
        {
            return await _credenciadoRepository.GetAllAsync();
        }

        public async Task<IEnumerable<CredenciadoLocationValueObject>> GetProximityAsync(double latitude, double longitude, int raio, string? name = "")
        {
            return await _credenciadoRepository.GetProximityAsync(latitude, longitude, raio, name);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _credenciadoRepository.DeleteAsync(id);
        }

        public async Task<Credenciado> UpdateAsync(Guid id, CredenciadoInput input)
        {
            var obj = new Credenciado()
            {
                Id = id,
                Nome = input.Nome,
                Email = input.Email,
                Telefone = input.Telefone,
                Formacoes = input.Formacoes,
                Especialidades = input.Especialidades,
                Atuacoes = input.Atuacoes,
                Servicos = input.Servicos,
                Location = input.Location
            };

            await _credenciadoRepository.UpdateAsync(obj);
            return await _credenciadoRepository.GetByIdAsync(obj.Id);
        }
    }
}
