using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class RandomCodeGenerator
    {
        private readonly Random _random;
        public RandomCodeGenerator()
        {
                _random = new Random();
        }
        public string GenerateCode(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
