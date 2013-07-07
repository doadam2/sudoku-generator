using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGenetic
{
    class Program
    {

        public static void drawSudoku(SudokuGeneric sg)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    Console.Write("|{0}", sg.getGene(i, j));
                Console.WriteLine("|");
            }
        }
        static SudokuGeneric[] generation = new SudokuGeneric[100];
        static void Main(string[] args)
        {
            for (int i = 0; i < generation.Length; i++)
                generation[i] = new SudokuGeneric();

            Console.WriteLine("Sudoku generator by Adam");
            Console.ReadKey();
            Console.Clear();
            int bestFitness = -1, count = 1;
            DateTime start = DateTime.Now;
            Random r = new Random();
            Console.WriteLine("Working...");
            StringBuilder sb = new StringBuilder();
            do
            {
                for (int times = 0; bestFitness != 0 && times < 100; times++)
                {
                    Array.Sort(generation, (g1, g2) => g1.Fitness - g2.Fitness);

                    bestFitness = generation[0].Fitness;

                    count++;

                    int more_random = 30;

                    for (int i = more_random; i < generation.Length; i++)
                        generation[i] = new SudokuGeneric(generation[i / (generation.Length / more_random)]);
                }
                //Console.SetCursorPosition(0, 0);
                //drawSudoku(generation[0]);
                sb.AppendFormat("Generation {0} ({1}), best={2}, G/S={3}\r", count, DateTime.Now.Subtract(start), bestFitness, (count/(DateTime.Now.Subtract(start).TotalSeconds+1)).ToString());
                Console.Title = sb.ToString();
                sb.Remove(0, sb.Length);
            } while (bestFitness != 0);
            Console.Clear();
            drawSudoku(generation[0]);
            TimeSpan end = DateTime.Now.Subtract(start); //this is including printing, which takes most of the time

            Console.CursorLeft = 0;
            Console.CursorTop = 10;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Created Sudoku puzzle in {0} generations within time {1}", count, end);
            Console.Beep();

            Console.ForegroundColor = ConsoleColor.White;

            Console.ReadKey();
        }
    }
}
