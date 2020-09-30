using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBazar.Entities
{
  public class tbl_Pincode
    {
        [Key]
        public int PincodetId { get; set; }

        [Required]
        public int Pincode { get; set; }

        // Foreign key   
        [Display(Name = "District")]
        public virtual int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual tbl_District Districts { get; set; }
    }
}
