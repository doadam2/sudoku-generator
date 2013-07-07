using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGenetic
{
    class SudokuGeneric
    {
        private static Random r = new Random();
        private int[,] m_Gene;
        public int Fitness { get; private set; }

        public SudokuGeneric()
        {
            m_Gene = new int[9,9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    m_Gene[i, j] = r.Next(1, 10);
            calculateFitness();
        }

        public int getGene(int i, int j)
        {
            return m_Gene[i, j];
        }

        public SudokuGeneric(SudokuGeneric parent)
        {
            m_Gene = new int[9,9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    m_Gene[i, j] = parent.getGene(i, j);
            mutate();
        }

        private void calculateFitness()
        {
            int sum = 0;

            // row check
            int[] arr = new int[10];
            for (int i = 0; i < 9; i++)
                arr[i] = 0;
            for (int i = 0; i < 9; i++)
            {
                
                for (int j = 0; j < 9; j++)
                    arr[m_Gene[i, j]]++;
                for (int n = 0; n < 10; n++)
                {
                    if (arr[n] > 1)
                        sum += arr[n] - 1;
                    arr[n] = 0;
                }
            }

            // column check
            for (int i = 0; i < 9; i++)
            {
      
                for (int j = 0; j < 9; j++)
                    arr[m_Gene[j, i]]++;

                for (int n = 0; n < 10; n++)
                {
                    if (arr[n] > 1)
                        sum += arr[n] - 1;
                    arr[n] = 0;
                }
            }



            // squares check
            for (int right = 0; right < 7; right += 3)
            {
                for (int k = 0; k < 7; k += 3)
                {

                    for (int i = 0; i < 9; i++)
                        arr[i] = 0;
                    for (int i = 0 + k; i < k + 3; i++)
                        for (int j = 0+right; j < 3+right; j++)
                            arr[m_Gene[i, j]]++;

                    for (int n = 0; n < 10; n++)
                    {
                        if (arr[n] > 1)
                            sum += arr[n] - 1;
                        arr[n] = 0;
                    }
                }
            }
            
            Fitness = sum;
        }

        public void mutate()
        {
            // Mutate multiple times for more unique creature
            for (int q = 1; q < r.Next(6); q++)
                m_Gene[r.Next(0, 9), r.Next(0, 9)] = r.Next(1, 10);

	    /* hard-coded for solving
		m_Gene[8,0] = 5;
		m_Gene[0,0] = 7;
		m_Gene[0,1] = 3;
		m_Gene[0,2] = 6;
		m_Gene[0,3] = 2;
		m_Gene[0,4] = 5;
		m_Gene[0,5] = 9;
		m_Gene[0,8] = 8;
		m_Gene[8,8] = 4;
*/
	    calculateFitness();
        }
    }
}
