using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CFKC.VPV.Services
{
    public class AddressApiMetadata
    {
        public static int[] ResidentialKeys { get; } = { 1111, 1121, 1122, 1123, 1124, 1125, 1126, 9500 };

        public static ReadOnlyDictionary<int, string> LandUseCodes { get; } =
            new ReadOnlyDictionary<int, string>
            (
                new Dictionary<int, string>
                {
                    { 1111, "Single Family (Non-Mobile Home Park)" },
                    { 1112, "Mobile Home Park" },
                    { 1121, "Townhouse" },
                    { 1122, "Duplex" },
                    { 1123, "Multifamily - 3 units" },
                    { 1124, "Multifamily - 4 units" },
                    { 1125, "Multifamily - 5 units or greater" },
                    { 1126, "Condominium" },
                    { 1200, "Hotel / Motel" },
                    { 2100, "Commercial (Non-Office)" },
                    { 2300, "Office" },
                    { 3110, "Heavy Industry" },
                    { 3120, "Light Industry / Storage / Distribution / Vehicle Sale/Service" },
                    { 3200, "Solid Waste Management" },
                    { 4110, "School" },
                    { 4120, "Training Outside Classrooms" },
                    { 4130, "Library" },
                    { 4200, "Emergency Response / Public Safety" },
                    { 4300, "Utilities" },
                    { 4500, "Medical" },
                    { 4600, "Cemetery" },
                    { 4700, "Military Base" } ,
                    { 4800, "Institutional" },
                    { 5100, "Pedestrian Movement" },
                    { 5211, "Garage" },
                    { 5212, "Paved Parking / Other Paved Lots" },
                    { 5220, "Driving" },
                    { 5400, "Railroad" },
                    { 5500, "Water-Based Movement" },
                    { 5600, "Airport" },
                    { 5700, "Spacecraft" },
                    { 6100, "Bus" },
                    { 6200, "Spectator Sports" },
                    { 6300, "Theater" },
                    { 6400, "Convention and Exhibition" },
                    { 6500, "Mass Training and Drills" },
                    { 6610, "Social or Cultural Assembly" },
                    { 6620, "Church" },
                    { 6700, "Museum" },
                    { 6800, "Historical" },
                    { 7100, "Park" },
                    { 7200, "Golf Course" },
                    { 7310, "Condominium Common Area" },
                    { 7320, "Single Family Common Area" },
                    { 7330, "Duplex / Townhouse Common Area" },
                    { 7340, "Multifamily - 3 units - Common Area" },
                    { 7350, "Multifamily - 4 units - Common Area" },
                    { 7360, "Multifamily - 5+ units - Common Area" },
                    { 7400, "Other Recreation" },
                    { 8100, "Agricultural" },
                    { 8200, "Horticultural" },
                    { 8300, "Extraction" },
                    { 8400, "Forest / Logging" },
                    { 9100, "Not Applicable" },
                    { 9210, "Single Family untested for acreage" },
                    { 9220, "Industrial untested for heavy/light" },
                    { 9230, "Outbuilding untested for surroundings" },
                    { 9240, "Misc. improvement untested for surroundings" },
                    { 9250, "Building on Leased Land" },
                    { 9260, "Exempt" },
                    { 9270, "Locally Assessed" },
                    { 9280, "Condo untested for residence or common area" },
                    { 9300, "Underground Space" },
                    { 9400, "Permanent Open Space (e.g. flood)" },
                    { 9500, "Vacant Residential" },
                    { 9600, "Vacant Non-Residential (including billboards)" }
                }
            );
    }
}
