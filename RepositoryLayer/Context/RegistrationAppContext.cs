using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Context
{
    //Create the Context class here -> 
    //The RegistrationAppContext class is your Entity Framework Core (EF Core) database context, which acts as a bridge between your ASP.NET Core application and the database. 
    public class RegistrationAppContext : DbContext
    {
        public RegistrationAppContext(DbContextOptions<RegistrationAppContext> options) :base(options)
        { }


        public virtual DbSet<Entity.UserEntity> Users { get; set; }//Here the UserEntity defines the table structure in the dataBase and the users is the name of the table 
    }
  
    
}
