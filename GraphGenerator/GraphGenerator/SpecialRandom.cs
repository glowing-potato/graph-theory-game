using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphGenerator
{
    public class SpecialRandom
    {

        private Random random;
        private int last = 0;

        public SpecialRandom(Random random)
        {
            this.random = random;
        }

        public int Next()
        {
            return random.Next();
        }

        public int NextNoRepeat()
        {
            int rand;
            while (true)
            {
                rand = random.Next();
                if (rand != last) break;
            }
            last = rand;
            return rand;
        }

        public int NextNotEqualTo(int value)
        {
            int rand;
            while (true)
            {
                rand = random.Next();
                if (rand != value) break;
            }
            last = rand;
            return rand;
        }

    }
}
