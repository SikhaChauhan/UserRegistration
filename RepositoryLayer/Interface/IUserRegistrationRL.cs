using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO;

namespace RepositoryLayer.Interface
{
    public interface IUserRegistrationRL  
    {
        string GetHello(string name);

        bool RegisterUserRL(RegistrationDTO Data);

        string GetUserRL();

        string LoginUserRL(LoginDTO login);

    }
}
