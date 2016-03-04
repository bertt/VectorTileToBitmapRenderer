using System.Windows;
using BruTile;
using BruTile.Predefined;
using Mapsui.Layers;
using VectorTileToBitmapRenderer;

namespace VectorTileSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();

            //MapControl.Map.Layers.Add(new TileLayer(KnownTileSources.Create()));
            MapControl.Map.Layers.Add(new TileLayer(CreateVectorTileTileSource()));
        }

        public ITileSource CreateVectorTileTileSource()
        {
            return new HttpVectorTileSource(new GlobalSphericalMercator(),
            //   "http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", new[] { "a", "b", "c" }, 
            //"http://basemapsbeta.arcgis.com/arcgis/rest/services/World_Basemap/VectorTileServer/tile/{z}/{x}/{y}.pbf",
            "https://vector.mapzen.com/osm/all/{z}/{x}/{y}.mvt?api_key=vector-tiles-LM25tq4",
            name: "vector tile");
        }
    }
}
