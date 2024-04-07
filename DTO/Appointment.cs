﻿using System;
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
        public int PatientId { get; set; }

        public String PatientName { get; set; }
        public String PatientLastName { get; set; }

        public int DoctorId { get; set; }

        public String DoctorName { get; set; }
        public String DoctorLastName { get; set; }

        public int ServiceId { get; set; }

        public String ServiceName { get; set; }

        [Required(ErrorMessage = "Branch id is required")]
        public int BranchId { get; set; }

        public String BranchName { get; set; }

        [Required(ErrorMessage = "Star appointment time is required")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End appointment time is required")]
        public DateTime EndTime { get; set; }

        public String Text { get; set; } //Este es el motive 

        public String Status { get; set; }
    }
}
