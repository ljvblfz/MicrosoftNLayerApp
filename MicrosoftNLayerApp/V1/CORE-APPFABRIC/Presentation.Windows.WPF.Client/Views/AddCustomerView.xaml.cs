using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client
{
    public partial class AddCustomerView : UserControl
    {
        private Dictionary<string, bool> _errors = new Dictionary<string, bool>();

        public AddCustomerView()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(AddCustomerView_Loaded);
        }

        private void AddCustomerView_Loaded(object sender, RoutedEventArgs e)
        {
            //add handler for validation errors
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
            DependencyProperty.Register("IsValidData", typeof(bool), typeof(AddCustomerView), new UIPropertyMetadata(false));

        private void txtCompanyName_LostFocus(object sender, RoutedEventArgs e)
        {
            string fallbackValue = "Company Name Here...";
            string currentText = this.txtCompanyName.Text.Trim();

            if (string.IsNullOrEmpty(currentText))
                this.txtCompanyName.Text = fallbackValue;
        }

        private void txtCompanyName_GotFocus(object sender, RoutedEventArgs e)
        {
            string fallbackValue = "Company Name Here...";
            string currentText = this.txtCompanyName.Text.Trim();

            if (currentText.Equals(fallbackValue, StringComparison.InvariantCultureIgnoreCase))
                this.txtCompanyName.Text = string.Empty;
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            //update validation sources            
            this.TB_ZIPCode.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}