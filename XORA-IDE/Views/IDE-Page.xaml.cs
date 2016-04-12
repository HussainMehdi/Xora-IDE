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
using System.Windows.Markup;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
namespace XORA_IDE
{
    /// <summary>
    /// Interaction logic for IDE_Page.xaml
    /// </summary>
    public partial class IDE_Page : UserControl
    {
        public static object selectedObj;

        AdornerLayer aLayer;

        bool _isDown;
        bool _isDragging;
        bool selected = false;
        UIElement selectedElement = null;
        string SelectedComponentXaml = null;
      //  ModulesController mod_controller;
        Point _startPoint;
        private double _originalLeft;
        private double _originalTop;

        public IDE_Page()
        {
            InitializeComponent();
            CanvasBorder.BorderThickness = new Thickness(0.4);
            //   mod_controller = new ModulesController();


        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            var canvas = sender as Canvas;
            var pos = e.GetPosition(canvas);
//            string xaml = "";
            string data = e.Data.GetData(typeof(string)).ToString();

            // Facade._ModuleControl.DevelopModule(data);

       
            //Console.Out.WriteLine(xaml);
            
         //   if (xaml != string.Empty)
           // { 
              // StringReader stringReader = new StringReader(xaml);
            //XmlReader xmlReader = XmlReader.Create(stringReader);

            UIElement element = Facade._ModuleControl.DevelopModule(data);


            //element.Uid = "btn_01";
            canvas.Children.Add(element);

           
            Canvas.SetLeft(element as UIElement, pos.X);
            Canvas.SetTop(element as UIElement, pos.Y);

            // Update Properties of Loaded module
            Facade.UpdateModuleOnCanvasProperties(element as UIElement);

            //}


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseLeftButtonDown += new MouseButtonEventHandler(Window1_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(DragFinishedMouseHandler);
            this.MouseMove += new MouseEventHandler(Window1_MouseMove);
            this.MouseLeave += new MouseEventHandler(Window1_MouseLeave);

        //    KeyDown += new System.Windows.Input.KeyEventHandler(CheckCanvasKeys);
            myCanvas.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(myCanvas_PreviewMouseLeftButtonDown);
            //myCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(myCanvas_PreviewMouseLeftButtonDown);
            myCanvas.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(DragFinishedMouseHandler);

          //  myCanvas.KeyDown += new System.Windows.Input.KeyEventHandler(CheckCanvasKeys);
           // MessageBox.Show("IDE PAGE");
        }

        // Handler for drag stopping on leaving the window
        void Window1_MouseLeave(object sender, MouseEventArgs e)
        {
            StopDragging();
            e.Handled = true;
            

        }


        void CheckCanvasKeys(object sender, System.Windows.Input.KeyEventArgs e)
        {

            MessageBox.Show("Key Pressed: " + e.Key);
            //  MessageBox.Show(sender.ToString());
        }

        // Handler for drag stopping on user choise
        void DragFinishedMouseHandler(object sender, MouseButtonEventArgs e)
        {

            

            StopDragging();
            e.Handled = true;
        }

        // Method for stopping dragging
        private void StopDragging()
        {
            

            if (_isDown)
            {
                _isDown = false;
                _isDragging = false;
            }
        }

        // Hanler for providing drag operation with selected element
        void Window1_MouseMove(object sender, MouseEventArgs e)
        {

         //   using (Stream stream = File.Create("D:\\Test.xml"))
            {
               
     //          string xml =  XamlWriter.Save(this);
       //         XDocument doc = XDocument.Parse(xml);
         //       File.WriteAllText("test.xml", doc.ToString());
            }

            if (_isDown)
            {
                if ((!_isDragging) &&
                    ((Math.Abs(e.GetPosition(myCanvas).X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                    (Math.Abs(e.GetPosition(myCanvas).Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)))
                    _isDragging = true;

                if (_isDragging)
                {
                    Point position = Mouse.GetPosition(myCanvas);
                    Canvas.SetTop(selectedElement, position.Y - (_startPoint.Y - _originalTop));
                    Canvas.SetLeft(selectedElement, position.X - (_startPoint.X - _originalLeft));
                }
                
            }
        }

        // Handler for clearing element selection, adorner removal
        void Window1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selected)
            {

                selected = false;
                if (selectedElement != null)
                {
                    aLayer.Remove(aLayer.GetAdorners(selectedElement)[0]);
                    selectedElement = null;
                }
            }
        }

        // Handler for element selection on the canvas providing resizing adorner
        void myCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Remove selection on clicking anywhere the window
            if (selected)
            {
                selected = false;
                if (selectedElement != null)
                {
                    // Remove the adorner from the selected element
                    aLayer.Remove(aLayer.GetAdorners(selectedElement)[0]);
                    selectedElement = null;
                }
            }


            // If any element except canvas is clicked, 
            // assign the selected element and add the adorner
            if (e.Source != myCanvas)
            {
                _isDown = true;
                _startPoint = e.GetPosition(myCanvas);

                

                selectedElement = e.Source as UIElement;

               // Facade._ModuleControl.GetModuleOnCanvasByUID(selectedElement.Uid);

               // selectedElement.Uid;



               MainWindow.LoadedProperties.SelectedObject=  Facade._ModuleControl.GetModulePropertiesOnCanvasByUID(selectedElement.Uid);


                // Load Original MS properties
              //  MainWindow.LoadedProperties.SelectedObject = selectedElement;
                // Disable it when custom is made




                MainWindow.LoadedProperties.Refresh();


                selectedObj =  e.Source;

                if (selectedElement != null)

                {
                    Clipboard.SetDataObject(selectedElement);

                    var apple = Clipboard.GetDataObject() as UIElement;

                  //  Console.WriteLine("Clipboard : " + apple.Uid);
                }

                /*     StringReader readstr = new StringReader("<TextBox TextWrapping=\"WrapWithOverflow\" Name=\"textBox\" Width=\"120\" Height=\"23\" Canvas.Left=\"171\" Canvas.Top=\"275\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">Hello World</TextBox>");
                     XmlReader xml = XmlReader.Create(readstr);

                     myCanvas.Children.Add((UIElement)
                     XamlReader.Load(xml));

                     Console.Write(XamlWriter.Save(selectedElement));
                     */

                //     MessageBox.Show(selectedElement.Uid.ToString() );

                //myCanvas.Children.Remove


                // Console.Write(XamlWriter.Save(selectedElement));


                //////////////////////////////////////////////////////////////////////

                ///////////////////////////////////////////////////////////////////////




                Facade._ModuleControl.UpdateXAMLbyCanvasElementProperties(selectedElement.Uid.ToString());



                //  Console.WriteLine(uid);

                _originalLeft = Canvas.GetLeft(selectedElement);
                _originalTop = Canvas.GetTop(selectedElement);

                aLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                aLayer.Add(new ResizingAdorner(selectedElement));
                selected = true;
                e.Handled = true;


                
                if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
                {
                    //MessageBox.Show("Double Clicked !!");

                    EventBuilder ThisBuilder = new EventBuilder();


                    ThisBuilder.Show();
                }


            }




        }

        private void myCanvas_DragEnter(object sender, DragEventArgs e)
        {
            string data = e.Data.GetData(typeof(string)).ToString();
            Console.Out.WriteLine(data);


    


        }

        private void Preview_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (selectedElement != null)
            {
                Facade._ModuleControl.UpdateModuleOnCanvasProperties(selectedElement);

                MainWindow.LoadedProperties.SelectedObject = Facade._ModuleControl.GetModulePropertiesOnCanvasByUID(selectedElement.Uid);


                MainWindow.LoadedProperties.Refresh();

            }
        }

        private void Preview_Drag_LeaveUpdate(object sender, DragEventArgs e)
        {
            if (selectedElement != null)
                Console.Out.WriteLine("drag Leave update");
        }

        private void myCanvas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Key Canvas sensed : " + e.Key);
        }
    }
}

