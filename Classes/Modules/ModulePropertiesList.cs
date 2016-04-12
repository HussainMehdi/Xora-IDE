using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XORA_IDE
{ 
    public class ModulePropertiesList 
    {
        public List<Field> Fields { set; get; }
        
       

        public ModulePropertiesList()
        {
            Fields = new List<Field>();
            
        }

        /*
        public void Add(String FieldType, String FieldData, String ComponentChange, List<String> FieldKey, String FieldCategory, Dictionary<string, string>  FieldDescription)
        {
            Fields.Add(new Field(FieldType, FieldData, ComponentChange, FieldKey, FieldCategory, FieldDescription));
        }
        */

        public void Add(String FieldType, String FieldData, String ComponentChange, String FieldKey, String FieldCategory, Dictionary<string, string> FieldDescription)
        {
            Fields.Add(new Field(FieldType, FieldData,ComponentChange, FieldKey, FieldCategory, FieldDescription));
        }


        public static ModulePropertyGrid BuildPropertyGrid(List<Field> Fields)
        {
            ModulePropertyGrid m_Property = new ModulePropertyGrid();

            for (var i = 0; i < Fields.Count; i++)
            {
                if (Fields[i].Key.Count > 1)
                    m_Property.Add(new ModuleProperty(Fields[i].Data, Fields[i].Key, false, true, Fields[i].Category, Fields[i].Desc["en-us"]));
                else
                    m_Property.Add(new ModuleProperty(Fields[i].Data, Fields[i].Key[0], false, true, Fields[i].Category,  Fields[i].Desc["en-us"]));

                //  m_Property.Add(new ModuleProperty("Name", "First Property", false, true, Fields[i].Category, "This is a good prop"));
            }

            return m_Property;
        }
    
    }



    

   public class Field
    { 
        public String Type, Data, Category, ComponentChng;
        public Dictionary<string, string> Desc;
        public List<String> Key;
    /*    
        public Field(String FieldType, String FieldData, String ComponentChange , List<String> FieldKey, String FieldCategory, Dictionary<string, string> FieldDescription)
        {
            Type     = FieldType;
            Data     = FieldData;
            Key      = FieldKey;
            Content  = ComponentChange;
            Desc     = FieldDescription;
            Category = FieldCategory;
        }
       */

        public Field(String FieldType, String FieldData, String ComponentChange, String FieldKey, String FieldCategory, Dictionary<string, string> FieldDescription)
        {
            Type = FieldType;
            Data = FieldData;
            var Keys = new List<String>();
            Keys.Add(FieldKey);
            ComponentChng = ComponentChange;
            Key = Keys;
            Desc = FieldDescription;
            Category = FieldCategory;
        }

    }
}
