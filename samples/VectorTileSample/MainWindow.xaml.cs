using BruTile;
using BruTile.Predefined;
using Mapsui.Layers;
using Mapsui.UI.Xaml;
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
            var mapControl = new MapControl();
            grid.Children.Add(mapControl);

            mapControl.Map.Layers.Add(new TileLayer(KnownTileSources.Create()));
            mapControl.Map.Layers.Add(new TileLayer(CreateVectorTileTileSource()));
        }

        public ITileSource CreateVectorTileTileSource()
        {
            return new HttpVectorTileSource(
                new GlobalSphericalMercator(),
                "https://vector.mapzen.com/osm/all/{z}/{x}/{y}.mvt?api_key=vector-tiles-LM25tq4",
                name: "vector tile");
        }
    }
}
