using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XORA_IDE.Views
{
    /// <summary>
    /// Interaction logic for Toolbox.xaml
    /// </summary>
    public partial class Toolbox : UserControl
    {
       // ModulesController _ModuleControl = new ModulesController();


        Models.ToolboxModel mymodel = new Models.ToolboxModel();


       
        private void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var item = e.OriginalSource as TextBlock;

            if (Mouse.LeftButton == MouseButtonState.Pressed && item != null)
                DragDrop.DoDragDrop(sender as DependencyObject, ((TextBlock)item).Text, System.Windows.DragDropEffects.All);
        }

        public Toolbox()
        {
            InitializeComponent();

       //     Data_Grid.AddComponent("Common", "Button",  Globals.MODULES_PATH+ @"\Button\img\icon.png");
        //    Data_Grid.AddComponent("Common", "Label", Globals.MODULES_PATH + @"\Label\img\icon.png");



            foreach (string[] x in Facade._ModuleControl.GetToolBoxComponentsList())
            {
                mymodel.AddComponent(x[0],x[1],x[2]);
            }


            //mymodel.AddComponent("Common", "Apple", "null");

            treeView1.DataContext = mymodel.GetData();
            
            //treeView1.DataContext = Data;
            
      

       }
       
        public void SetData(Models.ToolboxModel DataGrid)
        {
            treeView1.DataContext = DataGrid.GetData();
        }
            
        }
    }

