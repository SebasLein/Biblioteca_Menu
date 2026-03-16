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
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    Console.WriteLine("Modulo en construccion...");
                    break;

                case 6:
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opcion invalida");
                    break;
            }
        }
    }
}