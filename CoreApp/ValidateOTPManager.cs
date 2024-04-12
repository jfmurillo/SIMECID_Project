using DataAccess.CRUD;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class ValidateOTPManager
    {
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





        /*        public string GetUserOTP(string email, string otp)
                {
                    var vc = new ValidateOTPCrudFactory();
                    var result = vc.GetUserOTP(email, otp);

                    string emailResult = result.Email;
                    string otpResult = result.OTP;

                    return (otpResult);
                }


                public bool ValidateOtp(string email, string otpInput)
                {
                    var Otp = GetUserOTP(email, otpInput);
                    if (Otp == otpInput)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }*/
    }
}
