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
    public virtual List<DeskCell> GetAvailableCellsToMove(bool withAttackRange = false)
    {
        return null;
    }

    ////////////////
    public virtual void SetupFigure(ChessColor color, DeskCell cell)
    {
        FigureColor = color;
        m_FigureSprite.overrideSprite = m_FiguresSprites[(int)color];
        Cell = cell;

        if (color == ChessColor.White)
            DeskController.Instance.WhiteFigures.Add(this);
        else
            DeskController.Instance.BlackFigures.Add(this);
    }

    ////////////////
    public virtual void MoveFigure(DeskCell targetCell)
    {
        transform.SetParent(targetCell.transform);
        transform.DOLocalMove(Vector3.zero, 1).SetEase(Ease.InOutCubic);

        Cell.Figure = null;

        Cell = targetCell;
        targetCell.Figure = this;
    }
}
