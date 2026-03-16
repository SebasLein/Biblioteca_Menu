using System;

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
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opcion: ");

            option = Convert.ToInt32(Console.ReadLine());

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
                    Console.WriteLine("Modulo busquedas y reportes en construccion...");
                    Console.ReadKey();
                    break;

                case 5:
                    Console.WriteLine("Modulo guardar/cargar datos en construccion...");
                    Console.ReadKey();
                    break;

                case 6:
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opcion invalida");
                    Console.ReadKey();
                    break;
            }

        } while (option != 6);
    }

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

            option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Registrando libro...");
                    Console.ReadKey();
                    break;

                case 2:
                    ListBooksMenu();
                    break;

                case 3:
                    Console.WriteLine("Mostrando detalle del libro...");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.WriteLine("Actualizando libro...");
                    Console.ReadKey();
                    break;

                case 5:
                    Console.WriteLine("Eliminando libro...");
                    Console.ReadKey();
                    break;

                case 6:
                    Console.WriteLine("Volviendo...");
                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("Opcion invalida");
                    Console.ReadKey();
                    break;
            }

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

            option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Listando todos los libros...");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.WriteLine("Listando libros disponibles...");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Listando libros prestados...");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.WriteLine("Volviendo...");
                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("Opcion invalida");
                    Console.ReadKey();
                    break;
            }

        } while (option != 4);
    }

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

            option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Registrando usuario...");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.WriteLine("Listando usuarios...");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Actualizando usuario...");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.WriteLine("Eliminando usuario...");
                    Console.ReadKey();
                    break;

                case 5:
                    Console.WriteLine("Volviendo...");
                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("Opcion invalida");
                    Console.ReadKey();
                    break;
            }

        } while (option != 5);
    }

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

            option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine("Registrando prestamo...");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.WriteLine("Devolviendo libro...");
                    Console.ReadKey();
                    break;

                case 3:
                    Console.WriteLine("Listando prestamos activos...");
                    Console.ReadKey();
                    break;

                case 4:
                    Console.WriteLine("Volviendo...");
                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("Opcion invalida");
                    Console.ReadKey();
                    break;
            }

        } while (option != 4);
    }
}