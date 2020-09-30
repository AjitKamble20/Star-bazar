using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBazar.Entities
{
   public class tbl_District
    {
        [Key]
        public int DistrictId { get; set; }

        [Required]
        public string DistrictName { get; set; }

        // Foreign key   
        [Display(Name = "State")]
        public virtual int StateId { get; set; }

        [ForeignKey("StateId")]
        public virtual tbl_State States { get; set; }
    }
}
