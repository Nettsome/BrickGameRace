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
    public static List<Car> cars = new();       // Список всех машин (думаю, лучше перенести в отдельный файл)
    private Random _rnd = new();


    public void CreateNewCar()
    {
        // Создаем в рандомном месте машинку, так чтобы она не пересекалась с остальными  

        cars.Add(new Car(new Cell(3, 4)));       // временная реализация

    }

    public void Move()
    {
        // Сдвигаем одновременно все машинки, кроме своей, вниз 
        // Сделать проверку пересечения своей машинки с остальными и если она пересекает, то завершаем игру
    }

    public List<Cell> AllCells
    {
        get
        {
            List<Cell> list = new();

            foreach (Car car in cars) 
            {
                list.AddRange(car.CarCells);
            }

            return list;
        }
    }
}
