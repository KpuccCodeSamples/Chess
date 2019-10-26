﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopFigure : ChessFigure
{
    ////////////////
    public override List<Vector2> GetAvailableCellsCoordinates()
    {
        return GetBishopAvailableCells((int)Cell.CellCoordinates.x, (int)Cell.CellCoordinates.y, FigureColor);
    }

    ////////////////
    public static List<Vector2> GetBishopAvailableCells(int rowIndex, int columnIndex, ChessColor figureColor)
    {
        List<Vector2> availableCells = new List<Vector2>();

        // проверяем ячейки по диагонали вправо вверх
        for (int i = 1; i <= 7; i++)
        {
            int cellColumnNum = columnIndex + i;
            int cellRowNum = rowIndex + i;

            DeskCell cell = DeskController.Instance.GetCell(cellRowNum, cellColumnNum);

            // если ячейка за пределами доски, пропускаем и дальше не смотрим
            if (cell == null)
                break; ;

            // если в ячейке есть своя фигура, пропускаем и дальше не смотрим
            if (cell.Figure != null && cell.Figure.FigureColor == figureColor)
                break;

            availableCells.Add(cell.CellCoordinates);

            // если в ячейке есть чужая фигура, дальше не смотрим
            if (cell.Figure != null && cell.Figure.FigureColor != figureColor)
                break;
        }

        // проверяем ячейки по диагонали влево вниз
        for (int i = -1; i >= -7; i--)
        {
            int cellColumnNum = columnIndex + i;
            int cellRowNum = rowIndex + i;

            DeskCell cell = DeskController.Instance.GetCell(cellRowNum, cellColumnNum);

            // если ячейка за пределами доски, пропускаем
            if (cell == null)
                break;

            // если в ячейке есть своя фигура, пропускаем и дальше не смотрим
            if (cell.Figure != null && cell.Figure.FigureColor == figureColor)
                break;

            availableCells.Add(cell.CellCoordinates);

            // если в ячейке есть чужая фигура, дальше не смотрим
            if (cell.Figure != null && cell.Figure.FigureColor != figureColor)
                break;
        }

        // проверяем ячейки по диагонали влево вверх
        for (int i = 1; i <= 7; i++)
        {
            int cellRowNum = rowIndex + i;
            int cellColumnNum = columnIndex - i;

            DeskCell cell = DeskController.Instance.GetCell(cellRowNum, cellColumnNum);

            // если ячейка за пределами доски, пропускаем
            if (cell == null)
                break;

            // если в ячейке есть своя фигура, пропускаем и дальше не смотрим
            if (cell.Figure != null && cell.Figure.FigureColor == figureColor)
                break;

            availableCells.Add(cell.CellCoordinates);

            // если в ячейке есть чужая фигура, дальше не смотрим
            if (cell.Figure != null && cell.Figure.FigureColor != figureColor)
                break;
        }

        // проверяем ячейки по диагонали вправо вниз
        for (int i = -1; i >= -7; i--)
        {
            int cellRowNum = rowIndex + i;
            int cellColumnNum = columnIndex - i;

            DeskCell cell = DeskController.Instance.GetCell(cellRowNum, cellColumnNum);

            // если ячейка за пределами доски, пропускаем
            if (cell == null)
                break;

            // если в ячейке есть своя фигура, пропускаем и дальше не смотрим
            if (cell.Figure != null && cell.Figure.FigureColor == figureColor)
                break;

            availableCells.Add(cell.CellCoordinates);

            // если в ячейке есть чужая фигура, дальше не смотрим
            if (cell.Figure != null && cell.Figure.FigureColor != figureColor)
                break;
        }

        return availableCells;
    }
}
