using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AccountBookSimu
{
    public partial class MainForm : Form
    {
        private DataManager _data;

        public MainForm()
        {
            InitializeComponent();

            _data = new DataManager();
        }

        private void InitControls()
        {
            this.listBox_PayPattern.Items.Clear();
            this.button_RemovePayPattern.Enabled = false;
            this.button_EditPayPattern.Enabled = false;
            this.button_Simulate.Enabled = false;
        }

        private void AddPayPattern()
        {
            MakePayPatternForm makePayPatternDialog = new MakePayPatternForm();
            foreach (string s in this.listBox_PayPattern.Items)
            {
                makePayPatternDialog.listedPayPatternNames.Add(s);
            }
            makePayPatternDialog.fNewPattern = true;
            DialogResult result = makePayPatternDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                PayPattern payPattern = makePayPatternDialog.payPattern;
                _data.AddPayPattern(payPattern);
                this.listBox_PayPattern.Items.Add(payPattern.RequiredPayPattern.Name);
                this.button_Simulate.Enabled = true;
            }
        }

        private void RemovePayPattern()
        {
            int iPattern = this.listBox_PayPattern.SelectedIndex;
            this.listBox_PayPattern.Items.RemoveAt(iPattern);
            _data.RemovePayPattern(iPattern);

            if (this.listBox_PayPattern.Items.Count == 0)
            {
                this.button_RemovePayPattern.Enabled = false;
                this.button_EditPayPattern.Enabled = false;
                this.button_Simulate.Enabled = false;
            }
        }

        private void EditPayPattern()
        {
            MakePayPatternForm makePayPatternDialog = new MakePayPatternForm();
            foreach (string s in this.listBox_PayPattern.Items)
            {
                makePayPatternDialog.listedPayPatternNames.Add(s);
            }
            makePayPatternDialog.payPattern = _data.GetPayPattern(this.listBox_PayPattern.SelectedIndex);
            makePayPatternDialog.fNewPattern = false;
            DialogResult result = makePayPatternDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                PayPattern payPattern = makePayPatternDialog.payPattern;
                _data.EditPayPattern(this.listBox_PayPattern.SelectedIndex, payPattern);
                this.listBox_PayPattern.Items[this.listBox_PayPattern.SelectedIndex] = payPattern.RequiredPayPattern.Name;
            }
        }

        private void NewFile()
        {
            this.listBox_PayPattern.Items.Clear();
            _data = new DataManager();
            this.dateTimePicker_From.Value = DateTime.Today;
            this.dateTimePicker_To.Value = DateTime.Today;
            this.button_RemovePayPattern.Enabled = false;
            this.button_EditPayPattern.Enabled = false;
            this.button_Simulate.Enabled = false;
        }

        private void Open()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TXTファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _data.OpenSettingFile(ofd.FileName);
                this.InitControls();
                for (int i = 0; i < _data.NPayPattern; i++)
                {
                    PayPattern pp = _data.GetPayPattern(i);
                    this.listBox_PayPattern.Items.Add(pp.RequiredPayPattern.Name);
                    this.dateTimePicker_From.Value = _data.SimulateFrom;
                    this.dateTimePicker_To.Value = _data.SimulateTo;
                }
                this.button_Simulate.Enabled = true;
            }
        }

        private void SaveAs()
        {
            if (this.dateTimePicker_From.Value > this.dateTimePicker_To.Value)
            {
                MessageBox.Show("期間の指定が不正です。シミュレーションの開始時期は終了時期以前に設定してください。");
                this.dateTimePicker_From.Focus();
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "新しいファイル.txt";
            sfd.Filter = "TXTファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
            sfd.Title = "保存先のファイルを選択してください";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _data.SetTerm(this.dateTimePicker_From.Value, this.dateTimePicker_To.Value);
                _data.ListedPayPatternNames = new List<string>();
                foreach (string s in this.listBox_PayPattern.Items)
                {
                    _data.ListedPayPatternNames.Add(s);
                }

                _data.SaveSettingFile(sfd.FileName);
            }
        }

        private void Simulate()
        {
            if (this.dateTimePicker_From.Value > this.dateTimePicker_To.Value)
            {
                MessageBox.Show("期間の指定が不正です。シミュレーションの開始時期は終了時期以前に設定してください。");
                this.dateTimePicker_From.Focus();
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "新しいファイル.csv";
            sfd.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            sfd.Title = "保存先のファイルを選択してください";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _data.SetTerm(this.dateTimePicker_From.Value, this.dateTimePicker_To.Value);
                _data.ListedPayPatternNames = new List<string>();
                foreach (string s in this.listBox_PayPattern.Items)
                {
                    _data.ListedPayPatternNames.Add(s);
                }

                ProgressForm progressForm = new ProgressForm(_data, sfd.FileName);
                progressForm.ShowDialog();
            }
        }

        private void button_AddPayPattern_Click(object sender, EventArgs e)
        {
            this.AddPayPattern();
        }

        private void button_RemovePayPattern_Click(object sender, EventArgs e)
        {
            this.RemovePayPattern();
        }

        private void button_EditPayPattern_Click(object sender, EventArgs e)
        {
            this.EditPayPattern();
        }

        private void button_Simulate_Click(object sender, EventArgs e)
        {
            this.Simulate();
        }

        private void button_SaveSetting_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        private void ToolStripMenuItem_FileOpen_Click(object sender, EventArgs e)
        {
            this.Open();
        }

        private void ToolStripMenuItem_Save_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ToolStripMenuItem_SaveAs_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        private void ToolStripMenuItem_Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStripMenuItem_AddPattern_Click(object sender, EventArgs e)
        {
            this.AddPayPattern();
        }

        private void ToolStripMenuItem_RemovePattern_Click(object sender, EventArgs e)
        {
            this.RemovePayPattern();
        }

        private void ToolStripMenuItem_EditPattern_Click(object sender, EventArgs e)
        {
            this.EditPayPattern();
        }

        private void ToolStripMenuItem_Simulate_Click(object sender, EventArgs e)
        {
            this.Simulate();
        }

        private void button_RemovePayPattern_EnabledChanged(object sender, EventArgs e)
        {
            this.ToolStripMenuItem_RemovePattern.Enabled = this.button_RemovePayPattern.Enabled;
        }

        private void button_EditPayPattern_EnabledChanged(object sender, EventArgs e)
        {
            this.ToolStripMenuItem_EditPattern.Enabled = this.button_EditPayPattern.Enabled;
        }

        private void button_Simulate_EnabledChanged(object sender, EventArgs e)
        {
            this.ToolStripMenuItem_Simulate.Enabled = this.button_Simulate.Enabled;
        }

        private void listBox_PayPattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox_PayPattern.Items.Count > 0)
            {
                this.button_RemovePayPattern.Enabled = true;
                this.button_EditPayPattern.Enabled = true;
            }
        }

        private void ToolStripMenuItem_NewFile_Click(object sender, EventArgs e)
        {
            this.NewFile();
        }
    }
}
