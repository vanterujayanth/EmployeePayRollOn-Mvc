using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class CustomerModel
    {
        [Required(ErrorMessage = "NAME CANNOT BE EMPTY....")]
        [RegularExpression(@"[A-Z{1} a-z]{3,}")]
        public string Name { get; set; }
    }
}
