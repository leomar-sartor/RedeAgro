using RedeAgro.Entidade;
using RedeAgro.Inputs;
using RedeAgro.Object;

namespace RedeAgro.Intefaces
{
    public interface ICredenciadoService
    {
        Task<Credenciado> AddAsync(CredenciadoInput obj);
        Task<Credenciado> GetByIdAsync(Guid id);
        Task<IEnumerable<Credenciado>> GetAllAsync();
        Task<IEnumerable<CredenciadoLocationValueObject>> GetProximityAsync(double latitude, double longitude, int raio, string? name);

        Task DeleteAsync(Guid id);
        Task<Credenciado> UpdateAsync(Guid Id, CredenciadoInput obj);
    }
}
