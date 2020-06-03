using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Objects
{
    class Marker
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public PointLatLng Point { get; set; }

        public Marker(int id, string name, PointLatLng point)
        {
            Id = id;
            Name = name;
            Point = point;
        }
    }
}
