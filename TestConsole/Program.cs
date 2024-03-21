
using CoreApp;
using DataAccess.CRUD;
using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace TestConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Program_menu();
            }

        }
        static void Program_menu()
        {
            Console.WriteLine("\nProgram Menu" +
           "\n1. User Info" +
           "\n2. " +
           "\n3. " +
           "\n4. Exit");

            var opc = Console.ReadLine();

            switch (opc)
            {
                case "1":
                    UserMenu();
                    break;
                case "2":
                    //AssetMenu();
                    break;
                case "3":
                   // MantainanceMenu();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
            }
        }

        static void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("\nUser Menu" +
                 "\n1. Create User" +
                 "\n2. Delete User" +
                 "\n3. Update User" +
                 "\n4. List Users" +
                 "\n5. Search user by Id" +
                 "\n6. Return to main menu" +
                 "\n7. Exit program");
                var opc_user = Console.ReadLine();
                switch (opc_user)
                {
                    case "1":
                        CreateUser();
                        break;
                    case "2":
                        DeleteUser();
                        break;
                    case "3":
                        UpdateUser();
                        break;
                    case "4":
                        ListUser();
                        break;
                    case "5":
                        SearchUserById();
                        break;
                    case "6":
                        Program_menu();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                }
            }

        }

        static void CreateUser()
        {
            Console.WriteLine("\nCreate user test");

            Console.WriteLine("\nEnter user name");
            var name = Console.ReadLine();

            Console.WriteLine("\nEnter user last name");
            var lastname = Console.ReadLine();

            Console.WriteLine("\nEnter user phone number");
            var phonenumber = Console.ReadLine();

            Console.WriteLine("\nEnter user email");
            var email = Console.ReadLine();

            Console.WriteLine("\nEnter user password");
            var password = Console.ReadLine();

            Console.WriteLine("\nEnter user sex");
            var sex = Console.ReadLine();

            Console.WriteLine("\nEnter user birthdate (yyyy-MM-dd)");
            var birthdateString = Console.ReadLine();

            DateTime birthdate;
            if (!DateTime.TryParse(birthdateString, out birthdate))
            {
                Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                return;
            }

            Console.WriteLine("\nEnter user role");
            var role = Console.ReadLine();

            Console.WriteLine("\nEnter user status");
            var status = Console.ReadLine();

            Console.WriteLine("\nEnter user adress");
            var adress = Console.ReadLine();

            var newUser = new User()
            {
                Name = name,
                LastName = lastname,
                PhoneNumber = int.Parse(phonenumber),
                Email = email,
                Password = password,
                Sex = sex,
                BirthDate = birthdate,
                Role = role,
                Status = status,
                Adress = adress
                


            };

            try
            {
                var um = new UserManager();
                um.Create(newUser);
                Console.WriteLine("\nUser Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }



            Console.ReadLine();
        }

        static void DeleteUser()
        {
            Console.WriteLine("\n Delete User ");

            Console.WriteLine("\nEnter User's id to delete:");
            string userInput = Console.ReadLine();

            try
            {
                int userIdToDelete = int.Parse(userInput);

                var userToDelete = new User { Id = userIdToDelete };

                var um = new UserManager();
                um.Delete(userToDelete);
                Console.WriteLine("\nUser Deleted");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid user ID. Please enter a valid integer.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError Deleting user: {ex.Message}");
            }
        }

        static void UpdateUser()

        {
            Console.WriteLine("\nUpdate User");

            Console.WriteLine("\nEnter User's id to update:");
            string userInput = Console.ReadLine();

            try
            {
                int userIdToUpdate = int.Parse(userInput);

                var userToUpdate = new User { Id = userIdToUpdate };
                var um = new UserManager();



                if (userToUpdate != null)
                {

                    Console.WriteLine("\nEnter new user information:");

                    Console.WriteLine("\nEnter user name");
                    userToUpdate.Name = Console.ReadLine();

                    Console.WriteLine("\nEnter user last name");
                    userToUpdate.LastName = Console.ReadLine();

                    Console.WriteLine("\nEnter user phone number");
                    userToUpdate.PhoneNumber = int.Parse(Console.ReadLine());

                    Console.WriteLine("\nEnter user email");
                    userToUpdate.Email = Console.ReadLine();

                    Console.WriteLine("\nEnter user password");
                    userToUpdate.Password = Console.ReadLine();

                    Console.WriteLine("\nEnter user sex");
                    userToUpdate.Sex = Console.ReadLine();

                    Console.WriteLine("\nEnter user birthdate (yyyy-MM-dd)");
                    var birthdateString = Console.ReadLine();

                    DateTime birthdate;
                    if (!DateTime.TryParse(birthdateString, out birthdate))
                    {
                        Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                        return;
                    }
                    userToUpdate.BirthDate = birthdate;

                    Console.WriteLine("\nEnter user role");
                    userToUpdate.Role = Console.ReadLine();

                    Console.WriteLine("\nEnter user status");
                    userToUpdate.Status = Console.ReadLine();

                    Console.WriteLine("\nEnter user adress");
                    userToUpdate.Adress = Console.ReadLine();

                    Console.WriteLine("\nUser Updated");

                    um.Update(userToUpdate);
                }
                else
                {
                    Console.WriteLine("\nUser not found with the given ID.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid user ID. Please enter a valid integer.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError updating user: {ex.Message}");
            }
        }

        static void ListUser()
        {
            var um = new UserManager();
            List<User> userList = um.RetrieveAll();
            if (userList.Count > 0)
            {
                // Serializar la lista de usuarios a formato JSON
                string jsonUsers = JsonConvert.SerializeObject(userList, Formatting.Indented);

                // Imprimir el JSON resultante
                Console.WriteLine("\tLista de Usuarios");
                Console.WriteLine(jsonUsers);
            }
            else
            {
                Console.WriteLine("No se encontraron usuarios.");
            }
        }

        static void SearchUserById()
        {

            Console.Write("Ingrese el ID del usuario a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                var um = new UserManager();
                User user = um.RetrieveById(userId);

                if (user != null)
                {

                    string jsonUser = JsonConvert.SerializeObject(user, Formatting.Indented);
                    Console.WriteLine("\nDatos del Usuario con el Id numero: " + userId);
                    Console.WriteLine(jsonUser);
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún usuario con el ID {userId}");
                }
            }
            else
            {
                Console.WriteLine("Entrada no válida para el ID del usuario.");
            }
        }
    }
}
