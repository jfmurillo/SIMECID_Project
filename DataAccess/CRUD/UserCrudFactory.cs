using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class UserCrudFactory : CrudFactory
    {
        public UserCrudFactory()
        {
            _dao = SqlDao.GetInstace();

        }

        public override void Create(BaseDTO baseDTO)
        {
            //Convertir BaseDTO en un usario
            var user = baseDTO as User;
            //Creacion de instructivo para que el Dao pueda ejecutar
            var sqlOperation = new SqlOperation { ProcedureName = "CRE_USER_PR" };
            sqlOperation.AddVarcharParam("P_NAME", user.Name);
            sqlOperation.AddVarcharParam("P_LAST_NAME", user.LastName);
            sqlOperation.AddIntParam("P_PHONE_NUMBER", user.PhoneNumber);
            sqlOperation.AddVarcharParam("P_EMAIL", user.Email);
            sqlOperation.AddVarcharParam("P_PASSWORD", user.Password);
            sqlOperation.AddVarcharParam("P_SEX", user.Sex);
            sqlOperation.AddDatetimeParam("P_BIRTHDATE", user.BirthDate);
            sqlOperation.AddVarcharParam("P_ROLE", user.Role);
            sqlOperation.AddVarcharParam("P_STATUS", user.Status);
            sqlOperation.AddVarcharParam("P_ADRESS", user.Adress);
            
            
            _dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var user = baseDTO as User;

            // Verificar si el usuario tiene un nombre válido
            if (user == null || user.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }

            var SqlOperation = new SqlOperation { ProcedureName = "DEL_USER_PR" };
            SqlOperation.AddIntParam("P_USERID", user.Id);

            _dao.ExecuteProcedure(SqlOperation);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var user = baseDTO as User;


            if (user == null || user.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }


            var sqlOperation = new SqlOperation { ProcedureName = "UPD_USER_PR" };
            sqlOperation.AddIntParam("P_USERID", user.Id);
            sqlOperation.AddVarcharParam("P_NAME", user.Name);
            sqlOperation.AddVarcharParam("P_LAST_NAME", user.LastName);
            sqlOperation.AddIntParam("P_PHONE_NUMBER", user.PhoneNumber);
            sqlOperation.AddVarcharParam("P_EMAIL", user.Email);
            sqlOperation.AddVarcharParam("P_PASSWORD", user.Password);
            sqlOperation.AddVarcharParam("P_SEX", user.Sex);
            sqlOperation.AddDatetimeParam("P_BIRTHDATE", user.BirthDate);
            sqlOperation.AddVarcharParam("P_ROLE", user.Role);
            sqlOperation.AddVarcharParam("P_STATUS", user.Status);
            sqlOperation.AddVarcharParam("P_ADRESS", user.Adress);


            _dao.ExecuteProcedure(sqlOperation);
        }
        
        
        private User BuildUser(Dictionary<string, object> row)
        {
            var userToReturn = new User()
            {
                Id = (int)row["User_Id"],
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
            return userToReturn;
        }
        
        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int Id)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_USER_BY_ID" };
            sqlOperation.AddIntParam("P_USER_ID", Id);
            var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResult.Count > 0)
            {
                var row = lstResult[0]; // Extract the first row from the result
                var userFound = BuildUser(row);
                return (T)Convert.ChangeType(userFound, typeof(T));
            }
            return default(T); // Return default value for type T if user not found
        }


        public override List<T> RetrieveAll<T>()
        {

            var userList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_USER_ALL" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var user = BuildUser(row);
                    userList.Add((T)Convert.ChangeType(user, typeof(T)));
                }


            }
            return userList;

        }


        
    }
}
