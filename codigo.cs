using System;

class Program
{
    static void Main()
    {
        ShowMainMenu();
    }

    static void ShowMainMenu()
    {
        int opcion = 0;

        while (opcion != 6)
        {
            Console.WriteLine("\n===== SISTEMA DE BIBLIOTECA =====");
            Console.WriteLine("1. Libros");
            Console.WriteLine("2. Usuarios");
            Console.WriteLine("3. Prestamos");
            Console.WriteLine("4. Busquedas y reportes");
            Console.WriteLine("5. Guardar / Cargar datos");
            Console.WriteLine("6. Salir");

            Console.Write("Seleccione una opcion: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1: ShowBooksMenu(); break;
                case 2: ShowUsersMenu(); break;
                case 3: ShowLoansMenu(); break;
                case 4: ShowSearchReportsMenu(); break;
                case 5: ShowPersistenceMenu(); break;
                case 6: ConfirmExitAndSave(); break;
                default: Console.WriteLine("Opcion invalida"); break;
            }
        }
    }

    // ================= LIBROS =================

    static void ShowBooksMenu()
    {
        int opcion = 0;

        while (opcion != 6)
        {
            Console.WriteLine("\n===== MENU LIBROS =====");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Listar libros");
            Console.WriteLine("3. Ver detalle de libro");
            Console.WriteLine("4. Actualizar libro");
            Console.WriteLine("5. Eliminar libro");
            Console.WriteLine("6. Volver");

            Console.Write("Seleccione una opcion: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1: RegisterBook(); break;
                case 2: ListBooksMenu(); break;
                case 3: ViewBookDetail(); break;
                case 4: UpdateBookMenu(); break;
                case 5: DeleteBook(); break;
                case 6: Console.WriteLine("Volviendo..."); break;
                default: Console.WriteLine("Opcion invalida"); break;
            }
        }
    }

    static void ListBooksMenu()
    {
        int opcion = 0;

        while (opcion != 4)
        {
            Console.WriteLine("\n===== LISTAR LIBROS =====");
            Console.WriteLine("1. Listar todos");
            Console.WriteLine("2. Listar disponibles");
            Console.WriteLine("3. Listar prestados");
            Console.WriteLine("4. Volver");

            Console.Write("Seleccione una opcion: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1: ListBooksAll(); break;
                case 2: ListBooksAvailable(); break;
                case 3: ListBooksBorrowed(); break;
                case 4: Console.WriteLine("Volviendo..."); break;
                default: Console.WriteLine("Opcion invalida"); break;
            }
        }
    }

    static void UpdateBookMenu()
    {
        int opcion = 0;

        while (opcion != 4)
        {
            Console.WriteLine("\n===== ACTUALIZAR LIBRO =====");
            Console.WriteLine("1. Editar titulo");
            Console.WriteLine("2. Editar autor");
            Console.WriteLine("3. Editar año / categoria");
            Console.WriteLine("4. Volver");

            Console.Write("Seleccione una opcion: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1: EditBookTitle(); break;
                case 2: EditBookAuthor(); break;
                case 3: EditBookYearCategory(); break;
                case 4: Console.WriteLine("Volviendo..."); break;
                default: Console.WriteLine("Opcion invalida"); break;
            }
        }
    }

    static void RegisterBook() => Console.WriteLine("Registrando libro...");
    static void ListBooksAll() => Console.WriteLine("Listando todos los libros...");
    static void ListBooksAvailable() => Console.WriteLine("Listando libros disponibles...");
    static void ListBooksBorrowed() => Console.WriteLine("Listando libros prestados...");
    static void ViewBookDetail() => Console.WriteLine("Mostrando detalle del libro...");
    static void EditBookTitle() => Console.WriteLine("Editando titulo del libro...");
    static void EditBookAuthor() => Console.WriteLine("Editando autor del libro...");
    static void EditBookYearCategory() => Console.WriteLine("Editando año o categoria...");
    static void DeleteBook() => Console.WriteLine("Eliminando libro (validar si está prestado)...");

    // ================= USUARIOS =================

    static void ShowUsersMenu()
    {
        int opcion = 0;

        while (opcion != 6)
        {
            Console.WriteLine("\n===== MENU USUARIOS =====");
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Ver detalle de usuario");
            Console.WriteLine("4. Actualizar usuario");
            Console.WriteLine("5. Eliminar usuario");
            Console.WriteLine("6. Volver");

            Console.Write("Seleccione una opcion: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1: RegisterUser(); break;
                case 2: ListUsers(); break;
                case 3: ViewUserDetail(); break;
                case 4: UpdateUserMenu(); break;
                case 5: DeleteUser(); break;
                case 6: Console.WriteLine("Volviendo..."); break;
                default: Console.WriteLine("Opcion invalida"); break;
            }
        }
    }

    static void RegisterUser() => Console.WriteLine("Registrando usuario...");
    static void ListUsers() => Console.WriteLine("Listando usuarios...");
    static void ViewUserDetail() => Console.WriteLine("Mostrando detalle de usuario...");
    static void UpdateUserMenu() => Console.WriteLine("Actualizando usuario...");
    static void DeleteUser() => Console.WriteLine("Eliminando usuario (validar préstamos activos)...");

    // ================= PRESTAMOS =================

