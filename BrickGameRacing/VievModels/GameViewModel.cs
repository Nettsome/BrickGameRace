using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrickGameRacing.Commands;
using BrickGameRacing.Models.Cars;
using BrickGameRacing.Models.Cells;
using BrickGameRacing.Models.Field;

namespace BrickGameRacing.VievModels;

public class GameViewModel
{
    private readonly ushort _rows = 20;
    private readonly ushort _cols = 10;
    public Field Field { get; init; }

    private Command? _startcommand;
    private Command? _movecommand;
    private Command? _stopcommand;

    private GameLogic? _game;

    public Command StartCommand => _startcommand = _startcommand ?? new(_ => Start(), _ => !_game?.IsActive ?? true);
    public Command MoveCommand => _movecommand ??= new(_ => Move(), _ => !_game?.IsActive ?? true);
    public Command? StopCommand => _startcommand ??= new(_ => Stop(), _ => !_game?.IsActive ?? true);    




    public GameViewModel()
    {
        Field = new(_rows, _cols);

        
    }

    private void Start()
    {
        _game = new(Field);

        _game.Start();
    }

    private void Move()
    {

    }

    private void Stop()
    {

    }
}
