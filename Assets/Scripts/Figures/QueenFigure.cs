using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenFigure : ChessFigure
{
    ////////////////
    public override List<DeskCell> GetAvailableCellsToMove(bool withAttackRange)
    {
        List<DeskCell> availableCells = RookFigure.GetRookAvailableCells((int)Cell.CellCoordinates.x, (int)Cell.CellCoordinates.y, FigureColor);

        foreach (DeskCell cell in BishopFigure.GetBishopAvailableCells((int)Cell.CellCoordinates.x, (int)Cell.CellCoordinates.y, FigureColor))
        {
            if (!availableCells.Contains(cell))
                availableCells.Add(cell);
        }

        return availableCells;
    }
}
