using System;
using System.IO;
using System.Linq;

class Day4 {
	static void Main() {
		var grid = File.ReadAllLines("input.txt").Select(line => line.ToCharArray()).ToArray();
		int count = 0, rows = grid.Length - 1, cols = grid[0].Length - 1;
		bool removedAny = true, first = true;
		while (removedAny) {
			removedAny = false;
			for (var row = 0; row <= rows; ++row)
				for (var col = 0; col <= cols; ++col) {
					if (grid[row][col] == '.') continue;
					var neighbours = 0;
					if (row > 0    && col > 0    && grid[row - 1][col - 1] != '.') neighbours++;
					if (row > 0                  && grid[row - 1][col    ] != '.') neighbours++;
					if (row > 0    && col < cols && grid[row - 1][col + 1] != '.') neighbours++;
					if (              col > 0    && grid[row    ][col - 1] != '.') neighbours++;
					if (              col < cols && grid[row    ][col + 1] != '.') neighbours++;
					if (row < rows && col > 0    && grid[row + 1][col - 1] != '.') neighbours++;
					if (row < rows               && grid[row + 1][col    ] != '.') neighbours++;
					if (row < rows && col < cols && grid[row + 1][col + 1] != '.') neighbours++;
					if (neighbours < 4) {
						count++;
						grid[row][col] = ' ';
						removedAny = true;
					}
				}
			if (first) {
				first = false;
				Console.WriteLine(count); // part 1 result
			}
			if (!removedAny) break; // optimization
			// reset for next iteration:
			for (var row = 0; row <= rows; ++row)
				for (var col = 0; col <= cols; ++col)
					if (grid[row][col] == ' ') grid[row][col] = '.';
		}
		Console.WriteLine(count); // part 2 result
	}
}
