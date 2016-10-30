# VectorTileToBitmapRenderer 
Renders vector tiles to PNGs. This makes it easy to add a vector tile source to a client that has bitmap tile support. It limits the possibilities of direct vector tile rendering. It is still possible to choose a custom styles though.

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
