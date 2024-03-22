using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class AppointmentCrudFactory : CrudFactory
    {
        public AppointmentCrudFactory() 
        {
            _dao = SqlDao.GetInstace();
        }
        public override void Create(BaseDTO baseDTO)
        {
            //throw new NotImplementedException();

            var appointment = baseDTO as Appointment;
            var sqlOperation = new SqlOperation { ProcedureName= "CRE_APPOINTMENT_PR" };
            sqlOperation.AddIntParam("P_PATIENT_ID", appointment.PatientId);
            sqlOperation.AddIntParam("P_DOCTOR_ID", appointment.DoctorId);
            sqlOperation.AddIntParam("P_SERVICE_ID", appointment.ServiceId);
            sqlOperation.AddIntParam("P_BRANCH_ID", appointment.BranchId);
            sqlOperation.AddDatetimeParam("P_APPOINTMENT_DATE", appointment.AppointmentDate);
            sqlOperation.AddVarcharParam("P_MOTIVE", appointment.Motive);
            sqlOperation.AddVarcharParam("P_STATUS", appointment.Status);

            //ir al dao a ejecutar la operacion
            _dao.ExecuteProcedure(sqlOperation);
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

    }
}
