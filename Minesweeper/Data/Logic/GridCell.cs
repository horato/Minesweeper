using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Data.Enum;
using Prism.Mvvm;

namespace Minesweeper.Data.Logic
{
    public class GridCell : BindableBase
    {
        private CellState _previousState;

        public GridRow Row { get; }
        public int Column { get; }

        public int NearbyBombsCount => GetNearbyBombsCount();
        public CellState CellState { get; private set; } = CellState.None;

        public bool HasTopCell => TopCell != null;
        public bool HasBottomCell => BottomCell != null;
        public bool HasLeftCell => LeftCell != null;
        public bool HasRightCell => RightCell != null;
        public bool HasTopLeftCell => TopLeftCell != null;
        public bool HasTopRightCell => TopRightCell != null;
        public bool HasBottomLeftCell => BottomLeftCell != null;
        public bool HasBottomRightCell => BottomRightCell != null;

        public GridCell TopCell { get; private set; }
        public GridCell BottomCell { get; private set; }
        public GridCell LeftCell { get; private set; }
        public GridCell RightCell { get; private set; }
        public GridCell TopLeftCell { get; private set; }
        public GridCell TopRightCell { get; private set; }
        public GridCell BottomLeftCell { get; private set; }
        public GridCell BottomRightCell { get; private set; }

        public GridCell(GridRow row, int column)
        {
            Row = row;
            Column = column;
        }

        public void CreateLink(GridCell leftCell, GridCell rigthCell, GridCell topCell, GridCell bottomCell, GridCell topLeftCell, GridCell topRightCell, GridCell bottomLeftCell, GridCell bottomRightCell)
        {
            LeftCell = leftCell;
            RightCell = rigthCell;
            TopCell = topCell;
            BottomCell = bottomCell;
            TopLeftCell = topLeftCell;
            TopRightCell = topRightCell;
            BottomLeftCell = bottomLeftCell;
            BottomRightCell = bottomRightCell;
        }

        private int GetNearbyBombsCount()
        {
            var nearbyCells = EnumerateNearbyCells().ToList();
            if (nearbyCells.Any())
                return nearbyCells.Count(x => x.IsBombNode());

            return 0;
        }

        public bool IsBombNode()
        {
            return CellState == CellState.Bomb || _previousState == CellState.Bomb;
        }

        public void SetCellState(CellState state)
        {
            CellState = state;
            RaisePropertyChanged(nameof(CellState));
            RaisePropertyChanged(nameof(NearbyBombsCount));
        }

        public IEnumerable<GridCell> EnumerateNearbyCells(bool onlyNonDiagonal = false)
        {
            if (HasTopCell)
                yield return TopCell;
            if (HasBottomCell)
                yield return BottomCell;
            if (HasLeftCell)
                yield return LeftCell;
            if (HasRightCell)
                yield return RightCell;

            if (onlyNonDiagonal)
                yield break;

            if (HasTopLeftCell)
                yield return TopLeftCell;
            if (HasTopRightCell)
                yield return TopRightCell;
            if (HasBottomLeftCell)
                yield return BottomLeftCell;
            if (HasBottomRightCell)
                yield return BottomRightCell;
        }

        public void MarkWithFlag()
        {
            _previousState = CellState;
            SetCellState(CellState.Flag);
        }

        public void ResetFlag()
        {
            if (CellState != CellState.Flag)
                return;

            SetCellState(_previousState);
            _previousState = default(CellState);
        }

        public bool IsFlagged()
        {
            return CellState == CellState.Flag;
        }

        public bool IsFlagJustified()
        {
            if (!IsFlagged())
                return false;

            return _previousState == CellState.Bomb;
        }
    }
}
