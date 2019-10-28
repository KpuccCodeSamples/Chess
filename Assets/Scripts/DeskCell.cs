using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskCell : MonoBehaviour
{
    public ChessFigure Figure { get; set; }
    public Vector2 CellCoordinates { get; private set; }
    public Image m_PlusImage;

    public static event Action<DeskCell> OnCellClicked;

    ///////////////
    public void SetupCell(Vector2 cellCoordinates, ChessFigure figure)
    {
        CellCoordinates = cellCoordinates;

        if (figure != null)
        {
            Figure = Instantiate(figure, gameObject.transform);

            if (CellCoordinates.x <= 1)
                Figure.SetupFigure(ChessColor.White, this);
            else
                Figure.SetupFigure(ChessColor.Black, this);
        }

        m_PlusImage.gameObject.SetActive(false);

        DeskController.OnFigureChosen += UpdateCellAvailability;
    }

    ///////////////
    public void OnClickCell()
    {
        OnCellClicked?.Invoke(this);
    }

    ///////////////
    private void UpdateCellAvailability(List<DeskCell> list)
    {
        if (list == null)
        {
            m_PlusImage.gameObject.SetActive(false);
            return;
        }

        bool isCellAvailableToMove = list.Contains(this);

        m_PlusImage.gameObject.SetActive(isCellAvailableToMove);
    }
}
