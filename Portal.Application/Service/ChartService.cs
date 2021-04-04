using Portal.DAL.Extensions;
using Newtonsoft.Json;
using Portal.Application.Interface;
using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Service
{
    public class ChartService: IChartService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChartService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ListResponseDto> COVID19()
        {
            var httpClient = _httpClientFactory.CreateClient();

            // all beds
            var allBeds = new IraqHospitalAllBedDto();
            var allBedsUrl = "https://covid19.healthdata.org/api/data/bed?location=143";
            var allBedsApi = httpClient.GetAsync(allBedsUrl).Result.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(allBedsApi))
            {
                var allBedsData = JsonConvert.DeserializeObject<ApiResultDto>(allBedsApi);

                allBeds.ReportData = allBedsData.values[0][1].TryToDateTime().GetValueOrDefault();
                allBeds.AllBedsAvailable = allBedsData.values[0][3].TryToInt();
                allBeds.AllBedsUsage = allBedsData.values[0][5].TryToInt();
                allBeds.IcuBedsAvailable = allBedsData.values[0][4].TryToInt();
                allBeds.IcuBedsUsage = allBedsData.values[0][6].TryToInt();
            }

            // icu beds
            var icuBeds = new IraqHospitalIcuBedDto
            {
                BedsAvailable = new List<decimal[]>(),
                BedsRange = new List<decimal[]>(),
                BedsMean = new List<decimal[]>()
            };

            var icuBedsUrl = "https://covid19.healthdata.org/api/data/hospitalization?location=143&measure%5B%5D=covid_ICU_bed&measure%5B%5D=available_icu_nbr&scenario%5B%5D=1";
            var icuBedsApi = httpClient.GetAsync(icuBedsUrl).Result.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(icuBedsApi))
            {
                var icuBedsData = JsonConvert.DeserializeObject<ApiResultDto>(icuBedsApi);

                foreach (var icuBedData in icuBedsData.values)
                {
                    var date = (decimal)icuBedData[0].TryToDateTime().GetValueOrDefault().Ticks;

                    icuBeds.BedsAvailable.Add(new[] { date, allBeds.IcuBedsAvailable });
                    icuBeds.BedsMean.Add(new[] { date, icuBedData[3].TryToDecimal() });
                    icuBeds.BedsRange.Add(new[] { date, icuBedData[5].TryToDecimal(), icuBedData[4].TryToDecimal() });
                }
            }

            // خروجی نهایی
            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Data = icuBeds
            };
        }
    }
}
