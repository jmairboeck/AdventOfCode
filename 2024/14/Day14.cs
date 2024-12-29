using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Day14 {
	static void Main() {
		var robots = new List<Rectangle>();
		var numberRegex = new Regex(@"-?\d+");
		using (var file = File.OpenText("input.txt")) {
			string line;
			while ((line = file.ReadLine()) != null) {
				var matches = numberRegex.Matches(line);
				robots.Add(new Rectangle(int.Parse(matches[0].Value), int.Parse(matches[1].Value), int.Parse(matches[2].Value), int.Parse(matches[3].Value)));
			}
		}
		int robotCount = robots.Count, result1 = 0, result2 = 0;
		for (var i = 0; result1 == 0 || result2 == 0; ++i) {
			int count1 = 0, count2 = 0, count3 = 0, count4 = 0;
			var sampleXs = new int[101]; // heuristic to determine the tree formation: how many robots have the same X coordinate?
			for (var j = 0; j < robotCount; ++j) {
				var robot = robots[j];
				robot.X += robot.Width;
				robot.Y += robot.Height;
				robot.X %= 101;
				robot.Y %= 103;
				if (robot.X < 0) robot.X += 101;
				if (robot.Y < 0) robot.Y += 103;
				if (robot.X < 50 && robot.Y < 51) count1++;
				if (robot.X > 50 && robot.Y < 51) count2++;
				if (robot.X < 50 && robot.Y > 51) count3++;
				if (robot.X > 50 && robot.Y > 51) count4++;
				sampleXs[robot.X]++;
				robots[j] = robot;
			}
			if (i == 99) result1 = count1 * count2 * count3 * count4;
			if (robotCount == robots.Select(r => r.Location).Distinct().Count() && sampleXs.Max() > 20) result2 = i + 1;
		}
		Console.WriteLine(result1);
		Console.WriteLine(result2);
	}
}
