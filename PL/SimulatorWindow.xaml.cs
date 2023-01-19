﻿using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        private Stopwatch timerStopWatch;
        private bool isTimerRun;
        BackgroundWorker timerWorker;
        BackgroundWorker backgroundWorker;
        BackgroundWorker barWorker;
        static readonly Random random = new Random();

        public SimulatorWindow()
        {
            InitializeComponent();
            this.Closing += (s, e) =>
            {
                e.Cancel = true;
            };


            //עבור שעון עצר:
            timerStopWatch = new Stopwatch();
            timerWorker = new BackgroundWorker();
            timerWorker.DoWork += TimerWorker_DoWork;
            timerWorker.ProgressChanged += TimerWorker_ProgressChanged;
            //to add RunComplete....
            timerWorker.WorkerReportsProgress = true;
            timerStopWatch.Restart();
            isTimerRun = true;
            timerWorker.RunWorkerAsync();

            //עבור הפעלת ההדמיה:
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            //barWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            //Simulator.Simulator.report += reportFunc;
            //backgroundWorker.RunWorkerAsync();

            //עבור bar:
            barWorker = new BackgroundWorker();
            barWorker.DoWork += BarWorker_DoWork;
            barWorker.ProgressChanged += BarWorker_ProgressChanged;
            barWorker.RunWorkerCompleted += BarWorker_RunWorkerCompleted;
            barWorker.WorkerReportsProgress = true;
            barWorker.WorkerSupportsCancellation = true;
            if (barWorker.IsBusy != true)
                // Start the asynchronous operation.
                barWorker.RunWorkerAsync(random.Next(3, 11)); //צריך להיות מותאם לזמן השינה של הסימולטור בין הזמנה להזמנה

        }

 
        private void BarWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // BackgroundWorker worker = sender as BackgroundWorker;
            int length = (int)e.Argument;
            for (int i = 1; i <= length; i++)
            {
                if (barWorker.CancellationPending == true)
                {
                    e.Cancel = true;
                    e.Result = stopwatch.ElapsedMilliseconds; // Unnecessary
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(1000);
                    barWorker.ReportProgress(i * 100 / length);
                }
            }
            e.Result = stopwatch.ElapsedMilliseconds;

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
                // e.Result throw System.InvalidOperationException
                barTB.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                // e.Result throw System.Reflection.TargetInvocationException
                barTB.Text = "Error: " + e.Error.Message; //Exception Message
            }
            else
            {
                long result = (long)e.Result;
                if (result < 1000)
                    barTB.Text = "Done after " + result + " ms.";
                else
                    barTB.Text = "Done after " + result / 1000 + " sec.";
            }
        }

        private void reportFunc(object obj)
        {
            if (obj.GetType() == typeof(string) && obj.ToString() == "finish order progress")
            {
                //e.Percentage = 2;

            }
        }

        //עבור הפעלת ההדמיה:
        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            //רשמו מתודות משקיפות (ראו בהמשך) לאירועי הסימולטור

            //then
            Simulator.Simulator.startSimulator();
            while (backgroundWorker.CancellationPending != false)
            {
                timerWorker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0: //מגיע מפי אל-התקדמות השעון של הדמיה כנ"ל

                    break;
                case 1://עדכון אובייקט הישות שבהדמיה-מגיע עם פרטי ההזמנה וכו'
                    var details = e.UserState;
                    break;
                case 2: //סיים לטפל בהזמנה/עדכון מצב אובייקט (אם נדרש)
                    break;
                case 3: //סיים את הסימולאציה/לבונוס: עדכון התקדמות (עבור progress bar)
                    break;
                default:
                    break;
            }
        }
        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            //בטלו את רישום מתודות ההשקפה לאירועי סימולטור
            //השלימו את עדכוני התצוגה ו-\או נתוני התצוגה בהתאם לדרישות והצרכים שלכם (למשל סגירת חלון ההדמיה לבסוף)

        }



        //עבור שעון עצר:
        private void TimerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerWorker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        private void TimerWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timerText = timerStopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            this.timerTextBlock.Text = timerText;
        }
        //זה לא אמור להיות ככה לדעתי
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            //Simulator.Simulator.stopSim(); //עבור עצירת ההדמיה:

            if (isTimerRun)         //עבור שעון עצר:
            {
                timerStopWatch.Stop();
                isTimerRun = false;
                this.Closing += (s, e) =>
                {
                    e.Cancel = false;
                };
            }

            if (barWorker.WorkerSupportsCancellation == true)        //עבור bar:

                // Cancel the asynchronous operation.
                barWorker.CancelAsync();
        }
    }
}