using System.Text;
using System.Windows;


namespace BrickGameRacing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameViewModel _viewModel;
        public MainWindow()
        {
            _viewModel = new();

            InitializeComponent();

            DataContext = _viewModel;
            GameElems.ItemsSource = _viewModel.Field;
        }

        private void GameElems_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _viewModel.Field.Width = (int)e.NewSize.Width;
            _viewModel.Field.Height = (int)e.NewSize.Height;
        }
    }
}