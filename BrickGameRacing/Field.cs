﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BrickGameRacing;

public class Field : List<CellInfo>, INotifyCollectionChanged, INotifyPropertyChanged
{
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
            if (row < 0 || row >= _rows || col < 0 || col >= _cols)
                throw new ArgumentOutOfRangeException($"Выход за границу игрового поля");
            return this[row * Cols + col];
        }
    }

    public Field(ushort rows = 20, ushort cols = 10)
    {
        Reset(rows, cols);
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
        for (ushort i = 0; i < Rows; i++)
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
            foreach (var cell in this)                          // cell = CellInfo
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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Зачем нужен этот SetField
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
