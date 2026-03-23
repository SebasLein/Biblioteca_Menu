using System;

namespace system_books.Models
{
    public class Libro
    {
        // Propiedades
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Anio { get; set; }
        public bool Disponible { get; set; }

        // Constructor vacío
        public Libro()
        {
            Disponible = true;
        }

        // Constructor completo
        public Libro(int id, string titulo, string autor, int anio)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            Anio = anio;
            Disponible = true;
        }

        // Método resumen corto
        public string ResumenCorto()
        {
            return $"{Id} - {Titulo} ({Autor})";
        }

        // Método detalle completo
        public string DetalleCompleto()
        {
            return $"ID: {Id}\nTítulo: {Titulo}\nAutor: {Autor}\nAño: {Anio}\nDisponible: {Disponible}";
        }

        // Override ToString
        public override string ToString()
        {
            return $"{Titulo} - {Autor} ({Anio})";
        }
    }
}