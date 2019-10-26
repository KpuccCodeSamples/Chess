using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFigure : ChessFigure
{
    public override List<Vector2> GetAvailableCellsCoordinates()
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

        List<Vector2> availableCells = new List<Vector2>();

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
            availableCells.Add(new Vector2(cellRowNum, cellColumnNum));
        }

        return availableCells;
    }
}
