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

        //public string BarText
        //{
        //    get { return (string)GetValue(BarTextProperty); }
        //    set { SetValue(BarTextProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for BarText.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty BarTextProperty =
        //DependencyProperty.Register("BarText", typeof(string), typeof(SimulatorWindow));

        private Stopwatch timerStopWatch;
        private bool isTimerRun;
        BackgroundWorker backgroundWorker;
        BackgroundWorker barWorker;
        private bool finish=false;
        private bool canClose = false;

        public SimulatorWindow()
        {
            InitializeComponent();

            Closing += SimulatorWindow_Closing;

            //this.Closing += (s, e) =>
            //{
            //    e.Cancel = true;
            //};

            timerStopWatch = new Stopwatch();
            timerStopWatch.Start();
            
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            isTimerRun = true;
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
            int length = (int)e.Argument;
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
            barTB.Text = (progress + "%");
            resultProgressBar.Value = progress;

        }
        private void BarWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                 barTB.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                 barTB.Text = "Error: " + e.Error.Message; //Exception Message
            }
        }

        private void reportFunc(object sender, EventArgs e)
        {
            ProgressChangedEventArgs p;
            if (e.GetType() == typeof(ReportArgs) && (e as ReportArgs).delay != -1)
            {
                Tuple<int, BO.Order> tuple = new((e as ReportArgs).delay, (e as ReportArgs).order);
                p = new(1, tuple);
                BackgroundWorker_ProgressChanged(sender, p);
            }
            else if ((e as ReportArgs).delay == -1)
            {
                string massege = (e as ReportArgs).massege;
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
            Simulator.Simulator.s_Report += reportFunc;
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
                    timerTextBlock.Text = timerStopWatch.Elapsed.ToString().Substring(0, 8);
                    break;
                case 1://עדכון אובייקט הישות שבהדמיה-מגיע עם פרטי ההזמנה וכו'
                    Dispatcher.Invoke(() =>
                    {
                        idText.Text = $"Order number in processing: {(e.UserState as Tuple<int, BO.Order>).Item2.Id}";
                        if ((e.UserState as Tuple<int, BO.Order>).Item2.Status.ToString() == "confirmed")
                        {
                            oldStatusText.Text = $"Current status: confirmed, {(e.UserState as Tuple<int, BO.Order>).Item2.OrderDate}";
                            newStatusText.Text = $"Future status: shipped, {DateTime.Now.AddSeconds((e.UserState as Tuple<int, BO.Order>).Item1)}";
                        }
                        else
                        {
                            oldStatusText.Text = $"Current status: shipped, {(e.UserState as Tuple<int, BO.Order>).Item2.ShipDate}";
                            newStatusText.Text = $"Future status: provided, {DateTime.Now.AddSeconds((e.UserState as Tuple<int, BO.Order>).Item1)}";
                        }
                        estimateTime.Text = $"Estimate time: {(e.UserState as Tuple<int, BO.Order>).Item1.ToString()} seconds";
                        if (barWorker.IsBusy != true)
                            // Start the asynchronous operation.
                            barWorker.RunWorkerAsync((e.UserState as Tuple<int, BO.Order>).Item1); //צריך להיות מותאם לזמן השינה של הסימולטור בין הזמנה להזמנה

                    });
                    break;
                case 2: //סיים לטפל בהזמנה/עדכון מצב אובייקט (אם נדרש)
                    Dispatcher.Invoke(() =>
                    {
                        //finishOrder.Text = $"{(e.UserState as String)}";
                        //if (finish)
                        //{

                        //    timerStopWatch.Stop();
                        //    isTimerRun = false;
                        //    Simulator.Simulator.stopSim();
                        //    barWorker.CancelAsync();
                        //    Simulator.Simulator.Report -= reportFunc;


                        //    this.Closing += (s, e) =>
                        //    {
                        //        e.Cancel = false;
                        //    };
                        //}
                    });
                    break;
                case 3: //סיים את הסימולאציה/לבונוס: עדכון התקדמות (עבור progress bar)
                    Dispatcher.Invoke(() =>
                    {
                        finishOrder.Text = "There are no orders left that are not updated - we have sent them all :)";
                    });

                    break;
                default:
                    break;
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.Simulator.s_Report -= reportFunc;
            Simulator.Simulator.s_StopSimulator -= cancelAsync;
          
            timerStopWatch.Stop();
            isTimerRun = false;
            barWorker.CancelAsync();

            canClose = true;
            Close();
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
                Simulator.Simulator.stopSim();
        }
    }

    //public partial class SimulatorWindow : Window
    //{

    //    public string timerText
    //    {
    //        get { return (string)GetValue(timerTextProperty); }
    //        set { SetValue(timerTextProperty, value); }
    //    }

    //    // Using a DependencyProperty as the backing store for timerText.  This enables animation, styling, binding, etc...
    //    public static readonly DependencyProperty timerTextProperty =
    //        DependencyProperty.Register("timerText", typeof(string), typeof(SimulatorWindow) );

    //    public string BarText
    //    {
    //        get { return (string )GetValue(BarTextProperty); }
    //        set { SetValue(BarTextProperty, value); }
    //    }

    //    // Using a DependencyProperty as the backing store for BarText.  This enables animation, styling, binding, etc...
    //    public static readonly DependencyProperty BarTextProperty =
    //        DependencyProperty.Register("BarText", typeof(string), typeof(SimulatorWindow));

    //    private Stopwatch timerStopWatch;
    //    private bool isTimerRun;
    //    BackgroundWorker timerWorker;
    //    BackgroundWorker backgroundWorker; //
    //    BackgroundWorker barWorker; //
    //    static readonly Random random = new Random(); //

    //    public SimulatorWindow()
    //    {
    //        InitializeComponent();
    //        this.Closing += (s, e) =>
    //        {
    //            e.Cancel = true;
    //        };


    //        //עבור שעון עצר:
    //        timerStopWatch = new Stopwatch();
    //        timerStopWatch.Start();

    //        timerWorker = new BackgroundWorker();
    //        timerWorker.DoWork += TimerWorker_DoWork;
    //        timerWorker.ProgressChanged += TimerWorker_ProgressChanged;
    //        timerWorker.WorkerReportsProgress = true;

    //        isTimerRun = true;
    //        timerWorker.RunWorkerAsync();


    //        //עבור הפעלת ההדמיה:
    //        backgroundWorker = new BackgroundWorker();
    //        backgroundWorker.DoWork += BackgroundWorker_DoWork;
    //        backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
    //        //barWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
    //        backgroundWorker.WorkerReportsProgress = true;
    //        backgroundWorker.WorkerSupportsCancellation = true;
    //        //Simulator.Simulator.report += reportFunc;
    //        //backgroundWorker.RunWorkerAsync();

    //        //עבור bar:
    //        barWorker = new BackgroundWorker();
    //        barWorker.DoWork += BarWorker_DoWork;
    //        barWorker.ProgressChanged += BarWorker_ProgressChanged;
    //        barWorker.RunWorkerCompleted += BarWorker_RunWorkerCompleted;
    //        barWorker.WorkerReportsProgress = true;
    //        barWorker.WorkerSupportsCancellation = true;
    //        if (barWorker.IsBusy != true)
    //            // Start the asynchronous operation.
    //            barWorker.RunWorkerAsync(random.Next(3, 11)); //צריך להיות מותאם לזמן השינה של הסימולטור בין הזמנה להזמנה

    //    }


    //    private void BarWorker_DoWork(object? sender, DoWorkEventArgs e)
    //    {
    //        Stopwatch stopwatch = new Stopwatch();
    //        stopwatch.Start();

    //        // BackgroundWorker worker = sender as BackgroundWorker;
    //        int length = (int)e.Argument;
    //        for (int i = 1; i <= length; i++)
    //        {
    //            if (barWorker.CancellationPending == true)
    //            {
    //                e.Cancel = true;
    //                e.Result = stopwatch.ElapsedMilliseconds; // Unnecessary
    //                break;
    //            }
    //            else
    //            {
    //                // Perform a time consuming operation and report progress.
    //                System.Threading.Thread.Sleep(1000);
    //                barWorker.ReportProgress(i * 100 / length);
    //            }
    //        }
    //        e.Result = stopwatch.ElapsedMilliseconds;

    //    }
    //    private void BarWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    //    {
    //        int progress = e.ProgressPercentage;
    //        barTB.Text = (progress + "%");
    //        resultProgressBar.Value = progress;

    //    }
    //    private void BarWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    //    {
    //        if (e.Cancelled == true)
    //        {
    //            // e.Result throw System.InvalidOperationException
    //            barTB.Text = "Canceled!";
    //        }
    //        else if (e.Error != null)
    //        {
    //            // e.Result throw System.Reflection.TargetInvocationException
    //            barTB.Text = "Error: " + e.Error.Message; //Exception Message
    //        }
    //        else
    //        {
    //            long result = (long)e.Result;
    //            if (result < 1000)
    //                barTB.Text = "Done after " + result + " ms.";
    //            else
    //                barTB.Text = "Done after " + result / 1000 + " sec.";
    //        }
    //    }

    //    private void reportFunc(object obj)
    //    {
    //        if (obj.GetType() == typeof(string) && obj.ToString() == "finish order progress")
    //        {
    //            //e.Percentage = 2;

    //        }
    //    }

    //    //עבור הפעלת ההדמיה:
    //    private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
    //    {
    //        //רשמו מתודות משקיפות (ראו בהמשך) לאירועי הסימולטור

    //        //then
    //        Simulator.Simulator.simulatorActivate();
    //        while (backgroundWorker.CancellationPending != false)
    //        {
    //            timerWorker.ReportProgress(1);
    //            Thread.Sleep(1000);
    //        }
    //    }
    //    private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    //    {
    //        switch (e.ProgressPercentage)
    //        {
    //            case 0: //מגיע מפי אל-התקדמות השעון של הדמיה כנ"ל

    //                break;
    //            case 1://עדכון אובייקט הישות שבהדמיה-מגיע עם פרטי ההזמנה וכו'
    //                var details = e.UserState;
    //                break;
    //            case 2: //סיים לטפל בהזמנה/עדכון מצב אובייקט (אם נדרש)
    //                break;
    //            case 3: //סיים את הסימולאציה/לבונוס: עדכון התקדמות (עבור progress bar)
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //    private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    //    {
    //        //בטלו את רישום מתודות ההשקפה לאירועי סימולטור
    //        //השלימו את עדכוני התצוגה ו-\או נתוני התצוגה בהתאם לדרישות והצרכים שלכם (למשל סגירת חלון ההדמיה לבסוף)

    //    }



    //    //עבור שעון עצר:
    //    private void TimerWorker_DoWork(object sender, DoWorkEventArgs e)
    //    {
    //        while (isTimerRun)
    //        {
    //            timerWorker.ReportProgress(1);
    //            Thread.Sleep(1000);
    //        }
    //    }
    //    private void TimerWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    //    {
    //        string timerText = timerStopWatch.Elapsed.ToString();
    //        timerText = timerText.Substring(0, 8);
    //        this.timerTextBlock.Text = timerText;
    //    }
    //    //זה לא אמור להיות ככה לדעתי
    //    private void Stop_Click(object sender, RoutedEventArgs e)
    //    {
    //        //Simulator.Simulator.stopSim(); //עבור עצירת ההדמיה:

    //        if (isTimerRun)         //עבור שעון עצר:
    //        {
    //            timerStopWatch.Stop();
    //            isTimerRun = false;
    //            this.Closing += (s, e) =>
    //            {
    //                e.Cancel = false;
    //            };
    //        }

    //        if (barWorker.WorkerSupportsCancellation == true)        //עבור bar:

    //            // Cancel the asynchronous operation.
    //            barWorker.CancelAsync();
    //    }
    //}
}
