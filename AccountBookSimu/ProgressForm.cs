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
            //MessageBox.Show("_maxProgress:" + _maxProgress.ToString());

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
            //for (int progressValue = 0; (progressValue <= _maxProgress) && (_asyncMessenger.ApplicationState == STATE_APP.SIMULATING); progressValue = _asyncMessenger.IntMessage)
            //{
            //    this.progressBar_Simulation.Value = progressValue;
            //    this.label_Message.Text = "シミュってるなう。けーね！";
            //    this.label_Percent.Text = progressValue.ToString() + "/" + _maxProgress.ToString();
            //}
            //while (_asyncMessenger.ApplicationState == STATE_APP.FILE_SAVING)
            //{
            //    this.label_Message.Text = "ファイル保存中なう。けーね！";
            //    this.label_Percent.Text = "";
            //}

            //MessageBox.Show("シミュレーションが終了しますた。(´・ω・`)");
            //this.Close();


            simulationThread = new Thread(new ThreadStart(this.DoSimulation));
            updateProgressBarThread = new Thread(new ThreadStart(this.UpdateProgressBar));
            simulationThread.Start();
            updateProgressBarThread.Start();

            //updateProgressBarThread.Join();

            //MessageBox.Show("シミュったお。(´・ω・`)");
            //this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            //_data.CancelSimulationFlag = true;
            //Thread.Sleep(1000); // シミュレーションを行っているスレッドが終了するのを待つ。
            //_data.CancelSimulationFlag = false;

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
            //Trace.TraceInformation("begin InvokeUpdateProgressBar");
            //switch (_asyncMessenger.ApplicationState)
            //{
            //    case STATE_APP.SIMULATING:
                    int progressValue = _asyncMessenger.IntMessage;
                    this.progressBar_Simulation.Value = progressValue;
                    this.label_Message.Text = "シミュり中。けーねっ！";
                    this.label_Percent.Text = progressValue.ToString() + "/" + _maxProgress.ToString();
            //        break;
            //    case STATE_APP.FILE_SAVING:
            //        this.progressBar_Simulation.Value = _maxProgress;
            //        this.label_Message.Text = "ファイル保存中。けーねっ！";
            //        this.label_Percent.Text = "";
            //        break;
            //    default:
            //        // 何もしない。
            //        break;
            //}
            //Trace.TraceInformation("end InvokeUpdateProgressBar");
        }

        private void InvokeCloseDialog()
        {
            this.Close();
        }
    }
}
