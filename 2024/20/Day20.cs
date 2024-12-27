using System;
using System.IO;

class Day20 {
	static void Main() {
		var input = File.ReadAllLines("input.txt");
		int rows = input.Length, cols = input[0].Length, x = -1, y = -1;
		var track = new int[rows, cols];
		for (var i = 0; i < rows; ++i) {
			for (var j = 0; j < cols; ++j) {
				if (input[i][j] == 'S') {
					x = j;
					y = i;
					track[i, j] = 0;
				} else {
					track[i, j] = -1;
				}
			}
		}
		for (var i = 1; input[y][x] != 'E'; ++i) {
			     if (".E".Contains(input[y - 1][x]) && track[y - 1, x] < 0) y--;
			else if (".E".Contains(input[y][x + 1]) && track[y, x + 1] < 0) x++;
			else if (".E".Contains(input[y + 1][x]) && track[y + 1, x] < 0) y++;
			else if (".E".Contains(input[y][x - 1]) && track[y, x - 1] < 0) x--;
			track[y, x] = i;
		}
		int count2Secs = 0, count20Secs = 0;
		for (var i = 0; i < rows; ++i) {
			for (var j = 0; j < cols; ++j) {
				if (track[i, j] < 0) continue;
				for (var k = 0; k <= 20; k++) {
					for (var l = 0; k + l <= 20; l++) {
						if (i >= k       && j < cols - l && l > 0 && track[i - k, j + l] - track[i, j] >= 100 + k + l) {
							if (k + l == 2) count2Secs++;
							count20Secs++;
						}
						if (i < rows - k && j < cols - l && k > 0 && track[i + k, j + l] - track[i, j] >= 100 + k + l) {
							if (k + l == 2) count2Secs++;
							count20Secs++;
						}
						if (i < rows - k && j >= l       && l > 0 && track[i + k, j - l] - track[i, j] >= 100 + k + l) {
							if (k + l == 2) count2Secs++;
							count20Secs++;
						}
						if (i >= k       && j >= l       && k > 0 && track[i - k, j - l] - track[i, j] >= 100 + k + l) {
							if (k + l == 2) count2Secs++;
							count20Secs++;
						}
					}
				}
			}
		}
		Console.WriteLine(count2Secs);
		Console.WriteLine(count20Secs);
	}
}
