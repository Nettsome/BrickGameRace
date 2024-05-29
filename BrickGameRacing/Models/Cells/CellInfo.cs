using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BrickGameRacing.Models.Cells;

public class CellInfo(short row, ushort col) : Cell(row, col)
{
    public int Left { get; set; }       // отступ от левого края канваса
    public int Top { get; set; }        // отступ от верхнего края канваса
    public int Size { get; set; }
    public Thickness Margin => new(Left, Top, 0, 0);        // отступы
}
