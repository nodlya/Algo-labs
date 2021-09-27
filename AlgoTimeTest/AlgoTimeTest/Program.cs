using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AlgoTimeTest
{
    class Program
    {
        public static Random Rnd = new Random(666);
        public static string Path = @"Data.csv";
        public static List<int> ElapsedTime = new List<int>(); 
        static void Main(string[] args)
        {
            int times = int.Parse(Console.ReadLine());
            CallAlgo(times);
            File.AppendAllText(Path, "\n");
            
        }

        static void CallAlgo(int times)
        {
            using (FileStream fstream = new FileStream(Path, FileMode.Append))
            {
                for (int i = 0; i < times; i++)
                {
                    int elapsedTime;
                    int tries = 0;
                    do
                    {
                        elapsedTime = PerformAlgo(i);
                        tries++;
                        if (ElapsedTime.Count < 2) break;
                    }
                    while (elapsedTime > 2 * ElapsedTime[i - 1] || tries < 3);

                    ElapsedTime.Add(elapsedTime);
                    byte[] array = 
                        System.Text.Encoding.Default.GetBytes(elapsedTime.ToString()+";");
                    fstream.Write(array);
                    //File.AppendAllText(Path, elapsedTime + ";"); 
                }
            }
        }

        static int PerformAlgo(int k)
        {
            Stopwatch stopwatch = new Stopwatch();
            //prepare

            //int[] mas = new int[k];
            //for (int i = 0; i < k; i++)
            //    mas[i] = Rnd.Next();

            int[,] mas1 = new int[k,k];
            int[,] mas2 = new int[k, k];
            for (int i = 0;i<k;i++)
                for (int j = 0; j<k;j++)
                {
                    mas1[i, j] = Rnd.Next();
                    mas2[i, j] = Rnd.Next();
                }
            int[,] result = new int[mas1.Length, mas1.Length];

            //int sum=0;
            //int multiple = 1;

            //int v = Rnd.Next();
            //var x = 1.5;
            //double p = 0;

            stopwatch.Start();

            //start algo

            //2 algo
            //for (int i=0; i<mas.Length;i++)
            //    sum += mas[i];

            //3 algo
            //for (int i = 0; i < mas.Length; i++)
            //multiple *= mas[i];


            //4 algo 
            //for (int j = 1; j <= k; j++)
            //    p += v * Math.Pow(x, j - 1);

            //5 algo
            //for (int i=0; i<mas.Length-1; i++)
            //    for (int j=i+1; j<mas.Length; j++)
            //        if (mas[i]>mas[j])
            //        {
            //            int temp = mas[i];
            //            mas[i] = mas[j];
            //            mas[j] = temp;
            //        }

            //6 algo
            
            for (int i = 0; i < mas1.GetLength(0); i++)
            {
                for (int j = 0; j < mas2.GetLength(1); j++)
                {
                    for (int l = 0; k < mas1.GetLength(0); l++)
                    {
                        result[i, j] += result[i, l] * result[l, j];
                    }
                }
            }

            //finish algo


            stopwatch.Stop();
            return (int)stopwatch.ElapsedTicks * 10;
        }

        static void EmptyCsv()
        {
            File.WriteAllText(Path, string.Empty);
        }


        static int[,] MatrixMultiply(int[,] mas1, int[,] mas2)
        {
            int len = mas1.GetLength(0);
            int[,] result = new int[len,len];
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    for (int k = 0; k < len; k++)
                    {
                        result[i, j] += mas1[i, k] * mas2[k, j];
                    }
                }
            }


            return result;
        }

        static void PrintMatrix(int[,]mas)
        {
            int k = mas.GetLength(0);
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                    Console.Write(mas[i, j]+" ");
                Console.WriteLine();
            }
            
        }
        
    }
}
