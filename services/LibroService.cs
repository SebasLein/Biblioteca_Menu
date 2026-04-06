using System;
using System.Collections.Generic;
using system_books.Models;
using System.Linq;

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

        // Buscar por ISBN
        public Libro BuscarPorISBN(string isbn)
        {
            return libros.Find(l => l.ISBN == isbn);
        }

        // Buscar por título
        public List<Libro> BuscarPorTitulo(string titulo)
        {
            return libros.FindAll(l => l.Titulo.ToLower().Contains(titulo.ToLower()));
        }

        // Buscar por autor
        public List<Libro> BuscarPorAutor(string autor)
        {
            return libros.FindAll(l => l.Autor.ToLower().Contains(autor.ToLower()));
        }

        // Ordenar por título
        public List<Libro> OrdenarPorTitulo()
        {
            return libros.OrderBy(l => l.Titulo).ToList();
        }

        // Ordenar por año
        public List<Libro> OrdenarPorAnio()
        {
            return libros.OrderBy(l => l.Anio).ToList();
        }
    }
}