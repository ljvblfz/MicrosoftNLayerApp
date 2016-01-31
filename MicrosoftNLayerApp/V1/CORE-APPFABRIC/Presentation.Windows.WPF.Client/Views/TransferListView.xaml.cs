using System;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client
{
	public partial class TransferListView : UserControl
	{
        public TransferListView()
		{
			this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(TransferListView_Loaded);
		}

        private void TransferListView_Loaded(object sender, RoutedEventArgs e)
        {
            this.dpInitialDate.DisplayDate = DateTime.Today.AddMonths(-6);
            this.dpEndDate.DisplayDate = DateTime.Today.AddMonths(6);
        }
	}
}