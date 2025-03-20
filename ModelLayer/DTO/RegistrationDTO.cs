using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class RegistrationDTO
    {
        public string FirstName {  get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } 

        public string Password { get; set; } = string.Empty;

        override
        public string ToString() 
        {
            return FirstName + " " + LastName + " " + UserName + " " + Email + " " + Phone + " " + Password;
        }
    }
}
