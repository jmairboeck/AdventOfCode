using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day16 {
	static char[,] maze;
	static int minScore;

	static void Walk(int x, int y, int score, char dir) {
		if (score >= minScore) return;
		if (maze[y, x] == 'E') {
			minScore = score;
			return;
		}
		switch (dir) {
			case '^':
				if (maze[y - 1, x] != '#') Walk(x, y - 1, score + 1, '^');
				if (maze[y, x - 1] != '#') {
					maze[y, x] = '#';
					Walk(x - 1, y, score + 1001, '<');
					maze[y, x] = '.';
				}
				if (maze[y, x + 1] != '#') {
					maze[y, x] = '#';
					Walk(x + 1, y, score + 1001, '>');
					maze[y, x] = '.';
				}
				break;
			case '>':
				if (maze[y, x + 1] != '#') Walk(x + 1, y, score + 1, '>');
				if (maze[y - 1, x] != '#') {
					maze[y, x] = '#';
					Walk(x, y - 1, score + 1001, '^');
					maze[y, x] = '.';
				}
				if (maze[y + 1, x] != '#') {
					maze[y, x] = '#';
					Walk(x, y + 1, score + 1001, 'v');
					maze[y, x] = '.';
				}
				break;
			case 'v':
				if (maze[y + 1, x] != '#') Walk(x, y + 1, score + 1, 'v');
				if (maze[y, x + 1] != '#') {
					maze[y, x] = '#';
					Walk(x + 1, y, score + 1001, '>');
					maze[y, x] = '.';
				}
				if (maze[y, x - 1] != '#') {
					maze[y, x] = '#';
					Walk(x - 1, y, score + 1001, '<');
					maze[y, x] = '.';
				}
				break;
			case '<':
				if (maze[y, x - 1] != '#') Walk(x - 1, y, score + 1, '>');
				if (maze[y + 1, x] != '#') {
					maze[y, x] = '#';
					Walk(x, y + 1, score + 1001, 'v');
					maze[y, x] = '.';
				}
				if (maze[y - 1, x] != '#') {
					maze[y, x] = '#';
					Walk(x, y - 1, score + 1001, '^');
					maze[y, x] = '.';
				}
				break;
		}
	}

	static void Main() {
		var input = File.ReadAllLines("input.txt");
		int rows = input.Length, cols = input[0].Length, x = -1, y = -1;
		maze = new char[rows, cols];
		minScore = int.MaxValue;
		for (var i = 0; i < rows; ++i) {
			for (var j = 0; j < cols; ++j) {
				maze[i, j] = input[i][j];
				if (maze[i, j] == 'S') {
					x = j;
					y = i;
				}
			}
		}
		Walk(x, y, 0, '>');
		Console.WriteLine(minScore);
	}
}
