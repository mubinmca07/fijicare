using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model
{
   public class MenuItemList:ModelBase
    {
        public MenuItemList()
        {
                
        }

        public string Name { get; set; }
        public string Icon { get; set; }
        public string Parameter { get; set; }
    }
}
