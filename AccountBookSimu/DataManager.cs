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
        private STATE_APP _applicationState;
        private bool _fCancelSimulation;
        public List<string> ListedPayPatternNames;
        public AsyncMessenger asyncMessenger;
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

        public STATE_APP ApplicationState
        {
            get { return _applicationState; }
            set { _applicationState = value; }
        }

        public bool CancelSimulationFlag
        {
            set { _fCancelSimulation = value; }
        }
        #endregion

        #region Constructor
        public DataManager()
        {
            _payPatterns = new List<PayPattern>();
            _simuResult = new List<SimuOneDayResult>();
            _fileManager = new FileManager();
            _applicationState = STATE_APP.NO_SIMULATING;

            ListedPayPatternNames = new List<string>();
            asyncMessenger = null;
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
            asyncMessenger.ApplicationState = STATE_APP.SIMULATING;

            if (_simuResult == null)
            {
                asyncMessenger.ApplicationState = STATE_APP.ABORT;
                throw new InvalidOperationException("DoSimulation()");
            }

            _simuResult = new List<SimuOneDayResult>();

            int acc = 0;
            bool fAddResult = false;
            int[] increaseAmounts = new int[_payPatterns.Count];    // オプションで付加する金額の増減分
            for (int i = 0; i < increaseAmounts.Length; i++)
            {
                increaseAmounts[i] = 0;
            }

            for (DateTime d = _simuFrom; d < _simuTo; d = d.AddDays(1))
            {
                SimuOneDayResult result = new SimuOneDayResult();
                result.Date = d;
                int iPayPattern = 0;
                foreach (PayPattern pp in _payPatterns)
                {
                    fAddResult = false;

                    // 期間オプションが指定されており，かつ期間外である場合
                    if (
                        (pp.TermOption.Enabled == true) &&
                        ((d < pp.TermOption.TermFrom) || (pp.TermOption.TermTo < d))
                        )
                    {
                        // 何もせず次の収支パターンを見に行く。
                        iPayPattern++;
                        continue;
                    }

                    // 金額の増減オプションが指定されている場合
                    if (pp.IncreaseOption.Enabled == true)
                    {
                        bool fIncrase = false;
                        switch (pp.IncreaseOption.Frequency)
                        {
                            case FREQUENCY.DAILY:
                                fIncrase = true;
                                break;
                            case FREQUENCY.WEEKLY:
                                if (d.DayOfWeek == pp.IncreaseOption.Weekday)
                                {
                                    fIncrase = true;
                                }
                                break;
                            case FREQUENCY.MONTHLY:
                                switch (pp.IncreaseOption.DayType)
                                {
                                    case DAYTYPE.DAY:
                                        if (d.Day == pp.IncreaseOption.Day)
                                        {
                                            fIncrase = true;
                                        }
                                        break;
                                    case DAYTYPE.ORDINAL:
                                        if (this.IsOrdinalWeekday(d, pp.IncreaseOption.OrdinalNumber, pp.IncreaseOption.Weekday))
                                        {
                                            fIncrase = true;
                                        }
                                        break;
                                    case DAYTYPE.MONTHEND:
                                        if (d.Day == DateTime.DaysInMonth(d.Year, d.Month))
                                        {
                                            fIncrase = true;
                                        }
                                        break;
                                    default:
                                        throw new Exception();
                                }
                                break;
                            case FREQUENCY.YEARLY:
                                if (
                                    (d.Month == pp.IncreaseOption.Month) &&
                                    (d.Day == pp.IncreaseOption.Day)
                                    )
                                {
                                    fIncrase = true;
                                }
                                break;
                            case FREQUENCY.ONLY_ONCE:
                                if (
                                    (d.Year == pp.IncreaseOption.DateTimeValue.Year) &&
                                    (d.Month == pp.IncreaseOption.DateTimeValue.Month) &&
                                    (d.Day == pp.IncreaseOption.DateTimeValue.Day)
                                    )
                                {
                                    fIncrase = true;
                                }
                                break;
                        }

                        if (fIncrase)
                        {
                            increaseAmounts[iPayPattern] += pp.IncreaseOption.Amount * ((pp.IncreaseOption.IncomeOrPay == (INCOME_OR_PAY)INCREASE_OR_DECREASE.INCREASE) ? 1 : -1);
                        }
                    }

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
                        int amount = pp.RequiredPayPattern.Amount + increaseAmounts[iPayPattern];
                        result.OneDayResult.Add(
                            pp.RequiredPayPattern.Name,
                            amount * ((pp.RequiredPayPattern.IncomeOrPay == INCOME_OR_PAY.INCOME) ? 1 : -1)
                            );

                        acc += amount * ((pp.RequiredPayPattern.IncomeOrPay == INCOME_OR_PAY.INCOME) ? 1 : -1);
                        result.Accumulation = acc;
                    }

                    iPayPattern++;
                }

                if (result.OneDayResult.Count > 0)
                {
                    _simuResult.Add(result);
                }
            }

            asyncMessenger.ApplicationState = STATE_APP.FILE_SAVING;
        }

        public void SaveFile(string filePath)
        {
            Trace.TraceInformation("begin DataManager.SaveFile");

            if (asyncMessenger == null)
            {
                asyncMessenger.ApplicationState = STATE_APP.ABORT;
                throw new InvalidOperationException("SaveFile()");
            }

            asyncMessenger.ApplicationState = STATE_APP.FILE_SAVING;

            string fileText = "年月日,収支,累積収支";

            foreach (string s in ListedPayPatternNames)
            {
                fileText += "," + s;
            }

            fileText += "\n";

            this.asyncMessenger.IntMessage = 0;

            foreach (SimuOneDayResult r in _simuResult)
            {
                //Trace.TraceInformation("  r.Date:" + r.Date.ToString("yyyy/MM/dd"));
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
                asyncMessenger.IntMessage++;
            }

            Trace.TraceInformation("  go to FileManager.SaveSimulatedFile");
            _fileManager.SaveSimulatedFile(filePath, fileText);

            asyncMessenger.ApplicationState = STATE_APP.NO_SIMULATING;

            Trace.TraceInformation("end DataManager.SaveFile");
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
                    int iFileData = 3 + (11 * i);
                    PayPattern pp = new PayPattern(0);
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

                    // TermOptionが指定されている場合
                    if (fileData[iFileData + 5][0] != '-')
                    {
                        pp.TermOption.Enabled = true;
                        if (
                            (DateTime.TryParse(fileData[iFileData + 5], out pp.TermOption.TermFrom) == false) ||
                            (DateTime.TryParse(fileData[iFileData + 6], out pp.TermOption.TermTo) == false)
                            )
                        {
                            // catch以降の処理に進むため，Exceptionを発生させる。
                            throw new Exception();
                        }
                    }
                    else
                    {
                        pp.TermOption.Enabled = false;
                        pp.TermOption.TermFrom = DateTime.Today;
                        pp.TermOption.TermTo = DateTime.Today;
                    }

                    // IncreaseDecreaseOptionが指定されている場合
                    if (fileData[iFileData + 7][0] != '-')
                    {
                        pp.IncreaseOption.Enabled = true;
                        string[] increaseDecreaseTypeSelect = fileData[iFileData + 7].Split(',');
                        pp.IncreaseOption.Frequency = (FREQUENCY)(int.Parse(increaseDecreaseTypeSelect[0]));
                        pp.IncreaseOption.DayType = (DAYTYPE)(int.Parse(increaseDecreaseTypeSelect[1]));
                        switch (pp.IncreaseOption.DayType)
                        {
                            case DAYTYPE.WEEKDAY:
                                pp.IncreaseOption.Weekday = (DayOfWeek)(int.Parse(fileData[iFileData + 8]));
                                break;
                            case DAYTYPE.DAY:
                                pp.IncreaseOption.Day = int.Parse(fileData[iFileData + 8]);
                                break;
                            case DAYTYPE.ORDINAL:
                                string[] ordinalNumberAndWeekday = fileData[iFileData + 8].Split(',');
                                pp.IncreaseOption.OrdinalNumber = int.Parse(ordinalNumberAndWeekday[0]);
                                pp.IncreaseOption.Weekday = (DayOfWeek)int.Parse(ordinalNumberAndWeekday[1]);
                                break;
                            case DAYTYPE.MONTHEND:
                                // 何もしない。
                                break;
                            case DAYTYPE.MONTH_AND_DAY:
                                string[] monthAndDay = fileData[iFileData + 8].Split(',');
                                pp.IncreaseOption.Month = int.Parse(monthAndDay[0]);
                                pp.IncreaseOption.Day = int.Parse(monthAndDay[1]);
                                break;
                            case DAYTYPE.DATETIME:
                                if (DateTime.TryParse(fileData[iFileData + 8], out pp.IncreaseOption.DateTimeValue) == false)
                                {
                                    // catch以降の処理に進むため，Exceptionを発生させる。
                                    throw new Exception();
                                }
                                break;
                        }
                        int increaseDecreaseAmount = 0;
                        int.TryParse(fileData[iFileData + 9], out increaseDecreaseAmount);
                        pp.IncreaseOption.Amount = increaseDecreaseAmount;
                        pp.IncreaseOption.IncomeOrPay = (fileData[iFileData + 10] == "0" ? INCOME_OR_PAY.PAY : INCOME_OR_PAY.INCOME);
                    }
                    else
                    {
                        pp.IncreaseOption.Enabled = false;
                        pp.IncreaseOption.Frequency = FREQUENCY.MONTHLY;
                        pp.IncreaseOption.DayType = DAYTYPE.DAY;
                        pp.IncreaseOption.Month = 1;
                        pp.IncreaseOption.Day = 1;
                        pp.IncreaseOption.Weekday = DayOfWeek.Sunday;
                        pp.IncreaseOption.OrdinalNumber = 1;
                        pp.IncreaseOption.DateTimeValue = DateTime.Today;
                        pp.IncreaseOption.Amount = 0;
                        pp.IncreaseOption.IncomeOrPay = INCOME_OR_PAY.PAY;
                    }

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

                if (pp.TermOption.Enabled == true)
                {
                    settingText += pp.TermOption.TermFrom.ToString("yyyy/MM/dd") + "\n";
                    settingText += pp.TermOption.TermTo.ToString("yyyy/MM/dd") + "\n";
                }
                else
                {
                    settingText += "-\n";
                    settingText += "-\n";
                }

                if (pp.IncreaseOption.Enabled == true)
                {
                    settingText += (int)pp.IncreaseOption.Frequency + ",";
                    settingText += (int)pp.IncreaseOption.DayType + "\n";
                    switch (pp.IncreaseOption.DayType)
                    {
                        case DAYTYPE.DAY:
                            settingText += pp.IncreaseOption.Day.ToString() + "\n";
                            break;
                        case DAYTYPE.WEEKDAY:
                            settingText += (int)pp.IncreaseOption.Weekday + "\n";
                            break;
                        case DAYTYPE.MONTH_AND_DAY:
                            settingText += pp.IncreaseOption.Month.ToString() + "," + pp.IncreaseOption.Day.ToString() + "\n";
                            break;
                        case DAYTYPE.ORDINAL:
                            settingText += pp.IncreaseOption.OrdinalNumber.ToString() + "," + (int)pp.IncreaseOption.Weekday + "\n";
                            break;
                        case DAYTYPE.MONTHEND:
                            settingText += "\n";
                            break;
                        case DAYTYPE.DATETIME:
                            settingText += pp.IncreaseOption.DateTimeValue.ToString("yyyy/MM/dd") + "\n";
                            break;
                    }
                    settingText += pp.IncreaseOption.Amount.ToString() + "\n";
                    settingText += (int)pp.IncreaseOption.IncomeOrPay + "\n";
                }
                else
                {
                    settingText += "-\n";
                    settingText += "-\n";
                    settingText += "-\n";
                    settingText += "-\n";
                }
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
