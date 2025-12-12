using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

class Day8 {
	static void Main() {
		var points = File.ReadAllLines("input.txt").Select(line => {
			var coords = line.Split(',');
			return new Vector3(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2]));
		}).ToArray();
		var distances = new List<(float, int, int)>();
		for (var i = 0; i < points.Length - 1; ++i)
			for (var j = i + 1; j < points.Length; ++j)
				distances.Add((Vector3.DistanceSquared(points[i], points[j]), i, j));
		distances.Sort();
		var circuits = new Dictionary<int, HashSet<int>>();
		var indexes = new int[points.Length];
		for (var i = 0; i < points.Length; ++i) {
			circuits[i] = new HashSet<int> { i };
			indexes[i] = i;
		}

		for (var i = 0;; ++i) {
			if (i == 1000) // part 1
				Console.WriteLine(circuits.Select(c => c.Value.Count).OrderByDescending(c => c).Take(3).Aggregate(1, (a, b) => a * b));
			var (_, j, k) = distances[i];
			if (indexes[j] != indexes[k]) {
				circuits[indexes[j]].UnionWith(circuits[indexes[k]]);
				var indexToRemove = indexes[k];
				foreach (var index in circuits[indexes[k]])
					indexes[index] = indexes[j];
				circuits.Remove(indexToRemove);
				if (circuits.Count == 1) { // part 2
					Console.WriteLine(points[j].X * points[k].X);
					return;
				}
			}
		}
	}
}
