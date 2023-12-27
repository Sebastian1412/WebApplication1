using System.Data.Entity;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rut { get; set; }
        public string Correo { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
    }
}

public class DbModels : DbContext
{
    public DbSet<Usuarios> Usuarios { get; set; }
}

