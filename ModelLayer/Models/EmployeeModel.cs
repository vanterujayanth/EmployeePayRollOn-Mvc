using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class EmployeeModel
    {
        [RegularExpression(@"[0-9]", ErrorMessage = "ID MUST BE A NUMBER")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "NAME CANNOT BE EMPTY....")]
        [RegularExpression(@"[A-Z{1} a-z]{3,}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "PROFILEIMAGE CANNOT BE EMPTY....")]
        public string ProfileImage { get; set; }

        [Required(ErrorMessage = "GENDER CANNOT BE EMPTY....")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "DEPARTMENT CANNOT BE EMPTY....")]
        public string Department { get; set; }

        [Required(ErrorMessage = "SALARY CANNOT BE EMPTY....")]
        [RegularExpression(@"[0-9]{1,}")]
        public long Salary { get; set; }

        [Required(ErrorMessage = "DATE CANNOT BE EMPTY....")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "NOTES CANNOT BE EMPTY....")]
        [RegularExpression(@"[A-Z{1} a-z]{3,}")]
        public string Notes { get; set; }
    }
}
