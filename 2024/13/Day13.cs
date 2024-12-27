using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Day13 {
	static double ax, ay, bx, by;

	static bool IsPositiveInt(double v) => v > 0 && Math.Abs(Math.Round(v) - v) < 0.001;

	static long Play(double px, double py) {
		// this is taken from AGG trans_affine::inverse_transform:
		// https://git.haiku-os.org/haiku/tree/headers/libs/agg/agg_trans_affine.h#n308
		// Anti-Grain Geometry - Version 2.4
		// Copyright (C) 2002-2005 Maxim Shemanarev (http://www.antigrain.com)
		//
		// Permission to copy, use, modify, sell and distribute this software 
		// is granted provided this copyright notice appears in all copies. 
		// This software is provided "as is" without express or implied
		// warranty, and with no claim as to its suitability for any purpose.
		double d = 1.0 / (ax * by - bx * ay);
		double a = px * d;
		double b = py * d;
		px = a * by - b * bx;
		py = b * ax - a * ay;
		// end of the excerpt from AGG

		if (IsPositiveInt(px) && IsPositiveInt(py)) return 3 * (long)Math.Round(px) + (long)Math.Round(py);
		return 0;
	}

	static void Main() {
		var pattern = new Regex(@"\d+");
		long sumOrig = 0, sumShifted = 0;
		using (var file = File.OpenText("input.txt")) {
			do {
				var numbers = pattern.Matches(file.ReadLine());
				ax = double.Parse(numbers[0].Value);
				ay = double.Parse(numbers[1].Value);
				numbers = pattern.Matches(file.ReadLine());
				bx = double.Parse(numbers[0].Value);
				by = double.Parse(numbers[1].Value);
				numbers = pattern.Matches(file.ReadLine());
				double px = double.Parse(numbers[0].Value), py = double.Parse(numbers[1].Value);
				double newpx = px + 10000000000000, newpy = py + 10000000000000;
				sumOrig += Play(px, py);
				sumShifted += Play(newpx, newpy);
			} while (file.ReadLine() != null);
		}
		Console.WriteLine(sumOrig);
		Console.WriteLine(sumShifted);
	}
}
