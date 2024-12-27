using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Operation {
	public string i1, op, i2, o;

	public bool Eval(bool v1, bool v2) {
		switch (op) {
			case "AND": return v1 && v2;
			case "OR":  return v1 || v2;
			case "XOR": return v1 != v2;
		}
		return false;
	}
}

class Day24 {
	static void Main() {
		var values = new Dictionary<string, bool>();
		var operations = new List<Operation>();
		using (var file = File.OpenText("input.txt")) {
			string line;
			while ((line = file.ReadLine()) != string.Empty) {
				var parts = line.Split(": ");
				values[parts[0]] = parts[1] == "1";
			}
			while ((line = file.ReadLine()) != null) {
				var parts = line.Split(" ");
				operations.Add(new Operation { i1 = parts[0], op = parts[1], i2 = parts[2], o = parts[4] });
			}
		}
		var numberRegex = new Regex(@"\d{2}");
		var maxZ = "z" + operations.Max(o => {
			var match = numberRegex.Match(o.o);
			return match.Success ? int.Parse(match.Value) : -1;
		});
		while (!values.ContainsKey(maxZ)) {
			foreach (var op in operations) {
				if (!values.ContainsKey(op.o) && values.TryGetValue(op.i1, out var v1) && values.TryGetValue(op.i2, out var v2)) {
					values[op.o] = op.Eval(v1, v2);
				}
			}
		}
		var result = 0L;
		for (var i = 0; values.TryGetValue($"z{i:D2}", out var bit); ++i) {
			if (bit) result |= 1L << i;
		}
		Console.WriteLine(result);

		var bogus = new SortedSet<string>();
		HashSet<string> xorInputs = new HashSet<string>(), andInputs = new HashSet<string>(), andOutputs = new HashSet<string>(), orInputs = new HashSet<string>(); // checking sets for full-adder validation rules
		foreach (var op in operations) {
			if (op.i1 == "x00" || op.i1 == "y00") continue; // special case the first bit which is a half-adder
			switch (op.op) {
				// full-adder validation rules
				case "XOR":
					if (op.i1.StartsWith("x") || op.i1.StartsWith("y")) { // XOR outputs that are used for input bits occur as inputs in a XOR and an AND
						xorInputs.Add(op.o);
						andInputs.Add(op.o);
					}
					else if (!op.o.StartsWith("z")) { // otherwise it must be used to output a bit
						bogus.Add(op.o);
					}
					break;
				case "AND":
					orInputs.Add(op.o); // all AND outputs must occur as OR input
					break;
				case "OR":
					andOutputs.Add(op.i1); // all OR inputs must be AND outputs
					andOutputs.Add(op.i2);
					if (op.o != maxZ && op.o.StartsWith("z")) { // OR outputs are a carry and all but the final one are intermediate
						bogus.Add(op.o);
					}
					break;
			}
		}
		xorInputs.ExceptWith(operations.Where(o => o.op == "XOR").Select(o => o.i1));
		xorInputs.ExceptWith(operations.Where(o => o.op == "XOR").Select(o => o.i2));
		foreach (var xorInput in xorInputs) bogus.Add(xorInput);
		andInputs.ExceptWith(operations.Where(o => o.op == "AND").Select(o => o.i1));
		andInputs.ExceptWith(operations.Where(o => o.op == "AND").Select(o => o.i2));
		foreach (var andInput in andInputs) bogus.Add(andInput);
		orInputs.ExceptWith(operations.Where(o => o.op == "OR").Select(o => o.i1));
		orInputs.ExceptWith(operations.Where(o => o.op == "OR").Select(o => o.i2));
		foreach (var orInput in orInputs) bogus.Add(orInput);
		andOutputs.ExceptWith(operations.Where(o => o.op == "AND").Select(o => o.o));
		foreach (var andOutput in andOutputs) bogus.Add(andOutput);
		Console.WriteLine(string.Join(',', bogus));
	}
}
