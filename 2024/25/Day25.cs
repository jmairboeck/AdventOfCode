using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day25 {
	static void Main() {
		List<int[]> locks = new List<int[]>(), keys = new List<int[]>();
		using (var file = File.OpenText("input.txt")) {
			do {
				var item = new int[5];
				(file.ReadLine() == "#####" ? locks : keys).Add(item);
				for (var i = 0; i < 5; ++i) {
					var line = file.ReadLine();
					for (var j = 0; j < 5; ++j) {
						if (line[j] == '#') item[j]++;
					}
				}
				file.ReadLine(); // bottom line
			} while (file.ReadLine() != null);
		}
		var count = 0;
		foreach (var @lock in locks) {
			foreach (var key in keys) {
				if (@lock.Zip(key, (l, k) => l + k).All(s => s <= 5)) count++;
			}
		}
		Console.WriteLine(count);
	}
}
