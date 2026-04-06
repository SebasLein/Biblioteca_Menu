using System;
using System.Collections.Generic;
using System.Linq;
using system_books.Models;

namespace system_books.Services
{
    public class PrestamoService
    {
        private List<Prestamo> prestamos = new List<Prestamo>();

        // Crear préstamo
        public void CrearPrestamo(Prestamo prestamo)
        {
            prestamo.Libro.Disponible = false;
            prestamo.Estado = EstadoPrestamo.Activo;
            prestamos.Add(prestamo);
        }

        // Devolver libro
        public void DevolverLibro(int prestamoId)
        {
            var prestamo = prestamos.Find(p => p.Id == prestamoId);

            if (prestamo != null && prestamo.Estado == EstadoPrestamo.Activo)
            {
                prestamo.Estado = EstadoPrestamo.Devuelto;
                prestamo.FechaDevolucion = DateTime.Now;
                prestamo.Libro.Disponible = true;
            }
        }

        // Obtener todos
        public List<Prestamo> ObtenerTodos()
        {
            return prestamos;
        }

        // Buscar por usuario
        public List<Prestamo> BuscarPorUsuario(int usuarioId)
        {
            return prestamos.Where(p => p.Usuario.Id == usuarioId).ToList();
        }

        // Buscar activos
        public List<Prestamo> ObtenerPrestamosActivos()
        {
            return prestamos.Where(p => p.Estado == EstadoPrestamo.Activo).ToList();
        }

        // KPIs
        public int TotalPrestamos()
        {
            return prestamos.Count;
        }

        public int PrestamosActivos()
        {
            return prestamos.Count(p => p.Estado == EstadoPrestamo.Activo);
        }

        public int PrestamosFinalizados()
        {
            return prestamos.Count(p => p.Estado != EstadoPrestamo.Activo);
        }
    }
}