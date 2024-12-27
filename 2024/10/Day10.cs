using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

class Day10 {
	static string[] map;
	static int rows, cols, ratings = 0;
	static HashSet<Point> peaks;

	static void Walk(int x, int y) {
		if (map[y][x] == '9') {
			peaks.Add(new Point(x, y));
			ratings++;
		} else {
			if (y > 0        && map[y - 1][x] == map[y][x] + 1) Walk(x, y - 1); // go up
			if (x < cols - 1 && map[y][x + 1] == map[y][x] + 1) Walk(x + 1, y); // go right
			if (y < rows - 1 && map[y + 1][x] == map[y][x] + 1) Walk(x, y + 1); // go down
			if (x > 0        && map[y][x - 1] == map[y][x] + 1) Walk(x - 1, y); // go left
		}
	}

	static void Main() {
		map = File.ReadAllLines("input.txt");
		rows = map.Length;
		cols = map[0].Length;
		var scores = 0;
		for (var i = 0; i < rows; ++i) {
			for (var j = 0; j < cols; ++j) {
				if (map[i][j] == '0') {
					peaks = new HashSet<Point>();
					Walk(j, i);
					scores += peaks.Count;
				}
			}
		}
		Console.WriteLine(scores);
		Console.WriteLine(ratings);
	}
}
