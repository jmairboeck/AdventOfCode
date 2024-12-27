using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

class Day8 {
	static string[] input;
	static int rows, cols;
	static HashSet<Point> positionsSingle, positionsAll;

	static void Process(char c) {
		for (var i = 0; i < rows; ++i) {
			for (var j = 0; j < cols; ++j) {
				if (input[i][j] == c) {
					for (var k = 0; k < rows; ++k) {
						for (var l = 0; l < cols; ++l) {
							if (input[k][l] == c && (i != k || j != l)) {
								for (int x = l, y = k; x >= 0 && x < cols && y >= 0 && y < rows; x += l - j, y += k - i) {
									var p = new Point(x, y);
									if (x == l + l - j && y == k + k - i)
										positionsSingle.Add(p);
									positionsAll.Add(p);
								}
							}
						}
					}
				}
			}
		}
	}

	static void Main() {
		input = File.ReadAllLines("input.txt");
		rows = input.Length;
		cols = input[0].Length;
		positionsSingle = new HashSet<Point>();
		positionsAll = new HashSet<Point>();
		for (var c = '0'; c <= '9'; ++c)
			Process(c);
		for (var c = 'A'; c <= 'Z'; ++c)
			Process(c);
		for (var c = 'a'; c <= 'z'; ++c)
			Process(c);
		Console.WriteLine(positionsSingle.Count);
		Console.WriteLine(positionsAll.Count);
	}
}
