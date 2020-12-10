using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoLib.Data;
using DemoLib.Data.DTO;
using DemoLib.Data.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProbeApi.Controllers
{
    public class ProbesController : ControllerBase
    {
        private IProbeService _service;
        private IConfig _config;

        public ProbesController(IProbeService service, IConfig congfig)
        {
            _service = service;
            _config = congfig;
        }


        /// <summary>
        /// Read file Bonjour.json
        /// </summary>
        [Route("/_bonjour.js")]
        public string  Bonjour()
        {
            return _service.BonjourJson();

        }


        /// <summary>
        /// Show list of modified probes    
        /// </summary>
        /// <returns>List modified probes</returns>
        [Route("/probes")]
        [HttpGet]
        public async Task<DtoProbes> GetAllProbes()
        {
            var result = await _service.GetAllModifiedProbes();
            return result;
        }
        /// <summary>
        /// Show one modified probe
        /// </summary>
        /// <param name="id">UniqId of Probe</param>
        /// <returns>One modified probe</returns>
        [Route("/probes/{id}")]
        [HttpGet("{id}")]
        public async Task<DtoProbe> GetOne(string id)
         {
            var result = await _service.GetOneModifiedProbeById(id);
            return result;
         }


        /// <summary>
        /// Complete probe with filter
        /// </summary>
        /// <param name="id">UniqId of Probe</param>
        /// <returns>Count of matches and current time of request</returns>
        [Route("/probes/{id}/data")]
        [HttpGet("{id}")]
        public async Task<DtoDataProbe> GeetDataCountFromProbeUsingFilter(string id)
         {
             var result = await _service.GetDataFromProbeUsingFilterType(id);
             return result;
         }

    }
}
