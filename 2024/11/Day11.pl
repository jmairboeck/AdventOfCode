#!/usr/bin/env perl

use strict;
use warnings;

my @stones;

open(FILE, '<', 'input.txt');
my $line = <FILE>;
chomp($line);
@stones = split(/ /, $line);
close(FILE);

sub blink {
	my @newStones;
	for (@stones) {
		if ($_ eq '0') {
			push @newStones, '1';
			next;
		}
		my $length = length;
		if ($length % 2 == 0) {
			push @newStones, substr($_, 0, $length / 2);
			my $secondHalf = substr($_, $length / 2);
			$secondHalf =~ s/^0+(?=[0-9])//;
			push @newStones, $secondHalf;
		}
		else {
			push @newStones, $_ * 2024;
		}
	}
	@stones = @newStones;
}

for my $i (1..25) {
	blink();
}

print scalar @stones, "\n";

for my $i (26..75) {
	# print $i, "\n";
	blink();
}

print scalar @stones, "\n";
