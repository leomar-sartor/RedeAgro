using RedeAgro.Entidade;
using RedeAgro.Object;

namespace RedeAgro.Intefaces
{
    public interface ICredenciadoRepository
    {
        Task AddAsync(Credenciado obj);
        Task<Credenciado> GetByIdAsync(Guid id);
        Task<IEnumerable<Credenciado>> GetAllAsync();
        Task<IEnumerable<CredenciadoLocationValueObject>> GetProximityAsync(double latitude, double longitude, int raio, string? name);

        Task DeleteAsync(Guid id);
        Task UpdateAsync(Credenciado id);
    }
}
