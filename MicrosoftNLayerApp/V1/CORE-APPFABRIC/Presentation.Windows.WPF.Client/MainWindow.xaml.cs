using System.Windows;

namespace Microsoft.Samples.NLayerApp.Presentation.Windows.WPF.Client
{   
    public partial class MainWindow : Window 
    {
       public MainWindow()
		{
			this.InitializeComponent();
		}

       private void Grip_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
       {
           DragMove();    
       }
    }
}