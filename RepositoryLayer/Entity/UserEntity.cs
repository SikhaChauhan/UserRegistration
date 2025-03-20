using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    //Define the table structure here 
    public class UserEntity
    {
        [Key]
        public int UserId {  get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email {  get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string PasswordHash { get; set; }

       //After defining structure Create a context class ->  

       
    }
}
