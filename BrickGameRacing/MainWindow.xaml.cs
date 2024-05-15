﻿using System.Text;
using System.Windows;
using System.Windows.Input;
using BrickGameRacing.VievModels;


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

            InputBindings.Add(new KeyBinding(_viewModel.StartCommand, Key.Space, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(_viewModel.MoveCommand, Key.Left, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(_viewModel.MoveCommand, Key.Right, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(_viewModel.StopCommand, Key.Escape, ModifierKeys.None));

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