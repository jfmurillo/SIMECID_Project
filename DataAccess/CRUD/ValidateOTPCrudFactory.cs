using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class ValidateOTPCrudFactory: CrudFactory
    {
        public ValidateOTPCrudFactory()
        {
            _dao = SqlDao.GetInstace();
        }

        public override void Create(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public T ValidateOTP<T>(string email, string otp) 
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "VALIDATE_OTP_PR" };
            sqlOperation.AddVarcharParam("P_EMAIL", email);
            sqlOperation.AddVarcharParam("P_OTP", otp);
            var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResult.Count > 0)
            {
                var row = lstResult[0];
                var VLDotp = BuildOTP(row);
                return (T)Convert.ChangeType(VLDotp, typeof(T));
            }
            return default(T);
        }

        private ValidateOTP BuildOTP(Dictionary<string, object> row)
        {
            var vldOTP = new ValidateOTP()
            {
                Email = (string)row["EMAIL"],
                OTP = (string)row["OTP"],
            };
            return vldOTP;
        }
    }
}
