using Microsoft.AspNetCore.Identity;
using RedeAgro.Models;

namespace RedeAgro
{
    public static class DataInitializer
    {
        internal static async void CadastrarPrimeirosUsuarios(UserManager<UserApp> userManager)
        {
            try
            {
                UserApp userAdmin = userManager.FindByEmailAsync("admin@meuagro.net").Result;
                if (userAdmin is null)
                {
                    var newUserAdmin = new UserApp
                    {
                        UserName = "admin@meuagro.net",
                        Email = "admin@meuagro.net",
                        EmailConfirmed = true
                    };

                    IdentityResult result = userManager.CreateAsync(newUserAdmin, "Xilindr0_").Result;

                    userManager.AddToRoleAsync(newUserAdmin, "ADMIN").Wait();
                }
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }


            UserApp userProdutor = userManager.FindByEmailAsync("user@meuagro.net").Result;
            if (userProdutor is null)
            {
                var newUserProdutor = new UserApp
                {
                    UserName = "user@meuagro.net",
                    Email = "user@meuagro.net",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(newUserProdutor, "Xilindr0_").Result;

                userManager.AddToRoleAsync(newUserProdutor, "USER").Wait();
            }
        }
    }
}
