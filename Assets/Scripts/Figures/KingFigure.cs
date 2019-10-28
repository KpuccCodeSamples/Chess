using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingFigure : ChessFigure
{
    public bool m_WasMoved = false;

    private DeskCell m_LongCastlingCell;
    private DeskCell m_ShortCastlingCell;

    ////////////////
    public override void SetupFigure(ChessColor color, DeskCell cell)
    {
        base.SetupFigure(color, cell);

        int rowIndex = (int)cell.CellCoordinates.x;
        int columnIndex = (int)Cell.CellCoordinates.y;

        m_ShortCastlingCell = DeskController.Instance.GetCell(rowIndex, columnIndex + 2);
        m_LongCastlingCell = DeskController.Instance.GetCell(rowIndex, columnIndex - 2);
    }

    ////////////////
    public override List<DeskCell> GetAvailableCellsToMove(bool withAttackRange = false)
    {
        List<DeskCell> availableCells = new List<DeskCell>();

        // проверяем стандартный диапазон
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int cellRowNum = (int)Cell.CellCoordinates.x + i;
                int cellColumnNum = (int)Cell.CellCoordinates.y + j;

                DeskCell cell = DeskController.Instance.GetCell(cellRowNum, cellColumnNum);

                if (withAttackRange)
                {
                    availableCells.Add(cell);
                    continue;
                }

                // если ячейка за пределами доски, пропускаем
                if (cell == null)
                    continue;

                // если в ячейке есть своя фигура, пропускаем
                if (cell.Figure != null && cell.Figure.FigureColor == FigureColor)
                    continue;

                // если ячейка под атакой, ход запрещен
                if (DeskController.Instance.IsCellUnderAttack(cell, FigureColor))
                    continue;

                availableCells.Add(cell);
            }
        }

        if (withAttackRange)
            return availableCells;

        // проверяем рокировку, если король не двигался и не под шахом
        if (!m_WasMoved && !IsUnderCheck())
        {
            bool isKingWhite = FigureColor == ChessColor.White;

            int kingColumnIndex = (int)Cell.CellCoordinates.y;
            bool isLongCastlingAvailable = true;
            bool isShortCastlingAvailable = true;

            // проверяем, чтобы между королем и ладьей не было фигур
            // проверяем короткую рокировку
            for (int i = kingColumnIndex + 1; i < 7; i++)
            {
                int rowIndex = isKingWhite ? 0 : 7;

                DeskCell cell = DeskController.Instance.GetCell(rowIndex, i);

                if (cell.Figure != null)
                {
                    isShortCastlingAvailable = false;
                    break;
                }
            }

            // проверяем длинную рокировку
            for (int i = kingColumnIndex - 1; i > 0; i--)
            {
                int rowIndex = isKingWhite ? 0 : 7;

                DeskCell cell = DeskController.Instance.GetCell(rowIndex, i);

                if (cell.Figure != null)
                {
                    isLongCastlingAvailable = false;
                    break;
                }
            }

            DeskCell rookCell = null;

            // проверяем ладьи, они должны стоять на своих местах и ими не должен быть совершен ход
            // если все условия выполнены - король шагает в сторону ладьи на 2 клетки
            if (isKingWhite)
            {
                if (isLongCastlingAvailable)
                {
                    rookCell = DeskController.Instance.GetCell(0, 0);

                    if (IsRookAvailableToCastling(rookCell))
                    {
                        availableCells.Add(m_LongCastlingCell);
                    }
                }

                if (isShortCastlingAvailable)
                {
                    rookCell = DeskController.Instance.GetCell(0, 7);

                    if (IsRookAvailableToCastling(rookCell))
                    {
                        availableCells.Add(m_ShortCastlingCell);
                    }
                }
            }
            else
            {
                if (isLongCastlingAvailable)
                {
                    rookCell = DeskController.Instance.GetCell(7, 0);

                    if (IsRookAvailableToCastling(rookCell))
                    {
                        availableCells.Add(m_LongCastlingCell);
                    }
                }

                if (isShortCastlingAvailable)
                {
                    rookCell = DeskController.Instance.GetCell(7, 7);

                    if (IsRookAvailableToCastling(rookCell))
                    {
                        availableCells.Add(m_ShortCastlingCell);
                    }
                }
            }
        }

        return availableCells;
    }

    ////////////////
    public override void MoveFigure(DeskCell targetCell)
    {
        base.MoveFigure(targetCell);

        // если король не делал ход, то надо проверить выполняется ли рокирвока
        if (!m_WasMoved)
        {
            int rowIndex = (int)targetCell.CellCoordinates.x;

            if (targetCell.CellCoordinates == m_LongCastlingCell.CellCoordinates)
            {
                RookFigure rook = DeskController.Instance.GetCell(rowIndex, 0).Figure as RookFigure;
                rook.MoveFigure(rook.m_CastlingCell);
            }
            else if (targetCell.CellCoordinates == m_ShortCastlingCell.CellCoordinates)
            {
                RookFigure rook = DeskController.Instance.GetCell(rowIndex, 7).Figure as RookFigure;
                rook.MoveFigure(rook.m_CastlingCell);
            }
        }
        
        m_WasMoved = true;
    }

    ////////////////
    public bool IsUnderCheck()
    {
        return DeskController.Instance.IsCellUnderAttack(Cell, FigureColor);
    }

    ////////////////
    private bool IsRookAvailableToCastling(DeskCell rookCell)
    {
        if (rookCell.Figure != null && rookCell.Figure is RookFigure)
        {
            RookFigure rook = rookCell.Figure as RookFigure;

            return !rook.m_WasMoved;
        }

        return false;
    }
}
