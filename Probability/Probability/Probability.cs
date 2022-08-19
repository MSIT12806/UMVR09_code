using System;

namespace Probability
{
    public static class Probability
    {
        //0.7,0.7,0.7
        public static double Union(params double[] vals)
        {
            double result = -1;
            int length = vals.Length;

            if (length == 0) return result;
            else if (length == 1) return vals[0];
            else
            {
                double happend = 0;
                for (int i = 0; i < length; i++)
                {
                    double notHappend = 1 - happend;
                    notHappend = notHappend * (1 - vals[i]);
                    happend = 1 - notHappend;
                }
                result = happend;
                return result;
            }
        }
        public static double Intersection(params double[] vals)
        {
            return 0;
        }
    }
}
