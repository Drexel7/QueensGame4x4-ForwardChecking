using System;
using System.Collections.Generic;
using System.Linq;

namespace QueensGame
{
    public class Board
    {
        public int[,] pozOcupata = new int[4, 4]; // pozitiile ocupate
        public int QueenCount { get; private set; } //nr regine pe tabla

        public List<(int x, int y)> PlacedQueens { get; private set; } = new List<(int x, int y)>();

        public void PlaceQueen(int x, int y)
        {
            if (pozOcupata[x, y] == 0 && QueenCount < 4)
            {
                pozOcupata[x, y] = 100; // pozitia unde se afla o regina
                PlacedQueens.Add((x, y)); // adaug coord gasite in lista PlacedQueens
                QueenCount++;
            }
        }

        public Board()
        {
            pozOcupata = new int[4, 4];
            QueenCount = 0;
        }

        public void ResetGame()
        {
            for (int m = 0; m < 4; m++)
            {
                for (int n = 0; n < 4; n++)
                {
                    pozOcupata[m, n] = 0;
                }
            }
            QueenCount = 0;
        }

        public void HandleMouseClick(int x, int y)
        {
            if (pozOcupata[x, y] == 0 && QueenCount < 4) // Daca celula nu e ocupata, plasez regina
            {
                pozOcupata[x, y] = 100;
                QueenCount++;

                // marcare celule folosite
                for (int k = 0; k < 4; k++)
                {
                    // constrangeri linie, coloana, daca sunt ocupate de o regina
                    if (pozOcupata[x, k] != 100) pozOcupata[x, k] = Math.Max(pozOcupata[x, k], 1);
                    if (pozOcupata[k, y] != 100) pozOcupata[k, y] = Math.Max(pozOcupata[k, y], 1);

                    // constrangeri diagonala
                    if ((x + k) < 4 && (y + k) < 4 && pozOcupata[x + k, y + k] != 100)
                        pozOcupata[x + k, y + k] = Math.Max(pozOcupata[x + k, y + k], 1);
                    if ((x - k) >= 0 && (y - k) >= 0 && pozOcupata[x - k, y - k] != 100)
                        pozOcupata[x - k, y - k] = Math.Max(pozOcupata[x - k, y - k], 1);
                    if ((x + k) < 4 && (y - k) >= 0 && pozOcupata[x + k, y - k] != 100)
                        pozOcupata[x + k, y - k] = Math.Max(pozOcupata[x + k, y - k], 1);
                    if ((x - k) >= 0 && (y + k) < 4 && pozOcupata[x - k, y + k] != 100)
                        pozOcupata[x - k, y + k] = Math.Max(pozOcupata[x - k, y + k], 1);
                }
            }
            else
            {
                System.Media.SystemSounds.Beep.Play(); // notificare de zgomot in caz de o celula este deja ocupata
            }
        }



        public List<(int, int)> CalculateBlockedSpaces()
        {
            List<(int, int)> blockedSpaces = new List<(int, int)>();

            // parcurgem tabla pentru a determina celulele blocate pe baza poz reginelor
            for (int i = 0; i < 4; i++)  
            {
                for (int j = 0; j < 4; j++) //4x4 
                {
                    // daca exista o regina, marchez blocare linie-col-dig
                    if (pozOcupata[i, j] == 100) //100-regina 
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            blockedSpaces.Add((i, x)); // blochez toate celulele din acelasi rand
                            blockedSpaces.Add((x, j)); // -//-coloana

                            // diag (jos-dreapta) 
                            if (i + x < 4 && j + x < 4)
                                blockedSpaces.Add((i + x, j + x));

                            // Diag (sus-dreapta) 
                            if (i - x >= 0 && j + x < 4)
                                blockedSpaces.Add((i - x, j + x));

                            // Diag (jos-stânga) 
                            if (i + x < 4 && j - x >= 0)
                                blockedSpaces.Add((i + x, j - x));

                            // Diag (sus-stânga) 
                            if (i - x >= 0 && j - x >= 0)
                                blockedSpaces.Add((i - x, j - x));
                        }
                    }
                }
            }

            // elimin duplicatele din lista de celule blocate
            return blockedSpaces.Distinct().ToList();
        }
    }
}

 