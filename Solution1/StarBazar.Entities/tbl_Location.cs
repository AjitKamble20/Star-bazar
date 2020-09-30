using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBazar.Entities
{
   public class tbl_Location
    {
        [Key]
        public int DistrictId { get; set; }

        [Required]
        public string DistrictName { get; set; }

        // Foreign key   
        [Display(Name = "Pincode")]
        public virtual int PincodeId { get; set; }

        public int Pincode { get; set; }

        [ForeignKey("PincodeId")]
        public virtual PincodeTable PincodeTables { get; set; }
    }
}
