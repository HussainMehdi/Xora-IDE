using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace XORA_IDE
{
    public static class Globals
    {
        public static string MODULES_PATH = @"\Modules";
        public static string MODULE_PROP_FILE = @"\properties.xcomp";


        public static Dictionary<string, XNamespace> DefaultComponentsProperties = new Dictionary<string, XNamespace>
        {
            {"Uid","http://schemas.microsoft.com/winfx/2006/xaml" },
            {"Width","" },
            {"Height","" },
            {"Canvas.Left","" },
            { "Canvas.Top",""}

        };


        




    }
}
