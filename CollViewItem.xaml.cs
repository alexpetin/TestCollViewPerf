namespace TestCollViewPerf;

public partial class CollViewItem
{
    public CollViewItem()
    {
        InitializeComponent();
        MessagingCenter.Send(this, string.Empty, 1);
    }

    ~CollViewItem()
    {
        MessagingCenter.Send(this, string.Empty, -1);
    }
}