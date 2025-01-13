using QueensGame;
using System;
using System.Collections.Generic;
using System.Drawing;

public class DrawBoard
{
    private Board board;
    private Image queenImage;

    public DrawBoard(Board board)
    {
        this.board = board;
        this.queenImage = Image.FromFile("queen.png");
    }

    public void Draw(Graphics g, bool showHints)
    {
        Color lightSquareColor = Color.LightBlue;
        Color darkSquareColor = Color.MidnightBlue;
        List<(int, int)> blockedSpaces = new List<(int, int)>();

        // daca fwrd e activat - colorez celulele blocate
        if (showHints)
        {
            blockedSpaces = board.CalculateBlockedSpaces(); 
        }

        // desenare tabela
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                bool isBlocked = blockedSpaces.Contains((i, j));

                if (isBlocked)
                {
                    g.FillRectangle(Brushes.Red, 100 + i * 100, 100 + j * 100, 100, 100);
                }
                else
                {
                    Color squareColor = (i + j) % 2 == 0 ? lightSquareColor : darkSquareColor;
                    g.FillRectangle(new SolidBrush(squareColor), 100 + i * 100, 100 + j * 100, 100, 100);
                }

                g.DrawRectangle(Pens.Black, 100 + i * 100, 100 + j * 100, 100, 100); // conturul celulelor
            }
        }
        if (showHints)
        {
            ShowUnsafeSquares(g); 
        }
        
        //afisare regine, dupa ce fac FW-CK
        DrawQueens(g);
    }

    private void DrawQueens(Graphics g)
    {
        for (int a = 0; a < 4; a++)  
        {
            for (int b = 0; b < 4; b++)  
            {
                if (board.pozOcupata[a, b] == 100) // =100 - regina
                {
                    g.DrawImage(queenImage, 100 + a * 100 + 10, 100 + b * 100 + 10, 80, 80);  // centrare regina 80px - 10 stg -100 drp
                }
            }
        }
    }
    //afisare celule rosii pt pozitii blocate
    private void ShowUnsafeSquares(Graphics g)
    {
        var blockedSpaces = board.CalculateBlockedSpaces();

        foreach (var square in blockedSpaces)
        {
            int x = square.Item1;
            int y = square.Item2;
            g.FillRectangle(Brushes.Red, 100 + x * 100, 100 + y * 100, 100, 100);  
            g.DrawRectangle(Pens.Black, 100 + x * 100, 100 + y * 100, 100, 100);  
        }
    }
}