using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickGameRacing;

public class GameViewModel
{
    private readonly ushort _rows = 20;
    private readonly ushort _cols = 10;
    public Field Field { get; init; }

    public GameViewModel()
    { 
        Field = new(_rows, _cols);
    }

    public CellTypeConverter TypeConverter { get; } = new();

}
