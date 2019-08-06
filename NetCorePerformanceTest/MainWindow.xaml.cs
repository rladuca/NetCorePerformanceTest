using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace NetCorePerformanceTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var scrollViewer = GetVisualChild<ScrollViewer>(dataGrid);
            var stopwatch = Stopwatch.StartNew();
            ScrollDown(scrollViewer, () => MessageBox.Show($"{stopwatch.ElapsedMilliseconds} ms"));
        }

        static void ScrollDown(ScrollViewer scrollViewer, Action finishAction) {
            if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                finishAction();
                return;
            }
            scrollViewer.PageDown();
            scrollViewer.Dispatcher.BeginInvoke(new Action (() => ScrollDown(scrollViewer, finishAction)), DispatcherPriority.ApplicationIdle);
        }
        static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var visual = (Visual)VisualTreeHelper.GetChild(parent, i);
                if ((child = visual as T) == null)
                    child = GetVisualChild<T>(visual);
                if (child != null)
                    break;                    
            }
            return child;
        }
    }

    public class ViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public ViewModel()
        {
            var items = new ObservableCollection<Item>();
            for (int i = 0; i < 3000; i++)
                items.Add(new Item(i, $"Item{i}", i % 2 == 0, DateTime.Now.AddHours(i)));
            Items = items;
        }
    }

    public class Item
    {
        public Item(int id, string name, bool isChecked, DateTime date)
        {
            Id = Id2 = Id3 = Id4 = id;
            Name = Name2 = Name3 = Name4 = name;
            IsChecked = IsChecked2 = IsChecked3 = IsChecked4 = isChecked;
            Date = Date2 = Date3 = Date4 = date;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public DateTime Date { get; set; }
        public int Id2 { get; set; }
        public string Name2 { get; set; }
        public bool IsChecked2 { get; set; }
        public DateTime Date2 { get; set; }
        public int Id3 { get; set; }
        public string Name3 { get; set; }
        public bool IsChecked3 { get; set; }
        public DateTime Date3 { get; set; }
        public int Id4 { get; set; }
        public string Name4 { get; set; }
        public bool IsChecked4 { get; set; }
        public DateTime Date4 { get; set; }
    }
}
