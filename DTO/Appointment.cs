using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Appointment : BaseDTO
    {
        public int PatientId;

        public int NurseId;

        public int DoctorId;

        public int ServiceId;

        public int BranchId;

        public int DiagnosticId;

        public DateTime AppointmentDate;

        public string motive;

        public string status;

    }
}
