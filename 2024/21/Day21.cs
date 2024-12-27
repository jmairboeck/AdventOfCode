using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Day21 {
	static string[,][] matrixNumeric = new string[11, 11][] {
		// 0,                       1,                       2,                     3,                       4,                     5,                     6,                     7,                       8,                       9,                       A
		{ new[]{ "" },             new[]{ "^<" },           new[]{ "^" },          new[]{ "^>", ">^" },     new[]{ "^^<" },        new[]{ "^^" },         new[]{ "^^>", ">^^" }, new[]{ "^^^<" },         new[]{ "^^^" },          new[]{ "^^^>", ">^^^" }, new[]{ ">" }            }, // 0
		{ new[]{ ">v" },           new[]{ "" },             new[]{ ">" },          new[]{ ">>" },           new[]{ "^" },          new[]{ "^>", ">^" },   new[]{ "^>>", ">>^" }, new[]{ "^^" },           new[]{ "^^>", ">^^" },   new[]{ "^^>>", ">>^^" }, new[]{ ">>v" }          }, // 1
		{ new[]{ "v" },            new[]{ "<" },            new[]{ "" },           new[]{ ">" },            new[]{ "^<", "<^" },   new[]{ "^" },          new[]{ "^>", ">^" },   new[]{ "^^<", "<^^" },   new[]{ "^^" },           new[]{ "^^>", ">^^" },   new[]{ ">v", "v>" }     }, // 2
		{ new[]{ "<v", "v<" },     new[]{ "<<" },           new[]{ "<" },          new[]{ "" },             new[]{ "^<<", "<<^" }, new[]{ "^<", "<^" },   new[]{ "^" },          new[]{ "^^<<", "<<^^" }, new[]{ "^^<", "<^^" },   new[]{ "^^" },           new[]{ "v" }            }, // 3
		{ new[]{ ">vv" },          new[]{ "v" },            new[]{ ">v", "v>" },   new[]{ ">>v", "v>>" },   new[]{ "" },           new[]{ ">" },          new[]{ ">>" },         new[]{ "^" },            new[]{ "^>", ">^" },     new[]{ "^>>", ">>^" },   new[]{ ">>vv" }         }, // 4
		{ new[]{ "vv" },           new[]{ "<v", "v<" },     new[]{ "v" },          new[]{ ">v", "v>" },     new[]{ "<" },          new[]{ "" },           new[]{ ">" },          new[]{ "^<", "<^" },     new[]{ "^" },            new[]{ "^>", ">^" },     new[]{ ">vv", "vv>" }   }, // 5
		{ new[]{ "<vv", "vv<" },   new[]{ "<<v", "v<<" },   new[]{ "<v", "v<" },   new[]{ "v" },            new[]{ "<<" },         new[]{ "<" },          new[]{ "" },           new[]{ "^<<", "<<^" },   new[]{ "^<", "<^" },     new[]{ "^" },            new[]{ "vv" }           }, // 6
		{ new[]{ ">vvv" },         new[]{ "vv" },           new[]{ ">vv", "vv>" }, new[]{ ">>vv", "vv>>" }, new[]{ "v" },          new[]{ ">v", "v>" },   new[]{ ">>v", "v>>" }, new[]{ "" },             new[]{ ">" },            new[]{ ">>" },           new[]{ ">>vvv" }        }, // 7
		{ new[]{ "vvv" },          new[]{ "<vv", "vv<" },   new[]{ "vv" },         new[]{ ">vv", "vv>" },   new[]{ "<v", "v<" },   new[]{ "v" },          new[]{ ">v", "v>" },   new[]{ "<" },            new[]{ "" },             new[]{ ">" },            new[]{ ">vvv", "vvv>" },  }, // 8
		{ new[]{ "<vvv", "vvv<" }, new[]{ "<<vv", "vv<<" }, new[]{ "<vv", "vv<" }, new[]{ "vv" },           new[]{ "<<v", "v<<" }, new[]{ "<v", "v<" },   new[]{ "v" },          new[]{ "<<" },           new[]{ "<" },            new[]{ "" },             new[]{ "vvv" }          }, // 9
		{ new[]{ "<" },            new[]{ "^<<" },          new[]{ "^<", "<^" },   new[]{ "^" },            new[]{ "^^<<" },       new[]{ "^^<", "<^^" }, new[]{ "^^" },         new[]{ "^^^<<" },        new[]{ "^^^<", "<^^^" }, new[]{ "^^^" },          new[]{ "" }             }  // A
	}, matrixDirection = new string[5, 5][] {
		// ^,                   >,                   v,                   <,              A
		{ new[]{ "" },         new[]{ "v>", ">v" }, new[]{ "v" },        new[]{ "v<" },  new[]{ ">" }        }, // ^
		{ new[]{ "<^", "^<" }, new[]{ "" },         new[]{ "<" },        new[]{ "<<" },  new[]{ "^" }        }, // >
		{ new[]{ "^" },        new[]{ ">" },        new[]{ "" },         new[]{ "<" },   new[]{ ">^", "^>" } }, // v
		{ new[]{ ">^" },       new[]{ ">>" },       new[]{ ">" },        new[]{ "" },    new[]{ ">>^" }      }, // <
		{ new[]{ "<" },        new[]{ "v" },        new[]{ "v<", "<v" }, new[]{ "v<<" }, new[]{ "" }         }  // A
	};
	static int[,] matrixDirectionLength = new int[5, 5] {
		// ^, >, v, <, A
		{  0, 2, 1, 2, 1 }, // ^
		{  2, 0, 1, 2, 1 }, // >
		{  1, 1, 0, 1, 2 }, // v
		{  2, 2, 1, 0, 3 }, // <
		{  1, 1, 2, 3, 0 }  // A
	};

	static IEnumerable<string> InputNumeric(string input) {
		var current = 10; // A
		var results = new List<StringBuilder>() {
			new StringBuilder()
		};
		foreach (var c in input) {
			int index = "0123456789A".IndexOf(c), numResults = results.Count;
			for (var i = 1; i < matrixNumeric[current, index].Length; ++i) {
				for (var j = 0; j < numResults; ++j) {
					var newResult = new StringBuilder(results[j].ToString());
					newResult.Append(matrixNumeric[current, index][i]).Append('A');
					results.Add(newResult);
				}
			}
			for (var i = 0; i < numResults; ++i) {
				results[i].Append(matrixNumeric[current, index][0]).Append('A');
			}
			current = index;
		}
		return results.Select(r => r.ToString());
	}

	static IEnumerable<string> InputDirection(string input) {
		var current = 4; // A
		var results = new List<StringBuilder>() {
			new StringBuilder()
		};
		foreach (var c in input) {
			int index = "^>v<A".IndexOf(c), numResults = results.Count;
			for (var i = 1; i < matrixDirection[current, index].Length; ++i) {
				for (var j = 0; j < numResults; ++j) {
					var newResult = new StringBuilder(results[j].ToString());
					newResult.Append(matrixDirection[current, index][i]).Append('A');
					results.Add(newResult);
				}
			}
			for (var i = 0; i < numResults; ++i) {
				results[i].Append(matrixDirection[current, index][0]).Append('A');
			}
			current = index;
		}
		return results.Select(r => r.ToString());
	}

	static int InputDirectionLength(string input) {
		int sum = 0, current = 4; // A
		foreach (var c in input) {
			var index = "^>v<A".IndexOf(c);
			sum += matrixDirectionLength[current, index] + 1;
			current = index;
		}
		return sum;
	}

	static void Main() {
		int sum2 = 0, sum25 = 0;
		using (var file = File.OpenText("input.txt")) {
			string line;
			while ((line = file.ReadLine()) != null) {
				var number = int.Parse(line);
				var instructions = InputNumeric(line + "A");
				for (var i = 0; i < 24; ++i) {
					var list = instructions.SelectMany(InputDirection).ToList();
					var minLength = list.Min(c => c.Length);
					instructions = list.Where(c => c.Length == minLength);
					if (i == 1) {
						sum2 += minLength * number;
					}
				}
				var mine = instructions.Min(InputDirectionLength);
				sum25 += mine * number;
			}
		}
		Console.WriteLine(sum2);
		Console.WriteLine(sum25);
	}
}
