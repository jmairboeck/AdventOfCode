using System;
using System.Collections.Generic;
using System.IO;

class Day15 {
	static char[,] largeMap;

	// MoveUp and MoveDown may make tentative moves that need to be reverted later

	static bool MoveUp(int x, int y) {
		if (largeMap[y - 1, x] == '#' || largeMap[y - 1, x + 1] == '#') return false;
		if (largeMap[y - 1, x] == '[') {
			if (!MoveUp(x, y - 1)) return false;
		}
		if (largeMap[y - 1, x] == ']') {
			if (!MoveUp(x - 1, y - 1)) return false;
		}
		if (largeMap[y - 1, x + 1] == '[') {
			if (!MoveUp(x + 1, y - 1)) return false;
		}
		largeMap[y - 1, x] = '[';
		largeMap[y - 1, x + 1] = ']';
		largeMap[y, x] = largeMap[y, x + 1] = '.';
		return true;
	}

	static bool MoveRight(int x, int y) {
		if (largeMap[y, x + 2] == '#') return false;
		if (largeMap[y, x + 2] == '[') {
			if (!MoveRight(x + 2, y)) return false;
		}
		largeMap[y, x + 1] = '[';
		largeMap[y, x + 2] = ']';
		largeMap[y, x] = '.';
		return true;
	}

	static bool MoveDown(int x, int y) {
		if (largeMap[y + 1, x] == '#' || largeMap[y + 1, x + 1] == '#') return false;
		if (largeMap[y + 1, x] == '[') {
			if (!MoveDown(x, y + 1)) return false;
		}
		if (largeMap[y + 1, x] == ']') {
			if (!MoveDown(x - 1, y + 1)) return false;
		}
		if (largeMap[y + 1, x + 1] == '[') {
			if (!MoveDown(x + 1, y + 1)) return false;
		}
		largeMap[y + 1, x] = '[';
		largeMap[y + 1, x + 1] = ']';
		largeMap[y, x] = largeMap[y, x + 1] = '.';
		return true;
	}

	static bool MoveLeft(int x, int y) {
		if (largeMap[y, x - 1] == '#') return false;
		if (largeMap[y, x - 1] == ']') {
			if (!MoveLeft(x - 2, y)) return false;
		}
		largeMap[y, x - 1] = '[';
		largeMap[y, x] = ']';
		largeMap[y, x + 1] = '.';
		return true;
	}

	static void Main() {
		var input = new List<string>();
		char[,] smallMap;
		int xs = -1, ys = -1, xl = -1, yl = -1;
		using (var file = File.OpenText("input.txt")) {
			string line;
			while ((line = file.ReadLine()) != string.Empty) {
				input.Add(line);
			}
			smallMap = new char[input.Count, input[0].Length];
			largeMap = new char[input.Count, input[0].Length * 2];
			for (var i = 0; i < input.Count; ++i) {
				for (var j = 0; j < input[i].Length; ++j) {
					smallMap[i, j] = input[i][j];
					largeMap[i, j * 2] = input[i][j] == 'O' ? '[' : input[i][j];
					largeMap[i, j * 2 + 1] = input[i][j] == 'O' ? ']' : input[i][j];
					if (input[i][j] == '@') {
						xs = j;
						xl = j * 2;
						ys = yl = i;
						smallMap[i, j] = largeMap[i, j * 2] = largeMap[i, j * 2 + 1] = '.';
					}
				}
			}
			while ((line = file.ReadLine()) != null) {
				foreach (var c in line) {
					int i;
					var backup = (char[,])largeMap.Clone(); // make a backup to make back-tracking easier
					switch (c) {
						case '^':
							for (i = ys - 1; smallMap[i, xs] == 'O'; --i);
							if (smallMap[i, xs] == '.') {
								ys--;
								if (i < ys) {
									smallMap[i, xs] = 'O';
									smallMap[ys, xs] = '.';
								}
							}
							if (largeMap[yl - 1, xl] == '[') MoveUp(xl, yl - 1);
							if (largeMap[yl - 1, xl] == ']') MoveUp(xl - 1, yl - 1);
							if (largeMap[yl - 1, xl] == '.') yl--;
							else largeMap = backup;
							break;
						case '>':
							for (i = xs + 1; smallMap[ys, i] == 'O'; ++i);
							if (smallMap[ys, i] == '.') {
								xs++;
								if (i > xs) {
									smallMap[ys, i] = 'O';
									smallMap[ys, xs] = '.';
								}
							}
							if (largeMap[yl, xl + 1] == '[') MoveRight(xl + 1, yl);
							if (largeMap[yl, xl + 1] == '.') xl++;
							break;
						case 'v':
							for (i = ys + 1; smallMap[i, xs] == 'O'; ++i);
							if (smallMap[i, xs] == '.') {
								ys++;
								if (i > ys) {
									smallMap[i, xs] = 'O';
									smallMap[ys, xs] = '.';
								}
							}
							if (largeMap[yl + 1, xl] == '[') MoveDown(xl, yl + 1);
							if (largeMap[yl + 1, xl] == ']') MoveDown(xl - 1, yl + 1);
							if (largeMap[yl + 1, xl] == '.') yl++;
							else largeMap = backup;
							break;
						case '<':
							for (i = xs - 1; smallMap[ys, i] == 'O'; --i);
							if (smallMap[ys, i] == '.') {
								xs--;
								if (i < xs) {
									smallMap[ys, i] = 'O';
									smallMap[ys, xs] = '.';
								}
							}
							if (largeMap[yl, xl - 1] == ']') MoveLeft(xl - 2, yl);
							if (largeMap[yl, xl - 1] == '.') xl--;
							break;
					}
				}
			}
		}
		int smallSum = 0, largeSum = 0;
		for (var i = 0; i < input.Count; ++i) {
			for (var j = 0; j < input[i].Length; ++j) {
				if (smallMap[i, j] == 'O') smallSum += 100 * i + j;
			}
			for (var j = 0; j < input[i].Length * 2; ++j) {
				if (largeMap[i, j] == '[') largeSum += 100 * i + j;
			}
		}
		Console.WriteLine(smallSum);
		Console.WriteLine(largeSum);
	}
}
