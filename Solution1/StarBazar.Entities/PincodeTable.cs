using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBazar.Entities
{
  public class PincodeTable
    {
        [Key]
        public int PincodeId { get; set; }

        [Required]
        public int Pincode { get; set; }

      
    }
}
