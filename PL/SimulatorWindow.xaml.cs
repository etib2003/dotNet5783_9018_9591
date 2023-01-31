using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Simulator.ReportArgs;


namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    /// 

    public partial class SimulatorWindow : Window
    {
        public string timerText
        {
            get { return (string)GetValue(timerTextProperty); }
            set { SetValue(timerTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for timerText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty timerTextProperty =
            DependencyProperty.Register("timerText", typeof(string), typeof(SimulatorWindow));

        public string BarText
        {
            get { return (string)GetValue(BarTextProperty); }
            set { SetValue(BarTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BarText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BarTextProperty =
        DependencyProperty.Register("BarText", typeof(string), typeof(SimulatorWindow));

        public int ProgressBarValue
        {
            get { return (int)GetValue(progressBarProperty); }
            set { SetValue(progressBarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty progressBarProperty =
            DependencyProperty.Register("ProgressBarValue", typeof(int), typeof(SimulatorWindow));

        public string FinishText
        {
            get { return (string)GetValue(FinishTextProperty); }
            set { SetValue(FinishTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FinishText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FinishTextProperty =
            DependencyProperty.Register("FinishText", typeof(string), typeof(SimulatorWindow));

        public Tuple<int, BO.Order> TupleBind
        {
            get { return (Tuple<int, BO.Order>)GetValue(TupleBindProperty); }
            set { SetValue(TupleBindProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TupleBindProperty =
            DependencyProperty.Register("TupleBind", typeof(Tuple<int, BO.Order>), typeof(SimulatorWindow));

        public string CurrentStatus
        {
            get { return (string)GetValue(CurrentStatusProperty); }
            set { SetValue(CurrentStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentStatusProperty =
            DependencyProperty.Register("CurrentStatus", typeof(string), typeof(SimulatorWindow));

        public string FutureStatus
        {
            get { return (string)GetValue(FutureStatusProperty); }
            set { SetValue(FutureStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FutureStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FutureStatusProperty =
            DependencyProperty.Register("FutureStatus", typeof(string), typeof(SimulatorWindow));

        private Stopwatch timerStopWatch;
        BackgroundWorker backgroundWorker;
        BackgroundWorker barWorker;
        private bool canClose = false;

        private Action action;

        public SimulatorWindow(Action isSimActive)
        {
            InitializeComponent();

            Closing += SimulatorWindow_Closing!;

            timerStopWatch = new Stopwatch();
            timerStopWatch.Start();

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.RunWorkerAsync();

            //bar:
            barWorker = new BackgroundWorker();
            barWorker.DoWork += BarWorker_DoWork;
            barWorker.ProgressChanged += BarWorker_ProgressChanged;
            barWorker.RunWorkerCompleted += BarWorker_RunWorkerCompleted;
            barWorker.WorkerReportsProgress = true;
            barWorker.WorkerSupportsCancellation = true;

            this.action = isSimActive;
            action();
        }

        private void SimulatorWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = !canClose;
        }
        /// <summary>
        /// Handles the progress of the bar
        /// </summary>

        private void BarWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            int length = (int)e.Argument!;
            for (int i = 1; i <= length; i++)
            {
                if (barWorker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                barWorker.ReportProgress(i * 100 / length);
                System.Threading.Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Handles the progress of the bar in view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            BarText = (progress + "%");
            ProgressBarValue=progress;
        }

        /// <summary>
        /// Finish bar progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                BarText = "Canceled!";
            }
            else if (e.Error != null)
            {
                BarText = "Error: " + e.Error.Message; //Exception Message
            }
        }

        /// <summary>
        /// Acts according to the report received from the simulator
        /// </summary>
        private void reportFunc(object sender, EventArgs e)
        {
            try
            {
                // Check if the event argument is of type ReportArgs and the delay property is not -1
                if (e.GetType() == typeof(ReportArgs) && (e as ReportArgs)!.delay != -1)
                {
                    Tuple<int, BO.Order> tuple = new((e as ReportArgs)!.delay, (e as ReportArgs)!.order);
                    // Report progress with the tuple as the user state
                    backgroundWorker.ReportProgress(1, tuple);
                }
                // Check if the delay property of the ReportArgs object is -1
                else if ((e as ReportArgs)!.delay == -1)
                {
                    // Get the massege property of the ReportArgs object
                    string massege = (e as ReportArgs)!.massege;
                    if (massege == "Finish order progress")
                    {
                        // Report progress with the massege as the user state
                        backgroundWorker.ReportProgress(2, massege);
                    }
                    else if (massege == "Finish simulation")
                    {
                        // Report progress with the massege as the user state
                        backgroundWorker.ReportProgress(3, massege);
                    }
                }
                // If the event argument is not of type ReportArgs or the delay property is -1
                else
                {
                    // Report progress with the event argument as the user state
                    backgroundWorker.ReportProgress(0, e);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Erorr, Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Activates the simulator operation
        /// </summary>
        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            try
            {
                Simulator.Simulator.s_Report += reportFunc!;
                Simulator.Simulator.s_StopSimulator += cancelAsync;
                Simulator.Simulator.simulatorActivate();
                while (!backgroundWorker.CancellationPending)
                {
                    backgroundWorker.ReportProgress(0);
                    Thread.Sleep(1000);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Erorr, Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancelAsync()
        {
            backgroundWorker.CancelAsync();
        }

        /// <summary>
        /// Updates the display in the simulator window
        /// </summary>

        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            try
            {
                switch (e.ProgressPercentage)
                {
                    case 0:
                        timerText = timerStopWatch.Elapsed.ToString().Substring(0, 8);
                        break;
                    case 1: //Updates order details in the display
                        FinishText = "";
                        TupleBind = e.UserState as Tuple<int, BO.Order>;
                        if ((e.UserState as Tuple<int, BO.Order>)!.Item2.Status.ToString() == "confirmed")
                        {
                            CurrentStatus = $"Current status: confirmed, {TupleBind!.Item2.OrderDate}";
                            FutureStatus = $"Future status: shipped, {DateTime.Now.AddSeconds(TupleBind!.Item1)}";
                        }
                        else
                        {
                            CurrentStatus = $"Current status: shipped, {TupleBind!.Item2.ShipDate}";
                            FutureStatus = $"Future status: provided, {DateTime.Now.AddSeconds(TupleBind!.Item1)}";
                        }
                        if (barWorker.IsBusy != true)
                            // Start the asynchronous operation.
                            barWorker.RunWorkerAsync(TupleBind!.Item1);
                        break;
                    case 2:
                        if(TupleBind!=null)
                                FinishText = $"Finish Order number {TupleBind.Item2.Id}";
                        break;
                    case 3:
                        FinishText = "There are no orders left that are not updated - we have sent them all :)";
                        break;
                    default:
                        break;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Erorr, Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Completes the simulator operation
        /// </summary>
        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.Simulator.s_Report -= reportFunc!;
            Simulator.Simulator.s_StopSimulator -= cancelAsync;

            timerStopWatch.Stop();
            barWorker.CancelAsync();
            action();

            canClose = true;
            Close();
        }

        /// <summary>
        /// Helps the simulator by pressing the stop button
        /// </summary>
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.stopSim();
        }
    }
}
