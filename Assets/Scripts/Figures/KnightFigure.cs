using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFigure : ChessFigure
{
    public override List<DeskCell> GetAvailableCellsToMove(bool withAttackRange)
    {
        Vector2[] cellOffsets = new Vector2[8]
        {
            new Vector2(2, 1),
            new Vector2(2, -1),
            new Vector2(-2, 1),
            new Vector2(-2, -1),
            new Vector2(1, 2),
            new Vector2(1, -2),
            new Vector2(-1, 2),
            new Vector2(-1, -2),
        };

        List<DeskCell> availableCells = new List<DeskCell>();

        foreach (Vector2 coord in cellOffsets)
        {
            int cellRowNum = (int)coord.x + (int)Cell.CellCoordinates.x;
            int cellColumnNum = (int)coord.y + (int)Cell.CellCoordinates.y;

            DeskCell cell = DeskController.Instance.GetCell(cellRowNum, cellColumnNum);

            // если ячейка за пределами доски, пропускаем
            if (cell == null)
                continue;

            // если в ячейке есть своя фигура, пропускаем
            if (cell.Figure != null && cell.Figure.FigureColor == FigureColor)
                continue;

            // добавляем ячейку в доступные
            availableCells.Add(cell);
        }

        return availableCells;
    }
}
