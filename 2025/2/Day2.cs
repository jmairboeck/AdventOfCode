using System;
using System.IO;

class Day2 {
	static void Main() {
		long sum1 = 0L, sum2 = 0L;
		var ranges = File.ReadAllText("input.txt").Split(',');
		foreach (var rangeStr in ranges) {
			var range = rangeStr.Split('-');
			long start = long.Parse(range[0]), end = long.Parse(range[1]);
			for (var number = start; number <= end; ++number) {
				var str = number.ToString();
				int numParts;
				for (numParts = 2; numParts <= str.Length; ++numParts) {
					if (str.Length % numParts != 0) continue;
					var partLength = str.Length / numParts;
					var first = str.Remove(partLength);
					int i;
					for (i = partLength; i < str.Length; i += partLength)
						if (str.Substring(i, partLength) != first) break;
					if (i == str.Length) break;
				}
				if (numParts <= str.Length) {
					if (numParts == 2) sum1 += number;
					sum2 += number;
				}
			}
		}
		Console.WriteLine(sum1);
		Console.WriteLine(sum2);
	}
}
