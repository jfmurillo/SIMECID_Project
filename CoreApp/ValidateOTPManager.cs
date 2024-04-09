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
            public ValidateOTP ValidateOTP(string verifyEmail, string verifyOtp)
            {
                var vc = new ValidateOTPCrudFactory();
                return vc.ValidateOTP<ValidateOTP>(verifyEmail, verifyOtp);
            }
        }
}
