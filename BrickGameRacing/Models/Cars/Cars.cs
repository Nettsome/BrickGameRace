using BrickGameRacing.Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BrickGameRacing.Models.Cars;

public class Cars
{
    public static Queue<PassingCar> passingCars = new();                                    // Оптимизация: может сделать вместо очереди хэш талблицу
    public static Car? MainCar;                                                             // основная машина 
    public static List<Cell> LinesCenters = new();

    private Random _rnd = new();

    // может сделать отдельный метод для создания основной машины
    // чтобы основная машина создавалась только внизу



    public Cars(List<Cell> centers)
    {
        // метод создания главной машины
        // задание центров для каждой полосы

        foreach (var cell in centers)
        {
            LinesCenters.Add(cell);
        }

        CreateNewPassingCar();
        //CreateMainCar
    }

    public void CreateMainCar()
    {
        // 
    }

    public void CreateNewPassingCar()
    {
        // сделать ограничение, чтобы машины создавались на какой-то из двух полос дороги и нигде больше

        // Создаем в рандомном месте машинку, так чтобы она не пересекалась с остальными  
        // и так, чтобы основная машина смогла проехать между побочными машинами 

        passingCars.Enqueue(new PassingCar(LinesCenters[_rnd.Next(0, LinesCenters.Count)]));       // временная реализация

    }

    public List<Cell> MovePassingCars()                   // TODO: может тоже сделать не void, а List
    {
        // Сдвигаем одновременно все машинки, кроме своей, вниз 
        // Сделать проверку пересечения главной машинки с остальными и если она пересекает, то завершаем игру

        List<Cell> movedCars = new();

        foreach (var car in passingCars)
        {
            car.Move();
            movedCars.AddRange(car.CarCells);
        }

        return movedCars;
    }

    public Cell GetFirstCarCenterCell()         // rename
    {
        var car = passingCars.Peek();
        return car.CenterCell;
    }

    public void Dequeue()
    {
        // Очистка поля, а то у нас на поле остается отрисовка машины (думаю убрать ее путем скрытия машины под экран)
        passingCars.Dequeue();
    }


    public List<Cell> AllCells
    {
        get
        {
            List<Cell> list = new();

            //list.AddRange(MainCar.CarCells);

            foreach (Car car in passingCars) 
            {
                list.AddRange(car.CarCells);
            }

            return list;
        }
    }
}


public enum Direction
{
    Left, Right
}
