using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        _gamethread = new Thread(() =>
        {
            short movecounter = 0;
            while (IsActive)
            {
                //// Генерация машины 
                ////cars.CreateNewPassingCar();

                if (movecounter > 4)
                {
                    movecounter = 0;
                    cars?.CreateNewPassingCar();
                }

                UpdateField(NextMove());                     

                Thread.Sleep(300);
                movecounter++;
            }
        });
        _gamethread.Start();
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
        field.MoveBorders();
        MovePassingCars();
    }

    private List<Cell> MovePassingCars()          // сделать вывод в виде List<Cell>
    {
        List<Cell> movedCars = new();
        if (cars.AllCells.Count != 0)
        {
            movedCars.AddRange(cars.MovePassingCars());

            if (cars?.GetFirstCarCenterCell().Row > field.Rows + 1)
            {
                cars?.Dequeue();
            }
        }

        return movedCars;
    }


    private List<Cell> NextMove()           // нужен для одновременной работы генерации машин и передвижения стен и машин
    { 
        List<Cell> nextMove = new();

        lock (_sync)
        {
            // Генерация машины
            //cars?.CreateNewPassingCar();
            nextMove.AddRange(field.MoveBorders());
            nextMove.AddRange(MovePassingCars());
        }

        return nextMove;
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
}