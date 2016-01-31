using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client
{
	public partial class EditCustomerView : UserControl
	{
        private Dictionary<string, bool> _errors = new Dictionary<string, bool>();

        public EditCustomerView()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EditCustomerView_Loaded);            
        }

        private void EditCustomerView_Loaded(object sender, RoutedEventArgs e)
        {
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(OnErrorEvent));
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
            DependencyProperty.Register("IsValidData", typeof(bool), typeof(EditCustomerView), new UIPropertyMetadata(true));
	}
}