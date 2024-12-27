#!/usr/bin/env perl

use warnings;
use strict;

my $sumall = 0;
my $sumenabled = 0;
my $enable = 1;

open(FILE, '<', 'input.txt');
while(<FILE>) {
	while(/(?<mul>mul\((?<arg1>\d+),(?<arg2>\d+)\))|(?<do>do\(\))|(?<dont>don't\(\))/g) {
		if ($+{mul}) {
			$sumall += $+{arg1} * $+{arg2};
			$sumenabled += $+{arg1} * $+{arg2} if $enable;
		}
		$enable = 1 if $+{do};
		$enable = 0 if $+{dont};
	}
}
close(FILE);

print $sumall, "\n";
print $sumenabled, "\n";
