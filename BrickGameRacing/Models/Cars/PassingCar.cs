using BrickGameRacing.Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickGameRacing;

/// 
/// * Сделать методы генерации новых машин
///     - Нужно, чтобы всегда был проход между машинами 
///     - И нужно, чтобы они генерировались на разных полосах (только как сделать так, чтобы этот класс понимал, где расположены эти полосы)
/// * Сделать так, чтобы при движении как-то удалялись данные о типе клетки для прошлого соcтояния машины
/// 
/// 

public class PassingCar(Cell centerCell) : Car(centerCell)
{
    private Random _rnd = new();



    public void Move()
    {
        foreach (var cell in CarCells)
        {
            cell.Row++;
        }
    }
}
