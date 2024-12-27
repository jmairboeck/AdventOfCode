using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day12 {
	static string[] map;
	static int rows, cols, area;
	static bool[,] visited;
	static List<Tuple<int, int>> tops, rights, bottoms, lefts;

	static void Flood(int x, int y) {
		if (visited[y, x]) return;
		area++;
		visited[y, x] = true;
		if (y == 0        || map[y][x] != map[y - 1][x]) tops.Add(Tuple.Create(y, x));
		else Flood(x, y - 1);
		if (x == cols - 1 || map[y][x] != map[y][x + 1]) rights.Add(Tuple.Create(x, y));
		else Flood(x + 1, y);
		if (y == rows - 1 || map[y][x] != map[y + 1][x]) bottoms.Add(Tuple.Create(y, x));
		else Flood(x, y + 1);
		if (x == 0        || map[y][x] != map[y][x - 1]) lefts.Add(Tuple.Create(x, y));
		else Flood(x - 1, y);
	}

	static void Main() {
		map = File.ReadAllLines("input.txt");
		rows = map.Length;
		cols = map[0].Length;
		int sumPerimeter = 0, sumSides = 0;
		visited = new bool[rows, cols];
		for (var i = 0; i < rows; ++i) {
			for (var j = 0; j < cols; ++j) {
				area = 0;
				tops = new List<Tuple<int, int>>();
				rights = new List<Tuple<int, int>>();
				bottoms = new List<Tuple<int, int>>();
				lefts = new List<Tuple<int, int>>();
				Flood(j, i);
				sumPerimeter += area * (tops.Count + rights.Count + bottoms.Count + lefts.Count);
				tops.Sort();
				rights.Sort();
				bottoms.Sort();
				lefts.Sort();
				var sides = 4;
				for (var k = 1; k < tops.Count; ++k) {
					if (tops[k - 1].Item1 != tops[k].Item1 || tops[k - 1].Item2 < tops[k].Item2 - 1) sides++;
				}
				for (var k = 1; k < rights.Count; ++k) {
					if (rights[k - 1].Item1 != rights[k].Item1 || rights[k - 1].Item2 < rights[k].Item2 - 1) sides++;
				}
				for (var k = 1; k < bottoms.Count; ++k) {
					if (bottoms[k - 1].Item1 != bottoms[k].Item1 || bottoms[k - 1].Item2 < bottoms[k].Item2 - 1) sides++;
				}
				for (var k = 1; k < lefts.Count; ++k) {
					if (lefts[k - 1].Item1 != lefts[k].Item1 || lefts[k - 1].Item2 < lefts[k].Item2 - 1) sides++;
				}
				sumSides += area * sides;
			}
		}
		Console.WriteLine(sumPerimeter);
		Console.WriteLine(sumSides);
	}
}
