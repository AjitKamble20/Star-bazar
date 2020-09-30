using StarBazar.Database;
using StarBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarBazar.Services
{
   public class ConfigService
    {
        public Config GetConfig(string Key)
        {
            using (var context=new CbContext())
            {
                return context.Configrations.Find(Key);
            }
        }

        public int ShopPageSize()
        {
            using (var context = new CbContext())
            {
                var pageSizeConfig = context.Configrations.Find("ShopPageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }
        }
    }
}
