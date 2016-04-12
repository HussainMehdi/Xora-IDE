using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XORA_IDE.Models
{
    enum EventType
    {
        Action,
        JS_code,
        CSharp_code
    }

    class EventsList
    {
        EventType type { set; get; }
        String ActionCode { set; get; }
        String Title { set; get; }



        public static List<EventsList> CreateEventList()
        {
            return new List<EventsList>();
        }




    }
}
