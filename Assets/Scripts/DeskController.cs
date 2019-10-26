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

    public DeskRow[] m_DeskRows;
    public ChessFigure[] m_ChessFiguresPrefabs;

    private int m_TurnCounter;
    private bool m_IsWhiteTurn;
    private List<Vector2> m_AvailableCells;

    public const int RowAndColumnCount = 8;

    public static event Action<List<Vector2>> OnFigureChosen;

    /////////////
    private void Start()
    {
        for (int i = 0; i < RowAndColumnCount; i++)
        {
            m_DeskRows[i].SetupRow(i);
        }

        m_IsWhiteTurn = true;

        DeskCell.OnCellClicked += OnCellClicked;
    }

    /////////////
    public DeskCell GetCell(int rowIndex, int columnIndex)
    {
        if (rowIndex >= RowAndColumnCount || columnIndex >= RowAndColumnCount || rowIndex < 0 || columnIndex < 0)
            return null;

        return m_DeskRows[rowIndex].m_RowCells[columnIndex];
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
            m_AvailableCells = ChosenFigure.GetAvailableCellsCoordinates();
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
        if (!m_AvailableCells.Contains(targetCell.CellCoordinates))
            return;

        // если съедаем фигуру, надо убрать ее с доски
        if (targetCell.Figure != null)
        {
            // добавить анимацию убирания фигуры с доски вместо уничтожения объекта
            Destroy(targetCell.Figure.gameObject);
        }

        // двигаем фигуру
        ChosenFigure.MoveFigure(targetCell);
        ChooseFigure(null);

        m_TurnCounter++;
        m_IsWhiteTurn = m_TurnCounter % 2 == 0;
    }
}
