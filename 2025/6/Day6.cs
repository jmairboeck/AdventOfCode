using System;
using System.IO;
using System.Linq;

class Day6 {
	static void Main() {
		var sum = 0L;
		var input = File.ReadAllLines("input.txt");
		var words = input.Select(line => line.Split(new []{ ' ' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();
		int rows = input.Length, cols = words[0].Length;
		var indexes = Enumerable.Range(0, rows - 1);
		for (var col = 0; col < cols; ++col) {
			switch (words[rows - 1][col]) {
				case "+":
					sum += indexes.Aggregate(0L, (number, row) => number + long.Parse(words[row][col]));
					break;
				case "*":
					sum += indexes.Aggregate(1L, (number, row) => number * long.Parse(words[row][col]));
					break;
			}
		}
		Console.WriteLine(sum);

		sum = 0L;
		cols = input[0].Length;
		var lastOperation = '\0';
		var intermediate = 0L;
		for (var col = 0; col < cols; ++col) {
			var digits = indexes.Where(row => input[row][col] != ' ').ToArray();
			if (digits.Length == 0) continue; // separator column
			var number = digits.Select((row, i) => (long)Math.Pow(10, digits.Length - 1 - i) * (input[row][col] - '0')).Sum();
			if (input[rows - 1][col] != ' ') { // next operation
				sum += intermediate;
				lastOperation = input[rows - 1][col];
				switch (lastOperation) {
					case '+':
						intermediate = 0L;
						break;
					case '*':
						intermediate = 1L;
						break;
					}
			}
			switch (lastOperation) {
				case '+':
					intermediate += number;
					break;
				case '*':
					intermediate *= number;
					break;
			}
		}
		Console.WriteLine(sum + intermediate);
	}
}
