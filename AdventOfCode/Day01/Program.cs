using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day01 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Part 1: " + Part1().Result);
            Console.WriteLine("Part 2: " + Part2().Result);
            
        }

        static async Task<int> Part1() {
            var ints = await LoadInput.GetInput();
            return ints.Aggregate(0, (total, next) => total + next);
        }

        static async Task<int> Part2() {
            var seen = new HashSet<int>();
            var offsets = await LoadInput.GetInput();
            var totalOffset = 0;
            var index = 0;
            seen.Add(totalOffset);
            while (true) {
                var current = offsets[index];
                totalOffset += current;
                if(seen.Contains(totalOffset)) {
                    return totalOffset;
                }
                seen.Add(totalOffset);
                index = (index + 1) % offsets.Count();
            }
        }
    }
}