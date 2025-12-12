using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day10 {
	static int[] buttons;
	static Stack<int> numbers;
	static int maxDepth;

	static void PressLights(int number) {
		if (numbers.Count >= maxDepth) return;
		foreach (var button in buttons) {
			var newNumber = button ^ number;
			if (newNumber == 0) {
				maxDepth = numbers.Count + 1;
				return;
			}
		}
		foreach (var button in buttons) {
			var newNumber = button ^ number;
			if (numbers.Contains(newNumber)) continue;
			numbers.Push(newNumber);
			PressLights(newNumber);
			numbers.Pop();
		}
	}

	static void PressJoltages(int[] joltages, int depth) {
		if (depth >= maxDepth) return;
		foreach (var button in buttons) {
			var copy = (int[])joltages.Clone();
			for (var i = 0; i < copy.Length; i++)
				if ((button & (1 << i)) != 0) copy[i]--;
			//Console.WriteLine(string.Join(',', copy));
			if (Array.TrueForAll(copy, j => j == 0)) {
				maxDepth = depth;
				//Console.WriteLine(depth);
				return;
			}
		}
		foreach (var button in buttons) {
			var copy = (int[])joltages.Clone();
			for (var i = 0; i < copy.Length; i++)
				if ((button & (1 << i)) != 0) copy[i]--;
			if (Array.IndexOf(copy, -1) >= 0) continue;
			PressJoltages(copy, depth + 1);
		}
	}

	static void Main() {
		int countLights = 0, countJoltage = 0;
		using (var stream = File.OpenText("input.txt")) {
			string line;
			while ((line = stream.ReadLine()) != null) {
				var words = line.Split(" [](){}".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				var lights = 0;
				for (var i = 0; i < words[0].Length; i++)
					if (words[0][i] == '#') lights += 1 << i;
				var bits = new List<string[]>();
				for (var i = 1; i < words.Length - 1; i++)
					bits.Add(words[i].Split(','));
				bits.Sort((a, b) => b.Length - a.Length);
				buttons = bits.Select(b => b.Sum(n => 1 << int.Parse(n))).ToArray();
				numbers = new Stack<int>();
				maxDepth = int.MaxValue;
				PressLights(lights);
				countLights += maxDepth;
				var joltages = words[words.Length - 1].Split(',').Select(int.Parse).ToArray();
				/*maxDepth = int.MaxValue;
				PressJoltages(joltages, 1);
				countJoltage += maxDepth;*/
			}
		}
		Console.WriteLine(countLights);
		Console.WriteLine(countJoltage);
	}
}
