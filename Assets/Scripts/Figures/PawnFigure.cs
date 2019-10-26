using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnFigure : ChessFigure
{
    public override List<Vector2> GetAvailableCellsCoordinates()
    {
        List<Vector2> availableCells = new List<Vector2>();

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
                    availableCells.Add(cellToCheck.CellCoordinates);

                if (figureRow == 6)
                {
                    cellToCheck = DeskController.Instance.GetCell(figureRow - 2, figureColumn);

                    if (cellToCheck.Figure == null)
                        availableCells.Add(cellToCheck.CellCoordinates);
                }

                // пешки могут атаковать противника только по диагонали
                // проверяем первую диагональную ячейку
                cellToCheck = DeskController.Instance.GetCell(figureRow - 1, figureColumn - 1);

                if (cellToCheck != null && cellToCheck.Figure != null && cellToCheck.Figure.FigureColor != FigureColor)
                {
                    availableCells.Add(cellToCheck.CellCoordinates);
                }

                // проверяем вторую диагональную ячейку
                cellToCheck = DeskController.Instance.GetCell(figureRow - 1, figureColumn + 1);

                if (cellToCheck != null && cellToCheck.Figure != null && cellToCheck.Figure.FigureColor != FigureColor)
                {
                    availableCells.Add(cellToCheck.CellCoordinates);
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
                    availableCells.Add(cellToCheck.CellCoordinates);
                
                if (figureRow == 1)
                {
                    cellToCheck = DeskController.Instance.GetCell(figureRow + 2, figureColumn);

                    if (cellToCheck.Figure == null)
                        availableCells.Add(cellToCheck.CellCoordinates);
                }

                // проверяем первую диагональную ячейку
                cellToCheck = DeskController.Instance.GetCell(figureRow + 1, figureColumn - 1);

                if (cellToCheck != null && cellToCheck.Figure != null && cellToCheck.Figure.FigureColor != FigureColor)
                {
                    availableCells.Add(cellToCheck.CellCoordinates);
                }

                // проверяем вторую диагональную ячейку
                cellToCheck = DeskController.Instance.GetCell(figureRow + 1, figureColumn + 1);

                if (cellToCheck != null && cellToCheck.Figure != null && cellToCheck.Figure.FigureColor != FigureColor)
                {
                    availableCells.Add(cellToCheck.CellCoordinates);
                }
            }
        }

        return availableCells;
    }
}
