using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DemoLib.Data.Count;
using DemoLib.Data.DTO;
using Newtonsoft.Json;

namespace DemoLib.Data.Service
{
    public class ProbeService : IProbeService,
    {
        private HttpClient _client;
        private readonly IConfig _config;

        public ProbeService(IConfig config)
        {
            _config = config;
            InitializeClient();
        }


        public void InitializeClient()
        {
            var authorization = new AuthenticationHeaderValue("Bearer", _config.Token);

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = authorization;
        }



        public string BonjourJson()
        {
            return File.ReadAllText("bonjour.json");
        }
        public async Task<DtoProbes> GetAllModifiedProbes()
        {
            var result = await GetAllProbes();
            return new DtoProbes("1.0.1", result);
        }

        public async Task<DtoProbe> GetOneModifiedProbeById(string id)
        {
            var result = await GetOneProbeById(id);
            return new DtoProbe(result);
        }

        public async Task<DtoDataProbe> GetDataFromProbeUsingFilterType(string id)
        {
            var probe = await GetOneProbeById(id);
            switch (probe.Type)
            {
                case "UserFilter":
                    return await GetDataFromProbeWithUserFilter(probe);
                case "API":
                    return await GetDataFromProbeWithApiFilter(probe);
            }
            return default;
        }



        /// <summary>
        ///   Get all probe from database.      
        /// </summary>
        /// <returns> Returns list of probes </returns>
        public async Task<List<Probe>> GetAllProbes()
        {

            var url = _config.Url + _config.ListOfProbeEndPoint
                .Replace("{appId}", _config.AppId)
                .Replace("{tableId}", _config.TableId);

            var result = await _client.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var desObj = JsonConvert.DeserializeObject<ListIncomingData>(await result.Content.ReadAsStringAsync());
                var probes = desObj.Data.Select(r => r.Fields).ToList();
                return probes;
            }

            return default;
        }

        /// <summary>
        /// Get all probes from database and find one with UniqId
        /// </summary>
        /// <param name="id"> Probe Id</param>
        /// <returns>One probe</returns>
        public async Task<Probe> GetOneProbeById(string id)
        {
            var url = _config.Url + _config.OneProbeEndPoint
                .Replace("{appId}", _config.AppId)
                .Replace("{tableId}", _config.TableId)
                .Replace("{UniqId}", id);

            var result = await _client.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var desObj = JsonConvert.DeserializeObject<ListIncomingData>(await result.Content.ReadAsStringAsync());

                var probe = desObj.Data.Select(s => s.Fields).ToList();

                return probe[0];
            }


            return default;
        }


        /// <summary>
        /// Complete Probe with UserFilter
        /// </summary>
        /// <param name="id">Probe UniqId</param>
        /// <returns>DTODataProbe</returns>
        private async Task<DtoDataProbe> GetDataFromProbeWithUserFilter(Probe probe)
        {
            var url = _config.Url + _config.FilterEndpoint
                .Replace("{appId}", probe.AppId)
                .Replace("{tableId}", probe.TableId)
                .Replace("{filter}", probe.UserFilter);

            var mainResponce = await _client.GetAsync(url);
            if (mainResponce.IsSuccessStatusCode)
            {
                var desObj = JsonConvert.DeserializeObject<DataCount>(await mainResponce.Content.ReadAsStringAsync());

                return new DtoDataProbe(desObj);
            }

            return default;
        }


        /// <summary>
        /// Complete probe with APi (POST)  
        /// </summary>
        /// <param name="probe">Probes that comes from get request</param>
        /// <returns>DTODataProbe. Count of matches and current time of request</returns>
        private async Task<DtoDataProbe> GetDataFromProbeWithApiFilter(Probe probe)
        {

            var url = _config.Url + _config.ApiFilterEndPoint
                .Replace("{appId}", probe.AppId)
                .Replace("{tableId}", probe.TableId);

            var postData = new StringContent(probe.Filter, Encoding.UTF8, "application/json");

            var mainResponce = await _client.PostAsync(url, postData);
            if (mainResponce.IsSuccessStatusCode)
            {
                var responce = await mainResponce.Content.ReadAsStringAsync();
                var desObj = JsonConvert.DeserializeObject<DataCount>(await mainResponce.Content.ReadAsStringAsync());

                return new DtoDataProbe(desObj);
            }

            return default;
        }

    }
}