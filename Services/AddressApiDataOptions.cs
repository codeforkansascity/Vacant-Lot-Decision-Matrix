namespace CFKC.VPV.Services
{

    /// <summary>
    /// ConfigurationOptionsBuilder for reusable CFKCAddressApi dataRepo
    /// this is incomplete and not stable
    /// </summary>
    public class AddressApiDataOptions
    {
        private string defaultBaseUrl = "http://dev-api.codeforkc.org/address-attributes-city-id/V0/";

        private string defaultQueryStringPostfix = "?city=Kansas%20City&state=MO";

        private string _baseUrl;
        public string BaseUrl
        {
            get { return _baseUrl ?? defaultBaseUrl; }
        }

        private string _queryStringPostfix;
        public string QueryStringPostfix
        {
            get { return _queryStringPostfix ?? defaultQueryStringPostfix; }
        }

        /// <summary>
        /// Sets the base url for the public restful API "Address Api" project
        /// </summary>
        /// <param name="baseUrl"></param>
        public AddressApiDataOptions SetBaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }
        /// <summary>
        /// Sets the querystring postfix for public Address API ...
        /// CFKC Address api uses a common schema for thier querystrings eg "/{someParam}?city=Kansas%20City&state=MO"
        /// you can change the default with this method
        /// </summary>
        /// <param name="postfix"></param>
        public AddressApiDataOptions SetQueryStringPostfix(string postfix)
        {
            _queryStringPostfix = postfix;
            return this;
        }
    }
}
