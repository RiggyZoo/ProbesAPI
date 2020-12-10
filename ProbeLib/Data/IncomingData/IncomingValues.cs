using System;

namespace DemoLib.Data
{
    public class IncomingValues
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int  Ver { get; set; }
        public Probe Fields { get; set; }
    }
}