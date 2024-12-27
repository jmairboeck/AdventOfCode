using System;
using System.IO;

class Day18 {
	static int[,] memory;
	static int shortestPath;

	static void Walk(int x, int y, int steps) {
		if (steps >= shortestPath - (70 - x) - (70 - y)) return;
		if (x == 70 && y == 70) {
			shortestPath = steps;
			return;
		}
		memory[y, x] = steps;
		//Console.WriteLine("entering {0},{1} - {2} steps", x, y, steps);
		if (y >  0 && memory[y - 1, x] > steps) Walk(x, y - 1, steps + 1);
		if (x < 70 && memory[y, x + 1] > steps) Walk(x + 1, y, steps + 1);
		if (y < 70 && memory[y + 1, x] > steps) Walk(x, y + 1, steps + 1);
		if (x >  0 && memory[y, x - 1] > steps) Walk(x - 1, y, steps + 1);
		//Console.WriteLine("leaving {0},{1} - {2} steps", x, y, steps);
	}

	static void Main() {
		memory = new int[71, 71];
		shortestPath = int.MaxValue;
		for (var i = 0; i < 71; ++i) {
			for (var j = 0; j < 71; ++j) {
				memory[i, j] = int.MaxValue;
			}
		}
		using (var file = File.OpenText("input.txt")) {
			for (var i = 0; i < 1024; ++i) {
				var numbers = file.ReadLine().Split(',');
				memory[int.Parse(numbers[1]), int.Parse(numbers[0])] = -1;
			}
		}
		/*for (var i = 0; i < 71; ++i) {
			for (var j = 0; j < 71; ++j) {
				Console.Write(memory[i, j] > 0 ? '#' : '.');
			}
			Console.WriteLine();
		}
		Console.WriteLine();*/
		Walk(0, 0, 0);
		/*for (var i = 0; i < 71; ++i) {
			for (var j = 0; j < 71; ++j) {
				Console.Write(memory[i, j] < 0 ? '#' : memory[i, j] != int.MaxValue ? 'O' : '.');
			}
			Console.WriteLine();
		}*/
		Console.WriteLine(shortestPath);
	}
}
