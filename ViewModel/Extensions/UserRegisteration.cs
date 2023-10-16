using Models;

namespace ViewModel
{
    public static class UserRegisteration
    {
        public static User ToUser(this RegisterationViewModel RegisterationData)
        {
            return new User
            {
                UserName = RegisterationData.UserName,
                Email = RegisterationData.Email,
                PhoneNumber = RegisterationData.PhoneNumber,
            };
        }
    }
}
