using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountBookSimu
{
    public partial class MakePayPatternForm : Form
    {
        public PayPattern payPattern;
        public List<string> listedPayPatternNames;

        public MakePayPatternForm()
        {
            InitializeComponent();

            this.listedPayPatternNames = new List<string>();

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

            this.ReadInputs();
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

            if (this.IsNewName() == false)
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
        private void ReadInputs()
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
    }
}
