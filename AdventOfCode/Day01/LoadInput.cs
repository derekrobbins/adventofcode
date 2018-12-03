using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day01 {
    public static class LoadInput {
        public static async Task<int[]> GetInput() {
            var contents = await File.OpenText("input.txt").ReadToEndAsync();
            return contents
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }
}