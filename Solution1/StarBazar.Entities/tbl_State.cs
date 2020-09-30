using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBazar.Entities
{
   public class tbl_State
    {
        [Key]
        public int StateId { get; set; }

        [Required]
        public string StateName { get; set; }
    }
}
