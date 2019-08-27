using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Final_DD
{
    class Tuile
    {
        int id, eventid;
        string eventType;

        public int Id { get { return id; } }
        public int EventId { get { return eventid; } }
        public string EventType { get { return eventType; } }

        public Tuile(int Id, string EventType, int EventId)
        {
            this.id = Id;
            this.eventid = EventId;
            this.eventType = EventType;
        }
    }
}
