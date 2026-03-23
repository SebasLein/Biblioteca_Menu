using System;

namespace system_books.Models
{
    public class Prestamo
    {
        // Propiedades
        public int Id { get; set; }
        public Libro Libro { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public EstadoPrestamo Estado { get; set; }

        // Constructor vacío
        public Prestamo()
        {
            Estado = EstadoPrestamo.Activo;
            FechaDevolucion = null;
        }

        // Constructor completo
        public Prestamo(int id, Libro libro, Usuario usuario, DateTime fechaPrestamo, DateTime fechaVencimiento)
        {
            Id = id;
            Libro = libro;
            Usuario = usuario;
            FechaPrestamo = fechaPrestamo;
            FechaVencimiento = fechaVencimiento;
            Estado = EstadoPrestamo.Activo;
            FechaDevolucion = null;
        }

                // Verifica si el préstamo está vencido
        public bool EstaVencido()
        {
            return DateTime.Now > FechaVencimiento && Estado == EstadoPrestamo.Activo;
        }

        // Calcula días transcurridos desde el préstamo
        public int DiasTranscurridos()
        {
            return (DateTime.Now - FechaPrestamo).Days;
        }

        // Resumen corto
        public string ResumenCorto()
        {
            return $"Prestamo #{Id} - {Libro.Titulo} a {Usuario.Nombre}";
        }

        // Detalle completo
        public string DetalleCompleto()
        {
            return $"ID: {Id}\nLibro: {Libro.Titulo}\nUsuario: {Usuario.Nombre}\nFecha Préstamo: {FechaPrestamo}\nFecha Vencimiento: {FechaVencimiento}\nFecha Devolución: {FechaDevolucion}\nEstado: {Estado}";
        }

        // Override ToString
        public override string ToString()
        {
            return $"{Libro.Titulo} -> {Usuario.Nombre} ({Estado})";
        }
    }
}