using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace tempore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Task> list;
        DispatcherTimer dispatcherTimer;
        Task currentlyRunningTask;
        Boolean aTaskIsRunning = false;
        TextBlock currentTextBlock;

        public MainPage()
        {
            this.InitializeComponent();
               list = new ObservableCollection<Task>
               {
                    new Task("Hi", "hello",1),
                    new Task("Hi", "hello",1),
               };
        }

        public void addTask(Object sender, RoutedEventArgs e)
        {
            list.Add(new Task("Hi", "hello", 1));
        }

        private void timer(object sender, RoutedEventArgs e)
        {
            if (!aTaskIsRunning)
            {
                currentlyRunningTask = new Task("a", "b", 50); //tasks[...];
                                                               // currentTextBlock = textBlocks[...];
                DispatcherTimerSetup();
            }
            else {
                stopTimer();
            }
        }

        private void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            if (currentlyRunningTask.getTimesToTick() == 0)
            {
                dispatcherTimer.Stop();
                stopTimer();
                currentTextBlock.Text = "Finished";
            }
            currentlyRunningTask.setTimesToTick(currentlyRunningTask.getTimesToTick() - 1);
            currentTextBlock.Text = currentlyRunningTask.getTimesToTick().ToString();
        }

        private void stopTimer()
        {
            currentTextBlock.Text = "Paused";
            aTaskIsRunning = false;
            dispatcherTimer.Stop();
        }

    }

    public class Task
    {
        public String title;
        public String description;
        private int timesToTick;
        private int totalTime;

        public Task(String title, String description, int totalTime)
        {
            this.title = title;
            this.description = description;
            this.timesToTick = totalTime;
            this.totalTime = totalTime;
        }

        public int getTimesToTick()
        {
            return this.timesToTick;
        }

        public int getTotalTime()
        {
            return this.totalTime;
        }

        public void setTimesToTick(int x)
        {
            this.timesToTick = x;
        }
    }
}
