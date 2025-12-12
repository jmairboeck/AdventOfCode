using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day12 {
	static void Main() {
		var count = 0;
		using (var stream = File.OpenText("input.txt")) {
			var line = stream.ReadLine(); // first index line ("0:")
			var shapeSizes = new List<int>();
			do {
				var shape = 0;
				while ((line = stream.ReadLine()) != string.Empty)
					shape += line.Count(c => c == '#');
				shapeSizes.Add(shape);
			} while (!(line = stream.ReadLine()).Contains('x')); // next index line or first area line
			do {
				var parts = line.Split(" x:".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
				var sum = 0;
				for (var i = 0; i < shapeSizes.Count; i++)
					sum += shapeSizes[i] * parts[i + 2];
				if (parts[0] * parts[1] >= sum)
					count++;
			} while ((line = stream.ReadLine()) != null);
		}
		Console.WriteLine(count);
	}
}
