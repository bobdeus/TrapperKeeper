using System;
using System.Windows.Forms;

namespace TrapperKeeper.Extensions
{
    public static class DataGridViewExtensions
    {
        private const int ShowPasswordColumnIndex = 2;

        public static bool IsNotValidButton(this DataGridViewSelectedCellCollection selectedCells)
        {
            return selectedCells[0].GetType() != typeof(DataGridViewButtonCell);
        }
        public static bool IsShowButton(this DataGridViewSelectedCellCollection selectedCells)
        {
            return selectedCells[0].ColumnIndex == ShowPasswordColumnIndex;
        }
    }
}