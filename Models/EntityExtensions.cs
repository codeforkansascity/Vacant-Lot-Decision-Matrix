using CFKC.VPV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFKC.VPV.Entities
{
    public static class EntityExtensions
    {
        public static GeometryDto ToDto(this Geometry geometry)
        {
            return GeometryDto.ToGeometryDto(geometry);
        }
    }
}
