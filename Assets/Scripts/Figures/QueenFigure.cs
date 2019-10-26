using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenFigure : ChessFigure
{
    ////////////////
    public override List<Vector2> GetAvailableCellsCoordinates()
    {
        List<Vector2> availableCells = RookFigure.GetRookAvailableCells((int)Cell.CellCoordinates.x, (int)Cell.CellCoordinates.y, FigureColor);

        foreach (Vector2 coord in BishopFigure.GetBishopAvailableCells((int)Cell.CellCoordinates.x, (int)Cell.CellCoordinates.y, FigureColor))
        {
            if (!availableCells.Contains(coord))
                availableCells.Add(coord);
        }

        return availableCells;
    }
}
