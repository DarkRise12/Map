using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map.Markers
{
    public class CustomGMapMarker : GMarkerGoogle
    {
        public int Id { get; set; }
        public CustomGMapMarker(int id, PointLatLng latLng) : base (latLng, GMarkerGoogleType.red)
        {
            Id = id;
        }

    }
}
