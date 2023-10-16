using Microsoft.AspNetCore.Identity;
using Models;
using ViewModel;

namespace Repository_Pattern
{
    public class AccountManager : MainManager<User>
    {
        UserManager<User> UserManager { get; set; }
        SignInManager<User> SignInManager { get; set; }
        public AccountManager(MyDBContext _dBContext, UserManager<User> _UserManager, SignInManager<User> _SignInManager) : base(_dBContext)
        {
            UserManager = _UserManager;
            SignInManager = _SignInManager;
        }

        public async Task<IdentityResult> Register(RegisterationViewModel RegisterationData)
        {
            return await UserManager.CreateAsync(RegisterationData.ToUser(), RegisterationData.Password);
        }

        public async Task<SignInResult> Login(LoginViewModel LoginData)
        {
            var user = await UserManager.FindByEmailAsync(LoginData.Email);

            return await SignInManager.PasswordSignInAsync(user?.UserName, LoginData.Password, LoginData.IsPresistent, true);
        }

        public async void Logout()
        {
            await SignInManager.SignOutAsync();
        }
    }
}
