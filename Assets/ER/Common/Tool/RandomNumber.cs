using System;

namespace ER
{
    public class RandomNumber
    {
        public static int seed = -1;
        private static Random random;
        private static int old_time = 0;
        public static void InitSeed(int seed = -1)
        {
            seed = DateTime.Now.Millisecond;
            random = new Random();
        }
        public static float RangeF(float min, float max)
        {
            if(random==null)random = new Random(seed);
            float number = (float)random.NextDouble() * (max - min) + min;
            if(DateTime.Now.Millisecond - old_time < 10)
            {
                random = new Random((int)(number * 1000) + seed);
            }
            old_time = DateTime.Now.Millisecond;
            return number;
        }
        public static float RangeF()
        {
            if (random == null) random = new Random(seed);
            float number = (float)random.NextDouble();
            if (DateTime.Now.Millisecond - old_time < 10)
            {
                random = new Random((int)(number * 1000) + seed);
            }
            old_time = DateTime.Now.Millisecond;
            return number;
        }
    }
}