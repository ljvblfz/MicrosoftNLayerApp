using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client
{
    /// <summary>
    /// Interaction logic for PerformOrder.xaml
    /// </summary>
    public partial class PerformOrderView : UserControl
    {
        private int _errors = 0;

        public PerformOrderView()
        {
            InitializeComponent();
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
        }

        private void OnErrorEvent(object o, RoutedEventArgs args)
        {
            if (args.OriginalSource != null)
            {
                if (Validation.GetHasError((DependencyObject)args.OriginalSource))
                    this._errors++;
                else
                    this._errors--;
            }

            this.IsValidData = (this._errors == 0);
        }

        public bool IsValidData
        {
            get { return (bool)GetValue(IsValidDataProperty); }
            set { SetValue(IsValidDataProperty, value); }
        }

        public static readonly DependencyProperty IsValidDataProperty =
            DependencyProperty.Register("IsValidData", typeof(bool), typeof(PerformOrderView), new UIPropertyMetadata(false));
    }
}