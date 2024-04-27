﻿using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class MedicalReportCrudFactory : CrudFactory
    {

        public MedicalReportCrudFactory()
        {
            _dao = SqlDao.GetInstace();

        }

        public override void Create(BaseDTO baseDTO)
        {
            // Convertir BaseDTO en un examen médico
            var medRep = baseDTO as MedicalReport;

            // Crear la operación SQL para ejecutar el procedimiento almacenado
            var sqlOperation = new SqlOperation { ProcedureName = "CRE_MEDICAL_REPORT_PR" };
            sqlOperation.AddIntParam("P_USER_ID", medRep.PatientId);
            sqlOperation.AddVarcharParam("P_USER_NAME", medRep.PatientName);
            sqlOperation.AddVarcharParam("P_USER_LAST_NAME", medRep.PatientLastName);
            sqlOperation.AddVarcharParam("P_HISTORIAL", medRep.Historial);
            sqlOperation.AddVarcharParam("P_MEDICAL_NOTES", medRep.MedicalNotes);
            sqlOperation.AddDatetimeParam("P_DATE", medRep.Date);
          
            // Ejecutar el procedimiento almacenado
            _dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var medRep = baseDTO as MedicalReport;

            // Verificar si el usuario tiene un nombre válido
            if (medRep == null || medRep.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }

            var SqlOperation = new SqlOperation { ProcedureName = "DEL_MEDICAL_REPORT_PR" };
            SqlOperation.AddIntParam("P_MEDICAL_REPORT_ID", medRep.Id);

            _dao.ExecuteProcedure(SqlOperation);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var medRep = baseDTO as MedicalReport;

            // Crear la operación SQL para ejecutar el procedimiento almacenado
            var sqlOperation = new SqlOperation { ProcedureName = "UPD_MEDICAL_REPORT_PR" };
            sqlOperation.AddIntParam("P_MEDICAL_REPORT_ID", medRep.Id);
            sqlOperation.AddIntParam("P_USER_ID", medRep.PatientId);
            sqlOperation.AddVarcharParam("P_USER_NAME", medRep.PatientName);
            sqlOperation.AddVarcharParam("P_USER_LAST_NAME", medRep.PatientLastName);
            sqlOperation.AddVarcharParam("P_HISTORIAL", medRep.Historial);
            sqlOperation.AddVarcharParam("P_MEDICAL_NOTES", medRep.MedicalNotes);
            sqlOperation.AddDatetimeParam("P_DATE", medRep.Date);

            // Ejecutar el procedimiento almacenado
            _dao.ExecuteProcedure(sqlOperation);
        }

        private MedicalReport BuildMedicalReport(Dictionary<string, object> row)
        {
            var ReportToReturn = new MedicalReport()
            {
                Id = (int)row["MEDICAL_REPORT_ID"],
                PatientId = (int)row["USER_ID"],
                PatientName = (string)row["USER_NAME"],
                PatientLastName = (string)row["USER_LAST_NAME"],
                Historial = (string)row["HISTORIAL"],
                MedicalNotes = (string)row["MEDICAL_NOTES"],
                Date = (DateTime)row["DATE"]
               
            };

            return ReportToReturn;
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {

            var reportList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_MEDICAL_REPORT_ALL" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var medReport = BuildMedicalReport(row);
                    reportList.Add((T)Convert.ChangeType(medReport, typeof(T)));
                }


            }
            return reportList;

        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }


    }
}
