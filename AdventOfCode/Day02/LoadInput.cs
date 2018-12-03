using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day02 {
    public static class LoadInput {
        public static async Task<string[]> GetInput() {
            var contents = await File.OpenText("input.txt").ReadToEndAsync();
            return contents
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
        }
    }
}