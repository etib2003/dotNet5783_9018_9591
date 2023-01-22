using DocumentFormat.OpenXml.Bibliography;
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
        //private bool isTimerRun;
        BackgroundWorker backgroundWorker;
        BackgroundWorker barWorker;
        //private bool finish = false;
        private bool canClose = false;

        public SimulatorWindow()
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
            //isTimerRun = true;
            backgroundWorker.RunWorkerAsync();

            //עבור bar:
            barWorker = new BackgroundWorker();
            barWorker.DoWork += BarWorker_DoWork;
            barWorker.ProgressChanged += BarWorker_ProgressChanged;
            barWorker.RunWorkerCompleted += BarWorker_RunWorkerCompleted;
            barWorker.WorkerReportsProgress = true;
            barWorker.WorkerSupportsCancellation = true;
        }

        private void SimulatorWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = !canClose;
        }

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

        private void BarWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            BarText = (progress + "%");
            ProgressBarValue=progress;
        }

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

        private void reportFunc(object sender, EventArgs e)
        {
            ProgressChangedEventArgs p;
            if (e.GetType() == typeof(ReportArgs) && (e as ReportArgs)!.delay != -1)
            {
                Tuple<int, BO.Order> tuple = new((e as ReportArgs)!.delay, (e as ReportArgs)!.order);
                p = new(1, tuple);
                BackgroundWorker_ProgressChanged(sender, p);
            }
            else if ((e as ReportArgs)!.delay == -1)
            {
                string massege = (e as ReportArgs)!.massege;
                if (massege == "Finish order progress")
                {
                    p = new(2, massege);
                    BackgroundWorker_ProgressChanged(sender, p);
                }
                else if (massege == "Finish simulation")
                {
                    p = new(3, massege);
                    BackgroundWorker_ProgressChanged(sender, p);
                }
            }
            else
            {
                p = new(0, e);
                BackgroundWorker_ProgressChanged(sender, p);
            }
        }

        //עבור הפעלת ההדמיה:
        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            //רשמו מתודות משקיפות (ראו בהמשך) לאירועי הסימולטור
            Simulator.Simulator.s_Report += reportFunc!;
            Simulator.Simulator.s_StopSimulator += cancelAsync;
            Simulator.Simulator.simulatorActivate();
            while (!backgroundWorker.CancellationPending)
            {
                backgroundWorker.ReportProgress(0);
                Thread.Sleep(1000);
            }
        }

        private void cancelAsync()
        {
            backgroundWorker.CancelAsync();
        }

        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0: //מגיע מפי אל-התקדמות השעון של הדמיה כנ"ל
                    timerText = timerStopWatch.Elapsed.ToString().Substring(0, 8);
                    break;
                case 1://עדכון אובייקט הישות שבהדמיה-מגיע עם פרטי ההזמנה וכו'
                    Dispatcher.Invoke(() =>
                    {
                        TupleBind = e.UserState as Tuple<int, BO.Order>;
                        if ((e.UserState as Tuple<int, BO.Order>)!.Item2.Status.ToString() == "confirmed")
                        {
                            CurrentStatus= $"Current status: confirmed, {(e.UserState as Tuple<int, BO.Order>)!.Item2.OrderDate}";
                            FutureStatus= $"Future status: shipped, {DateTime.Now.AddSeconds((e.UserState as Tuple<int, BO.Order>)!.Item1)}";
                        }
                        else
                        {
                            CurrentStatus = $"Current status: shipped, {(e.UserState as Tuple<int, BO.Order>)!.Item2.ShipDate}";
                            FutureStatus = $"Future status: provided, {DateTime.Now.AddSeconds((e.UserState as Tuple<int, BO.Order>)!.Item1)}";
                        }
                        if (barWorker.IsBusy != true)
                            // Start the asynchronous operation.
                            barWorker.RunWorkerAsync((e.UserState as Tuple<int, BO.Order>)!.Item1); //צריך להיות מותאם לזמן השינה של הסימולטור בין הזמנה להזמנה

                    });
                    break;
                case 2: //סיים לטפל בהזמנה/עדכון מצב אובייקט (אם נדרש)
                    Dispatcher.Invoke(() =>
                    {
                    });
                    break;
                case 3: //סיים את הסימולאציה/לבונוס: עדכון התקדמות (עבור progress bar)
                    Dispatcher.Invoke(() =>
                    {
                        FinishText = "There are no orders left that are not updated - we have sent them all :)";
                    });

                    break;
                default:
                    break;
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.Simulator.s_Report -= reportFunc!;
            Simulator.Simulator.s_StopSimulator -= cancelAsync;

            timerStopWatch.Stop();
            //isTimerRun = false;
            barWorker.CancelAsync();

            canClose = true;
            Close();
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.stopSim();
        }
    }
}
