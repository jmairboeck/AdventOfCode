using System;
using System.Collections.Generic;
using System.IO;

class Day23 {
	static void Main() {
		var edges = new List<Tuple<string, string>>();
		using (var file = File.OpenText("input.txt")) {
			string line;
			while ((line = file.ReadLine()) != null) {
				var nodes = line.Split('-');
				Array.Sort(nodes);
				edges.Add(Tuple.Create(nodes[0], nodes[1]));
			}
		}
		var triples = new HashSet<Tuple<string, string, string>>();
		foreach (var edge1 in edges) {
			foreach (var edge2 in edges) {
				if (edge1 == edge2) continue;
				if (edge1.Item1 == edge2.Item1) {
					if (!edge1.Item1.StartsWith("t") && !edge1.Item2.StartsWith("t") && !edge2.Item2.StartsWith("t"))
						continue;
					if (edge1.Item2.CompareTo(edge2.Item2) < 0 && edges.Contains(Tuple.Create(edge1.Item2, edge2.Item2)))
						triples.Add(Tuple.Create(edge1.Item1, edge1.Item2, edge2.Item2));
					if (edge2.Item2.CompareTo(edge1.Item2) < 0 && edges.Contains(Tuple.Create(edge2.Item2, edge1.Item2)))
						triples.Add(Tuple.Create(edge1.Item1, edge2.Item2, edge1.Item2));
				}
				if (edge1.Item2 == edge2.Item2) {
					if (!edge1.Item1.StartsWith("t") && !edge1.Item2.StartsWith("t") && !edge2.Item1.StartsWith("t"))
						continue;
					if (edge1.Item1.CompareTo(edge2.Item1) < 0 && edges.Contains(Tuple.Create(edge1.Item1, edge2.Item1)))
						triples.Add(Tuple.Create(edge1.Item1, edge2.Item1, edge1.Item2));
					if (edge2.Item1.CompareTo(edge1.Item1) < 0 && edges.Contains(Tuple.Create(edge2.Item1, edge1.Item1)))
						triples.Add(Tuple.Create(edge2.Item1, edge1.Item1, edge1.Item2));
				}
				if (edge1.Item1 == edge2.Item2) {
					if (!edge1.Item1.StartsWith("t") && !edge1.Item2.StartsWith("t") && !edge2.Item1.StartsWith("t"))
						continue;
					if (edges.Contains(Tuple.Create(edge2.Item1, edge1.Item2)))
						triples.Add(Tuple.Create(edge2.Item1, edge1.Item1, edge1.Item2));
				}
				if (edge1.Item2 == edge2.Item1) {
					if (!edge1.Item1.StartsWith("t") && !edge1.Item2.StartsWith("t") && !edge2.Item2.StartsWith("t"))
						continue;
					if (edges.Contains(Tuple.Create(edge1.Item1, edge2.Item2)))
						triples.Add(Tuple.Create(edge1.Item1, edge1.Item2, edge2.Item2));
				}
			}
		}
		Console.WriteLine(triples.Count);
	}
}
