using BrickGameRacing.Models.Cells;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickGameRacing;

/// <summary>
/// Думаю лучше переделать этот класс и сделать что-то вроде биндинга в классе поля
/// </summary>

public class Borders
{
    private ushort Position;
    private ushort BorderLength;
    private static ushort WallLength = 3;
    //private static ushort counter = 0;              // static нужен, чтобы была синхронизация движения бордюров. По крайней мере я так считал, но оказалось не так
    private ushort counter = 0;

    public Cell[] Walls;

    public Borders(ushort length, ushort position)
    {
        Walls = new Cell[length];
        Position = position;
        BorderLength = length;
        

        Create();
    }

    private void Create()
    {
        for (ushort i = 0; i < BorderLength; i++)
        {
            Walls[i] = new(i, Position);

            if (counter < WallLength)
            {
                Walls[i].Type = CellType.Wall;
                counter++;
            }
            else
            {
                counter = 0;
            }
        }
        
    }

    public List<Cell> Move()
    {
        for (int i = BorderLength - 1;  i > 0; i--) 
        {
            Walls[i].Type = Walls[i - 1].Type;
        }


        if (counter <= WallLength && counter != 0)
        {
            Walls[0].Type = CellType.Wall;
            counter++;
        }
        else
        {
            Walls[0].Type = CellType.Empty;
            counter = 1;
        }

        return Walls.ToList();
    }

}
