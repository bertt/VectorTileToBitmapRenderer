using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using BruTile;
using BruTile.Cache;
using BruTile.Web;
using Terrain.Tile;

namespace VectorTileToBitmapRenderer
{
    public class CesiumTerrainTileSource : HttpTileSource
    {
        public CesiumTerrainTileSource(ITileSchema tileSchema, string urlFormatter, IEnumerable<string> serverNodes = null, string apiKey = null, string name = null, IPersistentCache<byte[]> persistentCache = null)
            : base(tileSchema, urlFormatter, serverNodes, apiKey, name, persistentCache, FetchTile)
        {
        }

        private static byte[] FetchTile(Uri url)
        {
            var gzipWebClient = new HttpClient(new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            return gzipWebClient.GetByteArrayAsync(url).Result;
        }

        public override byte[] GetTile(TileInfo tileInfo)
        {
            var bytes = base.GetTile(tileInfo);
            var index = tileInfo.Index;
            var terrainTile = TerrainTileParser.Parse(new MemoryStream(bytes));
            var tileWidth = Schema.GetTileWidth(tileInfo.Index.Level);
            var tileHeight = Schema.GetTileHeight(tileInfo.Index.Level);
            var cesiumTerrainToGdiRenderer = new CesiumTerrainToGdiRenderer(tileWidth, tileHeight, ToGeoJSONArray(tileInfo.Extent));
            var triangles = terrainTile.GetTriangles(index.Col, index.Row, int.Parse(index.Level));
            return cesiumTerrainToGdiRenderer.Render(triangles);
        }

        private IGeoJsonRenderer GetGeoJsonRenderer(TileInfo tileInfo, int tileWidth, int tileHeight)
        {
            if (UseGdi)
                return new GeoJsonToGdiRenderer(tileWidth, tileHeight, ToGeoJSONArray(tileInfo.Extent));
            return new GeoJsonToOpenTKRenderer(tileWidth, tileHeight, ToGeoJSONArray(tileInfo.Extent));
        }

        public bool UseGdi { private get; set; } = true;

        private static double[] ToGeoJSONArray(Extent extent)
        {
            // GeoJSON.NET has no class for bounding boxes. It just holds them in a double array. 
            // The spec says it should first the lowest and then all the highest values for all axes:
            // http://geojson.org/geojson-spec.html#bounding-boxes
            return new[] { extent.MinX, extent.MinY, extent.MaxX, extent.MaxY };

        }
    }
}
