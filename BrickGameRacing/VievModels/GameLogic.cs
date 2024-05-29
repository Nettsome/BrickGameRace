using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrickGameRacing.Models.Cars;
using BrickGameRacing.Models.Cells;
using BrickGameRacing.Models.Field;

namespace BrickGameRacing.VievModels;

public class GameLogic(Field field)
{
    public event Action? OnStopGame;
    private Cars? cars;
    private List<Cell> LinesCenters = new();

    public bool IsActive
    {
        get => Cell.IsActiveField;
        private set
        {
            Cell.IsActiveField = value;
            field.UpdateCells();
            if (!value) OnStopGame?.Invoke();
        }
    }

    public void Start()
    {
        if (IsActive) return;
        IsActive = true;

        InitGame();
    }

    private void InitGame()
    {
        IsActive = true;

        InitLines();
        UpdateField(InitCars());
    }

    private void InitLines()
    {
        LinesCenters.Clear();

        LinesCenters.Add(new Cell(-3, 3));
        LinesCenters.Add(new Cell(-3, 6));
    }

    private List<Cell> InitCars()
    {
        // Временная функция
        cars = new(LinesCenters);


        //cars.CreateNewCar();
        return cars.AllCells;
    }

    public void MoveMainCar(Direction dir)
    {
        // если существует главная машина, то передвигаем ее

        // Временно 
        MovePassingCars();
    }

    public void MovePassingCars()
     {
        // сделать ограничения, чтобы машина удалялась при выходе из поля видимости
        if (cars.AllCells.Count != 0)
        {
            cars?.MovePassingCars();
            field.ChangeCellsToMoveDown(cars.AllCells);

            if (cars?.GetFirstCarCenterCell().Row > field.Rows + 1)
            {
                cars?.Dequeue();
            }
        }
    }

    private void UpdateField(List<Cell> cells)
    {
        field.ChangeCells(cells);
    }
}