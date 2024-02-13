using Avalonia.Controls;
using Avalonia.Interactivity;
using Tebbet.ViewModels;

namespace Tebbet.Controls
{
    public partial class HistoryControl : UserControl
    {
        private HistoryViewModel viewModel;
        public HistoryControl()
        {
            InitializeComponent();
            viewModel = new HistoryViewModel();
            DataContext = viewModel;
            Withdraw.Click += Handler;
            Deposit.Click += Handler;
            Amount.KeyUp += Handler;
            Loose.PointerPressed += Handler;
            Win.PointerPressed += Handler;
            All.PointerPressed += Handler;
        }

        private void Handler(object sender, RoutedEventArgs args)
        {
            if (sender is Button button)
            {
                switch (button.Name)
                {
                    case "Withdraw":
                        if (Amount.Text != null)
                        {
                            double value = double.Parse(Amount.Text);
                            if (value > 0)
                            {
                                viewModel.Withdraw(value);
                            }
                        }
                        break;
                    case "Deposit":
                        if (Amount.Text != null)
                        {
                            double value = double.Parse(Amount.Text);
                            if (value > 0)
                            {
                                viewModel.Deposit(value);
                            }
                        }
                        break;
                }
            }
            if (sender is TextBox textBox)
            {
                if (textBox.Name is "Amount")
                {
                    if (textBox.Text != null)
                    {
                        try
                        {
                            double value = double.Parse(textBox.Text);
                        }
                        catch
                        {
                            textBox.Text = null;
                        }
                    }
                }
            }
            if (sender is TabItem tabItem)
            {
                if (tabItem.Name != null)
                {
                    viewModel.GetHistoryBets(tabItem.Name);
                }
            }
        }
    }
}
