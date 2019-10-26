using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public enum ChessColor
{
    White = 0,
    Black = 1
}

public class ChessFigure : MonoBehaviour
{
    public DeskCell Cell { get; private set; }
    public ChessColor FigureColor { get; private set; }

    public Image m_FigureSprite;
    public Sprite[] m_FiguresSprites;

    ////////////////
    public virtual List<Vector2> GetAvailableCellsCoordinates()
    {
        return null;
    }

    ////////////////
    public void SetupFigure(ChessColor color, DeskCell cell)
    {
        FigureColor = color;
        m_FigureSprite.overrideSprite = m_FiguresSprites[(int)color];
        Cell = cell;
    }

    ////////////////
    public void MoveFigure(DeskCell targetCell)
    {
        transform.SetParent(targetCell.transform);
        transform.DOLocalMove(Vector3.zero, 1).SetEase(Ease.InOutCubic);

        Cell.Figure = null;

        Cell = targetCell;
        targetCell.Figure = this;
    }
}
