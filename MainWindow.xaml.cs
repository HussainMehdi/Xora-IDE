
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Markup;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
namespace XORA_IDE
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    
    public partial class MainWindow : Window
    {


        Facade IDE_FACADE;
        DataGridView dataGridView1 = new DataGridView();

        ModulePropertyGrid myprop;
        public static PropertyGrid LoadedProperties;
        public static System.Windows.Controls.UserControl Toolbox ;
        
        public MainWindow()
        {
            InitializeComponent();
          //  JsonConvert.SerializeObject("");
            LoadedProperties = propertyGrid;
            Toolbox = IDE_Toolbox;

            myprop = new ModulePropertyGrid();
            
            myprop.Add(new ModuleProperty("Name", "Canvas", false, true, "Common","Test Canvas"));

            
            propertyGrid.SelectedObject = myprop;
            propertyGrid.PropertyValueChanged += PropertyValueChanged;
            
            propertyGrid.Refresh();

            IDE_FACADE = new Facade();


            
            
        }

        void PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
           // MessageBox.Show("Yes changed");

            //throw new NotImplementedException();
        }




        private void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var item = e.OriginalSource ;
          //  if (Mouse.LeftButton == MouseButtonState.Pressed && item != null)
                DragDrop.DoDragDrop(sender as DependencyObject, (item), System.Windows.DragDropEffects.All);
        }




        public void SetToolboxControl(Views.Toolbox tbox)
        {
            IDE_Toolbox=tbox;
        }


        private void MenuItem_SaveProject_Click(object sender, RoutedEventArgs e)
        {
            string xml = XamlWriter.Save(Page_canvas);
            XDocument doc = XDocument.Parse(xml);
            //    System.Windows.Forms.MessageBox.Show(doc.ToString());


         


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Xora Project|*.xoraproj";
            saveFileDialog1.Title = "Save Xora Project";
            saveFileDialog1.ShowDialog();




            if (saveFileDialog1.FileName != "")
            {
               
                // System.Windows.Forms.MessageBox.Show ( saveFileDialog1.FileName );
                 File.WriteAllText(saveFileDialog1.FileName, doc.ToString());
                string directory = System.IO.Path.Combine(Environment.CurrentDirectory, "compiler");

                directory = System.IO.Path.Combine(directory, "XORA_BuilderHTML.exe");

                Process.Start(directory," "+ saveFileDialog1.FileName + " AppAndroid/www/index.html");

            }
        }

      

        private void PropertyGrid_Update(object s, PropertyValueChangedEventArgs e)
        {
            // System.Windows.Forms.MessageBox.Show(e.ChangedItem.Label);
            //System.Windows.Forms.MessageBox.Show(e.ChangedItem.Value.ToString());
            var _PropertyGrid = s as PropertyGrid;

            GridItem gi = _PropertyGrid.SelectedGridItem;

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            while (gi.Parent != null)
            {
                gi = gi.Parent;
            }


            string uid = string.Empty, content=string.Empty;
            foreach (GridItem item in gi.GridItems)
            {
                foreach (GridItem GI in item.GridItems)
                {
                    if (GI.Label == "uid") uid = GI.Value.ToString();
                    else if (GI.Label == "Content" || GI.Label == "Text") content = GI.Value.ToString();
                }
                //ParseGridItems(item); //recursive
            }

            if (uid != string.Empty && content != string.Empty)
            {
                Console.WriteLine("Update Properties !");
                Console.WriteLine("uid:  " + uid);
                Console.WriteLine("content:  " + content);

                if (uid.Contains("btn"))
                {
                    var tmp = Facade._ModuleControl.GetModuleOnCanvasByUID(uid) as System.Windows.Controls.Button;
                    tmp.Content = content;
                }
                else
                     if (uid.Contains("lbl"))
                {
                    var tmp = Facade._ModuleControl.GetModuleOnCanvasByUID(uid) as System.Windows.Controls.Label;
                    tmp.Content = content;
                }
                else
                     if (uid.Contains("chk"))
                {
                    var tmp = Facade._ModuleControl.GetModuleOnCanvasByUID(uid) as System.Windows.Controls.CheckBox;
                    tmp.Content = content;
                }
                else
                       if (uid.Contains("radio"))
                {
                    var tmp = Facade._ModuleControl.GetModuleOnCanvasByUID(uid) as System.Windows.Controls.RadioButton;
                    tmp.Content = content;
                }
                else
                       if (uid.Contains("txtblk"))
                {
                    var tmp = Facade._ModuleControl.GetModuleOnCanvasByUID(uid) as System.Windows.Controls.TextBlock;
                    tmp.Text = content;
                }
            }

            // Console.WriteLine();

        }



        private void ParseGridItems(GridItem gi)
        {
            if (gi.GridItemType == GridItemType.Category)
                foreach (GridItem item in gi.GridItems)
                    ParseGridItems(item);

            dataGridView1.Rows.Add(gi.Label, gi.Value);

        }


        private void BuildAndroid_Click(object sender, RoutedEventArgs e)
        {
            string directory = System.IO.Path.Combine(Environment.CurrentDirectory, "AppAndroid");
            directory = System.IO.Path.Combine(directory, "build.bat");
            // directory = System.IO.Path.Combine(directory, "www");

            var psi = new ProcessStartInfo(directory);
            psi.WorkingDirectory = Environment.CurrentDirectory + @"\AppAndroid";

            Process.Start(psi);


            

        }

        private void dockingManager_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Console.WriteLine("Key Dockingmanager sensed : " + e.Key);
        }

        private void Page_canvas_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Console.WriteLine("Key page_canvas sensed : " + e.Key);
        }
    }

    
    

}


