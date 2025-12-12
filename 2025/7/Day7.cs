using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Day7 {
	static void Main() {
		using (var stream = File.OpenText("input.txt")) {
			var count = 0;
			var line = stream.ReadLine();
			var beams = new long[line.Length];
			beams[line.IndexOf('S')] = 1L;
			while ((line = stream.ReadLine()) != null)
				foreach (var i in Regex.Matches(line, @"\^").Cast<Match>().Select(m => m.Index))
					if (beams[i] > 0L) {
						count++;
						beams[i - 1] += beams[i];
						beams[i + 1] += beams[i];
						beams[i] = 0L;
					}
			Console.WriteLine(count);
			Console.WriteLine(beams.Sum());
		}
	}
}
