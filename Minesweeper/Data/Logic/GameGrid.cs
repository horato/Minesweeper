using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Data.Enum;
using Minesweeper.Data.Interfaces;

namespace Minesweeper.Data.Logic
{
    public class GameGrid : IGameGrid, IEnumerable<GridRow>
    {
        private readonly ISettings _settings;
        private readonly Dictionary<int, GridRow> _rows = new Dictionary<int, GridRow>();

        /// <summary> Returns row on given index or null </summary>
        /// <param name="i">Index</param>
        public GridRow this[int i] => i >= 0 && i < _rows.Count ? _rows[i] : null;
        public event EventHandler GridChanged;
        public bool Initialized { get; private set; }

        public GameGrid(ISettings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            InitializeRows(_settings.GridLength, _settings.GridWidth);
            CreateRowCellLinks();
            GenerateBombs(_settings.BombsCount);
            Initialized = true;
            GridChanged?.Invoke(this, EventArgs.Empty);
        }

        private void InitializeRows(int length, int width)
        {
            _rows.Clear();
            for (var rowNumber = 0; rowNumber < length; rowNumber++)
            {
                var row = new GridRow(rowNumber);
                for (var column = 0; column < width; column++)
                {
                    var cell = new GridCell(row, column);
                    row.AddCell(cell);
                }

                _rows.Add(row.Number, row);
            }
        }

        private void CreateRowCellLinks()
        {
            for (var rowNumber = 0; rowNumber < _rows.Count; rowNumber++)
            {
                var row = _rows[rowNumber];
                for (var column = 0; column < row.CellsCount; column++)
                {
                    var cell = row[column];
                    var leftCell = row[column - 1];
                    var rigthCell = row[column + 1];
                    var topCell = this[rowNumber - 1]?[column];
                    var bottomCell = this[rowNumber + 1]?[column];
                    var topLeftCell = this[rowNumber - 1]?[column - 1];
                    var topRightCell = this[rowNumber - 1]?[column + 1];
                    var bottomLeftCell = this[rowNumber + 1]?[column - 1];
                    var bottomRightCell = this[rowNumber + 1]?[column + 1];

                    cell.CreateLink(leftCell, rigthCell, topCell, bottomCell, topLeftCell, topRightCell, bottomLeftCell, bottomRightCell);
                }
            }
        }

        private void GenerateBombs(int bombsCount)
        {
            if (!_rows.Any())
                return;

            var random = new Random();
            var cells = GetAllCells();
            for (var i = 0; i < bombsCount; i++)
            {
                if (!cells.Any())
                    return;

                var index = random.Next(0, cells.Count - 1);
                cells[index].SetCellState(CellState.Bomb);
                cells.RemoveAt(index);
            }
        }

        private IList<GridCell> GetAllCells()
        {
            return _rows.SelectMany(x => x.Value.Select(y => y)).ToList();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<GridRow> GetEnumerator()
        {
            return _rows.Values.GetEnumerator();
        }

        public bool IsGameCompleted()
        {
            var cells = GetAllCells();
            if (!cells.Any())
                return false;
            if (cells.Any(x => x.CellState == CellState.None))
                return false;
            if (cells.Where(x => x.IsFlagged()).Any(x => !x.IsFlagJustified()))
                return false;

            return true;
        }
    }
}
