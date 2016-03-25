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
    public partial class MainForm : Form
    {
        private DataManager _data;

        public MainForm()
        {
            InitializeComponent();

            _data = new DataManager();
        }

        private void button_AddPayPattern_Click(object sender, EventArgs e)
        {
            MakePayPatternForm makePayPatternDialog = new MakePayPatternForm();
            foreach (string s in this.listBox_PayPattern.Items)
            {
                makePayPatternDialog.listedPayPatternNames.Add(s);
            }

            DialogResult result = makePayPatternDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                PayPattern payPattern = makePayPatternDialog.payPattern;
                _data.AddPayPattern(payPattern);
                this.listBox_PayPattern.Items.Add(payPattern.RequiredPayPattern.Name);
                this.button_Simulate.Enabled = true;
            }
        }

        private void button_RemovePayPattern_Click(object sender, EventArgs e)
        {
            // this.button_Simulate.Enabled = false;
        }

        private void button_EditPayPattern_Click(object sender, EventArgs e)
        {

        }

        private void button_Simulate_Click(object sender, EventArgs e)
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

                _data.DoSimulation();
                _data.SaveFile(sfd.FileName);
            }
        }

        private void button_SaveSetting_Click(object sender, EventArgs e)
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

        private void ToolStripMenuItem_FileOpen_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_Save_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_SaveAs_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_Quit_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_AddPattern_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_RemovePattern_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_EditPattern_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_Simulate_Click(object sender, EventArgs e)
        {

        }
    }
}
