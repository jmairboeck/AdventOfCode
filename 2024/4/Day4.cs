using System;
using System.IO;

class Day4 {
	static void Main() {
		var text = File.ReadAllLines("input.txt");
		int count = 0, rows = text.Length, cols = text[0].Length;
		for (var i = 0; i < rows; ++i) {
			for (var j = 0; j < cols; ++j) {
				if (text[i][j] != 'X') continue;
				if (j < cols - 3 && // right
					text[i  ][j+1] == 'M' && text[i  ][j+2] == 'A' && text[i  ][j+3] == 'S') count++;
				if (j < cols - 3 && i < rows - 3 && // right down
					text[i+1][j+1] == 'M' && text[i+2][j+2] == 'A' && text[i+3][j+3] == 'S') count++;
				if (i < rows - 3 && // down
					text[i+1][j  ] == 'M' && text[i+2][j  ] == 'A' && text[i+3][j  ] == 'S') count++;
				if (j >= 3 && i < rows - 3 && // left down
					text[i+1][j-1] == 'M' && text[i+2][j-2] == 'A' && text[i+3][j-3] == 'S') count++;
				if (j >= 3 && // left
					text[i  ][j-1] == 'M' && text[i  ][j-2] == 'A' && text[i  ][j-3] == 'S') count++;
				if (j >= 3 && i >= 3 && // left up
					text[i-1][j-1] == 'M' && text[i-2][j-2] == 'A' && text[i-3][j-3] == 'S') count++;
				if (i >= 3 && // up
					text[i-1][j  ] == 'M' && text[i-2][j  ] == 'A' && text[i-3][j  ] == 'S') count++;
				if (j < cols - 3 && i >= 3 && // right up
					text[i-1][j+1] == 'M' && text[i-2][j+2] == 'A' && text[i-3][j+3] == 'S') count++;
			}
		}
		Console.WriteLine(count);
		count = 0;
		for (var i = 1; i < rows - 1; ++i) {
			for (var j = 1; j < cols - 1; ++j) {
				if (text[i][j] != 'A') continue;
				// M left, S right
				if (text[i-1][j-1] == 'M' && text[i+1][j-1] == 'M' && text[i+1][j+1] == 'S' && text[i-1][j+1] == 'S') count++;
				// M up, S down
				if (text[i-1][j-1] == 'M' && text[i-1][j+1] == 'M' && text[i+1][j+1] == 'S' && text[i+1][j-1] == 'S') count++;
				// M right, S left
				if (text[i-1][j+1] == 'M' && text[i+1][j+1] == 'M' && text[i+1][j-1] == 'S' && text[i-1][j-1] == 'S') count++;
				// M down, S up
				if (text[i+1][j+1] == 'M' && text[i+1][j-1] == 'M' && text[i-1][j+1] == 'S' && text[i-1][j-1] == 'S') count++;
			}
		}
		Console.WriteLine(count);
	}
}
