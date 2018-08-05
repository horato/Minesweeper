using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using Minesweeper.Data.Constants;
using Minesweeper.Data.Logic;

namespace Minesweeper.Data.Components
{
    /// <summary>
    /// Interaction logic for GridGenerator.xaml
    /// </summary>
    public partial class MinesweeperGridGenerator : UserControl
    {
        public static readonly DependencyProperty GameGridProperty = DependencyProperty.Register(
            "GameGrid", typeof(GameGrid), typeof(MinesweeperGridGenerator), new PropertyMetadata(default(GameGrid), OnGameGridChanged));

        public GameGrid GameGrid
        {
            get { return (GameGrid)GetValue(GameGridProperty); }
            set { SetValue(GameGridProperty, value); }
        }

        public MinesweeperGridGenerator()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            InitializeGrid(GameGrid);
        }

        private static void OnGameGridChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MinesweeperGridGenerator)d).OnGameGridChanged((GameGrid)e.OldValue, (GameGrid)e.NewValue);
        }

        private void OnGameGridChanged(GameGrid oldValue, GameGrid newValue)
        {
            if (oldValue != null)
                oldValue.GridChanged -= OnGridChanged;
            if (newValue != null)
                newValue.GridChanged += OnGridChanged;

            InitializeGrid(newValue);
        }

        private void OnGridChanged(object sender, EventArgs e)
        {
            InitializeGrid(GameGrid);
        }

        private void InitializeGrid(GameGrid gameGrid)
        {
            if (!IsLoaded || gameGrid?.Initialized != true)
                return;

            var grid = Template.FindName(ElementNames.MainGrid, this) as Grid;
            if (grid == null)
                throw new InvalidOperationException("Grid not found");

            grid.Children.Clear();
            GenerateGrid(gameGrid, grid);

            for (var column = 0; column < grid.ColumnDefinitions.Count; column++)
            {
                for (var row = 0; row < grid.RowDefinitions.Count; row++)
                {
                    var cell = gameGrid[row][column];
                    var textBlock = CreateButton(cell, column, row);
                    grid.Children.Add(textBlock);
                }
            }
        }

        private Button CreateButton(GridCell cell, int column, int row)
        {
            var buttonTemplate = FindResource(ElementNames.MinesweeperButtonTemplate) as DataTemplate;
            var button = buttonTemplate?.LoadContent() as MinesweeperButton;
            if (button == null)
                throw new InvalidOperationException("Button not found");

            button.DataContext = cell;
            Grid.SetColumn(button, column);
            Grid.SetRow(button, row);

            return button;
        }

        //private void TextBlockOnMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (!IsLoaded)
        //        return;

        //    var grid = Template.FindName(ElementNames.MainGrid, this) as Grid;
        //    if (grid == null)
        //        throw new InvalidOperationException("Grid not found");

        //    var textblock = (Button)sender;
        //    var selectedCell = (GridCell)textblock.DataContext;
        //    foreach (var tb in grid.Children.OfType<Button>())
        //    {
        //        var cell = (GridCell)tb.DataContext;
        //        if (cell.LeftCell == selectedCell
        //            || cell.RightCell == selectedCell
        //            || cell.TopCell == selectedCell
        //            || cell.BottomCell == selectedCell
        //            || cell.TopLeftCell == selectedCell
        //            || cell.TopRightCell == selectedCell
        //            || cell.BottomLeftCell == selectedCell
        //            || cell.BottomRightCell == selectedCell)
        //            tb.Background = Brushes.Blue;
        //        else
        //            tb.Background = Brushes.White;
        //    }

        //    textblock.Background = Brushes.Red;
        //}

        private void GenerateGrid(GameGrid gameGrid, Grid grid)
        {
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();

            foreach (var row in gameGrid)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            if (gameGrid.Any())
            {
                var row = gameGrid.First();
                foreach (var cells in row)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }
            }
        }
    }
}
