using System;
using System.Collections.Generic;
using System.IO;

class Day9 {
	static void CalculateAndPrintChecksum(List<int> disk) {
		ulong sum = 0;
		for (var i = 1; i < disk.Count; ++i) {
			if (disk[i] > 0) {
				sum += (ulong)(i * disk[i]);
			}
		}
		Console.WriteLine(sum);
	}

	static void Main() {
		var disk = new List<int>();
		using (var file = File.OpenText("input.txt")) {
			for (var i = 0; true; ++i) {
				int c = file.Read();
				if (c < 0) break;
				c -= '0';
				for (var j = 0; j < c; ++j)
					disk.Add(i);
				c = file.Read();
				if (c < 0) break;
				c -= '0';
				for (var j = 0; j < c; ++j)
					disk.Add(-1);
			}
		}
		var disk2 = new List<int>(disk);

		int begin = 0, end = disk.Count - 1;
		while (begin < end) {
			if (disk[begin] == -1 && disk[end] != -1) {
				disk[begin] = disk[end];
				disk[end] = -1;
			}
			if (disk[end] == -1) end--;
			if (disk[begin] != -1) begin++;
		}
		CalculateAndPrintChecksum(disk);

		end = disk.Count - 1;
		while (end >= 0) {
			if (disk2[end] == -1) end--;
			else {
				int count = 0, value = disk2[end];
				while (end >= 0 && disk2[end] == value) {
					end--;
					count++;
				}
				int freecount = 0;
				for (begin = 0; begin <= end; ++begin) {
					if (disk2[begin] == -1) freecount++;
					else freecount = 0;
					if (freecount == count) {
						while (count > 0) {
							disk2[begin] = value;
							disk2[end + count] = -1;
							count--;
							begin--;
						}
						break;
					}
				}
			}
		}
		CalculateAndPrintChecksum(disk2);
	}
}
