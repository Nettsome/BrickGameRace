using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BrickGameRacing.Models.Cars;
using BrickGameRacing.Models.Cells;
using BrickGameRacing.Models.Field;

namespace BrickGameRacing.VievModels;

public class GameLogic(Field field)
{
    public event Action? OnStopGame;

    private Cars? cars;
    private List<Cell> LinesCenters = new();

    private Thread? _gamethread;
    private object _sync = new();

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


        // Создание потока, в котором будет перемещаться поле и будут генерироваться машинки 

        //_gamethread = new Thread(() =>
        //{
        //    while (IsActive)
        //    {
        //        // Генерация машины 
        //        //cars.CreateNewPassingCar();
        //        field.Move();
        //        cars.MovePassingCars();
        //        Thread.Sleep(2000);
        //    }
        //});
        //_gamethread.Start();
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


        return cars.AllCells;
    }

    public void MoveMainCar(Direction dir)
    {
        // если существует главная машина, то передвигаем ее

        // Временно 
        MovePassingCars();
    }

    private void MovePassingCars()
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
        Application.Current.Dispatcher.Invoke(() =>
        {
            try
            {
                field.ChangeCells(cells);
            }
            catch
            {
                IsActive = false;
            }
        });
    }

    private void UpdateCarField(List<Cell> cells)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            try
            {
                field.ChangeCellsToMoveDown(cells);
            }
            catch
            {
                IsActive = false;
            }
        });
    }
}