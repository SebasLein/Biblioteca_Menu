using System;

namespace Models
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

        // Método resumen corto
        public string ResumenCorto()
        {
            return $"{Id} - {Nombre}";
        }

        // Método detalle completo
        public string DetalleCompleto()
        {
            return $"ID: {Id}\nNombre: {Nombre}\nEmail: {Email}\nActivo: {Activo}";
        }

        // Override ToString
        public override string ToString()
        {
            return $"{Nombre} ({Email})";
        }
    }
}