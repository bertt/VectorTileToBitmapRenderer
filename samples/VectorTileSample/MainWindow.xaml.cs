using System.Windows;
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
        readonly HttpVectorTileSource _httpVectorTileSource;
        readonly TileLayer _vectorTileLayer;
        public MainWindow()
        {
            InitializeComponent();

            MapControl.Map.Layers.Add(new TileLayer(KnownTileSources.Create()) { Name = "OpenStreetMap"});

            _httpVectorTileSource = CreateVectorTileTileSource();
            _vectorTileLayer = new TileLayer(_httpVectorTileSource) { Opacity = 0.5, Name = "Mapzen vector tiles"};
            MapControl.Map.Layers.Add(_vectorTileLayer);

            MapsuiLayerList.Initialize(MapControl.Map.Layers);
        }

        public HttpVectorTileSource CreateVectorTileTileSource()
        {
            return new HttpVectorTileSource(
                new GlobalSphericalMercator(),
                "https://tile.mapzen.com/mapzen/vector/v1/all/{z}/{x}/{y}.mvt?api_key=mapzen-tnjqimH",
                name: "vector tile");
        }

        private void GDI_OnClick(object sender, RoutedEventArgs e)
        {
            _httpVectorTileSource.UseGdi = true;
            _vectorTileLayer.ClearCache();
            MapControl.Refresh();
        }

        private void OpenTK_OnClick(object sender, RoutedEventArgs e)
        {
            _httpVectorTileSource.UseGdi = false;
            _vectorTileLayer.ClearCache();
            MapControl.Refresh();
        }
    }
}
