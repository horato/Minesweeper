using System.Windows;
using Minesweeper.Data.Enum;

namespace Minesweeper.Data.Components
{
    /// <summary>
    /// Interaction logic for MinesweeperButton.xaml
    /// </summary>
    public partial class MinesweeperButton
    {
        public static readonly DependencyProperty CellStateProperty = DependencyProperty.Register(
            "CellState", typeof(CellState), typeof(MinesweeperButton), new FrameworkPropertyMetadata(CellState.None) { BindsTwoWayByDefault = true });

        public CellState CellState
        {
            get { return (CellState)GetValue(CellStateProperty); }
            set { SetValue(CellStateProperty, value); }
        }

        public MinesweeperButton()
        {
            InitializeComponent();
        }
    }
}
