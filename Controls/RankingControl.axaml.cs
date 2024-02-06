using Avalonia.Controls;
using Tebbet.ViewModels;

namespace Tebbet.Controls
{
    public partial class RankingControl : UserControl
    {
        private readonly RankingViewModel viewModel;
        public RankingControl()
        {
            InitializeComponent();
            viewModel = new RankingViewModel();
            DataContext = viewModel;
        }
    }
}
