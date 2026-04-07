using System;
using System.Linq;
using services;
using Models;

class Program
{
    // Servicios (memoria)
    static LibroService libroService = new LibroService();
    static UsuarioService usuarioService = new UsuarioService();
    static PrestamoService prestamoService = new PrestamoService();

    static void Main()
    {
        // Seed inicial para que el sistema tenga datos
        SeedDataIfEmpty();

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("===== SISTEMA BIBLIOTECA =====");
            Console.WriteLine("1. Libros");
            Console.WriteLine("2. Usuarios");
            Console.WriteLine("3. Préstamos");
            Console.WriteLine("4. Búsquedas y reportes");
            Console.WriteLine("5. Guardar / Cargar datos");
            Console.WriteLine("6. Probar Services");
            Console.WriteLine("7. Comparar Arrays vs List");
            Console.WriteLine("8. Salir");
            Console.Write("Seleccione una opción: ");

            int option = ReadOption(1, 8);

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
                    ProbarServicios();
                    break;
                case 7:
                    CompararArrayVsList();
                    break;
                case 8:
                    running = !ConfirmExit();
                    break;
            }
        }
    }

    // =========================
    // SEED (2 libros, 2 usuarios, 1 préstamo)
    // =========================
    static void SeedDataIfEmpty()
    {
        if (libroService.ObtenerTotalLibros() > 0 ||
            usuarioService.ObtenerTotalUsuarios() > 0 ||
            prestamoService.TotalPrestamos() > 0)
            return;

        var libro1 = new Libro(1, "Cien Años de Soledad", "Gabriel García Márquez", 1967);
        var libro2 = new Libro(2, "1984", "George Orwell", 1949);

        var usuario1 = new Usuario(1, "Juan Pérez", "juan@email.com");
        var usuario2 = new Usuario(2, "Ana Gómez", "ana@email.com");

        libroService.AgregarLibro(libro1);
        libroService.AgregarLibro(libro2);
        usuarioService.AgregarUsuario(usuario1);
        usuarioService.AgregarUsuario(usuario2);

        // Préstamo activo seed (libro1 pasa a no disponible dentro del service al crear el préstamo)
        var prestamo1 = new Prestamo(1, libro1, usuario1, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));
        prestamoService.CrearPrestamo(prestamo1);
    }

    // =========================
    // HELPERS DE INPUT
    // =========================
    static int ReadOption(int min, int max)
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int option) && option >= min && option <= max)
                return option;

            Console.Write("Opción inválida. Intente nuevamente: ");
        }
    }

    static int ReadInt(string label)
    {
        while (true)
        {
            Console.Write(label);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int value))
                return value;

            Console.WriteLine("Entrada inválida. Debe ser un número.");
        }
    }

    static string ReadText(string label)
    {
        while (true)
        {
            Console.Write(label);
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("Entrada inválida. No puede estar vacía.");
        }
    }

    static bool ConfirmYesNo(string label)
    {
        while (true)
        {
            Console.Write(label);
            string? ans = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ans)) continue;

            ans = ans.Trim().ToUpper();
            if (ans == "S") return true;
            if (ans == "N") return false;

            Console.WriteLine("Respuesta inválida. Use S/N.");
        }
    }

    static void Pause()
    {
        Console.WriteLine("\nPresione una tecla para continuar...");
        Console.ReadKey();
    }

    // =========================
    // MENÚ LIBROS (CRUD real en COMMIT 2)
    // =========================
    static void BooksMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("===== MENÚ LIBROS =====");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Listar libros");
            Console.WriteLine("3. Ver detalle de libro");
            Console.WriteLine("4. Actualizar libro");
            Console.WriteLine("5. Eliminar libro");
            Console.WriteLine("0. Volver");

            int option = ReadOption(0, 5);

            switch (option)
            {
                case 0:
                    back = true;
                    break;
                default:
                    Console.WriteLine("[PENDIENTE] CRUD real de libros se implementa en COMMIT 2.");
                    Pause();
                    break;
            }
        }
    }

    // =========================
    // MENÚ USUARIOS (CRUD real en COMMIT 2)
    // =========================
    static void UsersMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("===== MENÚ USUARIOS =====");
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Ver detalle de usuario");
            Console.WriteLine("4. Actualizar usuario");
            Console.WriteLine("5. Eliminar usuario");
            Console.WriteLine("0. Volver");

            int option = ReadOption(0, 5);

            switch (option)
            {
                case 0:
                    back = true;
                    break;
                default:
                    Console.WriteLine("[PENDIENTE] CRUD real de usuarios se implementa en COMMIT 2.");
                    Pause();
                    break;
            }
        }
    }

    // =========================
    // MENÚ PRÉSTAMOS (CRUD real en COMMIT 3)
    // =========================
    static void LoansMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("===== MENÚ PRÉSTAMOS =====");
            Console.WriteLine("1. Registrar préstamo");
            Console.WriteLine("2. Registrar devolución");
            Console.WriteLine("3. Listar préstamos");
            Console.WriteLine("4. Ver detalle de préstamo");
            Console.WriteLine("5. Eliminar préstamo");
            Console.WriteLine("0. Volver");

            int option = ReadOption(0, 5);

            switch (option)
            {
                case 0:
                    back = true;
                    break;
                default:
                    Console.WriteLine("[PENDIENTE] CRUD real de préstamos se implementa en COMMIT 3.");
                    Pause();
                    break;
            }
        }
    }

    // =========================
    // BÚSQUEDAS / REPORTES (COMMIT 3)
    // =========================
    static void SearchReportsMenu()
    {
        Console.Clear();
        Console.WriteLine("[PENDIENTE] Búsquedas y reportes se implementan en COMMIT 3.");
        Pause();
    }

    // =========================
    // PERSISTENCIA (COMMIT 3)
    // =========================
    static void PersistenceMenu()
    {
        Console.Clear();
        Console.WriteLine("[PENDIENTE] Guardar/Cargar/Reset (simulado) se implementa en COMMIT 3.");
        Pause();
    }

    // =========================
    // OPCIÓN 6: PROBAR SERVICES (ya funcional desde COMMIT 1)
    // =========================
    static void ProbarServicios()
    {
        Console.Clear();
        Console.WriteLine("=== PRUEBA DE SERVICES ===\n");

        Console.WriteLine($"Total libros: {libroService.ObtenerTotalLibros()}");
        Console.WriteLine($"Libros disponibles: {libroService.ObtenerLibrosDisponibles()}");
        Console.WriteLine($"Libros prestados: {libroService.ObtenerLibrosPrestados()}");
        Console.WriteLine();

        Console.WriteLine($"Total usuarios: {usuarioService.ObtenerTotalUsuarios()}");
        Console.WriteLine($"Usuarios activos: {usuarioService.ObtenerUsuariosActivos()}");
        Console.WriteLine($"Usuarios inactivos: {usuarioService.ObtenerUsuariosInactivos()}");
        Console.WriteLine();

        Console.WriteLine($"Total préstamos: {prestamoService.TotalPrestamos()}");
        Console.WriteLine($"Préstamos activos: {prestamoService.PrestamosActivos()}");
        Console.WriteLine($"Préstamos finalizados: {prestamoService.PrestamosFinalizados()}");
        Console.WriteLine();

        Console.WriteLine("Ordenar libros por año:");
        foreach (var l in libroService.OrdenarPorAnio())
            Console.WriteLine(" - " + l.ResumenCorto());

        Console.WriteLine("\n=== FIN PRUEBA ===");
        Pause();
    }

    // =========================
    // OPCIÓN 7: COMPARAR ARRAYS vs LIST (ya funcional desde COMMIT 1)
    // =========================
    static void CompararArrayVsList()
    {
        Console.Clear();
        Console.WriteLine("=== COMPARACIÓN ARRAY vs LIST ===\n");

        // ARRAY: tamaño fijo
        string[] arrayNombres = new string[2];
        arrayNombres[0] = "Ana";
        arrayNombres[1] = "Juan";

        Console.WriteLine($"ARRAY tamaño fijo = {arrayNombres.Length}");
        Console.WriteLine($"ARRAY[0]={arrayNombres[0]}, ARRAY[1]={arrayNombres[1]}");

        Console.WriteLine("\nPara agregar un 3er elemento en ARRAY, toca crear uno nuevo y copiar:");
        string[] nuevoArray = new string[arrayNombres.Length + 1];
        for (int i = 0; i < arrayNombres.Length; i++)
            nuevoArray[i] = arrayNombres[i];
        nuevoArray[2] = "Carlos";

        Console.WriteLine($"Nuevo ARRAY tamaño = {nuevoArray.Length}");
        Console.WriteLine($"Nuevo ARRAY[2]={nuevoArray[2]}");

        Console.WriteLine("\nLIST: tamaño dinámico");
        var listNombres = new System.Collections.Generic.List<string>();
        listNombres.Add("Ana");
        listNombres.Add("Juan");
        Console.WriteLine($"LIST Count = {listNombres.Count}");

        listNombres.Add("Carlos");
        Console.WriteLine($"LIST Count después de Add = {listNombres.Count}");

        listNombres.Remove("Ana");
        Console.WriteLine($"LIST Count después de Remove = {listNombres.Count}");

        Console.WriteLine("\nConclusión:");
        Console.WriteLine("- ARRAY: tamaño fijo, para crecer toca copiar.");
        Console.WriteLine("- LIST: tamaño dinámico, Add/Remove fácil.");

        Pause();
    }

    // =========================
    // SALIDA
    // =========================
    static bool ConfirmExit()
    {
        bool save = ConfirmYesNo("¿Desea guardar antes de salir? (S/N): ");
        if (save)
        {
            Console.WriteLine("[INFO] Guardando datos... (simulación)");
            Console.WriteLine("[OK] Datos guardados.");
        }
        else
        {
            Console.WriteLine("[INFO] No se guardaron cambios.");
        }

        Console.WriteLine("[SYSTEM] Cerrando aplicación...");
        Pause();
        return true;
    }
}
