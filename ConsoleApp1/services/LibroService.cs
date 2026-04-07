using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace services
{
    public class LibroService
    {
        private List<Libro> libros = new List<Libro>();

        // =========================
        // CRUD BÁSICO
        // =========================

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

        // Eliminar libro
        public void EliminarLibro(Libro libro)
        {
            libros.Remove(libro);
        }

        // =========================
        // BÚSQUEDAS
        // =========================

        // Buscar por ID
        public Libro BuscarPorId(int id)
        {
            return libros.Find(l => l.Id == id);
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

        // =========================
        // ORDENACIÓN
        // =========================

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

        // =========================
        // KPIs
        // =========================

        // Total de libros
        public int ObtenerTotalLibros()
        {
            return libros.Count;
        }

        // Libros disponibles
        public int ObtenerLibrosDisponibles()
        {
            return libros.Count(l => l.Disponible);
        }

        // Libros prestados
        public int ObtenerLibrosPrestados()
        {
            return libros.Count(l => !l.Disponible);
        }
    }
}