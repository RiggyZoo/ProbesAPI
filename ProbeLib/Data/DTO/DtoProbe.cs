namespace DemoLib.Data.DTO
{
    public class DtoProbe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefaultMinValue { get; set; }
        public int DefaultMaxValue { get; set; }
        public string Note { get; set; }

        public DtoProbe(Probe probe)
        {
            Id = probe.UniqId;
            Name = probe.Name;
            DefaultMinValue = probe.LowValue;
            DefaultMaxValue = probe.HighValue;
            Note = probe.Description;
        }

    }
}