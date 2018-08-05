using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Data.Logic
{
    public class GridRow : IEnumerable<GridCell>
    {
        private readonly IDictionary<int, GridCell> _cells = new Dictionary<int, GridCell>();

        public int Number { get; }
        public int CellsCount => _cells.Count;

        /// <summary> Returns cell on given index or null </summary>
        /// <param name="i"> Index </param>
        public GridCell this[int i] => i >= 0 && i < _cells.Count ? _cells[i] : null;

        public GridRow(int number)
        {
            Number = number;
        }

        public void AddCell(GridCell cell)
        {
            _cells.Add(cell.Column, cell);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<GridCell> GetEnumerator()
        {
            return _cells.Values.GetEnumerator();
        }
    }
}
