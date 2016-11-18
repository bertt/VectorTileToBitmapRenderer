# VectorTileToBitmapRenderer 
Renders vector tiles to PNGs. This makes it easy to render a vector tile in a client that is capable of bitmap rendering. It is not possible to draw the vectors directly on your clients canvas which will reduce quality in some cases. It is however still possible to choose the styling used to render to bitmap.

It uses [mapbox-vector-tile-cs](https://github.com/bertt/mapbox-vector-tile-cs) to parse vector tiles. This turns is into [GeoJSON.NET](https://github.com/GeoJSON-Net/GeoJSON.Net) objects.

## Roadmap
* Do nothing unless a real need arises
* Wait a little more
* Use SkiaSharp rendering, remove GDI and OpenTK
* Add configurable styling
* Use vector tile style

## Sample
The repo contains a sample of the awesome psychedelic tile renderer which has value all of its own :)

![Alt text](/docs/psychedelic-tiles.png?raw=true "Optional Title")
