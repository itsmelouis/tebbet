using Avalonia.Controls;
using Avalonia.Interactivity;
using Tebbet.ViewModels;

namespace Tebbet.Controls
{
    public partial class CourseControl : UserControl
    {
        private CourseViewModel viewModel;
        public CourseControl(int id)
        {
            InitializeComponent();
            viewModel = new CourseViewModel(id);
            DataContext = viewModel;
            ButtonBet.Click += Handler;
            AmountBet.KeyUp += Handler;
        }

        private void Handler(object sender, RoutedEventArgs args)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.Name is "AmountBet")
                {
                    if (textBox.Text != null)
                    {
                        try
                        {
                            double value = double.Parse(textBox.Text);
                            viewModel.SetGain(value);
                            viewModel.TextBoxValue = value;
                        }
                        catch
                        {
                            textBox.Text = null;
                            viewModel.SetGain(0);
                            viewModel.TextBoxValue = 0;
                        }
                    }
                }
            }
            if(sender is Button button)
            {
                if (button.Name is "ButtonBet")
                {
                    viewModel.SaveBet();
                }
            }
        }
    }
}
