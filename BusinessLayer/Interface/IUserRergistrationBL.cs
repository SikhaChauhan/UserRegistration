using ModelLayer.DTO;

namespace BusinessLayer.Interface
{
    public interface IUserRergistrationBL
    {
        string GetUserBL();

        bool RegisterUserBL(RegistrationDTO registration);
        string LoginUserBL(LoginDTO login);
    }
}
