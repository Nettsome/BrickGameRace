using BrickGameRacing.Models.Cells;
using System;
using System.Collections.Generic;


namespace BrickGameRacing;
/// <summary>
/// Что нужно сделать в этом классе:
/// * Добавить какой-то отличительный знак для основной машины, т.е. той, которой управляет игрок
/// * Добавить нормальный мувмент
/// * 
/// </summary>
/// 


// Буду сейчас делать так
//              7
//           8  0  9
//           1  3  2
//           10 4  11
//           5     6

// может сделать отдельные класс для основной машины и для побочных
public class Car
{
    public Cell[] CarCells = new Cell[12];                                      // Может сделать в виде List<Cell>
    public Cell CenterCell
    {
        get { return CarCells[3]; }
        private set { CarCells[3] = value;}
    }

    
    // The center of the car is in the 3th position in array
    //             0
    //           1 3 2
    //             4
    //           5   6


    public Car(Cell centerCell)
    {
        AddNew(centerCell);
    }

    private void AddNew(Cell centerCell)
    {
        CarCells[3] = new Cell(centerCell, CellType.Car);
        CarCells[0] = new Cell((short)(centerCell.Row - 1), centerCell.Col, CellType.Car);       
        CarCells[1] = new Cell(centerCell.Row, (ushort)(centerCell.Col - 1), CellType.Car);
        CarCells[2] = new Cell(centerCell.Row, (ushort)(centerCell.Col + 1), CellType.Car);
        CarCells[4] = new Cell((short)(centerCell.Row + 1), centerCell.Col, CellType.Car);
        CarCells[5] = new Cell((short)(centerCell.Row + 2), (ushort)(centerCell.Col - 1), CellType.Car);
        CarCells[6] = new Cell((short)(centerCell.Row + 2), (ushort)(centerCell.Col + 1), CellType.Car);

        CarCells[7] = new Cell((short)(CarCells[0].Row - 1), centerCell.Col);
        CarCells[8] = new Cell(CarCells[0].Row, (ushort)(centerCell.Col - 1));
        CarCells[9] = new Cell(CarCells[0].Row, (ushort)(centerCell.Col + 1));
        CarCells[10] = new Cell(CarCells[4].Row, (ushort)(centerCell.Col - 1));
        CarCells[11] = new Cell(CarCells[4].Row, (ushort)(centerCell.Col + 1));
    }



}
