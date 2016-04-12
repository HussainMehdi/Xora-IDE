using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Xml;
using System.Windows.Markup;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XORA_IDE
{

    

    class ModulesController
    {
        List<Module> ModulesLoaded;
        Dictionary<string, int> ModulesIDCounter;
        Dictionary<string, UIElement> ModulesOnCanvas;
        Dictionary<string, ModulePropertiesList> ModulesPropOnCanvas;
        Dictionary<string, List<Models.EventsList> > ModulesEventListOnCanvas;




        public ModulesController()
        {
            ModulesLoaded            = new List<Module>();
            ModulesOnCanvas          = new Dictionary<string, UIElement>();
            ModulesPropOnCanvas      = new Dictionary<string, ModulePropertiesList>();
            ModulesEventListOnCanvas = new Dictionary<string, List<Models.EventsList>>();
            //ModulesLoaded.Add(DevelopModule());

            LoadModules(Globals.MODULES_PATH);
            Init_ModuleCounter();
           

        }

        public void RefreshGenericProperties()
        {

        }
        public UIElement GetModuleOnCanvasByUID(string uid)
        {
            if (ModulesOnCanvas.Keys.Contains(uid))
                return ModulesOnCanvas[uid];
            else return null;

        }

        public ModulePropertyGrid GetModulePropertiesOnCanvasByUID(string uid)
        {
            if (ModulesPropOnCanvas.Keys.Contains(uid))
            {
                var prop = ModulePropertiesList.BuildPropertyGrid(ModulesPropOnCanvas[uid].Fields);

                // Make it invisible
                // UID to reach that object the properties are for
                prop.Add(new ModuleProperty("uid", uid, true, false, "Dev_code", "The reference of object"));

                return prop;
            }
            else return null;

        }

        public List<Models.EventsList> GetModuleOnCanvasEventsByUID(string uid)
        {
            if (ModulesEventListOnCanvas.Keys.Contains(uid))
            {
                return ModulesEventListOnCanvas[uid];
            }
            else
                return null;
        }

        public void UpdateXAMLbyCanvasElementProperties(string uid)
        {
            if (ModulesOnCanvas.ContainsKey(uid))
            {
                // System.Windows.Forms.MessageBox.Show("I can update XAML of "+uid);
                var ModuleLoaded = ModulesOnCanvas[uid];
                string xml = XamlWriter.Save(ModuleLoaded);
                //XDocument doc = XDocument.Parse(xml);
                //  System.Windows.MessageBox.Show(xml);

                XmlNamespaceManager mngr = new XmlNamespaceManager(new NameTable());
                XmlParserContext parserContext = new XmlParserContext(null, mngr, null, XmlSpace.None);
                XmlTextReader txtReader = new XmlTextReader(xml, XmlNodeType.Element, parserContext);
                var doc = XElement.Load(txtReader);

                //doc.Value = "Apple";

                //System.Windows.MessageBox.Show(doc.Value);

                var Properties = ModulesPropOnCanvas[uid];

                for (int i = 0; i < Properties.Fields.Count; i++)
                {
                    if (Globals.DefaultComponentsProperties.ContainsKey(Properties.Fields[i].ComponentChng))
                    {
                        Console.WriteLine("Modyfing Props : " + Properties.Fields[i].ComponentChng);   
                    }
                }
            }
        }


        public void UpdateModuleOnCanvasProperties(UIElement e)
        {
            ///////////////////////////////////////////////////////////////////////////////
            var SelectedComponentXaml = XamlWriter.Save(e);
            /////////////////////////////////////////////////////////////////////////////////

            XmlNamespaceManager mngr = new XmlNamespaceManager(new NameTable());
            XmlParserContext parserContext = new XmlParserContext(null, mngr, null, XmlSpace.None);
            XmlTextReader txtReader = new XmlTextReader(SelectedComponentXaml, XmlNodeType.Element, parserContext);
            var doc = XElement.Load(txtReader);
            XNamespace xns = "http://schemas.microsoft.com/winfx/2006/xaml", app = "";


            Dictionary<string, string> SelectedProperties = new Dictionary<string, string>();

            for (int i = 0; i < Globals.DefaultComponentsProperties.Count; i++)
            {
                string      key = Globals.DefaultComponentsProperties.Keys.ElementAt(i);
                XNamespace  NS  = Globals.DefaultComponentsProperties[key];

                SelectedProperties.Add(key, doc.Attribute(NS + key).Value);
            }

            SelectedProperties.Add("Content", doc.Value);

            var fields = ModulesPropOnCanvas[SelectedProperties["Uid"]].Fields;
            for (int i=0; i<fields.Count; i++)
           if ( SelectedProperties.ContainsKey(  fields[i].ComponentChng) )
            {
                    fields[i].Key[0] = SelectedProperties[fields[i].ComponentChng];

                    //  System.Windows.MessageBox.Show("I can modify "+fields[i].ComponentChng);
              }

            /*for (int i = 0; i < SelectedProperties.Count; i++)
            {
                string key = SelectedProperties.Keys.ElementAt(i);
                Console.WriteLine("Key : " + key + ", Value: " + SelectedProperties[key]);
            }

            */


        }


        private int GenerateModuleID(string module)
        {
            string process = string.Empty;
            foreach (Module name in ModulesLoaded)
            {
                if (name.Name == module)
                    process = name.uid;
            }


            if (process != string.Empty && ModulesIDCounter.Keys.Contains(process))
            {
                return ++ModulesIDCounter[process];
            }
            else
                return 0;
        }

        public void LoadModules(string path)
        {
            foreach ( string Dir in    Directory.GetDirectories(Directory.GetCurrentDirectory() + path) )
            {
              string _module =  File.ReadAllText( Dir + Globals.MODULE_PROP_FILE );

                ModulesLoaded.Add(JsonConvert.DeserializeObject<Module>(_module));
                
              //Console.Out.WriteLine(_module);
                Console.Out.WriteLine("Loaded Module : " + ModulesLoaded[ModulesLoaded.Count-1].Name);
            }
        }


        public List<String[]> GetToolBoxComponentsList()
        {
            
            List<String[]> GeneratedComponents= new List <String[]>();

            for (int i = 0; i < ModulesLoaded.Count; i++ )

                GeneratedComponents.Add(new String[] { ModulesLoaded[i].Category, ModulesLoaded[i].Name, @"\" + Globals.MODULES_PATH + @"\" +   ModulesLoaded[i].Name + @"\" + ModulesLoaded[i].icon });

            return GeneratedComponents;
        }

        public string FindXAMLOfModule(string data) 
        {
            string xaml = string.Empty;
         //   var canvas=ca
            foreach (Module name in ModulesLoaded)
            {
                if (name.Name == data)
                    xaml = name.Script;

            }

            return xaml ;
        }

        public ModulePropertiesList GetDefaultPropertyOfModule(string data)
        {
            foreach (Module name in ModulesLoaded)
              {
                if (name.Name == data)
                    return name.Property_List;

              }
            return null;
        }

        private string FindUidKeyOfModule(string data)
        {
            string uid = string.Empty;
            foreach (Module name in ModulesLoaded)
            {
                if (name.Name == data)
                    uid = name.uid;

            }

            return uid;
        }

        private UIElement GenerateUIElement(string xaml)
        {
            StringReader stringReader = new StringReader(xaml);
            XmlReader xmlReader = XmlReader.Create(stringReader);

            return  (UIElement)XamlReader.Load(xmlReader);
            
        }


        public UIElement DevelopModule(string data)
        {
            string xaml = FindXAMLOfModule(data);
            int ID      = GenerateModuleID(data);
            string _UID = FindUidKeyOfModule(data) + ID.ToString();

            xaml = xaml.Replace(@"/>", @" Name = '" + _UID + @"'  />");

            UIElement gen_element = GenerateUIElement(xaml);
            gen_element.Uid = _UID;


            ModulesPropOnCanvas.Add( _UID ,GetDefaultPropertyOfModule(data));
            ModulesOnCanvas.Add( _UID, gen_element );
           // ModulesPropOnCanvas.Add()

            return gen_element;
            
        }

        #region ModuleDeveloper
        // This function is for us creating modules and will serialize it to load from Json

        public Module DevelopModule()
        {

            ModulePropertiesList myPropertyList = new ModulePropertiesList();
            
            var compDesc =   new Dictionary<string,string>();
            compDesc.Add("en-us", "The Content property of this element can be set to any arbitary value (including simple string values)");

            myPropertyList.Add("String", "Content", "content", "Button", "Common", compDesc);

            compDesc = new Dictionary<string, string>();
            compDesc.Add("en-us", "Gets or sets width of the element");
            myPropertyList.Add("Double", "Width", "Width", "80", "Layout", compDesc);

            compDesc = new Dictionary<string, string>();
            compDesc.Add("en-us", "Gets or sets height of the element");
            myPropertyList.Add("Double", "Height", "Heigth", "80", "Layout", compDesc);

            compDesc = new Dictionary<string, string>();
            compDesc.Add("en-us", "Gets or sets a value that represents the distance between the left side of an element and the left side of parent canvas");
            myPropertyList.Add("Double", "Left", "Canvas.Left", "0", "Layout", compDesc);

            compDesc = new Dictionary<string, string>();
            compDesc.Add("en-us", "Gets or sets a value that represents the distance between the top of an element and the top of parent canvas");
            myPropertyList.Add("Double", "Top", "Canvas.Top", "0", "Layout", compDesc);
            
            Module myModule = new Module() 
                               { 
                                 Name         = "Button",
                                 Category     = "Common",
                                 Script       = "<Button xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'" +
                                                " xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'"                   +
                                                " Width='80' Height='25' Content='Button' />",

                                Property_List = myPropertyList,
                                uid           = "btn_",
                                icon          = @"\img\icon.png"
                               };


        //    string a = Newtonsoft.Json.JsonConvert.SerializeObject(myModule);

          //  var apple = Newtonsoft.Json.JsonConvert.DeserializeObject<Module>(a);

          //  System.IO.File.WriteAllText(@"C:\object.json", a);
            return myModule;
        }
        #endregion ModuleDeveloper

        void Init_ModuleCounter()
        {
            ModulesIDCounter = new Dictionary<string, int>();
            foreach (Module x in ModulesLoaded)
            {
                if (! ModulesIDCounter.Keys.Contains ( x.uid ) )
                   ModulesIDCounter.Add(x.uid, 0);
            }
        }

    }
}
