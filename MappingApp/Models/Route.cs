using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingApp.Models
{
    public class Route
    {
        Location StartPoint { get; set; }
        Location EndPoint { get; set; }
        public Route(Location startPoint, Location endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}
