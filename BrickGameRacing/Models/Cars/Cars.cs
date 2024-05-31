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
    public static Queue<PassingCar> passingCars = new();                                    // Оптимизация?: может сделать вместо очереди хэш таблицу
    public static MainCar? MainCar;                                                          // основная машина 
    private static List<Cell> MainCarPositions = new();
    public static List<Cell> LinesCenters = new();

    private Random _rnd = new();

    // может сделать отдельный метод для создания основной машины
    // чтобы основная машина создавалась только внизу



    public Cars(List<Cell> linescenters)
    {
        foreach (var cell in linescenters)
        {
            LinesCenters.Add(cell);
            MainCarPositions.Add(new Cell((short)(cell.Row + 20), cell.Col));           // Переделать это как-нибудь. Если у нас измениться кол-во строк, то этот код не будет работать
        }

        CreateNewPassingCar();
        CreateMainCar();
    }

 //===================================================================================================================================================================================


    private void CreateMainCar()
    {
        // Сделать одну машину и постоянно ее двигать и затирать прошлое положение

        if (MainCar != null)
            return;

        MainCar = new(MainCarPositions[0]);
    }

    public List<Cell> MoveMainCar(Direction dir)
    {

        MainCar.MoveTo(new Cell(LinesCenters[0]));


        return MainCar.Cells;
    }



 //===================================================================================================================================================================================


    public void CreateNewPassingCar()
    {
        // Создаем в рандомном месте машинку, так чтобы она не пересекалась с остальными  
        // и так, чтобы основная машина смогла проехать между побочными машинами 

        var line = LinesCenters[_rnd.Next(0, LinesCenters.Count)];

        if (CanCreatePassingCar(line))
        {
            passingCars.Enqueue(new PassingCar(line));
        }
    }

    private bool CanCreatePassingCar(Cell line)
    {
        // Если полос движения будет больше двух, то эта логика может не работать
        if (passingCars.Count == 0)
            return true;
;
        var lastcar = passingCars.Last();


        if (lastcar.CenterCell.Col == line.Col)
        {
            if (lastcar.CenterCell.Row - line.Row > 4)
                return true;
        }
        else
        {
            if (lastcar.CenterCell.Row - line.Row > 8)
                return true;
        }
        return false;
    }

    public List<Cell> MovePassingCars()                   
    {
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

            list.AddRange(MainCar.CarCells);

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
