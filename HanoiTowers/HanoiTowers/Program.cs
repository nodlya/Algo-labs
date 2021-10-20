using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace HanoiTowers
{
    class Program
    {
        public static string Path = @"Data.csv";
        static void Main(string[] args)
        {
            for (int i = 1; i < 25; i++)
            {
                TowerOfHanoi Tower = new TowerOfHanoi(i);
                List<int> time = new List<int>();
                Stopwatch stopwatch = new Stopwatch();
                using (FileStream fstream = new FileStream(Path, FileMode.Append))
                {
                    for (int j = 0; j < 5; j++)
                    {
                        stopwatch.Start();
                        Tower.FirstMove();
                        stopwatch.Stop();
                        time.Add((int)stopwatch.ElapsedTicks * 10);
                    }
                    time.Sort();
                    int median = time[2];
                    byte[] array =
                        System.Text.Encoding.Default.GetBytes(median.ToString() + ";");
                    fstream.Write(array);
                }

                //Console.WriteLine("Количество перестановок: " + Tower.MovesCount);
            }
        }

    }
    class TowerOfHanoi
    {
        int discsCount;
        public int MovesCount { get; private set; }

        public TowerOfHanoi(int discsCount)
        {
            this.discsCount = discsCount;
            MovesCount = 0;
        }

        public void FirstMove()
        {
            Move(discsCount,1, 3, 2);
        }
        private void Move(int n, int from, int to, int other)
        {
            if (n > 0)
            {
                Move(n - 1, from, other, to);
                //Console.WriteLine("Диск номер {0} перенесён с башни {1} на башню {2}",
                //                   n, from, to);
                MovesCount++;
                Move(n - 1, other, to, from);
            }
        }
    }
}
