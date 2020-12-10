using System;
using DemoLib.Data.Count;

namespace DemoLib.Data.DTO
{
    public class DtoDataProbe
    {
        public float RespectiveValue { get; set; }
        public DateTime RespectiveTime => DateTime.Now;

        public DtoDataProbe(DataCount data)
        {
            RespectiveValue = data.Data.Count;
        }

    }
}