using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBookSimu
{
    public struct StructRequiredPayPattern
    {
        public bool Enabled;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if((value.Length >= 80)||string.IsNullOrEmpty(value)||string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name");
                }
                _name = value;
            }
        }

        public FREQUENCY Frequency;
        public DAYTYPE DayType;
        public DayOfWeek Weekday;

        private int _month;
        public int Month
        {
            get { return _month; }
            set
            {
                if ((value <= 0) || (13 <= value))
                {
                    throw new ArgumentOutOfRangeException("Month");
                }

                _month = value;
            }
        }

        private int _day;
        public int Day
        {
            get { return _day; }
            set
            {
                if((value <= 0)||(32 <= value))
                {
                    throw new ArgumentOutOfRangeException("Day");
                }

                _day = value;
            }
        }

        private int _ordinalNumber;
        public int OrdinalNumber
        {
            get { return _ordinalNumber; }
            set
            {
                if ((value <= 0) || (6 <= value))
                {
                    throw new ArgumentOutOfRangeException("OrdinalNumber");
                }

                _ordinalNumber = value;
            }
        }

        public DateTime DateTimeValue;

        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set
            {
                if((value < 0)||(1000000000 < value))
                {
                    throw new ArgumentOutOfRangeException("Amount");
                }

                _amount = value;
            }
        }

        public INCOME_OR_PAY IncomeOrPay;
    }
}
