using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookFigure : ChessFigure
{
    public bool m_WasMoved = false;

    public DeskCell m_CastlingCell;

    ////////////////
    public override void SetupFigure(ChessColor color, DeskCell cell)
    {
        base.SetupFigure(color, cell);

        int rowIndex = (int)cell.CellCoordinates.x;
        int columnIndex = (int)Cell.CellCoordinates.y;

        if (columnIndex == 0)
            m_CastlingCell = DeskController.Instance.GetCell(rowIndex, columnIndex + 3);
        else if (columnIndex == 7)
            m_CastlingCell = DeskController.Instance.GetCell(rowIndex, columnIndex - 2);
    }

    ////////////////
    public override List<DeskCell> GetAvailableCellsToMove(bool withAttackRange)
    {
        return GetRookAvailableCells((int)Cell.CellCoordinates.x, (int)Cell.CellCoordinates.y, FigureColor);
    }

    ////////////////
    public static List<DeskCell> GetRookAvailableCells(int rowIndex, int columnIndex, ChessColor figureColor)
    {
        List<DeskCell> availableCells = new List<DeskCell>();

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

            availableCells.Add(cell);

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

            availableCells.Add(cell);

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

            availableCells.Add(cell);

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

            availableCells.Add(cell);

            // если в ячейке есть чужая фигура, дальше не смотрим
            if (cell.Figure != null && cell.Figure.FigureColor != figureColor)
                break;
        }

        return availableCells;
    }

    ////////////////
    public override void MoveFigure(DeskCell targetCell)
    {
        base.MoveFigure(targetCell);

        m_WasMoved = true;
    }
}
