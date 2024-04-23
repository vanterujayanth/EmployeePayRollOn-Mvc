using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public  class LoginModel
    {
        [Required(ErrorMessage = "ID CANNOT BE EMPTY....")]
        [RegularExpression(@"[0-9]", ErrorMessage = "ID MUST BE A NUMBER")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "NAME CANNOT BE EMPTY....")]
        [RegularExpression(@"[A-Z{1} a-z]{3,}")]
        public string Name { get; set; }
    }
}
