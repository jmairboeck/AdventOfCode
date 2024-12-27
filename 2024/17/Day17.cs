using System;
using System.Collections.Generic;
using System.Linq;

class Day17 {
	static int a, b, c, ip;
	static List<int> program;

	static int ParseCombo() {
		var v = program[ip + 1];
		if (v <= 3) return v;
		switch(v) {
			case 4: return a;
			case 5: return b;
			case 6: return c;
		}
		Environment.Exit(1);
		return -1;
	}

	static void Main(string[] args) {
		a = int.Parse(args[0]);
		b = int.Parse(args[1]);
		c = int.Parse(args[2]);
		program = args[3].Split(',').Select(int.Parse).ToList();
		ip = 0;
		var written = false;
		while (ip < program.Count) {
			switch(program[ip]) {
				case 0:
					a = a / (1 << ParseCombo());
					break;
				case 1:
					b = b ^ program[ip + 1];
					break;
				case 2:
					b = ParseCombo() % 8;
					break;
				case 3:
					if (a != 0) {
						ip = program[ip + 1];
						continue;
					}
					break;
				case 4:
					b = b ^ c;
					break;
				case 5:
					if (written) Console.Write(',');
					Console.Write(ParseCombo() % 8);
					written = true;
					break;
				case 6:
					b = a / (1 << ParseCombo());
					break;
				case 7:
					c = a / (1 << ParseCombo());
					break;
			}
			ip += 2;
		}
		Console.WriteLine();
	}
}
