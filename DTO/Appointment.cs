using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Appointment : BaseDTO
    {
        [Required(ErrorMessage = "Patient id is required")]
        public int PatientId;

        public int DoctorId;

        public int ServiceId;

        [Required(ErrorMessage = "Branch id is required")]
        public int BranchId;

        [Required(ErrorMessage = "Date and time is required")]
        public DateTime AppointmentDate;

        public string Motive;

        public string Status;

    }
}
