using DataAccess.CRUD;
using DTO;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreApp
{
    public class UserManager
    {

        public async Task Create(User user)
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
            else if (!IsNull(user.Address))
            {
                throw new Exception("Address can't be null");
            }
            else if (!IsNull(user.Province))
            {
                throw new Exception("Province can't be null");
            };
            // TODO: Enable password hashing (Phase 5) — passwords currently stored as plain text.
            uc.Create(user);

            var otpM = new ValidateOTPManager();
            string OTP = string.Concat(Enumerable.Range(0, 6).Select(_ => RandomNumberGenerator.GetInt32(0, 10)));

            var daoOtp = new ValidateOTP();
            daoOtp.Email = user.Email;
            daoOtp.OTP = OTP;
            otpM.CreateOTP(daoOtp);
            var em = new EmailManager();
            await em.SendEmail(user.Email,OTP);
        }

        public List<User> RetrieveAll()
        {
            var uc = new UserCrudFactory();
            return uc.RetrieveAll<User>();
        }

        public List<User> RetrieveAllRoleUser()
        {
            var uc = new UserCrudFactory();
            return uc.RetrieveAllRoleUser<User>();
        }
        public User RetrieveById(int userId)
        {
            var uc = new UserCrudFactory();
            return uc.RetrieveById<User>(userId);
        }

        public User RetrieveUserByEmail(string email)
        {
            var uc = new UserCrudFactory();
            return uc.RetrieveUserByEmail<User>(email);
        }

        public User RetrieveRoleByUserEmail(string email)
        {
            var uc = new UserCrudFactory();
            return uc.RetrieveRoleByUserEmail<User>(email);
        }
        public List<UserUpdData> RetrieveUsersByRole(string role)
        {
            var factory = new UserCrudFactory();
            return factory.RetrieveUsersByRole(role);
        }

        public void Update(User user)
        {
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
            };

            uc.Update(user);

        }

        public void UpdateUserData(UserDataList userDataList)
        {
            if (userDataList == null || userDataList.Id == 0)
            {
                throw new ArgumentException("Invalid user.");
            }

            var uc = new UserCrudFactory();

            if (!IsValidName(userDataList.Name))
            {
                throw new Exception("Invalid name format");
            }
            else if (!IsValidLastName(userDataList.LastName))
            {
                throw new Exception("Invalid Lastname format");
            }
            else if (!IsValidPhoneNumber(userDataList.PhoneNumber))
            {
                throw new Exception("Invalid phone number format");
            }
            else if (!IsValidEmail(userDataList.Email))
            {
                throw new Exception("Email is required");
            }
          
            

            uc.UpdateUserData(userDataList);

        }

        public void UpdateEmployeeData(Employee employee)
        {
            var uc = new UserCrudFactory();
            uc.UpdateEmployeeData(employee);
        }
        public void UpdateUserRole(User user)
        {
            var uc = new UserCrudFactory();
            uc.UpdateUserRole(user);
        }
        public void DeleteUserData(UserUpdData userUpdData)
        {
            var uc = new UserCrudFactory();
            uc.DeleteUserData(userUpdData);
        }
        public void Delete(User user)
        {
            var uc = new UserCrudFactory();
            uc.Delete(user);
        }
        public User GetUserByEmail(string email)
        {
            var uc = new UserCrudFactory();
            var users = uc.RetrieveAll<User>();


            return users.FirstOrDefault(u => u.Email == email);
        }

        public async Task ForgotPassword(string email)
        {
            var otpM = new ValidateOTPManager();
            string OTP = string.Concat(Enumerable.Range(0, 6).Select(_ => RandomNumberGenerator.GetInt32(0, 10)));

            var daoOtp = new ValidateOTP();
            daoOtp.Email = email;
            daoOtp.OTP = OTP;
            otpM.CreateOTP(daoOtp);
            var em = new EmailManager();
            await em.SendEmail(email, OTP);
            
        }

        // TODO: Phase 5 — implement password hashing using PasswordHasher<User>.
        // Requires a DB migration to hash existing plain-text passwords before enabling.
        public bool Authenticate(string email, string password)
        {
            try
            {
                var uc = new UserCrudFactory();
                var user = uc.UserByEmailAndPassword<User>(email, password);

                if (user != null)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Authentication failed! Incorrect email or password.");
                }
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error during authentication");
            }
        }



        public void CreateOTP(ValidateOTP validateOTP)
        {
            var vc = new ValidateOTPCrudFactory();

            if (validateOTP.Email == null || validateOTP.OTP == null)
            {
                throw new ArgumentNullException("Email or OTP cannot be null or empty.");
            }

            vc.CreateOTP(validateOTP);
        }

        public List<ValidateOTP> RetrieveAllOTP()
        {
            var uc = new ValidateOTPCrudFactory();
            return uc.RetrieveAllOTP<ValidateOTP>();
        }

        public void UpdatePassword(string email, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("New password cannot be empty or null.", nameof(newPassword));
            }

            if (newPassword != confirmPassword)
            {
                throw new ArgumentException("Passwords don't match.", nameof(confirmPassword));
            }

            var user = GetUserByEmail(email);

            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            if (!IsValidPassword(newPassword))
            {
                throw new Exception("Password does not meet the requirements.");
            }

            var uc = new UserCrudFactory();
            uc.UpdateUserPassword(email, newPassword);
        }

        /* /////////////////////////////////////// VALICACIONES ///////////////////////////////////////*/


        private bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && char.IsUpper(name[0]) && name.All(c => char.IsLetter(c) && !char.IsWhiteSpace(c));
        }

        private bool IsValidLastName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && char.IsUpper(name[0]) && name.All(c => char.IsLetter(c) && !char.IsWhiteSpace(c));
        }

        public bool IsValidPassword(string password)
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
                else if (!char.IsLetterOrDigit(c))
                {
                    hasSpecialCharacter = true;
                }
            }

            return hasNumber && hasSpecialCharacter;
        }

        private bool IsValidEmail(string email)
        {
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

        private bool IsNull(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }


        private bool IsValidPhoneNumber(int phoneNumber)
        {

            return phoneNumber.ToString().Length == 8 && !phoneNumber.ToString().Contains(" ");
        }


        private bool IsValidRole(string role)
        {
            return !string.IsNullOrWhiteSpace(role) && char.IsUpper(role[0]) && role.All(c => char.IsLetter(c) && !char.IsWhiteSpace(c));
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

