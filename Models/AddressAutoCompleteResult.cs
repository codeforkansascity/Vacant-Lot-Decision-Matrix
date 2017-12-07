using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFKC.VPV.ClientApp.Models
{
    public class AddressAutoCompleteResult
    {
        public long ParcelId { get; set; }

        public string FormattedAddress { get; set; }

        public AddressAutoCompleteResult(long parcelId, string formattedAddress)
        {
            ParcelId = parcelId;

            FormattedAddress = formattedAddress;
        }
    }
}
