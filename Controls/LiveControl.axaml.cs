using Avalonia.Controls;
using Tebbet.ViewModels;

namespace Tebbet.Controls
{
    public partial class LiveControl : UserControl
    {
        private LiveViewModel viewModel;
        public LiveControl()
        {
            InitializeComponent();
            viewModel = new LiveViewModel();
            DataContext = viewModel;
        }
    }
}
