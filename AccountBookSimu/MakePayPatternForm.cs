using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AccountBookSimu
{
    public partial class MakePayPatternForm : Form
    {
        public PayPattern payPattern;
        public List<string> listedPayPatternNames;
        public bool fNewPattern;

        public MakePayPatternForm()
        {
            InitializeComponent();

            this.payPattern = new PayPattern(0);
            this.listedPayPatternNames = new List<string>();
            fNewPattern = true;
        }

        private void checkBox_Term_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox_Term.Enabled = this.checkBox_Term.Checked;
        }

        private void checkBox_IncreaseDecrease_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox_IncreaseDecrease.Enabled = this.checkBox_IncreaseDecrease.Checked;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (this.CheckInputs() == false)
            {
                return;
            }

            this.ReadInputs_RequiredPayPattern();
            this.ReadInputs_TermOption();
            this.ReadInputs_IncreaseDecreaseOption();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool CheckInputs()
        {
            if (
                (string.IsNullOrEmpty(this.textBox_Name.Text)) ||
                (string.IsNullOrWhiteSpace(this.textBox_Name.Text))
                )
            {
                MessageBox.Show("\"表示名\"欄にこの収支パターンの名前を記入してください。");
                return false;
            }

            if ((fNewPattern == true) && (this.IsNewName() == false))
            {
                MessageBox.Show("表示名は既に使われているため使用できません。別の名前を指定してください。");
                this.textBox_Name.Focus();
                this.textBox_Name.SelectAll();
                return false;
            }

            return true;
        }

        private bool IsNewName()
        {
            foreach (string s in this.listedPayPatternNames)
            {
                if (this.textBox_Name.Text == s)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ユーザーが入力した内容をデータメンバpayPatternに読み出す。
        /// </summary>
        private void ReadInputs_RequiredPayPattern()
        {
            payPattern.RequiredPayPattern.Name = this.textBox_Name.Text;

            if (this.radioButton_Daily.Checked == true) payPattern.RequiredPayPattern.Frequency = FREQUENCY.DAILY;
            if (this.radioButton_Weekly.Checked == true) payPattern.RequiredPayPattern.Frequency = FREQUENCY.WEEKLY;
            if (this.radioButton_Monthly.Checked == true) payPattern.RequiredPayPattern.Frequency = FREQUENCY.MONTHLY;
            if (this.radioButton_Yearly.Checked == true) payPattern.RequiredPayPattern.Frequency = FREQUENCY.YEARLY;
            if (this.radioButton_OnlyOnce.Checked == true) payPattern.RequiredPayPattern.Frequency = FREQUENCY.ONLY_ONCE;

            if (this.radioButton_Weekday.Checked == true)
            {
                payPattern.RequiredPayPattern.DayType = DAYTYPE.WEEKDAY;
                payPattern.RequiredPayPattern.Weekday = (DayOfWeek)this.comboBox_Weekday.SelectedIndex;
            }

            if (this.radioButton_Day.Checked == true)
            {
                payPattern.RequiredPayPattern.DayType = DAYTYPE.DAY;
                payPattern.RequiredPayPattern.Day = this.comboBox_Day.SelectedIndex + 1;
            }

            if (this.radioButton_Ordinal.Checked == true)
            {
                payPattern.RequiredPayPattern.DayType = DAYTYPE.ORDINAL;
                payPattern.RequiredPayPattern.OrdinalNumber = this.comboBox_Ordinal_Number.SelectedIndex + 1;
                payPattern.RequiredPayPattern.Weekday = (DayOfWeek)this.comboBox_Ordinal_Weekday.SelectedIndex;
            }

            if (this.radioButton_MonthEnd.Checked == true)
            {
                payPattern.RequiredPayPattern.DayType = DAYTYPE.MONTHEND;
            }

            if (this.radioButton_MonthAndDay.Checked == true)
            {
                payPattern.RequiredPayPattern.DayType = DAYTYPE.MONTH_AND_DAY;
                payPattern.RequiredPayPattern.Month = this.comboBox_MonthAndDay_Month.SelectedIndex + 1;
                payPattern.RequiredPayPattern.Day = this.comboBox_MonthAndDay_Day.SelectedIndex + 1;
            }

            if (this.radioButton_DateTime.Checked == true)
            {
                payPattern.RequiredPayPattern.DayType = DAYTYPE.DATETIME;
                payPattern.RequiredPayPattern.DateTimeValue = this.dateTimePicker_DateTime.Value;
            }

            payPattern.RequiredPayPattern.Amount = (int)this.numericUpDown_Amount.Value;
            payPattern.RequiredPayPattern.IncomeOrPay = (INCOME_OR_PAY)this.comboBox_InOrPay.SelectedIndex;
        }

        private void ReadInputs_TermOption()
        {
            payPattern.TermOption.Enabled = this.checkBox_Term.Checked;
            payPattern.TermOption.TermFrom = this.dateTimePicker_Term_From.Value;
            payPattern.TermOption.TermTo = this.dateTimePicker_Term_To.Value;
        }

        private void ReadInputs_IncreaseDecreaseOption()
        {
            payPattern.IncreaseOption.Enabled = this.checkBox_IncreaseDecrease.Checked;

            if (this.radioButton_IncreaseDecrease_Daily.Checked == true) payPattern.IncreaseOption.Frequency = FREQUENCY.DAILY;
            if (this.radioButton_IncreaseDecrease_Weekly.Checked == true) payPattern.IncreaseOption.Frequency = FREQUENCY.WEEKLY;
            if (this.radioButton_IncreaseDecrease_Monthly.Checked == true) payPattern.IncreaseOption.Frequency = FREQUENCY.MONTHLY;
            if (this.radioButton_IncreaseDecrease_Yearly.Checked == true) payPattern.IncreaseOption.Frequency = FREQUENCY.YEARLY;
            if (this.radioButton_IncreaseDecrease_OnlyOnce.Checked == true) payPattern.IncreaseOption.Frequency = FREQUENCY.ONLY_ONCE;

            if (this.radioButton_IncreaseDecrease_Weekday.Checked == true)
            {
                payPattern.IncreaseOption.DayType = DAYTYPE.WEEKDAY;
                payPattern.IncreaseOption.Weekday = (DayOfWeek)this.comboBox_IncreaseDecrease_Weekday.SelectedIndex;
            }

            if (this.radioButton_IncreaseDecrease_Day.Checked == true)
            {
                payPattern.IncreaseOption.DayType = DAYTYPE.DAY;
                payPattern.IncreaseOption.Day = this.comboBox_IncreaseDecrease_Day.SelectedIndex + 1;
            }

            if (this.radioButton_IncreaseDecrease_Ordinal.Checked == true)
            {
                payPattern.IncreaseOption.DayType = DAYTYPE.ORDINAL;
                payPattern.IncreaseOption.OrdinalNumber = this.comboBox_IncreaseDecrease_Ordinal_Number.SelectedIndex + 1;
                payPattern.IncreaseOption.Weekday = (DayOfWeek)this.comboBox_IncreaseDecrease_Ordinal_Weekday.SelectedIndex;
            }

            if (this.radioButton_IncreaseDecrease_MonthEnd.Checked == true)
            {
                payPattern.IncreaseOption.DayType = DAYTYPE.MONTHEND;
            }

            if (this.radioButton_IncreaseDecrease_MonthAndDay.Checked == true)
            {
                payPattern.IncreaseOption.DayType = DAYTYPE.MONTH_AND_DAY;
                payPattern.IncreaseOption.Month = this.comboBox_IncreaseDecrease_MonthAndDay_Month.SelectedIndex + 1;
                payPattern.IncreaseOption.Day = this.comboBox_IncreaseDecrease_MonthAndDay_Day.SelectedIndex + 1;
            }

            if (this.radioButton_IncreaseDecrease_DateTime.Checked == true)
            {
                payPattern.IncreaseOption.DayType = DAYTYPE.DATETIME;
                payPattern.IncreaseOption.DateTimeValue = this.dateTimePicker_IncreaseDecrease_DateTime.Value;
            }

            payPattern.IncreaseOption.Amount = (int)this.numericUpDown_IncreaseDecrease_Amount.Value;
            payPattern.IncreaseOption.IncomeOrPay = (INCOME_OR_PAY)this.comboBox_IncreaseDecrease_InOrDe.SelectedIndex;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radioButton_Daily_CheckedChanged(object sender, EventArgs e) { this.ChangeDayTypeEnabledState(sender); }
        private void radioButton_Weekly_CheckedChanged(object sender, EventArgs e) { this.ChangeDayTypeEnabledState(sender); }
        private void radioButton_Monthly_CheckedChanged(object sender, EventArgs e) { this.ChangeDayTypeEnabledState(sender); }
        private void radioButton_Yearly_CheckedChanged(object sender, EventArgs e) { this.ChangeDayTypeEnabledState(sender); }
        private void radioButton_OnlyOnce_CheckedChanged(object sender, EventArgs e) { this.ChangeDayTypeEnabledState(sender); }
        private void ChangeDayTypeEnabledState(object sender)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked == false)
            {
                return;
            }

            switch (radioButton.Name)
            {
                case "radioButton_Daily":
                    this.radioButton_Weekday.Enabled = false;
                    this.comboBox_Weekday.Enabled = false;

                    this.radioButton_Day.Enabled = false;
                    this.comboBox_Day.Enabled = false;

                    this.radioButton_Ordinal.Enabled = false;
                    this.comboBox_Ordinal_Number.Enabled = false;
                    this.comboBox_Ordinal_Weekday.Enabled = false;

                    this.radioButton_MonthEnd.Enabled = false;

                    this.radioButton_MonthAndDay.Enabled = false;
                    this.comboBox_MonthAndDay_Month.Enabled = false;
                    this.comboBox_MonthAndDay_Day.Enabled = false;

                    this.radioButton_DateTime.Enabled = false;
                    this.dateTimePicker_DateTime.Enabled = false;
                    break;
                case "radioButton_Weekly":
                    this.radioButton_Weekday.Checked = true;

                    this.radioButton_Weekday.Enabled = true;
                    this.comboBox_Weekday.Enabled = true;

                    this.radioButton_Day.Enabled = false;
                    this.comboBox_Day.Enabled = false;

                    this.radioButton_Ordinal.Enabled = false;
                    this.comboBox_Ordinal_Number.Enabled = false;
                    this.comboBox_Ordinal_Weekday.Enabled = false;

                    this.radioButton_MonthEnd.Enabled = false;

                    this.radioButton_MonthAndDay.Enabled = false;
                    this.comboBox_MonthAndDay_Month.Enabled = false;
                    this.comboBox_MonthAndDay_Day.Enabled = false;

                    this.radioButton_DateTime.Enabled = false;
                    this.dateTimePicker_DateTime.Enabled = false;
                    break;
                case "radioButton_Monthly":
                    if (
                        (this.radioButton_Day.Checked == false) &&
                        (this.radioButton_Ordinal.Checked == false) &&
                        (this.radioButton_MonthEnd.Checked == false)
                        )
                    {
                        this.radioButton_Day.Checked = true;
                    }

                    this.radioButton_Weekday.Enabled = false;
                    this.comboBox_Weekday.Enabled = false;

                    this.radioButton_Day.Enabled = true;
                    this.comboBox_Day.Enabled = true;

                    this.radioButton_Ordinal.Enabled = true;
                    this.comboBox_Ordinal_Number.Enabled = true;
                    this.comboBox_Ordinal_Weekday.Enabled = true;

                    this.radioButton_MonthEnd.Enabled = true;

                    this.radioButton_MonthAndDay.Enabled = false;
                    this.comboBox_MonthAndDay_Month.Enabled = false;
                    this.comboBox_MonthAndDay_Day.Enabled = false;

                    this.radioButton_DateTime.Enabled = false;
                    this.dateTimePicker_DateTime.Enabled = false;
                    break;
                case "radioButton_Yearly":
                    this.radioButton_MonthAndDay.Checked = true;

                    this.radioButton_Weekday.Enabled = false;
                    this.comboBox_Weekday.Enabled = false;

                    this.radioButton_Day.Enabled = false;
                    this.comboBox_Day.Enabled = false;

                    this.radioButton_Ordinal.Enabled = false;
                    this.comboBox_Ordinal_Number.Enabled = false;
                    this.comboBox_Ordinal_Weekday.Enabled = false;

                    this.radioButton_MonthEnd.Enabled = false;

                    this.radioButton_MonthAndDay.Enabled = true;
                    this.comboBox_MonthAndDay_Month.Enabled = true;
                    this.comboBox_MonthAndDay_Day.Enabled = true;

                    this.radioButton_DateTime.Enabled = false;
                    this.dateTimePicker_DateTime.Enabled = false;
                    break;
                case "radioButton_OnlyOnce":
                    this.radioButton_DateTime.Checked = true;

                    this.radioButton_Weekday.Enabled = false;
                    this.comboBox_Weekday.Enabled = false;

                    this.radioButton_Day.Enabled = false;
                    this.comboBox_Day.Enabled = false;

                    this.radioButton_Ordinal.Enabled = false;
                    this.comboBox_Ordinal_Number.Enabled = false;
                    this.comboBox_Ordinal_Weekday.Enabled = false;

                    this.radioButton_MonthEnd.Enabled = false;

                    this.radioButton_MonthAndDay.Enabled = false;
                    this.comboBox_MonthAndDay_Month.Enabled = false;
                    this.comboBox_MonthAndDay_Day.Enabled = false;

                    this.radioButton_DateTime.Enabled = true;
                    this.dateTimePicker_DateTime.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void radioButton_IncreaseDecrease_Daily_CheckedChanged(object sender, EventArgs e) { this.ChangeDayTypeEnabledState_IncreaseDecrease(sender); }
        private void radioButton_IncreaseDecrease_Weekly_CheckedChanged(object sender, EventArgs e) { this.ChangeDayTypeEnabledState_IncreaseDecrease(sender); }
        private void radioButton_IncreaseDecrease_Monthly_CheckedChanged(object sender, EventArgs e) { this.ChangeDayTypeEnabledState_IncreaseDecrease(sender); }
        private void radioButton_IncreaseDecrease_Yearly_CheckedChanged(object sender, EventArgs e) { this.ChangeDayTypeEnabledState_IncreaseDecrease(sender); }
        private void radioButton_IncreaseDecrease_OnlyOnce_CheckedChanged(object sender, EventArgs e) { this.ChangeDayTypeEnabledState_IncreaseDecrease(sender); }
        private void ChangeDayTypeEnabledState_IncreaseDecrease(object sender)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked == false)
            {
                return;
            }

            switch (radioButton.Name)
            {
                case "radioButton_IncreaseDecrease_Daily":
                    this.radioButton_IncreaseDecrease_Weekday.Enabled = false;
                    this.comboBox_IncreaseDecrease_Weekday.Enabled = false;

                    this.radioButton_IncreaseDecrease_Day.Enabled = false;
                    this.comboBox_IncreaseDecrease_Day.Enabled = false;

                    this.radioButton_IncreaseDecrease_Ordinal.Enabled = false;
                    this.comboBox_IncreaseDecrease_Ordinal_Number.Enabled = false;
                    this.comboBox_IncreaseDecrease_Ordinal_Weekday.Enabled = false;

                    this.radioButton_IncreaseDecrease_MonthEnd.Enabled = false;

                    this.radioButton_IncreaseDecrease_MonthAndDay.Enabled = false;
                    this.comboBox_IncreaseDecrease_MonthAndDay_Month.Enabled = false;
                    this.comboBox_IncreaseDecrease_MonthAndDay_Day.Enabled = false;

                    this.radioButton_IncreaseDecrease_DateTime.Enabled = false;
                    this.dateTimePicker_IncreaseDecrease_DateTime.Enabled = false;
                    break;
                case "radioButton_IncreaseDecrease_Weekly":
                    this.radioButton_IncreaseDecrease_Weekday.Checked = true;

                    this.radioButton_IncreaseDecrease_Weekday.Enabled = true;
                    this.comboBox_IncreaseDecrease_Weekday.Enabled = true;

                    this.radioButton_IncreaseDecrease_Day.Enabled = false;
                    this.comboBox_IncreaseDecrease_Day.Enabled = false;

                    this.radioButton_IncreaseDecrease_Ordinal.Enabled = false;
                    this.comboBox_IncreaseDecrease_Ordinal_Number.Enabled = false;
                    this.comboBox_IncreaseDecrease_Ordinal_Weekday.Enabled = false;

                    this.radioButton_IncreaseDecrease_MonthEnd.Enabled = false;

                    this.radioButton_IncreaseDecrease_MonthAndDay.Enabled = false;
                    this.comboBox_IncreaseDecrease_MonthAndDay_Month.Enabled = false;
                    this.comboBox_IncreaseDecrease_MonthAndDay_Day.Enabled = false;

                    this.radioButton_IncreaseDecrease_DateTime.Enabled = false;
                    this.dateTimePicker_IncreaseDecrease_DateTime.Enabled = false;
                    break;
                case "radioButton_IncreaseDecrease_Monthly":
                    if (
                        (this.radioButton_IncreaseDecrease_Day.Checked == false) &&
                        (this.radioButton_IncreaseDecrease_Ordinal.Checked == false) &&
                        (this.radioButton_IncreaseDecrease_MonthEnd.Checked == false)
                        )
                    {
                        this.radioButton_IncreaseDecrease_Day.Checked = true;
                    }

                    this.radioButton_IncreaseDecrease_Weekday.Enabled = false;
                    this.comboBox_IncreaseDecrease_Weekday.Enabled = false;

                    this.radioButton_IncreaseDecrease_Day.Enabled = true;
                    this.comboBox_IncreaseDecrease_Day.Enabled = true;

                    this.radioButton_IncreaseDecrease_Ordinal.Enabled = true;
                    this.comboBox_IncreaseDecrease_Ordinal_Number.Enabled = true;
                    this.comboBox_IncreaseDecrease_Ordinal_Weekday.Enabled = true;

                    this.radioButton_IncreaseDecrease_MonthEnd.Enabled = true;

                    this.radioButton_IncreaseDecrease_MonthAndDay.Enabled = false;
                    this.comboBox_IncreaseDecrease_MonthAndDay_Month.Enabled = false;
                    this.comboBox_IncreaseDecrease_MonthAndDay_Day.Enabled = false;

                    this.radioButton_IncreaseDecrease_DateTime.Enabled = false;
                    this.dateTimePicker_IncreaseDecrease_DateTime.Enabled = false;
                    break;
                case "radioButton_IncreaseDecrease_Yearly":
                    this.radioButton_IncreaseDecrease_MonthAndDay.Checked = true;

                    this.radioButton_IncreaseDecrease_Weekday.Enabled = false;
                    this.comboBox_IncreaseDecrease_Weekday.Enabled = false;

                    this.radioButton_IncreaseDecrease_Day.Enabled = false;
                    this.comboBox_IncreaseDecrease_Day.Enabled = false;

                    this.radioButton_IncreaseDecrease_Ordinal.Enabled = false;
                    this.comboBox_IncreaseDecrease_Ordinal_Number.Enabled = false;
                    this.comboBox_IncreaseDecrease_Ordinal_Weekday.Enabled = false;

                    this.radioButton_IncreaseDecrease_MonthEnd.Enabled = false;

                    this.radioButton_IncreaseDecrease_MonthAndDay.Enabled = true;
                    this.comboBox_IncreaseDecrease_MonthAndDay_Month.Enabled = true;
                    this.comboBox_IncreaseDecrease_MonthAndDay_Day.Enabled = true;

                    this.radioButton_IncreaseDecrease_DateTime.Enabled = false;
                    this.dateTimePicker_IncreaseDecrease_DateTime.Enabled = false;
                    break;
                case "radioButton_IncreaseDecrease_OnlyOnce":
                    this.radioButton_IncreaseDecrease_DateTime.Checked = true;

                    this.radioButton_IncreaseDecrease_Weekday.Enabled = false;
                    this.comboBox_IncreaseDecrease_Weekday.Enabled = false;

                    this.radioButton_IncreaseDecrease_Day.Enabled = false;
                    this.comboBox_IncreaseDecrease_Day.Enabled = false;

                    this.radioButton_IncreaseDecrease_Ordinal.Enabled = false;
                    this.comboBox_IncreaseDecrease_Ordinal_Number.Enabled = false;
                    this.comboBox_IncreaseDecrease_Ordinal_Weekday.Enabled = false;

                    this.radioButton_IncreaseDecrease_MonthEnd.Enabled = false;

                    this.radioButton_IncreaseDecrease_MonthAndDay.Enabled = false;
                    this.comboBox_IncreaseDecrease_MonthAndDay_Month.Enabled = false;
                    this.comboBox_IncreaseDecrease_MonthAndDay_Day.Enabled = false;

                    this.radioButton_IncreaseDecrease_DateTime.Enabled = true;
                    this.dateTimePicker_IncreaseDecrease_DateTime.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void MakePayPatternForm_Activated(object sender, EventArgs e)
        {
            if (fNewPattern == true)
            {
                this.InitNewPattern();
            }
            else
            {
                this.SetRequiredPattern();
                this.SetTermOption();
                this.SetIncreaseDecreaseOption();
            }
        }

        private void InitNewPattern()
        {
            this.comboBox_Weekday.SelectedIndex = 0;
            this.comboBox_Day.SelectedIndex = 0;
            this.comboBox_Ordinal_Number.SelectedIndex = 0;
            this.comboBox_Ordinal_Weekday.SelectedIndex = 0;
            this.comboBox_MonthAndDay_Month.SelectedIndex = 0;
            this.comboBox_MonthAndDay_Day.SelectedIndex = 0;
            this.comboBox_InOrPay.SelectedIndex = 0;

            this.comboBox_IncreaseDecrease_Weekday.SelectedIndex = 0;
            this.comboBox_IncreaseDecrease_Day.SelectedIndex = 0;
            this.comboBox_IncreaseDecrease_Ordinal_Number.SelectedIndex = 0;
            this.comboBox_IncreaseDecrease_Ordinal_Weekday.SelectedIndex = 0;
            this.comboBox_IncreaseDecrease_MonthAndDay_Month.SelectedIndex = 0;
            this.comboBox_IncreaseDecrease_MonthAndDay_Day.SelectedIndex = 0;
            this.comboBox_IncreaseDecrease_InOrDe.SelectedIndex = 0;

            this.radioButton_Monthly.Checked = true;
            this.radioButton_Day.Checked = true;

            this.radioButton_IncreaseDecrease_Monthly.Checked = true;
            this.radioButton_IncreaseDecrease_Day.Checked = true;

            this.dateTimePicker_DateTime.Value = DateTime.Today;
        }

        private void SetRequiredPattern()
        {
            this.textBox_Name.Text = payPattern.RequiredPayPattern.Name;

            this.comboBox_Weekday.SelectedIndex = (int)payPattern.RequiredPayPattern.Weekday;
            this.comboBox_Day.SelectedIndex = payPattern.RequiredPayPattern.Day - 1;
            this.comboBox_Ordinal_Number.SelectedIndex = payPattern.RequiredPayPattern.OrdinalNumber - 1;
            this.comboBox_Ordinal_Weekday.SelectedIndex = (int)payPattern.RequiredPayPattern.Weekday;
            this.comboBox_MonthAndDay_Month.SelectedIndex = payPattern.RequiredPayPattern.Month - 1;
            this.comboBox_MonthAndDay_Day.SelectedIndex = payPattern.RequiredPayPattern.Day - 1;
            this.comboBox_InOrPay.SelectedIndex = (int)payPattern.RequiredPayPattern.IncomeOrPay;
            this.dateTimePicker_DateTime.Value = payPattern.RequiredPayPattern.DateTimeValue;

            switch (payPattern.RequiredPayPattern.Frequency)
            {
                case FREQUENCY.DAILY:
                    this.radioButton_Daily.Checked = true;
                    break;
                case FREQUENCY.WEEKLY:
                    this.radioButton_Weekly.Checked = true;
                    break;
                case FREQUENCY.MONTHLY:
                    this.radioButton_Monthly.Checked = true;
                    break;
                case FREQUENCY.YEARLY:
                    this.radioButton_Yearly.Checked = true;
                    break;
                case FREQUENCY.ONLY_ONCE:
                    this.radioButton_OnlyOnce.Checked = true;
                    break;
            }

            switch (payPattern.RequiredPayPattern.DayType)
            {
                case DAYTYPE.WEEKDAY:
                    this.radioButton_Weekday.Checked = true;
                    break;
                case DAYTYPE.DAY:
                    this.radioButton_Day.Checked = true;
                    break;
                case DAYTYPE.ORDINAL:
                    this.radioButton_Ordinal.Checked = true;
                    break;
                case DAYTYPE.MONTHEND:
                    this.radioButton_MonthEnd.Checked = true;
                    break;
                case DAYTYPE.MONTH_AND_DAY:
                    this.radioButton_MonthAndDay.Checked = true;
                    break;
                case DAYTYPE.DATETIME:
                    this.radioButton_DateTime.Checked = true;
                    break;
            }

            this.numericUpDown_Amount.Value = payPattern.RequiredPayPattern.Amount;
            this.comboBox_InOrPay.SelectedIndex = (int)payPattern.RequiredPayPattern.IncomeOrPay;
        }

        private void SetTermOption()
        {
            this.checkBox_Term.Checked = payPattern.TermOption.Enabled;
            if (payPattern.TermOption.Enabled == false)
            {
                return;
            }

            this.dateTimePicker_Term_From.Value = payPattern.TermOption.TermFrom;
            this.dateTimePicker_Term_To.Value = payPattern.TermOption.TermTo;
        }

        private void SetIncreaseDecreaseOption()
        {
            this.checkBox_IncreaseDecrease.Checked = payPattern.IncreaseOption.Enabled;
            if (payPattern.IncreaseOption.Enabled == false)
            {
                return;
            }

            this.comboBox_IncreaseDecrease_Weekday.SelectedIndex = (int)payPattern.IncreaseOption.Weekday;
            this.comboBox_IncreaseDecrease_Day.SelectedIndex = payPattern.IncreaseOption.Day - 1;
            this.comboBox_IncreaseDecrease_Ordinal_Number.SelectedIndex = payPattern.IncreaseOption.OrdinalNumber - 1;
            this.comboBox_IncreaseDecrease_Ordinal_Weekday.SelectedIndex = (int)payPattern.IncreaseOption.Weekday;
            this.comboBox_IncreaseDecrease_MonthAndDay_Month.SelectedIndex = payPattern.IncreaseOption.Month - 1;
            this.comboBox_IncreaseDecrease_MonthAndDay_Day.SelectedIndex = payPattern.IncreaseOption.Day - 1;
            this.comboBox_IncreaseDecrease_InOrDe.SelectedIndex = (int)payPattern.IncreaseOption.IncomeOrPay;
            this.dateTimePicker_IncreaseDecrease_DateTime.Value = payPattern.IncreaseOption.DateTimeValue;

            switch (payPattern.IncreaseOption.Frequency)
            {
                case FREQUENCY.DAILY:
                    this.radioButton_IncreaseDecrease_Daily.Checked = true;
                    break;
                case FREQUENCY.WEEKLY:
                    this.radioButton_IncreaseDecrease_Weekly.Checked = true;
                    break;
                case FREQUENCY.MONTHLY:
                    this.radioButton_IncreaseDecrease_Monthly.Checked = true;
                    break;
                case FREQUENCY.YEARLY:
                    this.radioButton_IncreaseDecrease_Yearly.Checked = true;
                    break;
                case FREQUENCY.ONLY_ONCE:
                    this.radioButton_IncreaseDecrease_OnlyOnce.Checked = true;
                    break;
            }

            switch (payPattern.IncreaseOption.DayType)
            {
                case DAYTYPE.WEEKDAY:
                    this.radioButton_IncreaseDecrease_Weekday.Checked = true;
                    break;
                case DAYTYPE.DAY:
                    this.radioButton_IncreaseDecrease_Day.Checked = true;
                    break;
                case DAYTYPE.ORDINAL:
                    this.radioButton_IncreaseDecrease_Ordinal.Checked = true;
                    break;
                case DAYTYPE.MONTHEND:
                    this.radioButton_IncreaseDecrease_MonthEnd.Checked = true;
                    break;
                case DAYTYPE.MONTH_AND_DAY:
                    this.radioButton_IncreaseDecrease_MonthAndDay.Checked = true;
                    break;
                case DAYTYPE.DATETIME:
                    this.radioButton_IncreaseDecrease_DateTime.Checked = true;
                    break;
            }

            this.numericUpDown_IncreaseDecrease_Amount.Value = payPattern.IncreaseOption.Amount;
            this.comboBox_IncreaseDecrease_InOrDe.SelectedIndex = (int)payPattern.IncreaseOption.IncomeOrPay;
        }
    }
}
