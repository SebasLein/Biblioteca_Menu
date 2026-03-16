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
    static void DeleteBook() => Console.WriteLine("Eliminando libro...");

    // ===== STUBS TEMPORALES =====

    static void ShowUsersMenu() => Console.WriteLine("Modulo usuarios en construccion...");
    static void ShowLoansMenu() => Console.WriteLine("Modulo prestamos en construccion...");
    static void ShowSearchReportsMenu() => Console.WriteLine("Modulo busquedas en construccion...");
    static void ShowPersistenceMenu() => Console.WriteLine("Modulo persistencia en construccion...");
    static void ConfirmExitAndSave() => Console.WriteLine("Saliendo del sistema...");
}