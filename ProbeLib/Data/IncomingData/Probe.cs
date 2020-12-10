using System;

namespace DemoLib.Data
{
    public class Probe
    {
        public int UniqId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string AppId { get; set; }
        public string TableId { get; set; }
        public string UserFilter { get; set; }
        public string Filter { get; set; }
        public int LowValue { get; set; }
        public int HighValue { get; set; }
        public Documentation Documentation { get; set; }
        public string Description { get; set; }
    }
}