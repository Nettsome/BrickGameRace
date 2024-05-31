using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickGameRacing.Models.Cells;

public class Cell(short row, ushort col, CellType type = CellType.Empty)
{
    public short Row { get => row; set => row = value; }
    public ushort Col { get => col; set => col = value; }
    public CellType Type { get => type; set => type = value; }
    public static bool IsActiveField { get; set; } = false;
    public Cell(Cell cell, CellType type) : this(cell.Row, cell.Col, type) { }
    public Cell(Cell cell) : this(cell.Row, cell.Col, cell.Type) { }

    public override bool Equals(object? obj)
    {
        if (obj is Cell cell)
        {
            return Row == cell.Row && Col == cell.Col;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row.GetHashCode(), Col.GetHashCode());
    }
}

public enum CellType
{
    Empty, Car, Wall
}