using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MedicalExam : BaseDTO
    {
        public int PatientId { get; set; }

        public string Examtype { get; set; }

        public DateTime ExamDate { get; set; }

        public string Details { get; set; }
    }
}