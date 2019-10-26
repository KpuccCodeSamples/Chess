using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookFigure : ChessFigure
{
    ////////////////
    public override List<Vector2> GetAvailableCellsCoordinates()
    {
        return GetRookAvailableCells((int)Cell.CellCoordinates.x, (int)Cell.CellCoordinates.y, FigureColor);
    }

    ////////////////
    public static List<Vector2> GetRookAvailableCells(int rowIndex, int columnIndex, ChessColor figureColor)
    {
        List<Vector2> availableCells = new List<Vector2>();

        // проверяем ячейки по горизонтали вправо
        for (int i = 1; i <= 7; i++)
        {
            int cellColumnNum = columnIndex + i;

            DeskCell cell = DeskController.Instance.GetCell(rowIndex, cellColumnNum);

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

        // проверяем ячейки по горизонтали влево
        for (int i = -1; i >= -7; i--)
        {
            int cellColumnNum = columnIndex + i;

            DeskCell cell = DeskController.Instance.GetCell(rowIndex, cellColumnNum);

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

        // проверяем ячейки по вертикали вверх
        for (int i = 1; i <= 7; i++)
        {
            int cellRowNum = rowIndex + i;

            DeskCell cell = DeskController.Instance.GetCell(cellRowNum, columnIndex);

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

        // проверяем ячейки по вертикали вниз
        for (int i = -1; i >= -7; i--)
        {
            int cellRowNum = rowIndex + i;

            DeskCell cell = DeskController.Instance.GetCell(cellRowNum, columnIndex);

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
