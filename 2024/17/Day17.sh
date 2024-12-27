#!/usr/bin/env bash

# build the interpreter if it doesn't exist
[[ -f Day17.exe ]] || mcs Day17.cs

# input
eval "$(sed -Ee 's/Register (.): /\1=/g' -e 's/Program: /PROGRAM=/g' input.txt)"

# part 1
mono Day17.exe "$A" "$B" "$C" "$PROGRAM"

# part 2
i=0
while true; do
	output=$(mono Day17.exe "$i" "$B" "$C" "$PROGRAM")
	# echo try $i: $output
	if [[ "$output" = "$PROGRAM" ]]; then
		echo $i
		exit 0
	fi 
	(( i++ ))
done

