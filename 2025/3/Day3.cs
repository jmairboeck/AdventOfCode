using System;
using System.IO;

class Day3 {
	static long GetJoltage(string line, int exponent) {
		if (exponent < 0) return 0; // end condition
		for (var c = '9'; c > '0'; c--) {
			var i = line.IndexOf(c);
			if (i != -1 && i < line.Length - exponent)
				return (long)Math.Pow(10, exponent) * (c - '0') + GetJoltage(line.Substring(i + 1), exponent - 1);
		}
		throw new Exception(); // should not be reached
	}

	static void Main() {
		long sum1 = 0L, sum2 = 0L;
		using (var stream = File.OpenText("input.txt")) {
			string line;
			while ((line = stream.ReadLine()) != null) {
				sum1 += GetJoltage(line, 1);
				sum2 += GetJoltage(line, 11);
			}
		}
		Console.WriteLine(sum1);
		Console.WriteLine(sum2);
	}
}
