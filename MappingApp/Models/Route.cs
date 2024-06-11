using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingApp.Models
{
    public class Route(Location startPoint, Location endPoint, string name)
    {
        public string Name { get; set; } = name;
        public Location StartPoint { get; set; } = startPoint;
        public Location EndPoint { get; set; } = endPoint;
    }
}
