using System;

namespace system_books.Models
{
    public class Usuario
    {
        // Propiedades
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }

        // Constructor vacío
        public Usuario()
        {
            Activo = true;
        }

        // Constructor completo
        public Usuario(int id, string nombre, string email)
        {
            Id = id;
            Nombre = nombre;
            Email = email;
            Activo = true;
        }
    }
}