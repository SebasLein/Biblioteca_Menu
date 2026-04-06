using System;
using System.Collections.Generic;
using system_books.Models; // ajusta el namespace si es necesario

namespace system_books.Services
{
    public class LibroService
    {
        private List<Libro> libros = new List<Libro>();

        // Agregar libro
        public void AgregarLibro(Libro libro)
        {
            libros.Add(libro);
        }

        // Obtener todos
        public List<Libro> ObtenerTodos()
        {
            return libros;
        }

        // Eliminar libro (básico)
        public void EliminarLibro(Libro libro)
        {
            libros.Remove(libro);
        }
    }
}