﻿using tempore.Data;
using tempore.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;

namespace tempore
{
     public sealed partial class MasterDetailPage : Page
     {
          private ItemViewModel _lastSelectedItem;
          private ObservableCollection<ItemViewModel> list;
          DispatcherTimer dispatcherTimer;
          Task currentlyRunningTask;
          Boolean aTaskIsRunning = false;
          TextBlock currentTextBlock;

          public MasterDetailPage()
          {
               this.InitializeComponent();
          }

          public void addTask(Object sender, RoutedEventArgs e)
          {
               list.Add(ItemViewModel.FromItem(new Item()
               {
                    Title = TextBoxTitle.Text,
                    Id = 13,
                    Text = TextBoxText.Text,

               }));
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




     protected override void OnNavigatedTo(NavigationEventArgs e)
          {
               base.OnNavigatedTo(e);

               //var items = MasterListView.ItemsSource as List<ItemViewModel>;

               if (list == null)
               {
                    list = new ObservableCollection<ItemViewModel>();

                    foreach (var item in ItemsDataSource.GetAllItems())
                    {
                         list.Add(ItemViewModel.FromItem(item));
                    }

                  
               }

               if (e.Parameter != null)
               {
                    // Parameter is item ID
                    var id = (int)e.Parameter;
                    _lastSelectedItem =
                        list.Where((item) => item.ItemId == id).FirstOrDefault();
               }

               UpdateForVisualState(AdaptiveStates.CurrentState);

               // Don't play a content transition for first item load.
               // Sometimes, this content will be animated as part of the page transition.
               DisableContentTransitions();
          }

          private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
          {
               UpdateForVisualState(e.NewState, e.OldState);
          }

          private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
          {
               var isNarrow = newState == NarrowState;

               if (isNarrow && oldState == DefaultState && _lastSelectedItem != null)
               {
                    // Resize down to the detail item. Don't play a transition.
                    Frame.Navigate(typeof(DetailPage), _lastSelectedItem.ItemId, new SuppressNavigationTransitionInfo());
               }

               EntranceNavigationTransitionInfo.SetIsTargetElement(MasterListView, isNarrow);
               if (DetailContentPresenter != null)
               {
                    EntranceNavigationTransitionInfo.SetIsTargetElement(DetailContentPresenter, !isNarrow);
               }
          }

          private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
          {
               var clickedItem = (ItemViewModel)e.ClickedItem;
               _lastSelectedItem = clickedItem;

               if (AdaptiveStates.CurrentState == NarrowState)
               {
                    // Use "drill in" transition for navigating from master list to detail view
                    Frame.Navigate(typeof(DetailPage), clickedItem.ItemId, new DrillInNavigationTransitionInfo());
               }
               else
               {
                    // Play a refresh animation when the user switches detail items.
                    EnableContentTransitions();
               }
          }

          private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
          {
               // Assure we are displaying the correct item. This is necessary in certain adaptive cases.
               MasterListView.SelectedItem = _lastSelectedItem;
          }

          private void EnableContentTransitions()
          {
               DetailContentPresenter.ContentTransitions.Clear();
               DetailContentPresenter.ContentTransitions.Add(new EntranceThemeTransition());
          }

          private void DisableContentTransitions()
          {
               if (DetailContentPresenter != null)
               {
                    DetailContentPresenter.ContentTransitions.Clear();
               }
          }
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
