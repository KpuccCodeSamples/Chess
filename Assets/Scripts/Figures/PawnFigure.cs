using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnFigure : ChessFigure
{
    public override List<DeskCell> GetAvailableCellsToMove(bool withAttackRange)
    {
        List<DeskCell> availableCells = new List<DeskCell>();

        int figureRow = (int)Cell.CellCoordinates.x;
        int figureColumn = (int)Cell.CellCoordinates.y;
        DeskCell cellToCheck = null;

        // черные пешки шагают сверху вниз
        if (FigureColor == ChessColor.Black)
        {
            if (figureRow > 0)
            {
                cellToCheck = DeskController.Instance.GetCell(figureRow - 1, figureColumn);

                if (cellToCheck.Figure == null)
                    availableCells.Add(cellToCheck);

                // второй прыжок проверяем только, если в первой клетке нет фигуры
                if (figureRow == 6 && cellToCheck.Figure == null)
                {
                    cellToCheck = DeskController.Instance.GetCell(figureRow - 2, figureColumn);

                    if (cellToCheck.Figure == null)
                        availableCells.Add(cellToCheck);
                }

                // пешки могут атаковать противника только по диагонали
                // проверяем первую диагональную ячейку
                cellToCheck = DeskController.Instance.GetCell(figureRow - 1, figureColumn - 1);

                if (cellToCheck != null && cellToCheck.Figure != null && cellToCheck.Figure.FigureColor != FigureColor
                    || withAttackRange)
                {
                    availableCells.Add(cellToCheck);
                }

                // проверяем вторую диагональную ячейку
                cellToCheck = DeskController.Instance.GetCell(figureRow - 1, figureColumn + 1);

                if (cellToCheck != null && cellToCheck.Figure != null && cellToCheck.Figure.FigureColor != FigureColor
                    || withAttackRange)
                {
                    availableCells.Add(cellToCheck);
                }
            }
        }
        // белые шагают снизу вверх
        else
        {
            if (figureRow < 7)
            {
                // проверяем впереди стоящую ячейку
                cellToCheck = DeskController.Instance.GetCell(figureRow + 1, figureColumn);

                if (cellToCheck.Figure == null)
                    availableCells.Add(cellToCheck);

                // второй прыжок проверяем только, если в первой клетке нет фигуры
                if (figureRow == 1 && cellToCheck.Figure == null)
                {
                    cellToCheck = DeskController.Instance.GetCell(figureRow + 2, figureColumn);

                    if (cellToCheck.Figure == null)
                        availableCells.Add(cellToCheck);
                }

                // проверяем первую диагональную ячейку
                cellToCheck = DeskController.Instance.GetCell(figureRow + 1, figureColumn - 1);

                if (cellToCheck != null && cellToCheck.Figure != null && cellToCheck.Figure.FigureColor != FigureColor)
                {
                    availableCells.Add(cellToCheck);
                }

                // проверяем вторую диагональную ячейку
                cellToCheck = DeskController.Instance.GetCell(figureRow + 1, figureColumn + 1);

                if (cellToCheck != null && cellToCheck.Figure != null && cellToCheck.Figure.FigureColor != FigureColor)
                {
                    availableCells.Add(cellToCheck);
                }
            }
        }

        return availableCells;
    }
}
