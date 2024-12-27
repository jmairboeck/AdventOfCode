using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day7 {
	static ulong[] operands;
	static List<ulong> results = new List<ulong>();
	static bool withConcat;

	static void Accumulate(ulong intermediate, int i) {
		if (i < operands.Length) {
			Accumulate(intermediate + operands[i], i + 1);
			Accumulate(intermediate * operands[i], i + 1);
			if (withConcat)
				Accumulate(ulong.Parse(intermediate.ToString() + operands[i]), i + 1);
		}
		else {
			results.Add(intermediate);
		}
	}

	static void Main() {
		ulong sum = 0, sumWithConcat = 0;
		using (var file = File.OpenText("input.txt")) {
			string line;
			while ((line = file.ReadLine()) != null) {
				var parts = line.Split(':');
				var target = ulong.Parse(parts[0]);
				operands = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToArray();
				results.Clear();
				withConcat = false;
				Accumulate(operands[0], 1);
				if (results.Contains(target))
					sum += target;
				results.Clear();
				withConcat = true;
				Accumulate(operands[0], 1);
				if (results.Contains(target))
					sumWithConcat += target;
			}
		}
		Console.WriteLine(sum);
		Console.WriteLine(sumWithConcat);
	}
}
