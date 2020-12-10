using DemoLib.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoLib.Data.Service
{
    public interface IProbeService
    {
        string BonjourJson();
        Task<DtoProbes> GetAllModifiedProbes();
        Task<List<Probe>> GetAllProbes();
        Task<DtoDataProbe> GetDataFromProbeUsingFilterType(string id);
        Task<DtoProbe> GetOneModifiedProbeById(string id);
        Task<Probe> GetOneProbeById(string id);
        void InitializeClient();
    }
}