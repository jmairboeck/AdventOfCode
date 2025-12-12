using System;
using System.IO;

class Day1 {
	static void Main() {
		int position = 50, count1 = 0, count2 = 0;
		using (var stream = File.OpenText("input.txt")) {
			string line;
			while ((line = stream.ReadLine()) != null) {
				var number = int.Parse(line.Substring(1));
				count2 += number / 100;
				number %= 100;
				switch (line[0]) {
					case 'L':
						if (position < number && position > 0) count2++;
						position -= number;
						if (position < 0) position += 100;
						break;
					case 'R':
						position += number;
						if (position > 100) count2++;
						if (position >= 100) position -= 100;
						break;
				}
				if (position == 0) {
					count1++;
					if (number != 0) count2++;
				}
			}
		}
		Console.WriteLine(count1);
		Console.WriteLine(count2);
	}
}
