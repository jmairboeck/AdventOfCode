using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day2 {
	static IList<IList<int>> again = new List<IList<int>>();

	static bool Check(IList<int> numbers) {
		bool wasPositive = false, wasNegative = false;
		for (var i = 1; i < numbers.Count; ++i) {
			var diff = numbers[i] - numbers[i - 1];
			if (diff == 0 || diff < -3 || diff > 3) {
				again.Add(numbers);
				return false;
			}
			if (diff < 0) {
				if (wasPositive) {
					again.Add(numbers);
					return false;
				}
				wasNegative = true;
			}
			if (diff > 0) {
				if (wasNegative) {
					again.Add(numbers);
					return false;
				}
				wasPositive = true;
			}
		}
		return true;
	}

	static void Main() {
		int count = 0;
		using (var input = File.OpenText("input.txt")) {
			string line;
			while ((line = input.ReadLine()) != null) {
				var numberStrings = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				var numbers = numberStrings.Select(int.Parse).ToList();
				if (Check(numbers)) {
					count++;
				}
			}
		}
		Console.WriteLine(count);
		foreach (var numbers in again.ToList()) {
			for (int i = 0; i < numbers.Count; ++i) {
				var newNumbers = numbers.ToList();
				newNumbers.RemoveAt(i);
				if (Check(newNumbers)) {
					count++;
					break;
				}
			}
		}
		Console.WriteLine(count);
	}
}
