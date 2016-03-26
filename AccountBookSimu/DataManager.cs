using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AccountBookSimu
{
    public class DataManager
    {
        #region Data Member
        private List<PayPattern> _payPatterns;
        private List<SimuOneDayResult> _simuResult;
        private DateTime _simuFrom;
        private DateTime _simuTo;
        private FileManager _fileManager;
        public List<string> ListedPayPatternNames;
        #endregion

        #region Property
        public int NPayPattern
        {
            get
            {
                if (_payPatterns == null)
                {
                    return -1;
                }

                return _payPatterns.Count;
            }
        }

        public DateTime SimulateFrom
        {
            get { return _simuFrom; }
        }

        public DateTime SimulateTo
        {
            get { return _simuTo; }
        }
        #endregion

        #region Constructor
        public DataManager()
        {
            _payPatterns = new List<PayPattern>();
            _simuResult = new List<SimuOneDayResult>();
            _fileManager = new FileManager();

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

        public void EditPayPattern(int i, PayPattern pp)
        {
            if (
                (i < 0) ||
                (_payPatterns.Count - 1 < i)
                )
            {
                throw new ArgumentOutOfRangeException("i");
            }

            _payPatterns[i] = pp;
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
                                    if (this.IsOrdinalWeekday(d, pp.RequiredPayPattern.OrdinalNumber, pp.RequiredPayPattern.Weekday))
                                    {
                                        fAddResult = true;
                                    }
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

            _fileManager.SaveSimulatedFile(filePath, fileText);
        }

        public void ReadFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public bool OpenSettingFile(string filePath)
        {
            Trace.TraceInformation("filePath:{0}", filePath);
            string[] fileData = _fileManager.ReadSettingFile(filePath);
            if (
                (fileData == null) ||
                (fileData.Length < 1) ||
                (DateTime.TryParse(fileData[0], out _simuFrom) == false) ||
                (DateTime.TryParse(fileData[1], out _simuTo) == false)
                )
            {
                throw new FileFormatException(filePath);
            }

            _payPatterns = new List<PayPattern>();
            try
            {
                int nPattern = int.Parse(fileData[2]);
                for (int i = 0; i < nPattern; i++)
                {
                    int iFileData = 3 + (5 * i);
                    PayPattern pp = new PayPattern();
                    pp.RequiredPayPattern.Name = fileData[iFileData];
                    string[] typeSelect = fileData[iFileData + 1].Split(',');
                    pp.RequiredPayPattern.Frequency = (FREQUENCY)(int.Parse(typeSelect[0]));
                    pp.RequiredPayPattern.DayType = (DAYTYPE)(int.Parse(typeSelect[1]));
                    switch (pp.RequiredPayPattern.DayType)
                    {
                        case DAYTYPE.WEEKDAY:
                            pp.RequiredPayPattern.Weekday = (DayOfWeek)(int.Parse(fileData[iFileData + 2]));
                            break;
                        case DAYTYPE.DAY:
                            pp.RequiredPayPattern.Day = int.Parse(fileData[iFileData + 2]);
                            break;
                        case DAYTYPE.ORDINAL:
                            string[] ordinalNumberAndWeekday = fileData[iFileData + 2].Split(',');
                            pp.RequiredPayPattern.OrdinalNumber = int.Parse(ordinalNumberAndWeekday[0]);
                            pp.RequiredPayPattern.Weekday = (DayOfWeek)int.Parse(ordinalNumberAndWeekday[1]);
                            break;
                        case DAYTYPE.MONTHEND:
                            // 何もしない。
                            break;
                        case DAYTYPE.MONTH_AND_DAY:
                            string[] monthAndDay = fileData[iFileData + 2].Split(',');
                            pp.RequiredPayPattern.Month = int.Parse(monthAndDay[0]);
                            pp.RequiredPayPattern.Day = int.Parse(monthAndDay[1]);
                            break;
                        case DAYTYPE.DATETIME:
                            if (DateTime.TryParse(fileData[iFileData + 2], out pp.RequiredPayPattern.DateTimeValue) == false)
                            {
                                // catch以降の処理に進むため，Exceptionを発生させる。
                                throw new Exception();
                            }
                            break;
                    }
                    int amount = 0;
                    int.TryParse(fileData[iFileData + 3], out amount);
                    pp.RequiredPayPattern.Amount = amount;
                    pp.RequiredPayPattern.IncomeOrPay = (fileData[iFileData + 4] == "0" ? INCOME_OR_PAY.PAY : INCOME_OR_PAY.INCOME);
                    _payPatterns.Add(pp);
                }
            }
            catch
            {
                throw new FileFormatException(filePath);
            }

            return true;
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

            _fileManager.SaveSettingFile(filePath, settingText);
        }
        #endregion

        #region Private Method
        private bool IsOrdinalWeekday(DateTime dt, int ordinalNumber, DayOfWeek weekday)
        {
            DayOfWeek w = dt.DayOfWeek;
            if (w != weekday) return false;
            int day = dt.Day;
            int ord = 0;
            while (day > 0)
            {
                day -= 7;
                ord++;
            }

            return (ord == ordinalNumber);
        }
        #endregion
    }
}
