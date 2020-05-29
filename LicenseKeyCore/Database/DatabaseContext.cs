using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseKeyCore.Database.Entities;

namespace LicenseKeyCore.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DataKeys> tblDataKeys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {//Persist Security Info=False;Data Source=[server name];Initial Catalog=[DataBase Name];User ID=myUsername;Password=myPassword
         
           // optionsBuilder.UseSqlServer("Integrated Security=true;Data Source=.;Initial Catalog=DataKeysMicroservice;");

             optionsBuilder.UseSqlServer("Persist Security Info=False;Data Source=172.17.0.3,1433;Initial Catalog=DataKeysMicroservice;User ID=sa;Password=Applica6994+-*");
        }
    }
}
