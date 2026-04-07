using System;
using System.Collections.Generic;
using System.Linq;
using services;
using Models;

class Program
{
    // Servicios (memoria) -> NO readonly para permitir reset/load
    static LibroService libroService = new LibroService();
    static UsuarioService usuarioService = new UsuarioService();
    static PrestamoService prestamoService = new PrestamoService();

    // "Persistencia" simulada en memoria (snapshots)
    static List<Libro>? savedLibros;
    static List<Usuario>? savedUsuarios;
    static List<Prestamo>? savedPrestamos;

    static void Main()
    {
        SeedDataIfEmpty();

        bool running = true;
        while (running)
        {
            UpdateOverdueStatuses();

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
                case 1: BooksMenu(); break;
                case 2: UsersMenu(); break;
                case 3: LoansMenu(); break;
                case 4: SearchReportsMenu(); break;
                case 5: PersistenceMenu(); break;
                case 6: ProbarServicios(); break;
                case 7: CompararArrayVsList(); break;
                case 8: running = !ConfirmExit(); break;
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

        // Préstamo activo seed (service marca libro no disponible)
        var prestamo1 = new Prestamo(1, libro1, usuario1, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));
        prestamoService.CrearPrestamo(prestamo1);
    }

    // =========================
    // HELPERS
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
    // UPDATE VENCIDOS (Prestamo.EstaVencido + Estado)
    // =========================
    static void UpdateOverdueStatuses()
    {
        var all = prestamoService.ObtenerTodos();
        foreach (var p in all)
        {
            if (p.Estado == EstadoPrestamo.Activo && p.EstaVencido())
            {
                p.Estado = EstadoPrestamo.Vencido;
                if (p.Libro != null) p.Libro.Disponible = false;
            }
        }
    }

    // =========================
    // LIBROS (CRUD REAL)
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
            Console.Write("Seleccione una opción: ");

