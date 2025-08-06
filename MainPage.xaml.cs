using System.Collections;
using System.Windows.Input;

namespace TestCollViewPerf
{
    public partial class MainPage
    {
        public MainPage()
        {
            SetGroupedCommand = new Command(() =>
            {
                var now = DateTime.Now;
                IsGrouped = true;
                CollectionItems = Enumerable.Range(0, 1000)
                    .GroupBy(i => i / 20).ToList();
                SetActionTime(now);
            });
            SetUnGroupedCommand = new Command(() =>
            {
                var now = DateTime.Now;
                IsGrouped = false;
                CollectionItems = Enumerable.Range(0, 1000).ToList();
                SetActionTime(now);
            });
            Set4ColumnsCommand = new Command(() =>
            {
                var now = DateTime.Now;
                CVColumns = 4;
                SetActionTime(now);
            });
            Set6ColumnsCommand = new Command(() =>
            {
                var now = DateTime.Now;
                CVColumns = 6;
                SetActionTime(now);
            });
            void SetActionTime(DateTime now) =>
                ActionTime = (DateTime.Now - now).TotalSeconds;

            BindingContext = this;
            InitializeComponent();

            MessagingCenter.Subscribe<CollViewItem, int>(
                this, string.Empty, (s, i) =>
                {
                    lock (this) CollViewItemsCount += i;
                });
        }

        public ICommand SetGroupedCommand { get; }
        public ICommand SetUnGroupedCommand { get; }
        public ICommand Set4ColumnsCommand { get; }
        public ICommand Set6ColumnsCommand { get; }

        public bool IsGrouped
        {
            get => isGrouped;
            set { isGrouped = value; OnPropertyChanged(); }
        }
        private bool isGrouped;

        public IEnumerable? CollectionItems
        {
            get => collectionItems;
            set { collectionItems = value; OnPropertyChanged(); }
        }
        private IEnumerable? collectionItems;

        public int CVColumns
        {
            get => cvColumns;
            set { cvColumns = value; OnPropertyChanged(); }
        }
        private int cvColumns = 4;

        public int CollViewItemsCount
        {
            get => collViewItemsCount;
            set { collViewItemsCount = value; OnPropertyChanged(); }
        }
        private int collViewItemsCount;

        public double ActionTime
        {
            get => actionTime;
            set { actionTime = value; OnPropertyChanged(); }
        }
        private double actionTime;

        public double TotalMemory =>
            GC.GetTotalMemory(false) / 1024.0 / 1024;

        private void OnScrolled(object sender, ItemsViewScrolledEventArgs e) =>
            OnPropertyChanged(nameof(TotalMemory));
    }
}