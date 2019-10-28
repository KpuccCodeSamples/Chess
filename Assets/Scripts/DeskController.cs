using System;
using System.Collections.Generic;
using UnityEngine;

public enum ChessFiguresType
{
    Pawn = 0,
    Rook = 1,
    Knight = 2,
    Bishop = 3,
    Queen = 4,
    King = 5
}

public class DeskController : MonoBehaviour
{
    private static DeskController m_Instance;
    public static DeskController Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<DeskController>();

            return m_Instance;
        }
    }

    public ChessFigure ChosenFigure { get; private set; }

    public List<ChessFigure> WhiteFigures { get; private set; } = new List<ChessFigure>();
    public List<ChessFigure> BlackFigures { get; private set; } = new List<ChessFigure>();

    public DeskRow[] m_DeskRows;
    public ChessFigure[] m_ChessFiguresPrefabs;
    
    public bool m_IsWhiteTurn;

    private List<DeskCell> m_AvailableCells = new List<DeskCell>();

    public const int DeskSize = 8;

    public static event Action<List<DeskCell>> OnFigureChosen;

    /////////////
    private void Start()
    {
        for (int i = 0; i < DeskSize; i++)
        {
            m_DeskRows[i].SetupRow(i);
        }

        m_IsWhiteTurn = true;

        DeskCell.OnCellClicked += OnCellClicked;
    }

    /////////////
    public DeskCell GetCell(int rowIndex, int columnIndex)
    {
        if (rowIndex >= DeskSize || columnIndex >= DeskSize || rowIndex < 0 || columnIndex < 0)
            return null;

        return m_DeskRows[rowIndex].m_RowCells[columnIndex];
    }

    /////////////
    public bool IsCellUnderAttack(DeskCell cell, ChessColor figureToMoveColor)
    {
        List<ChessFigure> attackingFigures = figureToMoveColor == ChessColor.Black ? WhiteFigures : BlackFigures;

        for (int i = 0; i < attackingFigures.Count; i++)
        {
            ChessFigure figure = attackingFigures[i];
            
            List<DeskCell> cellUnderAttack = figure.GetAvailableCellsToMove(true);

            if (cellUnderAttack.Contains(cell))
                return true;
        }

        return false;
    }

    /////////////
    public ChessFigure GetFigurePrefab(ChessFiguresType type)
    {
        return m_ChessFiguresPrefabs[(int)type];
    }

    /////////////
    private void ChooseFigure(ChessFigure figure)
    {
        if (figure != null)
        {
            if (m_IsWhiteTurn && figure.FigureColor != ChessColor.White ||
                !m_IsWhiteTurn && figure.FigureColor != ChessColor.Black)
                return;
        }

        ChosenFigure = figure;

        if (ChosenFigure == null)
        {
            m_AvailableCells.Clear();
        }
        else
        {
            m_AvailableCells = ChosenFigure.GetAvailableCellsToMove();
        }

        OnFigureChosen?.Invoke(m_AvailableCells);
    }

    /////////////
    public void OnCellClicked(DeskCell targetCell)
    {
        // если фигура не выбрана, или выбрали фигуру такого же цвета, то просто выбираем фигуру
        if (ChosenFigure == null || targetCell.Figure != null && targetCell.Figure.FigureColor == ChosenFigure.FigureColor)
        {
            ChooseFigure(targetCell.Figure);
            return;
        }

        // если пытаемся пойти на недоступную клетку, то тормозим
        if (!m_AvailableCells.Contains(targetCell))
            return;

        // если съедаем фигуру, надо убрать ее с доски
        if (targetCell.Figure != null)
        {
            if (targetCell.Figure.FigureColor == ChessColor.Black)
                BlackFigures.Remove(targetCell.Figure);
            else
                WhiteFigures.Remove(targetCell.Figure);

            // добавить анимацию убирания фигуры с доски вместо уничтожения объекта
            Destroy(targetCell.Figure.gameObject);
        }

        // двигаем фигуру
        ChosenFigure.MoveFigure(targetCell);
        ChooseFigure(null);
        
        m_IsWhiteTurn = !m_IsWhiteTurn;
    }

    /////////////
    private KingFigure GetKing(ChessColor color)
    {
        for (int i = 0; i < WhiteFigures.Count; i++)
        {
            if (WhiteFigures[i] is KingFigure)
            {
                KingFigure king = WhiteFigures[i] as KingFigure;

                if (king.FigureColor == color)
                    return king;
            }
        }

        return null;
    }

    /////////////
    public bool CanKingMove(DeskCell cell, ChessColor kingColor)
    {
        KingFigure king = GetKing(kingColor);

        return king.GetAvailableCellsToMove().Count > 0;
    }
}
