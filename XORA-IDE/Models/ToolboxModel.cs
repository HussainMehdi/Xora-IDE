using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XORA_IDE.Models
{
    public class ToolboxModel
    {
        Dictionary<string, List<List<String>>> Data = new Dictionary<string, List<List<String>>>();

        public void AddComponent(string Category, string Name , string Icon)
        {
            List<string> comp = new List<string>() { Path.GetFullPath(Directory.GetCurrentDirectory()+Icon) , Name };
            
            if (Data.Keys.Contains(Category))
            {
                Data[Category].Add(comp);
            }
            else
            {
                Data.Add(Category, new List<List<String>>() { comp });
            }
        }

      public   Dictionary<string, List<List<String>>> GetData()
        {
            return Data;
        }

    }
}
