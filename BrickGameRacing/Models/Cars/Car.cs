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
public class Car
{
    private static List<Car>? cars;     // Список всех машин (думаю, лучше перенести в отдельный файл)


    // Хранит в себе клетки, из которых состоит машинка
    public Cell[] CarCells = new Cell[7];                                      // Может сделать в виде List<Cell>

    
    // The center of the car is in the 4th position in array
    //      1
    //    2 4 3
    //      5           
    //    6   7

    public Direction CarDirection { get; private set; }


    public Car(Cell centerCell, Direction carDirection = Direction.Down)
    {
        AddNew(centerCell);
        CarDirection = carDirection;
        CarDirection = carDirection;
    }

    private void AddNew(Cell centerCell)
    {
        // нужна проверка на выход за границы, которую я пока не знаю как сделать
        // сделать эту проверку в логике игры
        // проверяем при создании центральную клетку на выход за границы 

        CarCells[3] = new Cell(centerCell, CellType.Car);
        CarCells[0] = new Cell((ushort)(centerCell.Row - 1), centerCell.Col, CellType.Car);       
        CarCells[1] = new Cell(centerCell.Row, (ushort)(centerCell.Col - 1), CellType.Car);
        CarCells[2] = new Cell(centerCell.Row, (ushort)(centerCell.Col + 1), CellType.Car);
        CarCells[4] = new Cell((ushort)(centerCell.Row + 1), centerCell.Col, CellType.Car);
        CarCells[5] = new Cell((ushort)(centerCell.Row + 2), (ushort)(centerCell.Col - 1), CellType.Car);
        CarCells[6] = new Cell((ushort)(centerCell.Row + 2), (ushort)(centerCell.Col + 1), CellType.Car);
    }


    public void MoveDown()
    {
        // не думаю, что это наилучшая реализация. Можно объединить движение своей машины и остальных.
        // добавить направление движения через какой-нибудь GetShiftedCell

        foreach (var cell in CarCells)
        {
            cell.Row += 1;
        }
    }
}



public enum Direction
{
    Left, Right, Up, Down
}