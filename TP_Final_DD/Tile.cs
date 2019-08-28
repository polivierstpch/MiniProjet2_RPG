using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Final_DD
{
    class Tile
    {
        int id, eventid;
        string eventType;

        public int ID { get { return id; } }
        public int EventId { get { return eventid; } }
        public string EventType { get { return eventType; } }

        public Tile(int id, string eventType, int eventId)
        {
            this.id = id;
            this.eventid = eventId;
            this.eventType = eventType;
        }
    }
}
