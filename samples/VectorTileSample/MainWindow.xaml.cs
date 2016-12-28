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
        readonly CesiumTerrainTileSource _cesiumTerrainTileSource;
        readonly TileLayer _cesiumTerrainTileLayer;
        public MainWindow()
        {
            InitializeComponent();

            MapControl.Map.Layers.Add(new TileLayer(KnownTileSources.Create()) { Name = "OpenStreetMap"});

            _cesiumTerrainTileSource = CreateCesiumTerrainTileTileSource();
            MapsuiLayerList.Initialize(MapControl.Map.Layers);

            _cesiumTerrainTileSource = CreateCesiumTerrainTileTileSource();
            _cesiumTerrainTileLayer = new TileLayer(_cesiumTerrainTileSource) { Opacity = 0.5, Name = "Cesium Terrain tiles" };
            MapControl.Map.Layers.Add(_cesiumTerrainTileLayer);

        }

        public CesiumTerrainTileSource CreateCesiumTerrainTileTileSource()
        {
            return new CesiumTerrainTileSource(
                new GlobalSphericalMercator(),
                "http://assets.agi.com/stk-terrain/v1/tilesets/world/tiles/{z}/{x}/{y}.terrain",
                name: "cesium terrain tile");
        }


        private void GDI_OnClick(object sender, RoutedEventArgs e)
        {
            _cesiumTerrainTileSource.UseGdi = true;
            _cesiumTerrainTileLayer.ClearCache();
            MapControl.Refresh();
        }

        private void OpenTK_OnClick(object sender, RoutedEventArgs e)
        {
            _cesiumTerrainTileSource.UseGdi = false;
            _cesiumTerrainTileLayer.ClearCache();
            MapControl.Refresh();
        }
    }
}
