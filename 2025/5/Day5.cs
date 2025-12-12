using System;
using System.Collections.Generic;
using System.IO;

class Day5 {
	static void Main() {
		var items = 0;
		var ranges = new SortedSet<(long, long)>();
		using (var stream = File.OpenText("input.txt")) {
			string line;
			while ((line = stream.ReadLine()) != string.Empty) {
				var range = line.Split('-');
				ranges.Add((long.Parse(range[0]), long.Parse(range[1])));
			}
			while ((line = stream.ReadLine()) != null) {
				var number = long.Parse(line);
				foreach (var range in ranges) {
					var (start, end) = range;
					if (number >= start && number <= end) {
						items++;
						break;
					}
				}
			}
		}
		Console.WriteLine(items);

		long all = 0L, max = 0L;
		foreach (var range in ranges) {
			var (start, end) = range;
			if (max > end) continue;
			all += end - Math.Max(max, start) + 1;
			max = end + 1;
		}
		Console.WriteLine(all);
	}
}
