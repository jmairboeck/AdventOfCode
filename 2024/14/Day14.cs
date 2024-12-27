using System;
//using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Day14 {
	static void Main() {
		int count1 = 0, count2 = 0, count3 = 0, count4 = 0;
		//var grid = new int[103,101];
		using (var file = File.OpenText("input.txt")) {
			string line;
			while ((line = file.ReadLine()) != null) {
				var matches = Regex.Matches(line, @"-?\d+");
				int x = int.Parse(matches[0].Value), y = int.Parse(matches[1].Value), vx = int.Parse(matches[2].Value), vy = int.Parse(matches[3].Value);
				//grid[y, x] = true;
				x += 100 * vx;
				y += 100 * vy;
				x %= 101;
				y %= 103;
				if (x < 0) x += 101;
				if (y < 0) y += 103;
				//grid[y, x]++;
				if (x < 50 && y < 51) count1++;
				if (x > 50 && y < 51) count2++;
				if (x < 50 && y > 51) count3++;
				if (x > 50 && y > 51) count4++;
			}
		}
		Console.WriteLine(count1 * count2 * count3 * count4);
		/*for (var i = 0; i < 103; ++i) {
			for (var j = 0; j < 101; ++j) {
				Console.Write("{0}", grid[i, j]);
			}
			Console.WriteLine();
		}*/
	}
}
