using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace XORA_IDE
{
    class Facade
    {
        
        public static ModulesController _ModuleControl  = new ModulesController();
     

        //   Models.ToolboxModel ToolBoxModel = new Models.ToolboxModel();
   //     Views.Toolbox ToolBoxView = new Views.Toolbox();
       
         
            public static void UpdateModuleOnCanvasProperties(UIElement e)
            {
              _ModuleControl.UpdateModuleOnCanvasProperties(e as UIElement);
            }

        public Facade()
        {

       //     MessageBox.Show("Facade");

           // MessageBox.Show (  JsonConvert.SerializeObject(_ModuleControl.DevelopModule())   );
            //string Module_Json = JsonConvert.SerializeObject(_ModuleControl.DevelopModule());

          //  File.WriteAllText(@"Button.JSON", Module_Json);

          //  Module Ghoto = JsonConvert.DeserializeObject<Module>(Module_Json);


         //   foreach (string[] x in _ModuleControl.GetToolBoxComponentsList())
    //        {
     //           ToolBoxModel.AddComponent(x[0], x[1], x[2]);
//
      //          System.Console.Out.WriteLine(x[0]+" "+ x[1]+" " + x[2]);
      //      }

            //ToolBoxView.Data = ToolBoxModel.GetData();

         //   Views.Toolbox.TboxTree.DataContext = ToolBoxModel.GetData();

            //MainWindow.Toolbox = ToolBoxView;

            
         //   MainWindow.Toolbox.Resources = ToolBoxView.Resources;
        
        }

    }
}
