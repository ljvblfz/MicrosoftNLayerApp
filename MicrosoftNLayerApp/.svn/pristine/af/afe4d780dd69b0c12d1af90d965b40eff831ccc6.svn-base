using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client
{
    /// <summary>
    /// Interaction logic for PerformTransfer.xaml
    /// </summary>
    public partial class PerformTransferView : UserControl
    {
        private Dictionary<string, bool> _errors = new Dictionary<string, bool>();

        public PerformTransferView()
        {
            InitializeComponent();

            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
            this.Loaded += new RoutedEventHandler(PerformTransferView_Loaded);
        }

        private void PerformTransferView_Loaded(object sender, RoutedEventArgs e)
        {
            this.dragArea.MouseLeftButtonDown += delegate { this.dragArea.CaptureMouse(); };
            this.dragArea.MouseLeftButtonUp += delegate { this.dragArea.ReleaseMouseCapture(); };
            this.MouseMove += delegate(object s, MouseEventArgs args)
            {
                if (this.dragArea.IsMouseCaptured)
                {
                    Point p = this.searchAccountPopUp.TransformToAncestor(App.Current.MainWindow).Transform(new Point(0, 0));
                    this.searchAccountPopUp.HorizontalOffset = args.GetPosition(this).X - p.X -100;
                    this.searchAccountPopUp.VerticalOffset = args.GetPosition(this).Y - p.Y - 100;
                }
            };
        }

        private void OnErrorEvent(object o, RoutedEventArgs args)
        {
            if (args.OriginalSource != null)
            {
                TextBox txtBox = (TextBox)args.OriginalSource;

                if (!_errors.Keys.Contains(txtBox.Name))
                    _errors.Add(txtBox.Name, false);

                _errors[txtBox.Name] = Validation.GetHasError(txtBox);                    
            }

            this.IsValidData = (this._errors.Where(k => k.Value == true).Count() == 0);
        }

        public bool IsValidData
        {
            get { return (bool)GetValue(IsValidDataProperty); }
            set { SetValue(IsValidDataProperty, value); }
        }

        public static readonly DependencyProperty IsValidDataProperty =
            DependencyProperty.Register("IsValidData", typeof(bool), typeof(PerformTransferView), new UIPropertyMetadata(false));

        private void Storyboard_Completed(object sender, System.EventArgs e)
        {
            this.txtSourceAccount.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtDestinationAccount.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            this.txtAmount.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}