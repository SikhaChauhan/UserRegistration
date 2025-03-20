//using ModelLayer.DTO;

using BusinessLayer.Interface;
using ModelLayer.DTO;
using RepositoryLayer;
using RepositoryLayer.Interface;

namespace BusinessLayer
{
    public class UserRegistrationBL : IUserRergistrationBL
    {
        private readonly IUserRegistrationRL _userRegistrationRL;
        public UserRegistrationBL(IUserRegistrationRL userRegistrationRL) 
        {
            _userRegistrationRL = userRegistrationRL;
        }

        public string GetUserBL() 
        {
            
            
            return _userRegistrationRL.GetUserRL();
        }

        public bool RegisterUserBL(RegistrationDTO registration) 
        {
            //bool registered = _userRegistrationRL.RegisterUserRL(registration.ToString());

            return _userRegistrationRL.RegisterUserRL(registration);
        }

        public string LoginUserBL(LoginDTO login) 
        { 
            return _userRegistrationRL.LoginUserRL(login);
        }
    }
}
