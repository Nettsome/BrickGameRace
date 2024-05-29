﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BrickGameRacing.Models.Cells;

namespace BrickGameRacing.Models.Field;

public class Field : List<CellInfo>, INotifyCollectionChanged, INotifyPropertyChanged
{
    public Borders leftborder;
    public Borders rightborder;

    private int _width;
    private int _height;

    private ushort _rows;       // = 20 
    private ushort _cols;       // = 10 

    public ushort Rows       // кол-во строк
    {
        get => _rows;
        private set => _rows = ushort.Min(ushort.Max(value, 6), 50);
    }

    public ushort Cols
    {
        get => _cols;
        private set => _cols = ushort.Min(ushort.Max(6, value), 50);
    }

    public int Width
    {
        get => _width;
        set
        {
            SetField(ref _width, value);
            UpdateCells();
        }
    }

    public int Height
    {
        get => _height;
        set
        {
            SetField(ref _height, value);
            UpdateCells();
        }
    }

    private int CellSize => int.Min(Height / Rows, Width / Cols);
    private int LShift => (Width - CellSize * Cols) / 2;
    private int TShift => (Height - CellSize * Rows) / 2;

    private CellInfo this[int row, int col]
    {
        get
        {
            //        \/ подумать над этим
            if (row < -5 || row >= _rows + 5 || col < 0 || col >= _cols)
                throw new ArgumentOutOfRangeException($"Выход за границу игрового поля");
            return this[(row + 5) * Cols + col];
        }
    }

    public Field(ushort rows = 20, ushort cols = 10)
    {
        Reset(rows, cols);

        leftborder = new(rows, 0);
        rightborder = new(rows, (ushort)(cols - 1));

        ChangeCells(leftborder.Walls.ToList());
        ChangeCells(rightborder.Walls.ToList());
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public void Reset(ushort rows, ushort cols)
    {
        Rows = rows;
        Cols = cols;
        Reset();
    }
    public void Reset()
    {
        Clear();
        for (short i = -5; i < Rows + 5; i++)
        {
            for (ushort j = 0; j < Cols; j++)
            {
                Add(new CellInfo(i, j));
            }
        }
        UpdateCells();
    }

    public void UpdateCells()
    {
        if (Height > 0 && Width > 0)
        {
            foreach (var cell in this)                          
            {
                cell.Left = CellSize * cell.Col + LShift;
                cell.Top = CellSize * cell.Row + TShift;
                cell.Size = CellSize;
            }
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }

    public void ChangeCells(List<Cell> cells)
    {
        foreach (var cell in cells)
        {
            this[cell.Row, cell.Col].Type = cell.Type;
        }
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public void MoveBorders()
    {
        List<Cell> bordersmove = new();
        bordersmove.AddRange(leftborder.Move());
        bordersmove.AddRange(rightborder.Move());
        
        ChangeCells(bordersmove);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Зачем нужен этот SetField?
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
