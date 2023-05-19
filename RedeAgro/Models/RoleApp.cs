using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace RedeAgro.Models
{
    [CollectionName("Roles")]
    public class RoleApp : MongoIdentityRole<Guid>
    {
    }
}
