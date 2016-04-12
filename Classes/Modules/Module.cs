using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace XORA_IDE
{
    class Module 
    {
        public ModulePropertiesList Property_List
                                      { set; get; }
        public Events ModuleEvents    { set; get; }
        public String Category        { set; get; }
        public String Script          { set; get; }
        public String Name            { set; get; }
        public String uid             { set; get; }
        public String icon            { set; get; }

        public void RefreshModuleProperties()
        {
            
        }



    }
}
