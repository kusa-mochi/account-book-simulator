using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace AccountBookSimu
{
    public partial class ProgressForm : Form
    {
        private DataManager _data;
        private string _filePath;
        private int _maxProgress;
        private AsyncMessenger _asyncMessenger;
        private Thread simulationThread;
        private Thread updateProgressBarThread;
        private int _mouseX;
        private int _mouseY;

        delegate void updateProgressDelegate();

        public ProgressForm(DataManager data, string filePath)
        {
            InitializeComponent();

            if (
                (data == null) ||
                (filePath == null)
                )
            {
                throw new ArgumentNullException();
            }

            _data = data;
            _filePath = filePath;
            _maxProgress = 0;
            _asyncMessenger = new AsyncMessenger();

            this.InitControls();
        }

        private void InitControls()
        {
            DateTime from = _data.SimulateFrom;
            DateTime to = _data.SimulateTo;
            TimeSpan span = to - from;
            _maxProgress = span.Days;

            this.progressBar_Simulation.Minimum = 0;
            this.progressBar_Simulation.Maximum = _maxProgress;
            this.progressBar_Simulation.Value = 0;
            this.label_Message.Text = "シミュってるなう。けーね！";
            this.label_Percent.Text = "0/" + _maxProgress.ToString();
            _asyncMessenger.ApplicationState = STATE_APP.SIMULATING;
            _data.asyncMessenger = _asyncMessenger;
        }

        private void DoSimulation()
        {
            Trace.TraceInformation("begin _data.DoSimulation");
            _data.DoSimulation();
            Trace.TraceInformation("begin _data.SaveFile");
            _data.SaveFile(_filePath);
            Trace.TraceInformation("end _data.SaveFile");
        }

        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            simulationThread = new Thread(new ThreadStart(this.DoSimulation));
            updateProgressBarThread = new Thread(new ThreadStart(this.UpdateProgressBar));
            simulationThread.Start();
            updateProgressBarThread.Start();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            simulationThread.Abort();
            updateProgressBarThread.Abort();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void UpdateProgressBar()
        {
            Trace.TraceInformation("begin UpdateProgressBar");
            while (
                (_asyncMessenger.ApplicationState == STATE_APP.SIMULATING) ||
                (_asyncMessenger.ApplicationState == STATE_APP.FILE_SAVING)
                )
            {
                //Trace.TraceInformation("  app state:" + ((int)_asyncMessenger.ApplicationState).ToString());
                this.Invoke(new updateProgressDelegate(this.InvokeUpdateProgressBar));
            }

            MessageBox.Show("シミュったお。(´・ω・`)");
            Trace.TraceInformation("end UpdateProgressBar");
            this.Invoke(new updateProgressDelegate(this.InvokeCloseDialog));
        }

        private void InvokeUpdateProgressBar()
        {
            Trace.TraceInformation("begin InvokeUpdateProgressBar");
            int progressValue = _asyncMessenger.IntMessage;
            this.progressBar_Simulation.Value = progressValue;
            this.label_Message.Text = "シミュり中。けーねっ！";
            this.label_Percent.Text = progressValue.ToString() + "/" + _maxProgress.ToString();
            Trace.TraceInformation("end InvokeUpdateProgressBar");
        }

        private void InvokeCloseDialog()
        {
            this.Close();
        }

        //private void WindowClick(MouseEventArgs e)
        //{
        //    //if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
        //    //{
        //    //    _mouseX = e.X;
        //    //    _mouseY = e.Y;
        //    //}
        //}

        //private void WindowMove(MouseEventArgs e)
        //{
        //    //if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
        //    //{
        //    //    this.Left += e.X - _mouseX;
        //    //    this.Top += e.Y - _mouseY;
        //    //}
        //}

        //private void ProgressForm_MouseDown(object sender, MouseEventArgs e)
        //{
        //    this.WindowClick(e);
        //}

        //private void ProgressForm_MouseMove(object sender, MouseEventArgs e)
        //{
        //    this.WindowMove(e);
        //}

        //private void label_Message_MouseDown(object sender, MouseEventArgs e)
        //{
        //    this.WindowClick(e);
        //}

        //private void label_Message_MouseMove(object sender, MouseEventArgs e)
        //{
        //    this.WindowMove(e);
        //}

        //private void label_Percent_MouseDown(object sender, MouseEventArgs e)
        //{
        //    this.WindowClick(e);
        //}

        //private void label_Percent_MouseMove(object sender, MouseEventArgs e)
        //{
        //    this.WindowMove(e);
        //}

        //private void progressBar_Simulation_MouseDown(object sender, MouseEventArgs e)
        //{
        //    this.WindowClick(e);
        //}

        //private void progressBar_Simulation_MouseMove(object sender, MouseEventArgs e)
        //{
        //    this.WindowMove(e);
        //}
    }
}