            int option = ReadOption(0, 5);
            switch (option)
            {
                case 1: RegisterBook(); break;
                case 2: ListBooksMenu(); break;
                case 3: ViewBookDetail(); break;
                case 4: UpdateBookMenu(); break;
                case 5: DeleteBook(); break;
                case 0: back = true; break;
            }
        }
    }

    static void RegisterBook()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR LIBRO ===");

        int id = ReadInt("ID: ");
        var existing = libroService.BuscarPorId(id);
        if (existing != null)
        {
            Console.WriteLine("[ERROR] Ya existe un libro con ese ID.");
            Pause();
            return;
        }

        string titulo = ReadText("Título: ");
        string autor = ReadText("Autor: ");
        int anio = ReadInt("Año: ");

        var libro = new Libro(id, titulo, autor, anio);
        libroService.AgregarLibro(libro);

        Console.WriteLine("[OK] Libro registrado.");
        Console.WriteLine(libro.DetalleCompleto());
        Pause();
    }

    static void ListBooksMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("===== LISTAR LIBROS =====");
            Console.WriteLine("1. Listar todos");
            Console.WriteLine("2. Listar disponibles");
            Console.WriteLine("3. Listar prestados");
            Console.WriteLine("0. Volver");
            Console.Write("Seleccione una opción: ");

            int option = ReadOption(0, 3);
            var all = libroService.ObtenerTodos();

            switch (option)
            {
                case 1:
                    Console.WriteLine("\n--- TODOS ---");
                    if (all.Count == 0) Console.WriteLine("[INFO] No hay libros.");
                    else all.ForEach(l => Console.WriteLine(l.DetalleCompleto() + "\n---"));
                    Pause();
                    break;

                case 2:
                    Console.WriteLine("\n--- DISPONIBLES ---");
                    var disponibles = all.Where(l => l.Disponible).ToList();
                    if (disponibles.Count == 0) Console.WriteLine("[INFO] No hay libros disponibles.");
                    else disponibles.ForEach(l => Console.WriteLine(l.DetalleCompleto() + "\n---"));
                    Pause();
                    break;

                case 3:
                    Console.WriteLine("\n--- PRESTADOS ---");
                    var prestados = all.Where(l => !l.Disponible).ToList();
                    if (prestados.Count == 0) Console.WriteLine("[INFO] No hay libros prestados.");
                    else prestados.ForEach(l => Console.WriteLine(l.DetalleCompleto() + "\n---"));
                    Pause();
                    break;

                case 0:
                    back = true;
                    break;
            }
        }
    }

    static void ViewBookDetail()
    {
        Console.Clear();
        Console.WriteLine("=== DETALLE DE LIBRO ===");

        int id = ReadInt("ID del libro: ");
        var libro = libroService.BuscarPorId(id);

        if (libro == null)
        {
            Console.WriteLine("[INFO] Libro no encontrado.");
            Pause();
            return;
        }

        Console.WriteLine(libro.DetalleCompleto());
        Pause();
    }

    static void UpdateBookMenu()
    {
        Console.Clear();
        Console.WriteLine("=== ACTUALIZAR LIBRO ===");

        int id = ReadInt("ID del libro: ");
        var libro = libroService.BuscarPorId(id);

        if (libro == null)
        {
            Console.WriteLine("[INFO] Libro no encontrado.");
            Pause();
            return;
        }

        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("=== EDITAR LIBRO ===");
            Console.WriteLine(libro.ResumenCorto());
            Console.WriteLine("1. Editar título");
            Console.WriteLine("2. Editar autor");
            Console.WriteLine("3. Editar año");
            Console.WriteLine("0. Volver");
            Console.Write("Seleccione una opción: ");

            int option = ReadOption(0, 3);

            switch (option)
            {
                case 1:
                    libro.Titulo = ReadText("Nuevo título: ");
                    Console.WriteLine("[OK] Título actualizado.");
                    Pause();
                    break;

                case 2:
                    libro.Autor = ReadText("Nuevo autor: ");
                    Console.WriteLine("[OK] Autor actualizado.");
                    Pause();
                    break;

                case 3:
                    libro.Anio = ReadInt("Nuevo año: ");
                    Console.WriteLine("[OK] Año actualizado.");
                    Pause();
                    break;

                case 0:
                    back = true;
                    break;
            }
        }
    }

    static void DeleteBook()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR LIBRO ===");

        int id = ReadInt("ID del libro: ");
        var libro = libroService.BuscarPorId(id);

        if (libro == null)
        {
            Console.WriteLine("[INFO] Libro no encontrado.");
            Pause();
            return;
        }

        // Regla: NO eliminar si está en préstamo activo o no disponible
        bool prestamoActivo = prestamoService.ObtenerPrestamosActivos()
            .Any(p => p.Libro != null && p.Libro.Id == id);

        if (prestamoActivo || !libro.Disponible)
        {
            Console.WriteLine("[ERROR] No se puede eliminar: el libro está prestado (préstamo activo).");
            Pause();
            return;
        }

        bool confirm = ConfirmYesNo($"¿Seguro que desea eliminar '{libro.Titulo}'? (S/N): ");
        if (!confirm)
        {
            Console.WriteLine("[INFO] Cancelado.");
            Pause();
            return;
        }

        libroService.EliminarLibro(libro);
        Console.WriteLine("[OK] Libro eliminado.");
        Pause();
    }

    // =========================
    // USUARIOS (CRUD REAL)
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
            Console.Write("Seleccione una opción: ");

            int option = ReadOption(0, 5);

            switch (option)
            {
                case 1: RegisterUser(); break;
                case 2: ListUsers(); break;
                case 3: ViewUserDetail(); break;
                case 4: UpdateUserMenu(); break;
                case 5: DeleteUser(); break;
                case 0: back = true; break;
            }
        }
    }

    static void RegisterUser()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR USUARIO ===");

        int id = ReadInt("ID: ");
        var existing = usuarioService.BuscarPorId(id);
        if (existing != null)
        {
            Console.WriteLine("[ERROR] Ya existe un usuario con ese ID.");
            Pause();
            return;
        }

        string nombre = ReadText("Nombre: ");
        string email = ReadText("Email: ");

        var usuario = new Usuario(id, nombre, email);
        usuarioService.AgregarUsuario(usuario);

        Console.WriteLine("[OK] Usuario registrado.");
        Console.WriteLine(usuario.DetalleCompleto());
        Pause();
    }

    static void ListUsers()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE USUARIOS ===\n");

        var all = usuarioService.ObtenerTodos();
        if (all.Count == 0)
        {
            Console.WriteLine("[INFO] No hay usuarios.");
            Pause();
            return;
        }

        foreach (var u in all)
        {
            Console.WriteLine(u.DetalleCompleto());
            Console.WriteLine("---");
        }

        Pause();
    }

    static void ViewUserDetail()
    {
        Console.Clear();
        Console.WriteLine("=== DETALLE DE USUARIO ===");

        int id = ReadInt("ID del usuario: ");
        var user = usuarioService.BuscarPorId(id);

        if (user == null)
        {
            Console.WriteLine("[INFO] Usuario no encontrado.");
            Pause();
            return;
        }

        Console.WriteLine(user.DetalleCompleto());
        Pause();
    }

    static void UpdateUserMenu()
    {
        Console.Clear();
        Console.WriteLine("=== ACTUALIZAR USUARIO ===");

        int id = ReadInt("ID del usuario: ");
        var user = usuarioService.BuscarPorId(id);

        if (user == null)
        {
            Console.WriteLine("[INFO] Usuario no encontrado.");
            Pause();
            return;
        }

        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("=== EDITAR USUARIO ===");
            Console.WriteLine(user.ResumenCorto());
            Console.WriteLine($"Activo: {user.Activo}");
            Console.WriteLine("1. Editar nombre");
            Console.WriteLine("2. Editar email");
            Console.WriteLine("3. Activar / desactivar");
            Console.WriteLine("0. Volver");
            Console.Write("Seleccione una opción: ");

            int option = ReadOption(0, 3);

            switch (option)
            {
                case 1:
                    user.Nombre = ReadText("Nuevo nombre: ");
                    Console.WriteLine("[OK] Nombre actualizado.");
                    Pause();
                    break;

                case 2:
                    user.Email = ReadText("Nuevo email: ");
                    Console.WriteLine("[OK] Email actualizado.");
                    Pause();
                    break;

                case 3:
                    user.Activo = !user.Activo;
                    Console.WriteLine(user.Activo ? "[OK] Usuario activado." : "[OK] Usuario desactivado.");
                    Pause();
                    break;

                case 0:
                    back = true;
                    break;
            }
        }
    }

    static void DeleteUser()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR USUARIO ===");

        int id = ReadInt("ID del usuario: ");
        var user = usuarioService.BuscarPorId(id);

        if (user == null)
        {
            Console.WriteLine("[INFO] Usuario no encontrado.");
            Pause();
            return;
        }

        // Regla: NO eliminar si tiene préstamo activo
        bool prestamoActivo = prestamoService.ObtenerPrestamosActivos()
            .Any(p => p.Usuario != null && p.Usuario.Id == id);

        if (prestamoActivo)
        {
            Console.WriteLine("[ERROR] No se puede eliminar: el usuario tiene préstamos activos.");
            Pause();
            return;
        }

        bool confirm = ConfirmYesNo($"¿Seguro que desea eliminar a '{user.Nombre}'? (S/N): ");
        if (!confirm)
        {
            Console.WriteLine("[INFO] Cancelado.");
            Pause();
            return;
        }

        usuarioService.EliminarUsuario(user);
        Console.WriteLine("[OK] Usuario eliminado.");
        Pause();
    }

    // ====== PARTE 2 CONTINÚA DESDE AQUÍ ======
    // =========================
    // PRÉSTAMOS (CRUD REAL)
    // =========================
    static void LoansMenu()
    {
        bool back = false;
        while (!back)
        {
            UpdateOverdueStatuses();

            Console.Clear();
            Console.WriteLine("===== MENÚ PRÉSTAMOS =====");
            Console.WriteLine("1. Registrar préstamo");
            Console.WriteLine("2. Registrar devolución");
            Console.WriteLine("3. Listar préstamos");
            Console.WriteLine("4. Ver detalle de préstamo");
            Console.WriteLine("5. Eliminar préstamo");
            Console.WriteLine("0. Volver");
            Console.Write("Seleccione una opción: ");

            int option = ReadOption(0, 5);

            switch (option)
            {
                case 1: CreateLoan(); break;
                case 2: RegisterReturn(); break;
                case 3: ListLoansMenu(); break;
                case 4: ViewLoanDetail(); break;
                case 5: DeleteLoan(); break;
                case 0: back = true; break;
            }
        }
    }

    static void CreateLoan()
    {
        Console.Clear();
        Console.WriteLine("=== CREAR PRÉSTAMO ===");

        int userId = ReadInt("ID del usuario: ");
        var user = usuarioService.BuscarPorId(userId);

        if (user == null)
        {
            Console.WriteLine("[ERROR] Usuario no existe.");
            Pause();
            return;
        }

        if (!user.Activo)
        {
            Console.WriteLine("[ERROR] Usuario inactivo. No puede crear préstamos.");
            Pause();
            return;
        }

        int bookId = ReadInt("ID del libro: ");
        var book = libroService.BuscarPorId(bookId);

        if (book == null)
        {
            Console.WriteLine("[ERROR] Libro no existe.");
            Pause();
            return;
        }

        if (!book.Disponible)
        {
            Console.WriteLine("[ERROR] Libro no disponible (prestado).");
            Pause();
            return;
        }

        int dias = ReadInt("Días de préstamo (ej: 7): ");
        if (dias <= 0)
        {
            Console.WriteLine("[ERROR] Los días deben ser mayores a 0.");
            Pause();
            return;
        }

        int nextId = prestamoService.ObtenerTodos().Count == 0
            ? 1
            : prestamoService.ObtenerTodos().Max(p => p.Id) + 1;

        var fechaPrestamo = DateTime.Now;
        var fechaVenc = DateTime.Now.AddDays(dias);

        var prestamo = new Prestamo(nextId, book, user, fechaPrestamo, fechaVenc);

        // CrearPrestamo marca libro no disponible y estado Activo
        prestamoService.CrearPrestamo(prestamo);

        Console.WriteLine("[OK] Préstamo creado.");
        Console.WriteLine(prestamo.DetalleCompleto());
        Pause();
    }

    static void RegisterReturn()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR DEVOLUCIÓN ===");

        int id = ReadInt("ID del préstamo: ");
        var p = prestamoService.ObtenerTodos().FirstOrDefault(x => x.Id == id);

        if (p == null)
        {
            Console.WriteLine("[INFO] Préstamo no encontrado.");
            Pause();
            return;
        }

        UpdateOverdueStatuses();

        if (p.Estado != EstadoPrestamo.Activo)
        {
            Console.WriteLine("[INFO] El préstamo no está activo. Estado actual: " + p.Estado);
            Pause();
            return;
        }

        bool confirm = ConfirmYesNo("¿Confirmar devolución? (S/N): ");
        if (!confirm)
        {
            Console.WriteLine("[INFO] Cancelado.");
            Pause();
            return;
        }

        prestamoService.DevolverLibro(id);

        Console.WriteLine("[OK] Devolución registrada.");
        Console.WriteLine(p.DetalleCompleto());
        Pause();
    }

    static void ListLoansMenu()
    {
        bool back = false;
        while (!back)
        {
            UpdateOverdueStatuses();

            Console.Clear();
            Console.WriteLine("===== LISTAR PRÉSTAMOS =====");
            Console.WriteLine("1. Todos");
            Console.WriteLine("2. Activos");
            Console.WriteLine("3. Cerrados (Devuelto/Vencido)");
            Console.WriteLine("0. Volver");
            Console.Write("Seleccione una opción: ");

            int option = ReadOption(0, 3);
            var all = prestamoService.ObtenerTodos();

            switch (option)
            {
                case 1:
                    Console.WriteLine("\n--- TODOS ---");
                    if (all.Count == 0) Console.WriteLine("[INFO] No hay préstamos.");
                    else all.ForEach(p => Console.WriteLine(p.DetalleCompleto() + "\n---"));
                    Pause();
                    break;

                case 2:
                    Console.WriteLine("\n--- ACTIVOS ---");
                    var activos = prestamoService.ObtenerPrestamosActivos();
                    if (activos.Count == 0) Console.WriteLine("[INFO] No hay préstamos activos.");
                    else activos.ForEach(p => Console.WriteLine(p.DetalleCompleto() + "\n---"));
                    Pause();
                    break;

                case 3:
                    Console.WriteLine("\n--- CERRADOS ---");
                    var cerrados = all.Where(p => p.Estado != EstadoPrestamo.Activo).ToList();
                    if (cerrados.Count == 0) Console.WriteLine("[INFO] No hay préstamos cerrados.");
                    else cerrados.ForEach(p => Console.WriteLine(p.DetalleCompleto() + "\n---"));
                    Pause();
                    break;

                case 0:
                    back = true;
                    break;
            }
        }
    }

    static void ViewLoanDetail()
    {
        Console.Clear();
        Console.WriteLine("=== DETALLE PRÉSTAMO ===");

        int id = ReadInt("ID del préstamo: ");
        var p = prestamoService.ObtenerTodos().FirstOrDefault(x => x.Id == id);

        if (p == null)
        {
            Console.WriteLine("[INFO] Préstamo no encontrado.");
            Pause();
            return;
        }

        UpdateOverdueStatuses();

        Console.WriteLine(p.DetalleCompleto());
        Console.WriteLine("Días transcurridos: " + p.DiasTranscurridos());
        Console.WriteLine("¿Está vencido?: " + p.EstaVencido());
        Console.WriteLine("Estado: " + p.Estado);
        Pause();
    }

    static void DeleteLoan()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR PRÉSTAMO ===");

        int id = ReadInt("ID del préstamo: ");
        var list = prestamoService.ObtenerTodos();
        var p = list.FirstOrDefault(x => x.Id == id);

        if (p == null)
        {
            Console.WriteLine("[INFO] Préstamo no encontrado.");
            Pause();
            return;
        }

        UpdateOverdueStatuses();

        // Regla sugerida: no eliminar activos (primero devolver)
        if (p.Estado == EstadoPrestamo.Activo)
        {
            Console.WriteLine("[ERROR] No se puede eliminar un préstamo activo. Registre devolución primero.");
            Pause();
            return;
        }

        bool confirm = ConfirmYesNo($"¿Seguro que desea eliminar el préstamo #{p.Id}? (S/N): ");
        if (!confirm)
        {
            Console.WriteLine("[INFO] Cancelado.");
            Pause();
            return;
        }

        // Asegurar libro disponible si se elimina el préstamo
        if (p.Libro != null) p.Libro.Disponible = true;

        list.Remove(p);
        Console.WriteLine("[OK] Préstamo eliminado.");
        Pause();
    }

    // =========================
    // BÚSQUEDAS Y REPORTES
    // =========================
    static void SearchReportsMenu()
    {
        bool back = false;
        while (!back)
        {
            UpdateOverdueStatuses();

            Console.Clear();
            Console.WriteLine("===== BÚSQUEDAS Y REPORTES =====");
            Console.WriteLine("1. Buscar libro (id/título/autor)");
            Console.WriteLine("2. Buscar usuario (id/nombre)");
            Console.WriteLine("3. Reporte: préstamos activos");
            Console.WriteLine("4. Reporte: préstamos vencidos");
            Console.WriteLine("5. Resumen (KPIs)");
            Console.WriteLine("0. Volver");
            Console.Write("Seleccione una opción: ");

            int option = ReadOption(0, 5);

            switch (option)
            {
                case 1: SearchBook(); break;
                case 2: SearchUser(); break;
                case 3: ReportActiveLoans(); break;
                case 4: ReportOverdueLoans(); break;
                case 5: ReportSummary(); break;
                case 0: back = true; break;
            }
        }
    }

    static void SearchBook()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR LIBRO ===");
        Console.WriteLine("1. Por ID");
        Console.WriteLine("2. Por título");
        Console.WriteLine("3. Por autor");
        Console.WriteLine("0. Volver");
        Console.Write("Seleccione una opción: ");

        int opt = ReadOption(0, 3);
        if (opt == 0) return;

        if (opt == 1)
        {
            int id = ReadInt("ID: ");
            var b = libroService.BuscarPorId(id);
            Console.WriteLine(b != null ? b.DetalleCompleto() : "[INFO] No encontrado.");
            Pause();
            return;
        }

        if (opt == 2)
        {
            string t = ReadText("Texto en título: ");
            var list = libroService.BuscarPorTitulo(t);
            if (list.Count == 0) Console.WriteLine("[INFO] Sin resultados.");
            else list.ForEach(x => Console.WriteLine(x.DetalleCompleto() + "\n---"));
            Pause();
            return;
        }

        if (opt == 3)
        {
            string a = ReadText("Texto en autor: ");
            var list = libroService.BuscarPorAutor(a);
            if (list.Count == 0) Console.WriteLine("[INFO] Sin resultados.");
            else list.ForEach(x => Console.WriteLine(x.DetalleCompleto() + "\n---"));
            Pause();
            return;
        }
    }

    static void SearchUser()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR USUARIO ===");
        Console.WriteLine("1. Por ID");
        Console.WriteLine("2. Por nombre");
        Console.WriteLine("0. Volver");
        Console.Write("Seleccione una opción: ");

        int opt = ReadOption(0, 2);
        if (opt == 0) return;

        if (opt == 1)
        {
            int id = ReadInt("ID: ");
            var u = usuarioService.BuscarPorId(id);
            Console.WriteLine(u != null ? u.DetalleCompleto() : "[INFO] No encontrado.");
            Pause();
            return;
        }

        if (opt == 2)
        {
            string n = ReadText("Texto en nombre: ");
            var list = usuarioService.BuscarPorNombre(n);
            if (list.Count == 0) Console.WriteLine("[INFO] Sin resultados.");
            else list.ForEach(x => Console.WriteLine(x.DetalleCompleto() + "\n---"));
            Pause();
            return;
        }
    }

    static void ReportActiveLoans()
    {
        Console.Clear();
        Console.WriteLine("=== REPORTE PRÉSTAMOS ACTIVOS ===\n");

        var activos = prestamoService.ObtenerPrestamosActivos();
        if (activos.Count == 0) Console.WriteLine("[INFO] No hay préstamos activos.");
        else activos.ForEach(p => Console.WriteLine(p.DetalleCompleto() + "\n---"));

        Pause();
    }

    static void ReportOverdueLoans()
    {
        Console.Clear();
        Console.WriteLine("=== REPORTE PRÉSTAMOS VENCIDOS ===\n");

        UpdateOverdueStatuses();

        var vencidos = prestamoService.ObtenerTodos().Where(p => p.Estado == EstadoPrestamo.Vencido).ToList();
        if (vencidos.Count == 0) Console.WriteLine("[INFO] No hay préstamos vencidos.");
        else vencidos.ForEach(p => Console.WriteLine(p.DetalleCompleto() + "\n---"));

        Pause();
    }

    static void ReportSummary()
    {
        Console.Clear();
        Console.WriteLine("=== RESUMEN (KPIs) ===\n");

        Console.WriteLine($"Libros: Total={libroService.ObtenerTotalLibros()}, Disponibles={libroService.ObtenerLibrosDisponibles()}, Prestados={libroService.ObtenerLibrosPrestados()}");
        Console.WriteLine($"Usuarios: Total={usuarioService.ObtenerTotalUsuarios()}, Activos={usuarioService.ObtenerUsuariosActivos()}, Inactivos={usuarioService.ObtenerUsuariosInactivos()}");
        Console.WriteLine($"Préstamos: Total={prestamoService.TotalPrestamos()}, Activos={prestamoService.PrestamosActivos()}, Finalizados={prestamoService.PrestamosFinalizados()}");

        Pause();
    }

    // =========================
    // PERSISTENCIA SIMULADA (Save/Load/Reset)
    // =========================
    static void PersistenceMenu()
    {
        bool back = false;
        while (!back)
        {
            Console.Clear();
            Console.WriteLine("===== GUARDAR / CARGAR DATOS =====");
            Console.WriteLine("1. Guardar datos (simulación)");
            Console.WriteLine("2. Cargar datos (simulación)");
            Console.WriteLine("3. Reiniciar datos (S/N)");
            Console.WriteLine("0. Volver");
            Console.Write("Seleccione una opción: ");

            int option = ReadOption(0, 3);

            switch (option)
            {
                case 1: SaveData(); break;
                case 2: LoadData(); break;
                case 3: ResetData(); break;
                case 0: back = true; break;
            }
        }
    }

    static void SaveData()
    {
        savedLibros = new List<Libro>(libroService.ObtenerTodos());
        savedUsuarios = new List<Usuario>(usuarioService.ObtenerTodos());
        savedPrestamos = new List<Prestamo>(prestamoService.ObtenerTodos());

        Console.WriteLine("[INFO] Guardando datos... (simulación)");
        Console.WriteLine($"[OK] Guardado: Libros={savedLibros.Count}, Usuarios={savedUsuarios.Count}, Préstamos={savedPrestamos.Count}");
        Pause();
    }

    static void LoadData()
    {
        if (savedLibros == null || savedUsuarios == null || savedPrestamos == null)
        {
            Console.WriteLine("[INFO] No hay datos guardados para cargar.");
            Pause();
            return;
        }

        Console.WriteLine("[INFO] Cargando datos... (simulación)");

        // Reiniciar servicios
        libroService = new LibroService();
        usuarioService = new UsuarioService();
        prestamoService = new PrestamoService();

        // Restaurar libros y usuarios por métodos públicos
        foreach (var b in savedLibros) libroService.AgregarLibro(b);
        foreach (var u in savedUsuarios) usuarioService.AgregarUsuario(u);

        // Prestamos: no hay método público para agregar sin lógica, así que agregamos a la lista real
        foreach (var p in savedPrestamos) prestamoService.ObtenerTodos().Add(p);

        // Recalcular disponibilidad de libros según préstamos
        foreach (var b in libroService.ObtenerTodos()) b.Disponible = true;
        foreach (var p in prestamoService.ObtenerTodos())
        {
            if (p.Estado == EstadoPrestamo.Activo || p.Estado == EstadoPrestamo.Vencido)
                if (p.Libro != null) p.Libro.Disponible = false;
        }

        UpdateOverdueStatuses();
        Console.WriteLine("[OK] Datos cargados.");
        Pause();
    }

    static void ResetData()
    {
        bool confirm = ConfirmYesNo("¿Seguro que desea REINICIAR los datos? (S/N): ");
        if (!confirm)
        {
            Console.WriteLine("[INFO] Cancelado.");
            Pause();
            return;
        }

        libroService = new LibroService();
        usuarioService = new UsuarioService();
        prestamoService = new PrestamoService();

        Console.WriteLine("[OK] Datos reiniciados (simulación).");
        Pause();
    }

    // =========================
    // OPCIÓN 6: PROBAR SERVICES
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

        Console.WriteLine("Libros ordenados por año:");
        foreach (var l in libroService.OrdenarPorAnio())
            Console.WriteLine(" - " + l.ResumenCorto());

        Console.WriteLine("\n=== FIN PRUEBA ===");
        Pause();
    }

    // =========================
    // OPCIÓN 7: COMPARAR ARRAYS vs LIST
    // =========================
    static void CompararArrayVsList()
    {
        Console.Clear();
        Console.WriteLine("=== COMPARACIÓN ARRAY vs LIST ===\n");

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
        var listNombres = new List<string>();
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
    // SALIDA (guardar S/N)
    // =========================
    static bool ConfirmExit()
    {
        bool save = ConfirmYesNo("¿Desea guardar antes de salir? (S/N): ");
        if (save)
        {
            SaveData();
            Console.WriteLine("[OK] Guardado antes de salir.");
        }
        else
        {
            Console.WriteLine("[INFO] No se guardaron cambios.");
            Pause();
        }

        Console.WriteLine("[SYSTEM] Cerrando aplicación...");
        Pause();
        return true;
    }
}
