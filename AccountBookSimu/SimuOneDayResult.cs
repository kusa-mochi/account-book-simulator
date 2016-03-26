using System;
using System.Collections.Generic;

namespace AccountBookSimu
{
    public class SimuOneDayResult
    {
        public DateTime Date;
        public Dictionary<string, int> OneDayResult;
        public int OneDayTotal
        {
            get
            {
                int ret = 0;
                foreach(KeyValuePair<string, int> r in OneDayResult)
                {
                    ret += r.Value;
                }
                return ret;
            }
        }
        public int Accumulation;

        public SimuOneDayResult()
        {
            OneDayResult = new Dictionary<string, int>();
        }
    }
}
