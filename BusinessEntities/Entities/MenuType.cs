
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class MenuType : IMenuType
    {

        public string MenuTypeName { get; set; }

        public MenuType()
        {
        }

        public MenuType(string menuTypeName)
        {
            MenuTypeName = menuTypeName;
        }
    }
}
