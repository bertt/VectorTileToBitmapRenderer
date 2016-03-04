using System.Collections.Generic;
using GeoJSON.Net.Feature;

namespace VectorTileToBitmapRenderer
{
    public interface IGeoJsonRenderer
    {
        byte[] Render(IEnumerable<FeatureCollection> featureCollections);
    }
}