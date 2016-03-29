using System;

namespace AccountBookSimu
{
    public struct StructTermOptionPayPattern
    {
        public bool Enabled;
        public DateTime TermFrom;
        public DateTime TermTo;

        public StructTermOptionPayPattern(int n)
        {
            Enabled = false;
            TermFrom = DateTime.Today;
            TermTo = DateTime.Today;
        }
    }
}
