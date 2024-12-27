using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day22 {
	static long MixAndPrune(long a, long b) {
		var mix = a ^ b;
		return mix & 0xffffff;
	}

	static void Main() {
		var sum = 0L;
		var profits = new Dictionary<uint, int>();
		using (var file = File.OpenText("input.txt")) {
			string line;
			while ((line = file.ReadLine()) != null) {
				var number = long.Parse(line);
				var profitKey = 0u;
				var lastPrice = (int)(number % 10);
				var soldAlready = new HashSet<uint>();
				for (var i = 0; i < 2000; ++i) {
					number = MixAndPrune(number, number << 6);
					number = MixAndPrune(number, number >> 5);
					number = MixAndPrune(number, number << 11);

					// The solution to part 2 is based on this existing solution: https://github.com/amgine/aoc.csharp/blob/master/2024/day22/Solution.cs
					var currentPrice = (int)(number % 10);
					profitKey = (profitKey << 8) | (byte)(sbyte)(currentPrice - lastPrice);
					if (i >= 3 && soldAlready.Add(profitKey)) {
						profits.TryGetValue(profitKey, out var profit);
						profits[profitKey] = profit + currentPrice;
					}
					lastPrice = currentPrice;
					// end of taken over excerpt
				}
				sum += number;
			}
		}
		Console.WriteLine(sum);
		Console.WriteLine(profits.Values.Max());
	}
}
