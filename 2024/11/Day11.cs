using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day11 {
	static List<ulong> stones;

	static void Blink() {
		var newStones = new List<ulong>();
		for (var i = 0; i < stones.Count; ++i) {
			if (stones[i] == 0) {
				stones[i] = 1;
				continue;
			}
			var stoneStr = stones[i].ToString();
			var length = stoneStr.Length;
			if (length % 2 == 0) {
				stones[i] = ulong.Parse(stoneStr.Remove(length / 2));
				newStones.Add(ulong.Parse(stoneStr.Substring(length / 2)));
			}
			else {
				stones[i] *= 2024;
			}
		}
		stones.AddRange(newStones);
	}

	static void Main() {
		using (var file = File.OpenText("input.txt")) {
			stones = file.ReadLine().Split(' ').Select(ulong.Parse).ToList();
		}
		for (var i = 0; i < 25; ++i) {
			Blink();
		}
		Console.WriteLine(stones.Count);
		for (var i = 25; i < 75; ++i) {
			//Console.WriteLine(i);
			Blink();
		}
		Console.WriteLine(stones.Count);
	}
}
