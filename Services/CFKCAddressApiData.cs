using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

namespace CFKC.VPV.Services
{
    public class CFKCAddressApiData
    {
        public string BaseUrl { get; }

        public string QueryStringPostfix { get; }

        public CFKCAddressApiData(Action<AddressApiDataOptions> configurationAction)
        {
            var options = new AddressApiDataOptions();

            configurationAction(options);

            BaseUrl = options.BaseUrl;

            QueryStringPostfix = options.QueryStringPostfix;
        }

        public CFKCAddressApiData()
        {
            var defaultOptions = new AddressApiDataOptions();

            BaseUrl = defaultOptions.BaseUrl;

            QueryStringPostfix = defaultOptions.QueryStringPostfix;
        }


        /// <summary>
        /// Retrieves a parcel by kivapin (city identifier), from the CFKC address api 
        /// </summary>
        /// <param name="kiva"></param>
        /// <returns>AddressApiParcel from kiva, returns null if theres an error or no parcel is found </returns>
        public AddressApiParcel GetParcelByKiva(int kiva)
        {
            var requestUrl = $"{BaseUrl}{kiva}{QueryStringPostfix}";

            AddressApiParcel parcel = null;

            using (var client = new HttpClient())
            {
                var res = client.GetAsync(requestUrl).Result;

                if (res.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                var json = res.Content.ReadAsStringAsync().Result;
                
                parcel = JsonConvert.DeserializeObject<ResponseObject>(json).data;
            }

            return parcel;
        }
    }
}
