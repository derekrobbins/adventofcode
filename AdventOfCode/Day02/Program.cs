using System;
using System.Threading.Tasks;
using System.Linq;

namespace Day02 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Part 1: " + Part1().Result);
            Console.WriteLine("Part 2: " + Part2().Result);
        }

        static async Task<int> Part1() {
            var ids = await LoadInput.GetInput();
            var withDuplcateLetters = ids.Count(x => x.HasLetterCount(2));
            var withTriplicateLetters = ids.Count(x => x.HasLetterCount(3));
            var checksum = withDuplcateLetters * withTriplicateLetters;
            return checksum;
        }

        static async Task<string> Part2() {
            var ids = await LoadInput.GetInput();
            
            // All strings are the same length so take the length of the first one
            var strLen = ids[0].Length;
            for(var i = 0; i < ids.Length; i++) {
                for(var j = i + 1; j < ids.Length; j++) {
                    var diffCount = 0;
                    var commonStrings = "";
                    for(var k = 0; k < strLen; k++) {
                        if(ids[i][k] == ids[j][k]) {
                            commonStrings += ids[i][k];
                        } else {
                            diffCount++;
                        }
                        if (diffCount > 1) {
                            break;
                        }
                        if(commonStrings.Length == strLen - 1) {
                            return commonStrings;
                        }
                        
                    }
                }
            }
            return null;
        }
    }

    public static class StrExt {
        public static bool HasLetterCount(this string s, int i) {
            return s
                .GroupBy(c => c)
                .Any(group => group.Count() == i);
        }
    }
}