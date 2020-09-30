using StarBazar.Database;
using StarBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBazar.Services
{
    public class UserDetailsService
    {
        #region SingleTon
        public static UserDetailsService Instance
        {
            get
            {
                if (instance == null) instance = new UserDetailsService();

                return instance;
            }
        }
        private static UserDetailsService instance { get; set; }

        private UserDetailsService()
        {
        }
        #endregion

        public List<tbl_State> GetStates()
        {
            using (var context = new CbContext())
            {
                return context.tbl_States.ToList();
            }
        }

        public List<tbl_District> GetDistrictByState(int id)
        {
            using (var context = new CbContext())
            {
                return context.tbl_Districts.Where(x => x.StateId == id).ToList();
            }
        }

        public List<tbl_Pincode> GetPincodeByDistrict(int id)
        {
            using (var context = new CbContext())
            {
                return context.tbl_Pincodes.Where(x => x.DistrictId == id).ToList();
            }
        }

        public tbl_Location GetLocations()
        {
            using (var context = new CbContext())
            {
                return context.tbl_Locations.FirstOrDefault();
            }
        }
    }

   
}
