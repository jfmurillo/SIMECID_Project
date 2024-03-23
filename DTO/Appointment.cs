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

        public String PatientName;
        public String PatientLastName;

        public int DoctorId;

        public String DoctorName;
        public String DoctorLastName;

        public int ServiceId;

        public String ServiceName;

        [Required(ErrorMessage = "Branch id is required")]
        public int BranchId;

        public String BranchName;

        [Required(ErrorMessage = "Star appointment time is required")]
        public DateTime StartTime;

        [Required(ErrorMessage = "End appointment time is required")]
        public DateTime EndTime;

        public String Text; //Este es el motive 

        public String Status;
    }
}
