using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBazar.Entities
{
   public class UserDetails
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string MobileNo { get; set; }

        [Display(Name = "State")]
        public virtual int StateId { get; set; }
       

        [Display(Name = "District")]
        public virtual int DistrictId { get; set; }

        [Display(Name = "Pincode")]
        public virtual int PincodetId { get; set; }

    }
}
