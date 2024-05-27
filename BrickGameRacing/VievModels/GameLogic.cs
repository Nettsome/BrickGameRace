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

        UpdateField(InitCars());
    }

    private List<Cell> InitCars()
    {
        // Временная функция
        cars = new();
        cars.CreateNewCar();
        return cars.AllCells;
    }

    private void UpdateField(List<Cell> cells)
    {
        field.ChangeCells(cells);
    }
}