using BrickGameRacing.Models.Cars;
using BrickGameRacing.Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickGameRacing;

public class MainCar(Cell centerCell) : Car(centerCell)
{
    Car emptyCar = new(centerCell);       // или lastcarposition или cartrace

    public void MoveTo(Cell cell)
    {
        this.CloneTo(emptyCar, CellType.Empty);

    }


    public List<Cell> Cells
    {
        get
        {
            List<Cell> cells = new();

            cells.AddRange(emptyCar.CarCells);
            cells.AddRange(this.CarCells);
            return cells;
        }
    }
}
