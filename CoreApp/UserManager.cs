using DataAccess.CRUD;
using DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreApp
{
    //Clase de negocio donde se aplican las validaciones funcionales 
    public class UserManager
    {
        public void Create(User user)
        {
            var uc = new UserCrudFactory();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Fields cannot be left blank");
            }

            if (!IsValidName(user.Name))
            {
                throw new Exception("Invalid name format");
            }
            else if (!IsValidLastName(user.LastName))
            {
                throw new Exception("Invalid Lastname format");
            }
            else if (!IsValidPhoneNumber(user.PhoneNumber))
            {
                throw new Exception("Invalid phone number format");
            }
            else if (!IsValidEmail(user.Email))
            {
                throw new Exception("Email is required");
            }
            else if (!IsValidPassword(user.Password))
            {
                throw new Exception("Invalid Password format");
            }
            /*else if (!IsValidSex(user.Sex))
            {
                throw new Exception("Invalid Sex format");
            }*/
            else if (!IsValidBirthDate(user.BirthDate))
            {
                throw new Exception("Invalid birth date format");
            }
            else if (!IsValidRole(user.Role))
            {
                throw new Exception("Invalid role format");
            }
            else if (!IsValidStatus(user.Status))
            {
                throw new Exception("Invalid status value");
            }
            else if (!IsValidAddress(user.Address))
            {
                throw new Exception("Invalid Adress format");
            }
            uc.Create(user);

        }

        public List<User> RetrieveAll()
        {
            var uc = new UserCrudFactory();
            return uc.RetrieveAll<User>();
        }

        public User RetrieveById(int userId)
        {
            var uc = new UserCrudFactory();
            return uc.RetrieveById<User>(userId);
        }


        public void Update(User user)
        {
            // Validar que el usuario no sea nulo y que tenga un ID válido
            if (user == null || user.Id == 0)
            {
                throw new ArgumentException("Invalid user.");
            }

            var uc = new UserCrudFactory();

            if (!IsValidName(user.Name))
            {
                throw new Exception("Invalid name format");
            }
            else if (!IsValidLastName(user.LastName))
            {
                throw new Exception("Invalid Lastname format");
            }
            else if (!IsValidPhoneNumber(user.PhoneNumber))
            {
                throw new Exception("Invalid phone number format");
            }
            else if (!IsValidEmail(user.Email))
            {
                throw new Exception("Email is required");
            }
            else if (!IsValidPassword(user.Password))
            {
                throw new Exception("Invalid Password format");
            }
            /*else if (!IsValidSex(user.Sex))
            {
                throw new Exception("Invalid Sex format");
            }*/
            else if (!IsValidBirthDate(user.BirthDate))
            {
                throw new Exception("Invalid birth date format");
            }
            else if (!IsValidRole(user.Role))
            {
                throw new Exception("Invalid role format");
            }
            else if (!IsValidStatus(user.Status))
            {
                throw new Exception("Invalid status value");
            }
            uc.Update(user);

        }

        public void Delete(User user)
        {
            var uc = new UserCrudFactory();
            uc.Delete(user);
        }

        private bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && char.IsUpper(name[0]) && name.All(c => char.IsLetter(c) && !char.IsWhiteSpace(c));
        }

        private bool IsValidLastName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && char.IsUpper(name[0]) && name.All(c => char.IsLetter(c) && !char.IsWhiteSpace(c));
        }

        private bool IsValidPassword(string password)
        {

            if (string.IsNullOrWhiteSpace(password))
                return false;


            if (password.Length < 8)
                return false;

            bool hasNumber = false;
            bool hasSpecialCharacter = false;

            foreach (char c in password)
            {

                if (char.IsDigit(c))
                {
                    hasNumber = true;
                }
                // Verifica si el carácter actual es un carácter especial
                else if (!char.IsLetterOrDigit(c))
                {
                    hasSpecialCharacter = true;
                }
            }

            // Retorna verdadero si la contraseña tiene al menos un número y un carácter especial
            return hasNumber && hasSpecialCharacter;
        }

        private bool IsValidEmail(string email)
        {
            // Verifica el formato del correo electrónico
            string emailRegexPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailRegexPattern))
            {
                throw new Exception("Email format is invalid");
            }

            return true;
        }


        private bool IsValidStatus(string status)
        {
            return !string.IsNullOrWhiteSpace(status) && char.IsUpper(status[0]) && status.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }



        private bool IsValidPhoneNumber(int phoneNumber)
        {

            return phoneNumber.ToString().Length == 8 && !phoneNumber.ToString().Contains(" ");
        }


        private bool IsValidRole(string role)
        {
            return !string.IsNullOrWhiteSpace(role) && char.IsUpper(role[0]) && role.All(c => char.IsLetter(c) && !char.IsWhiteSpace(c));
        }

        private bool IsValidAddress(string address)
        {
            return !string.IsNullOrWhiteSpace(address);
        }



        private bool IsValidBirthDate(DateTime birthDate)
        {
            string expectedFormat = "yyyy-MM-dd";

            if (!DateTime.TryParseExact(birthDate.ToString(expectedFormat), expectedFormat, null, DateTimeStyles.None, out _))
            {
                return false;
            }
            DateTime today = DateTime.Today;

            if (birthDate > today)
            {
                throw new Exception("The birth date cannot be greater than today's date.");
            }

            return true;
        }

    }
};

