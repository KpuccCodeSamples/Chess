using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingFigure : ChessFigure
{
    ////////////////
    public override List<Vector2> GetAvailableCellsCoordinates()
    {
        List<Vector2> availableCells = new List<Vector2>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int cellRowNum = (int)Cell.CellCoordinates.x + i;
                int cellColumnNum = (int)Cell.CellCoordinates.y + j;

                DeskCell cell = DeskController.Instance.GetCell(cellRowNum, cellColumnNum);

                // если ячейка за пределами доски, пропускаем
                if (cell == null)
                    continue;

                // если в ячейке есть своя фигура, пропускаем
                if (cell.Figure != null && cell.Figure.FigureColor == FigureColor)
                    continue;

                // TODO короля нельзя подставить под шах
                // DeskCell должен иметь метод, определяющий, находится ли ячейка под атакой черной или белой фигуры

                availableCells.Add(cell.CellCoordinates);
            }
        }
        
        return availableCells;
    }
}
