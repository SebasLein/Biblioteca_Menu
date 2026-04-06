using System;
using system_books.Models;

class Program
{
    static void Main()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("===== SISTEMA DE BIBLIOTECA =====");
            Console.WriteLine("1. Libros");
            Console.WriteLine("2. Usuarios");
            Console.WriteLine("3. Prestamos");
            Console.WriteLine("4. Busquedas y reportes");
            Console.WriteLine("5. Guardar / Cargar datos");
            Console.WriteLine("6. Probar modelos");
            Console.WriteLine("7. Salir");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Entrada invalida.");
                Console.ReadKey();
                continue;
            }

            switch (option)
            {
                case 1:
                    BooksMenu();
                    break;

                case 2:
                    UsersMenu();
                    break;

                case 3:
                    LoansMenu();
                    break;

                case 4:
                    SearchReportsMenu();
                    break;

                case 5:
                    PersistenceMenu();
                    break;

                case 6:
                    ProbarModelos();
                    break;

                case 7:
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opcion invalida");
                    Console.ReadKey();
                    break;
            }

        } while (option != 7);
    }

    // ============================
    // PRUEBA DE MODELOS (DEMO)
    // ============================
    static void ProbarModelos()
    {
        Console.Clear();
        Console.WriteLine("=== PRUEBA DE MODELOS ===\n");

        Libro libro1 = new Libro(1, "Cien Años de Soledad", "Gabriel García Márquez", 1967);
        Usuario usuario1 = new Usuario(1, "Juan Pérez", "juan@email.com");

        Prestamo prestamo1 = new Prestamo(
            1,
            libro1,
            usuario1,
            DateTime.Now.AddDays(-5),
            DateTime.Now.AddDays(5)
        );

        Console.WriteLine(libro1.ResumenCorto());
        Console.WriteLine(libro1.DetalleCompleto());

        Console.WriteLine("\n-------------------\n");

        Console.WriteLine(usuario1.ResumenCorto());
        Console.WriteLine(usuario1.DetalleCompleto());

        Console.WriteLine("\n-------------------\n");

        Console.WriteLine(prestamo1.ResumenCorto());
        Console.WriteLine(prestamo1.DetalleCompleto());

        Console.WriteLine("\n¿Está vencido?: " + prestamo1.EstaVencido());
        Console.WriteLine("Días transcurridos: " + prestamo1.DiasTranscurridos());

        Console.WriteLine("\n=== FIN PRUEBA ===");
        Console.ReadKey();
    }

    // ============================
    // MENÚ LIBROS
    // ============================
    static void BooksMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("===== MENU LIBROS =====");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Listar libros");
            Console.WriteLine("3. Ver detalle de libro");
            Console.WriteLine("4. Actualizar libro");
            Console.WriteLine("5. Eliminar libro");
            Console.WriteLine("6. Volver");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Entrada invalida");
                Console.ReadKey();
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("Registrando libro...");
                    break;

                case 2:
                    ListBooksMenu();
                    break;

                case 3:
                    Console.WriteLine("Mostrando detalle del libro...");
                    break;

                case 4:
                    Console.WriteLine("Actualizando libro...");
                    break;

                case 5:
                    Console.WriteLine("Eliminando libro...");
                    break;
            }

            Console.ReadKey();

        } while (option != 6);
    }

    static void ListBooksMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("===== LISTAR LIBROS =====");
            Console.WriteLine("1. Listar todos");
            Console.WriteLine("2. Listar disponibles");
            Console.WriteLine("3. Listar prestados");
            Console.WriteLine("4. Volver");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Entrada invalida");
                Console.ReadKey();
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("Listando todos los libros...");
                    break;

                case 2:
                    Console.WriteLine("Listando libros disponibles...");
                    break;

                case 3:
                    Console.WriteLine("Listando libros prestados...");
                    break;
            }

            Console.ReadKey();

        } while (option != 4);
    }

    // ============================
    // MENÚ USUARIOS
    // ============================
    static void UsersMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("===== MENU USUARIOS =====");
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Actualizar usuario");
            Console.WriteLine("4. Eliminar usuario");
            Console.WriteLine("5. Volver");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Entrada invalida");
                Console.ReadKey();
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("Registrando usuario...");
                    break;

                case 2:
                    Console.WriteLine("Listando usuarios...");
                    break;

                case 3:
                    Console.WriteLine("Actualizando usuario...");
                    break;

                case 4:
                    Console.WriteLine("Eliminando usuario...");
                    break;
            }

            Console.ReadKey();

        } while (option != 5);
    }

    // ============================
    // MENÚ PRÉSTAMOS
    // ============================
    static void LoansMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("===== MENU PRESTAMOS =====");
            Console.WriteLine("1. Registrar prestamo");
            Console.WriteLine("2. Devolver libro");
            Console.WriteLine("3. Listar prestamos activos");
            Console.WriteLine("4. Volver");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Entrada invalida");
                Console.ReadKey();
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("Registrando prestamo...");
                    break;

                case 2:
                    Console.WriteLine("Devolviendo libro...");
                    break;

                case 3:
                    Console.WriteLine("Listando prestamos activos...");
                    break;
            }

            Console.ReadKey();

        } while (option != 4);
    }

    // ============================
    // BUSQUEDAS Y REPORTES
    // ============================
    static void SearchReportsMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("===== BUSQUEDAS Y REPORTES =====");
            Console.WriteLine("1. Buscar libro por titulo");
            Console.WriteLine("2. Buscar usuario por nombre");
            Console.WriteLine("3. Reporte de libros prestados");
            Console.WriteLine("4. Volver");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Entrada invalida");
                Console.ReadKey();
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("Buscando libro...");
                    break;

                case 2:
                    Console.WriteLine("Buscando usuario...");
                    break;

                case 3:
                    Console.WriteLine("Generando reporte...");
                    break;
            }

            Console.ReadKey();

        } while (option != 4);
    }

    // ============================
    // PERSISTENCIA
    // ============================
    static void PersistenceMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("===== GUARDAR / CARGAR =====");
            Console.WriteLine("1. Guardar datos");
            Console.WriteLine("2. Cargar datos");
            Console.WriteLine("3. Volver");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Entrada invalida");
                Console.ReadKey();
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine("Guardando datos...");
                    break;

                case 2:
                    Console.WriteLine("Cargando datos...");
                    break;
            }

            Console.ReadKey();

        } while (option != 3);
    }
}