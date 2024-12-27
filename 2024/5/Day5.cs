using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

struct Rule {
	public string before, after;
}

class Day5 {
	static void Main() {
		var rules = new List<Rule>();
		int sumValid = 0, sumInvalid = 0;
		using (var file = File.OpenText("input.txt")) {
			string line;
			while ((line = file.ReadLine()) != string.Empty) {
				var numbers = line.Split('|');
				rules.Add(new Rule { before = numbers[0], after = numbers[1] });
			}
			while ((line = file.ReadLine()) != null) {
				var numbersOrig = line.Split(',');
				var numbers = numbersOrig.ToList();
				numbers.Sort((a, b) => {
					foreach (var rule in rules) {
						if (a == rule.before && b == rule.after) return -1;
						if (a == rule.after && b == rule.before) return 1;
					}
					return 0;
				});
				var valid = numbers.SequenceEqual(numbersOrig);
				var middleNumber = int.Parse(numbers[numbers.Count / 2]);
				if (valid) sumValid += middleNumber;
				else sumInvalid += middleNumber; 
			}
		}
		Console.WriteLine(sumValid);
		Console.WriteLine(sumInvalid);
	}
}
