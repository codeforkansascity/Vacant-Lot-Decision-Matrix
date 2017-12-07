using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CFKC.VPV.Models
{
    public class LatLngDto
    {
        public double Lat { get; set; }

        public double Lng { get; set; }


        public LatLngDto() { }

        public LatLngDto(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }


        public static IEnumerable<IEnumerable<LatLngDto>> ParseFromWKT(string wellKnownText)
        {
            IEnumerable<string> cleanedPolystrings = new[] { wellKnownText };

            if (wellKnownText.Contains("MULTIPOLYGON"))
            {
                wellKnownText = wellKnownText.Replace("MULTIPOLYGON (", "");

                wellKnownText = wellKnownText.Remove(wellKnownText.LastIndexOf(')'));

                cleanedPolystrings = wellKnownText.Split(")),((");
            }

            var parsedPolygons = cleanedPolystrings.Select(p =>
            {
                return Regex.Replace(p, "[^0-9., -]", "").Trim().Split(',')
               .Select(pair =>
               {
                   var v = pair.Split(' ');
                   return new LatLngDto { Lat = double.Parse(v[1]), Lng = double.Parse(v[0]) };
               });
            });

            return parsedPolygons;

        }
    }
}
