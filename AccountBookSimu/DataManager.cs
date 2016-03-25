using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AccountBookSimu
{
    public class DataManager
    {
        #region Data Member
        private List<PayPattern> _payPatterns;
        private List<SimuOneDayResult> _simuResult;
        private DateTime _simuFrom;
        private DateTime _simuTo;
        public List<string> ListedPayPatternNames;
        #endregion

        #region Constructor
        public DataManager()
        {
            _payPatterns = new List<PayPattern>();
            _simuResult = new List<SimuOneDayResult>();
            ListedPayPatternNames = new List<string>();
        }
        #endregion

        #region Public Method
        public void AddPayPattern(PayPattern pp)
        {
            _payPatterns.Add(pp);
        }

        public void RemovePayPattern(int i)
        {
            if (
                (i < 0) ||
                (_payPatterns.Count <= i)
                )
            {
                throw new ArgumentOutOfRangeException("i");
            }

            _payPatterns.RemoveAt(i);
        }

        public PayPattern GetPayPattern(int i)
        {
            return _payPatterns[i];
        }

        public void SetTerm(DateTime from, DateTime to)
        {
            if (from <= to)
            {
                _simuFrom = from;
                _simuTo = to;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void DoSimulation()
        {
            if (_simuResult == null)
            {
                throw new InvalidOperationException("DoSimulation()");
            }

            int acc = 0;
            bool fAddResult = false;

            for (DateTime d = _simuFrom; d < _simuTo; d = d.AddDays(1))
            {
                SimuOneDayResult result = new SimuOneDayResult();
                result.Date = d;
                foreach (PayPattern pp in _payPatterns)
                {
                    fAddResult = false;
                    switch (pp.RequiredPayPattern.Frequency)
                    {
                        case FREQUENCY.DAILY:
                            fAddResult = true;
                            break;
                        case FREQUENCY.WEEKLY:
                            if (d.DayOfWeek == pp.RequiredPayPattern.Weekday)
                            {
                                fAddResult = true;
                            }
                            break;
                        case FREQUENCY.MONTHLY:
                            switch (pp.RequiredPayPattern.DayType)
                            {
                                case DAYTYPE.DAY:
                                    if (d.Day == pp.RequiredPayPattern.Day)
                                    {
                                        fAddResult = true;
                                    }
                                    break;
                                case DAYTYPE.ORDINAL:
                                    throw new NotImplementedException();
                                    break;
                                case DAYTYPE.MONTHEND:
                                    if (d.Day == DateTime.DaysInMonth(d.Year, d.Month))
                                    {
                                        fAddResult = true;
                                    }
                                    break;
                                default:
                                    throw new Exception();
                            }
                            break;
                        case FREQUENCY.YEARLY:
                            if (
                                (d.Month == pp.RequiredPayPattern.Month) &&
                                (d.Day == pp.RequiredPayPattern.Day)
                                )
                            {
                                fAddResult = true;
                            }
                            break;
                        case FREQUENCY.ONLY_ONCE:
                            if (
                                (d.Year == pp.RequiredPayPattern.DateTimeValue.Year) &&
                                (d.Month == pp.RequiredPayPattern.DateTimeValue.Month) &&
                                (d.Day == pp.RequiredPayPattern.DateTimeValue.Day)
                                )
                            {
                                fAddResult = true;
                            }
                            break;
                    }

                    if (fAddResult)
                    {
                        result.OneDayResult.Add(
                            pp.RequiredPayPattern.Name,
                            pp.RequiredPayPattern.Amount * ((pp.RequiredPayPattern.IncomeOrPay == INCOME_OR_PAY.INCOME) ? 1 : -1)
                            );

                        acc += pp.RequiredPayPattern.Amount * ((pp.RequiredPayPattern.IncomeOrPay == INCOME_OR_PAY.INCOME) ? 1 : -1);
                        result.Accumulation = acc;
                    }
                }

                if (result.OneDayResult.Count > 0)
                {
                    _simuResult.Add(result);
                }
            }
        }

        public void SaveFile(string filePath)
        {
            string fileText = "年月日,収支,累積収支";

            foreach (string s in ListedPayPatternNames)
            {
                fileText += "," + s;
            }

            fileText += "\n";

            foreach (SimuOneDayResult r in _simuResult)
            {
                fileText += r.Date.ToString("yyyy/MM/dd");
                fileText += "," + r.OneDayTotal.ToString();
                fileText += "," + r.Accumulation.ToString();

                for (int iList = 0; iList < ListedPayPatternNames.Count; iList++)
                {
                    if (r.OneDayResult.ContainsKey(ListedPayPatternNames[iList]))
                    {
                        fileText += "," + r.OneDayResult[ListedPayPatternNames[iList]];
                    }
                    else
                    {
                        if (iList < ListedPayPatternNames.Count - 1)
                        {
                            fileText += ",";
                        }
                    }
                }
                fileText += "\n";
            }

            File.WriteAllText(filePath, fileText, Encoding.GetEncoding("shift_jis"));
        }

        public void ReadFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public void SaveSettingFile(string filePath)
        {
            string settingText = "";

            settingText += _simuFrom.ToString("yyyy/MM/dd") + "\n";
            settingText += _simuTo.ToString("yyyy/MM/dd") + "\n";
            settingText += _payPatterns.Count.ToString() + "\n";
            foreach (PayPattern pp in _payPatterns)
            {
                settingText += pp.RequiredPayPattern.Name + "\n";
                settingText += (int)pp.RequiredPayPattern.Frequency + ",";
                settingText += (int)pp.RequiredPayPattern.DayType + "\n";
                switch (pp.RequiredPayPattern.DayType)
                {
                    case DAYTYPE.DAY:
                        settingText += pp.RequiredPayPattern.Day.ToString() + "\n";
                        break;
                    case DAYTYPE.WEEKDAY:
                        settingText += (int)pp.RequiredPayPattern.Weekday + "\n";
                        break;
                    case DAYTYPE.MONTH_AND_DAY:
                        settingText += pp.RequiredPayPattern.Month.ToString() + "," + pp.RequiredPayPattern.Day.ToString() + "\n";
                        break;
                    case DAYTYPE.ORDINAL:
                        settingText += pp.RequiredPayPattern.OrdinalNumber.ToString() + "," + (int)pp.RequiredPayPattern.Weekday + "\n";
                        break;
                    case DAYTYPE.MONTHEND:
                        settingText += "\n";
                        break;
                    case DAYTYPE.DATETIME:
                        settingText += pp.RequiredPayPattern.DateTimeValue.ToString("yyyy/MM/dd") + "\n";
                        break;
                }
                settingText += pp.RequiredPayPattern.Amount.ToString() + "\n";
                settingText += (int)pp.RequiredPayPattern.IncomeOrPay + "\n";
            }

            File.WriteAllText(filePath, settingText, Encoding.UTF8);
        }
        #endregion
    }
}
