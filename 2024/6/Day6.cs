using System;
using System.IO;

class Day6 {
	static string[] input;
	static char[,] field;
	static int rows, cols, x = -1, y = -1;

	static void Reset() {
		for (int i = 0; i < rows; ++i) {
			for (int j = 0; j < cols; ++j) {
				field[i, j] = input[i][j];
				if (field[i, j] == '^') { // find the starting position (optimized for the concrete input)
					x = j;
					y = i;
					field[i, j] = '.'; // reset the starting position so that cycle detection works
				}
			}
		}
	}

	static int Run() {
		var countSteps = 1;
		var dir = input[y][x]; // take the starting direction from the input because we reset it above
		while (true) {
			switch (dir) {
				case '^':
					if (y == 0) return countSteps; // going off the board at the top
					if (field[y - 1, x] == '#') dir = '>';
					else y--;
					break;
				case '>':
					if (x == cols - 1) return countSteps; // going off at the right
					if (field[y, x + 1] == '#') dir = 'v';
					else x++;
					break;
				case 'v':
					if (y == rows - 1) return countSteps; // bottom
					if (field[y + 1, x] == '#') dir = '<';
					else y++;
					break;
				case '<':
					if (x == 0) return countSteps; // left
					if (field[y, x - 1] == '#') dir = '^';
					else x--;
					break;
			}
			if (field[y, x] == dir) return 0; // we already were here going in the same direction -> cycle detected
			if (field[y, x] == '.') { // we were not yet here -> mark and count it
				field[y, x] = dir;
				countSteps++;
			}
		}
	}

	static void Main() {
		input = File.ReadAllLines("input.txt");
		rows = input.Length;
		cols = input[0].Length;
		field = new char[rows, cols];
		Reset();
		Console.WriteLine(Run());
		var reference = (char[,])field.Clone(); // record the walking pattern of the first run
		var countPositions = 0;
		for (var i = 0; i < rows; ++i) {
			for (var j = 0; j < cols; ++j) {
				if ("^>v<".Contains(reference[i, j])) { // a new obstacle can only make a difference on the original path
					Reset();
					field[i, j] = '#'; // place an extra obstacle
					if (Run() == 0) countPositions++; // and check for a cycle
				}
			}
		}
		Console.WriteLine(countPositions);
	}
}