    static void ShowLoansMenu()
    {
        int opcion = 0;

        while (opcion != 6)
        {
            Console.WriteLine("\n===== MENU PRESTAMOS =====");
            Console.WriteLine("1. Crear prestamo");
            Console.WriteLine("2. Listar prestamos");
            Console.WriteLine("3. Ver detalle de prestamo");
            Console.WriteLine("4. Registrar devolucion");
            Console.WriteLine("5. Eliminar prestamo");
            Console.WriteLine("6. Volver");

            Console.Write("Seleccione una opcion: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1: CreateLoan(); break;
                case 2: ListLoansMenu(); break;
                case 3: ViewLoanDetail(); break;
                case 4: RegisterReturn(); break;
                case 5: DeleteLoan(); break;
                case 6: Console.WriteLine("Volviendo..."); break;
                default: Console.WriteLine("Opcion invalida"); break;
            }
        }
    }

    static void ListLoansMenu()
    {
        int opcion = 0;

        while (opcion != 4)
        {
            Console.WriteLine("\n===== LISTAR PRESTAMOS =====");
            Console.WriteLine("1. Todos");
            Console.WriteLine("2. Activos");
            Console.WriteLine("3. Cerrados");
            Console.WriteLine("4. Volver");

            Console.Write("Seleccione una opcion: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1: ListLoansAll(); break;
                case 2: ListLoansActive(); break;
                case 3: ListLoansClosed(); break;
                case 4: Console.WriteLine("Volviendo..."); break;
                default: Console.WriteLine("Opcion invalida"); break;
            }
        }
    }

    static void CreateLoan() => Console.WriteLine("Creando préstamo (validar usuario activo y libro disponible)...");
    static void ListLoansAll() => Console.WriteLine("Listando todos los préstamos...");
    static void ListLoansActive() => Console.WriteLine("Listando préstamos activos...");
    static void ListLoansClosed() => Console.WriteLine("Listando préstamos cerrados...");
    static void ViewLoanDetail() => Console.WriteLine("Mostrando detalle del préstamo...");
    static void RegisterReturn() => Console.WriteLine("Registrando devolución...");
    static void DeleteLoan() => Console.WriteLine("Eliminando préstamo...");

    // ================= BUSQUEDAS =================

    static void ShowSearchReportsMenu()
    {
        int opcion = 0;

        while (opcion != 4)
        {
            Console.WriteLine("\n===== BUSQUEDAS Y REPORTES =====");
            Console.WriteLine("1. Buscar libro");
            Console.WriteLine("2. Buscar usuario");
            Console.WriteLine("3. Reportes");
            Console.WriteLine("4. Volver");

            Console.Write("Seleccione una opcion: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1: SearchBook(); break;
                case 2: SearchUser(); break;
                case 3: ShowReportsMenu(); break;
                case 4: Console.WriteLine("Volviendo..."); break;
                default: Console.WriteLine("Opcion invalida"); break;
            }
        }
    }

    static void SearchBook() => Console.WriteLine("Buscando libro...");
    static void SearchUser() => Console.WriteLine("Buscando usuario...");

    static void ShowReportsMenu()
    {
        int opcion = 0;

        while (opcion != 5)
        {
            Console.WriteLine("\n===== REPORTES =====");
            Console.WriteLine("1. Prestamos por usuario");
            Console.WriteLine("2. Prestamos por libro");
            Console.WriteLine("3. Prestamos vencidos");
            Console.WriteLine("4. Resumen general");
            Console.WriteLine("5. Volver");

            Console.Write("Seleccione una opcion: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1: ReportByUser(); break;
                case 2: ReportByBook(); break;
                case 3: ReportOverdue(); break;
                case 4: ReportSummary(); break;
                case 5: Console.WriteLine("Volviendo..."); break;
                default: Console.WriteLine("Opcion invalida"); break;
            }
        }
    }

    static void ReportByUser() => Console.WriteLine("Reporte de préstamos por usuario...");
    static void ReportByBook() => Console.WriteLine("Reporte de préstamos por libro...");
    static void ReportOverdue() => Console.WriteLine("Reporte de préstamos vencidos...");
    static void ReportSummary() => Console.WriteLine("Resumen general de la biblioteca...");

    // ================= PERSISTENCIA =================

    static void ShowPersistenceMenu()
    {
        int opcion = 0;

        while (opcion != 4)
        {
            Console.WriteLine("\n===== GUARDAR / CARGAR DATOS =====");
            Console.WriteLine("1. Guardar datos");
            Console.WriteLine("2. Cargar datos");
            Console.WriteLine("3. Reiniciar datos");
            Console.WriteLine("4. Volver");

            Console.Write("Seleccione una opcion: ");
            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1: SaveData(); break;
                case 2: LoadData(); break;
                case 3: ConfirmResetData(); break;
                case 4: Console.WriteLine("Volviendo..."); break;
                default: Console.WriteLine("Opcion invalida"); break;
            }
        }
    }

    static void SaveData() => Console.WriteLine("Guardando datos...");
    static void LoadData() => Console.WriteLine("Cargando datos...");

    static void ConfirmResetData()
    {
        Console.Write("¿Seguro que desea reiniciar los datos? (S/N): ");
        string r = Console.ReadLine();

        if (r.ToUpper() == "S")
            Console.WriteLine("Datos reiniciados.");
        else
            Console.WriteLine("Operacion cancelada.");
    }

    // ================= SALIDA =================

    static void ConfirmExitAndSave()
    {
        Console.Write("¿Guardar antes de salir? (S/N): ");
        string r = Console.ReadLine();

        if (r.ToUpper() == "S")
            Console.WriteLine("Guardando datos antes de salir...");

        Console.WriteLine("Saliendo del sistema...");
    }
}