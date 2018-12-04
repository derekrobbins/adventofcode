using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day03 {
    public class Claim : IEquatable<Claim> {
        public int ClaimId;
        public int XOffset;
        public int YOffset;
        public int XSize;
        public int YSize;

        public static Claim FromString(string str) {
            var pieces = str.Split(' ');
            var id = pieces[0].TrimStart('#');
            var offset = pieces[2].Split(',');
            var size = pieces[3].Split('x');
            return new Claim {
                ClaimId = int.Parse(id),
                XOffset = int.Parse(offset[0]),
                YOffset = int.Parse(offset[1].TrimEnd(':')),
                XSize = int.Parse(size[0]),
                YSize = int.Parse(size[1])
            };
        }
        
        public override int GetHashCode() {
            return ClaimId;
        }
        public override bool Equals(object obj) {
            return Equals(obj as Claim);
        }
        public bool Equals(Claim obj) {
            return obj != null && obj.ClaimId == this.ClaimId;
        }
    }
    public static class LoadInput {
        public static async Task<Claim[]> GetInput() {
            var contents = await File.OpenText("input.txt").ReadToEndAsync();
            return contents
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(Claim.FromString)
                .ToArray();
        }
    }
}