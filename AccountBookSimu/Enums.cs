using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBookSimu
{
    public enum FREQUENCY
    {
        DAILY,
        WEEKLY,
        MONTHLY,
        YEARLY,
        ONLY_ONCE
    }

    public enum DAYTYPE
    {
        WEEKDAY,
        DAY,
        ORDINAL,
        MONTHEND,
        MONTH_AND_DAY,
        DATETIME
    }

    public enum INCOME_OR_PAY
    {
        PAY,
        INCOME
    }

    public enum INCREASE_OR_DECREASE
    {
        INCREASE,
        DECREASE
    }
}
