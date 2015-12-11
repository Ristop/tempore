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
        
    
        public MainPage()
        {
            this.InitializeComponent();
               list = new ObservableCollection<Task>
               {
                    new Task {title = "Hi", description = "hello" },
                    new Task {title = "test", description = "testing" },
               };
        }

        public void addTask(Object sender, RoutedEventArgs e)
        {
            list.Add(new Task { title = "Hi", description = "hello" });
        }

    }

    public class Task
     {
        public string title;
        public string description;
     }
}
