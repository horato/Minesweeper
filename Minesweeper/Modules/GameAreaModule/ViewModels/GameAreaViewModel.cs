using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Minesweeper.Data.Enum;
using Minesweeper.Data.Interfaces;
using Minesweeper.Data.Logic;
using Prism.Commands;
using Prism.Mvvm;

namespace Minesweeper.Modules.GameAreaModule.ViewModels
{
    public class GameAreaViewModel : BindableBase
    {
        public IGameGrid GameGrid { get; }

        public ICommand SetFlagCommand { get; }
        public ICommand NodeSelectedCommand { get; }

        public GameAreaViewModel(IGameGrid gameGrid)
        {
            SetFlagCommand = new DelegateCommand<GridCell>(OnSetFlag);
            NodeSelectedCommand = new DelegateCommand<GridCell>(OnNodeSelected);
            GameGrid = gameGrid;

            GenerateGrid();
        }

        private void OnSetFlag(GridCell cell)
        {
            switch (cell.CellState)
            {
                case CellState.None:
                case CellState.Bomb:
                    cell.MarkWithFlag();
                    CheckWinCondition();
                    break;
                case CellState.Flag:
                    cell.ResetFlag();
                    break;
                case CellState.Text:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnNodeSelected(GridCell cell)
        {
            switch (cell.CellState)
            {
                case CellState.None:
                    SetTextStateToNearbyNodes(cell);
                    CheckWinCondition();
                    break;
                case CellState.Bomb:
                    MessageBox.Show(":(");
                    GenerateGrid();
                    break;
                case CellState.Flag:
                case CellState.Text:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CheckWinCondition()
        {
            if (!GameGrid.IsGameCompleted())
                return;

            MessageBox.Show(":)!");
            GenerateGrid();
        }

        private void SetTextStateToNearbyNodes(GridCell cell)
        {
            cell.SetCellState(CellState.Text);
            if (cell.NearbyBombsCount != 0)
                return;

            foreach (var nearbyCell in cell.EnumerateNearbyCells(true).Where(x => x.CellState == CellState.None))
            {
                if (nearbyCell.NearbyBombsCount > 0)
                    nearbyCell.SetCellState(CellState.Text);
                else
                    SetTextStateToNearbyNodes(nearbyCell);
            }
        }

        private void GenerateGrid()
        {
            GameGrid.Initialize();
        }
    }
}
