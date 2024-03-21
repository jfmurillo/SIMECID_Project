using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class AdminCrudFactory : CrudFactory
    {
        public AdminCrudFactory()
        {
            _dao = SqlDao.GetInstace();

        }

        public override void Create(BaseDTO baseDTO)
        {
            //Convertir BaseDTO en un usario
            var admin = baseDTO as Admin;
            //Creacion de instructivo para que el Dao pueda ejecutar
            var sqlOperation = new SqlOperation { ProcedureName = "CRE_ADMIN_PR" };
            sqlOperation.AddVarcharParam("P_NAME", admin.Name);
            sqlOperation.AddVarcharParam("P_LAST_NAME", admin.LastName);
            sqlOperation.AddIntParam("P_PHONE_NUMBER", admin.PhoneNumber);
            sqlOperation.AddVarcharParam("P_EMAIL", admin.Email);
            sqlOperation.AddVarcharParam("P_PASSWORD", admin.Password);
            sqlOperation.AddVarcharParam("P_SEX", admin.Sex);
            sqlOperation.AddDatetimeParam("P_BIRTHDATE", admin.BirthDate);
            sqlOperation.AddVarcharParam("P_ROLE", admin.Role);
            sqlOperation.AddVarcharParam("P_STATUS", admin.Status);
            sqlOperation.AddVarcharParam("P_ADRESS", admin.Adress);


            _dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var admin = baseDTO as Admin;

            // Verificar si el usuario tiene un nombre válido
            if (admin == null || admin.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }

            var SqlOperation = new SqlOperation { ProcedureName = "DEL_ADMIN_PR" };
            SqlOperation.AddIntParam("P_ADMINID", admin.Id);

            _dao.ExecuteProcedure(SqlOperation);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var admin = baseDTO as Admin;


            if (admin == null || admin.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }


            var sqlOperation = new SqlOperation { ProcedureName = "UPD_ADMIN_PR" };
            sqlOperation.AddIntParam("P_ADMINID", admin.Id);
            sqlOperation.AddVarcharParam("P_NAME", admin.Name);
            sqlOperation.AddVarcharParam("P_LAST_NAME", admin.LastName);
            sqlOperation.AddIntParam("P_PHONE_NUMBER", admin.PhoneNumber);
            sqlOperation.AddVarcharParam("P_EMAIL", admin.Email);
            sqlOperation.AddVarcharParam("P_PASSWORD", admin.Password);
            sqlOperation.AddVarcharParam("P_SEX", admin.Sex);
            sqlOperation.AddDatetimeParam("P_BIRTHDATE", admin.BirthDate);
            sqlOperation.AddVarcharParam("P_ROLE", admin.Role);
            sqlOperation.AddVarcharParam("P_STATUS", admin.Status);
            sqlOperation.AddVarcharParam("P_ADRESS", admin.Adress);


            _dao.ExecuteProcedure(sqlOperation);
        }


        private Admin BuildAdmin(Dictionary<string, object> row)
        {
            var adminToReturn = new Admin()
            {
                Id = (int)row["Admin_Id"],
                Name = (string)row["Name"],
                LastName = (string)row["LastName"],
                PhoneNumber = (int)row["PhoneNumber"],
                Email = (string)row["Email"],
                Password = (string)row["Password"],
                Sex = (string)row["Sex"],
                BirthDate = (DateTime)row["Birthdate"],
                Role = (string)row["Role"],
                Status = (string)row["Status"],
                Adress = (string)row["Adress"],
                Created = (DateTime)row["Created"]
            };
            return adminToReturn;
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int Id)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_ADMIN_BY_ID" };
            sqlOperation.AddIntParam("P_ADMIN_ID", Id);
            var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResult.Count > 0)
            {
                var row = lstResult[0]; // Extract the first row from the result
                var adminFound = BuildAdmin(row);
                return (T)Convert.ChangeType(adminFound, typeof(T));
            }
            return default(T); // Return default value for type T if user not found
        }


        public override List<T> RetrieveAll<T>()
        {

            var adminList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_ADMIN_ALL" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var admin = BuildAdmin(row);
                    adminList.Add((T)Convert.ChangeType(admin, typeof(T)));
                }


            }
            return adminList;

        }



    }
}
