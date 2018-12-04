using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Linq;

/*
 * --- Day 3: No Matter How You Slice It ---
The Elves managed to locate the chimney-squeeze prototype fabric for Santa's suit (thanks to someone who helpfully wrote its box IDs on the wall of the warehouse in the middle of the night). Unfortunately, anomalies are still affecting them - nobody can even agree on how to cut the fabric.

The whole piece of fabric they're working on is a very large square - at least 1000 inches on each side.

Each Elf has made a claim about which area of fabric would be ideal for Santa's suit. All claims have an ID and consist of a single rectangle with edges parallel to the edges of the fabric. Each claim's rectangle is defined as follows:

The number of inches between the left edge of the fabric and the left edge of the rectangle.
The number of inches between the top edge of the fabric and the top edge of the rectangle.
The width of the rectangle in inches.
The height of the rectangle in inches.
A claim like #123 @ 3,2: 5x4 means that claim ID 123 specifies a rectangle 3 inches from the left edge, 2 inches from the top edge, 5 inches wide, and 4 inches tall. Visually, it claims the square inches of fabric represented by # (and ignores the square inches of fabric represented by .) in the diagram below:

...........
...........
...#####...
...#####...
...#####...
...#####...
...........
...........
...........
The problem is that many of the claims overlap, causing two or more claims to cover part of the same areas. For example, consider the following claims:

#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2
Visually, these claim the following areas:

........
...2222.
...2222.
.11XX22.
.11XX22.
.111133.
.111133.
........
The four square inches marked with X are claimed by both 1 and 2. (Claim 3, while adjacent to the others, does not overlap either of them.)
 */

namespace Day03 {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Part 1: " + Part1().Result);
            Console.WriteLine("Part 2: " + Part2());
        }
        
        /*
         * If the Elves all proceed with their own plans, none of them will have enough fabric.
         * How many square inches of fabric are within two or more claims?
         */
        static async Task<int> Part1() {
            var claims = await LoadInput.GetInput();
            var maxX = claims.Select(x => x.XOffset + x.XSize).Max();
            var maxY = claims.Select(x => x.YOffset + x.YSize).Max();
            var matrix = new int[maxX, maxY];
            var sqInches = 0;
            foreach (var claim in claims) {
                for (var x = 0; x < claim.XSize; x++) {
                    for (var y = 0; y < claim.YSize; y++) {
                        matrix[x + claim.XOffset, y + claim.YOffset]++;
                        if (matrix[x + claim.XOffset, y + claim.YOffset] == 2) {
                            sqInches++;
                        }
                    }
                }
            }
            return sqInches;
        }

        /*
         * --- Part Two ---
Amidst the chaos, you notice that exactly one claim doesn't overlap by even a single square inch of fabric with any other claim. If you can somehow draw attention to it, maybe the Elves will be able to make Santa's suit after all!

For example, in the claims above, only claim 3 is intact after all claims are made.

What is the ID of the only claim that doesn't overlap?
         */
        static int Part2() {
            var claims = LoadInput.GetInput().Result;
            var matrix = LoadDataIntoMatrix(claims);
            var unsharedSquares = matrix
                .SelectMany(hashSetCollection => hashSetCollection)
                .Where(claimsSet => claimsSet.Count == 1)
                .Select(claimsSet => claimsSet.First())
                .GroupBy(claim => claim)
                .Where(group => group.Key.XSize * group.Key.YSize == group.Count())
                .Select(x => x.First())
                .First();
            return unsharedSquares.ClaimId;
        }

        static HashSet<Claim>[][] LoadDataIntoMatrix(IEnumerable<Claim> claims) {
            var maxX = claims.Select(x => x.XOffset + x.XSize).Max();
            var maxY = claims.Select(x => x.YOffset + x.YSize).Max();
            var matrix = Enumerable.Range(0, maxX)
                .Select(x => Enumerable.Range(0, maxY)
                    .Select(y => new HashSet<Claim>())
                    .ToArray())
                .ToArray();
            foreach (var claim in claims) {
                for (var x = 0; x < claim.XSize; x++) {
                    for (var y = 0; y < claim.YSize; y++) {
                        matrix[x + claim.XOffset][y + claim.YOffset].Add(claim);
                    }
                }
            }
            return matrix;
        }
        
    }
    
    public static class MultidimensionalArrayExt {
        public static IEnumerable<T> GetColumn<T>(this T[,] array, int column)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                yield return array[i, column];
            }
        }
        
        public static IEnumerable<IEnumerable<T>> GetColumns<T>(this T[,] array) {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                yield return array.GetColumn(i);
            }
        }
    }
}