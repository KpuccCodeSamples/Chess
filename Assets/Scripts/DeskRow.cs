using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskRow : MonoBehaviour
{
    public DeskCell[] m_RowCells;
    
    ///////////////
    public void SetupRow(int rowIndex)
    {
        switch (rowIndex)
        {
            // устанавлиаем пешки
            case 1:
            case 6:
                for (int i = 0; i < m_RowCells.Length; i++)
                {
                    Vector2 coordinates = new Vector2(rowIndex, i);
                    ChessFigure figure = DeskController.Instance.GetFigurePrefab(ChessFiguresType.Pawn);

                    m_RowCells[i].SetupCell(coordinates, figure);
                }
                break;
            
            // устанавливаем особые фигуры 
            case 0:
            case 7:
                for (int i = 0; i < m_RowCells.Length; i++)
                {
                    ChessFigure figure = null; 

                    if (i == 0 || i == 7)
                    {
                        figure = DeskController.Instance.GetFigurePrefab(ChessFiguresType.Rook);
                    }
                    else if (i == 1 || i == 6)
                    {
                        figure = DeskController.Instance.GetFigurePrefab(ChessFiguresType.Knight);
                    }
                    else if (i == 2 || i == 5)
                    {
                        figure = DeskController.Instance.GetFigurePrefab(ChessFiguresType.Bishop);
                    }
                    else if (i == 3)
                    {
                        figure = DeskController.Instance.GetFigurePrefab(ChessFiguresType.Queen);
                    }
                    else
                    {
                        figure = DeskController.Instance.GetFigurePrefab(ChessFiguresType.King);
                    }

                    Vector2 coordinates = new Vector2(rowIndex, i);
                    m_RowCells[i].SetupCell(coordinates, figure);
                }
                break;

            default:
                for (int i = 0; i < m_RowCells.Length; i++)
                {
                    Vector2 coordinates = new Vector2(rowIndex, i);
                    m_RowCells[i].SetupCell(coordinates, null);
                }
                break;
        }
    }
}
