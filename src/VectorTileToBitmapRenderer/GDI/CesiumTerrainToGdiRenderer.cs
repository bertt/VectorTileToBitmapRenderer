using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using Terrain.Tile;

namespace VectorTileToBitmapRenderer
{
    public class CesiumTerrainToGdiRenderer
    {
        private readonly int _canvasWidth;
        private readonly int _canvasHeight;
        private readonly float _extentMinX;
        private readonly float _extentMinY;
        private readonly float _extentWidth;
        private readonly float _extentHeight;


        public CesiumTerrainToGdiRenderer(int canvasWidth, int canvasHeight, double[] boundingBox)
        {
            _canvasWidth = canvasWidth;
            _canvasHeight = canvasHeight;
            _extentMinX = (float)boundingBox[0];
            _extentMinY = (float)boundingBox[1];
            _extentWidth = (float)boundingBox[2] - _extentMinX;
            _extentHeight = (float)boundingBox[3] - _extentMinY;
        }


        public byte[] Render(List<Triangle> triangles)
        {
            var random = new Random();

            using (var bitmap = new Bitmap(_canvasWidth, _canvasHeight))
            using (var canvas = Graphics.FromImage(bitmap))
            {
                canvas.Transform = CreateTransformMatrix(_canvasWidth, _canvasHeight, _extentMinX, _extentMinY, _extentWidth, _extentHeight);

                foreach (var triangle in triangles)
                {
                    var points = ToGdi(triangle);

                    using (var brush = new SolidBrush(
                        Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))))
                    {
                        canvas.FillPolygon(brush, points);

                    }
                }
                return ToBytes(bitmap);
            }
        }

        private static PointF[] ToGdi(Triangle triangle)
        {
            var result = new List<PointF>();
            result.Add(SphericalMercator.FromLonLat(triangle.Coordinate1.X, triangle.Coordinate1.Y));
            result.Add(SphericalMercator.FromLonLat(triangle.Coordinate2.X, triangle.Coordinate2.Y));
            result.Add(SphericalMercator.FromLonLat(triangle.Coordinate3.X, triangle.Coordinate3.Y));
            result.Add(SphericalMercator.FromLonLat(triangle.Coordinate1.X, triangle.Coordinate1.Y));
            return result.ToArray();
        }


        private static Matrix CreateTransformMatrix(int canvasWidth, int canvasHeight, float minX, float minY, float width, float height)
        {
            // The code below needs no comments, it is fully intuitive.
            // I wrote in in one go and it ran correctly right away.
            var matrix = new Matrix();
            var flipMatrix = new Matrix(1, 0, 0, -1, 0, 0);
            matrix.Multiply(flipMatrix);
            matrix.Scale(canvasWidth / width, canvasHeight / height);
            var maxY = minY + height;
            matrix.Translate(-minX, -maxY);
            return matrix;
        }


        private static byte[] ToBytes(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}