using System;
using System.Collections.Generic;
using System.IO;

class Day19 {
	static string[] patterns;
	static Dictionary<string, long> cache = new Dictionary<string, long> { [string.Empty] = 1 };

	static long Match(string design) {
		if (cache.TryGetValue(design, out var count)) return count;
		foreach (var pattern in patterns) {
			if (design.StartsWith(pattern)) {
				count += Match(design.Substring(pattern.Length));
			}
		}
		cache[design] = count;
		return count;
	}

	static void Main() {
		long countDesigns = 0, countMatches = 0;
		using (var file = File.OpenText("input.txt")) {
			patterns = file.ReadLine().Split(", ");
			file.ReadLine();
			string design;
			while ((design = file.ReadLine()) != null) {
				var countThisDesign = Match(design);
				if (countThisDesign > 0) countDesigns++;
				countMatches += countThisDesign;
			}
		}
		Console.WriteLine(countDesigns);
		Console.WriteLine(countMatches);
	}
}
