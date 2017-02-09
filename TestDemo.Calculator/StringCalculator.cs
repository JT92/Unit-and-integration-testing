using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo.Calculator
{
    public class StringCalculator
    {
        private readonly IStore _store;

        public StringCalculator(IStore store)
        {
            _store = store;
        }

        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            var numbers = input.Split(',');
            var total = 0;
            foreach (var number in numbers)
            {
                total += TryParseToInteger(number);
            }
            if (_store != null)
            {
                if (IsPrime(total))
                    _store.Save(total);
            }
            return total;
        }

        private bool IsPrime(int number)
        {
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            for (int i = 3; i <= (int)(Math.Sqrt(number)); i += 2)
                if (number % i == 0)
                    return false;
            return true;
        }

        private int TryParseToInteger(string input)
        {
            int dest;
            if (!int.TryParse(input, out dest))
            {
                throw new ArgumentException("Input format was incorrect");
            }
            return dest;
        }
    }

}
