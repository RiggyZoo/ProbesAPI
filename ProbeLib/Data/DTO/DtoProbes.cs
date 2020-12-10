using System.Collections.Generic;
using System.Linq;

namespace DemoLib.Data.DTO
{
    public class DtoProbes
    {
        public string Version { get; set; }
        public List<DtoProbe> Probes { get; set; }

        public DtoProbes(string version, List<Probe> probes)
        {
            Version = version;
            Probes = probes.Select(probe => new DtoProbe(probe)).ToList();
        }
        
    }
}