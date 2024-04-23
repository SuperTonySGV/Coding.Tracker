using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding.Tracker.Models
{
    internal class CodingSessionModel
    {
        public class CodingSession
        {
            public int Id { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public int Duration { get; set; }
        }
    }
}
