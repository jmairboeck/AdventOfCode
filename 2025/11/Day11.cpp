#include <deque>
#include <fstream>
#include <iostream>
#include <map>
#include <string>
#include <vector>

std::map<int, std::vector<int>> graph;

int readWord(std::istream& stream) {
	char buffer[3];
	stream.read(buffer, 3);
	return (buffer[0] << 16) + (buffer[1] << 8) + buffer[2];
}

int traverse(int start, int end) {
	auto count = 0;
	std::deque<int> queue;
	queue.push_back(start);
	while (!queue.empty()) {
		auto key = queue.front();
		queue.pop_front();
		for (auto connection : graph[key]) {
			if (connection == end)
				count++;
			else
				queue.push_back(connection);
			//std::cout << std::hex << start << "->" << end << ": " << std::dec << queue.size() << "\n";
		}
	}
	return count;
}

int main() {
	{
		std::ifstream stream("input.txt");
		while (!stream.eof()) {
			auto key = readWord(stream);
			if (stream.eof()) break;
			stream.get(); // ':' separator
			while (stream.get() != '\n') {
				auto value = readWord(stream);
				if (stream.eof()) break;
				graph[key].push_back(value);
			}
		}
	}
	std::cout << traverse('you', 'out') << "\n";
	//std::cout << traverse('svr', 'dac') * traverse('dac', 'fft') * traverse('fft', 'out') + traverse('svr', 'fft') * traverse('fft', 'dac') * traverse('dac', 'out') << "\n"; //TODO: this doesn't work
}
