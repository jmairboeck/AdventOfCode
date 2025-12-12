using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

class Day9 {
	const float ScaleFactor = 100f; // to avoid overflowing memory when doing path operations

	static void Main() {
		var points = File.ReadAllLines("input.txt").Select(line => {
			var coords = line.Split(',');
			return new Point(int.Parse(coords[0]), int.Parse(coords[1]));
		}).ToArray();
		long maxAreaAll = 0L, maxAreaInside = 0L;
		using (var bitmap = new Bitmap(1, 1)) // dummy bitmap for getting a ...
		using (var graphics = Graphics.FromImage(bitmap)) // dummy Graphics for checking Region.IsEmpty below
		using (var path = new GraphicsPath()) {
			path.AddPolygon(points.Select(p => new PointF(p.X / ScaleFactor, p.Y / ScaleFactor)).ToArray());
			for (var i = 0; i < points.Length - 1; ++i)
				for (var j = i + 1; j < points.Length; ++j) {
					int width = Math.Abs(points[i].X - points[j].X), height = Math.Abs(points[i].Y - points[j].Y);
					var area = (width + 1L) * (height + 1L);
					if (area > maxAreaAll) maxAreaAll = area;
					if (area > maxAreaInside) {
						using (var region = new Region(new RectangleF(Math.Min(points[i].X, points[j].X) / ScaleFactor, Math.Min(points[i].Y, points[j].Y) / ScaleFactor, width / ScaleFactor, height / ScaleFactor))) {
							region.Exclude(path);
							if (region.IsEmpty(graphics)) maxAreaInside = area;
						}
					}
				}
		}
		Console.WriteLine(maxAreaAll);
		Console.WriteLine(maxAreaInside);
	}
}
