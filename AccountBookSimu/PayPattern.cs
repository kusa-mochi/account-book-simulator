using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBookSimu
{
    using StructIncreaseOptionPayPattern = StructRequiredPayPattern;

    public struct PayPattern
    {
        public StructRequiredPayPattern RequiredPayPattern;
        public StructTermOptionPayPattern TermOption;
        public StructIncreaseOptionPayPattern IncreaseOption;
    }
}
