using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LongestCommonSubSeq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // "ABAZDC", "BACBAD" > "ABAD"

            // "AGGTAB", "GXTXAYB" > "GTAB"

            // "aaaa", "aa" > "aa"

            Console.WriteLine(GetLongestSubSequence("aaaa", "aa"));
            Console.WriteLine(GetLongestSubSequence("ABAZDC", "BACBAD"));
            Console.WriteLine(GetLongestSubSequence("AGGTAB", "GXTXAYB"));


            Console.ReadKey();
        }

        public static double GetTotalPowerOf(int power, int yValue)
        {
            return yValue == 0 ? 1 : Math.Pow(2, yValue) + GetTotalPowerOf(2, yValue - 1);
        }

        public static string GetLongestSubSequence(string str1, string str2)
        {
            string result = "";

            // "ABAZDC", "BACBAD" > "ABAD"
            // for each letter

            int maxLength = str1.Length;
            if (str2.Length < maxLength)
            {
                maxLength = str2.Length;
            }
            IDictionary<string, string> str1Ht = GetSubSequences(str1, maxLength);
            IDictionary<string, string> str2Ht = GetSubSequences(str2, maxLength);

            IEnumerable<string> matchingKeys = str1Ht.Keys.Intersect(str2Ht.Keys);

            return matchingKeys.OrderByDescending(f => f.Length).FirstOrDefault();
        }

        public static IDictionary<string, string> GetSubSequences(string str, int maxLength)
        {
            char[] str1Array = str.ToCharArray();

            // use binary logic to figure out the letters to include

            // work out the maximum power so if there are 5 letters there 1+2+4+8+16=31
            // 
            double maxLengthBinary = GetTotalPowerOf(2, str1Array.Length);
         
            IDictionary<string, string> hashTable = new Dictionary<string, string>();

            // loop through all the possible numbers from 1 to the maximum power
            for (int position = 1; position <= maxLengthBinary; position++) {
                StringBuilder seq = new StringBuilder();

                for (int charPosition = 0; charPosition < str1Array.Length; charPosition++) {
                    // use binary logic to determine if we use the
                    // current letter on this round
                    if ((position & (int)Math.Pow(2, charPosition)) != 0) {
                        seq.Append(str1Array[charPosition]);
                    }
                }
                
                if (seq.Length > 1 && !hashTable.ContainsKey(seq.ToString()) && seq.Length <= maxLength)
                {
                    hashTable.Add(seq.ToString(), seq.ToString());
                }
            }

            return hashTable;
        }
    }
}
