using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AlgoTimeTest3
{
    class Program
    {
        public static Random Rnd = new Random(666);
        public static string Path = @"Data.csv";
        enum State
        {
            Empty,
            Visited
        }
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
                for (int i = 1; i <= times; i++)
                {
                    List<int> elapsedTime = new List<int>();
                    for (int j = 0; j < 5; j++)
                    {
                        elapsedTime.Add(PerformAlgo(i));
                    }

                    int min = elapsedTime.Min();
                    byte[] array =
                        System.Text.Encoding.Default.GetBytes(min.ToString() + ";");
                    fstream.Write(array);
                }
            }
        }

        static int PerformAlgo(int k)
        {
            Stopwatch stopwatch = new Stopwatch();
            var a = PrepareArray(k);
            int b = Rnd.Next();
            var c = PrepareArray(k);
            var d = PrepareMaze(k);
            
            stopwatch.Start();
            //start algo

            //Print(k); 
            //Sum(a);
            //Multiply(a);
            //Polinome(k,b);
            //BubbleSort(a);
            //QuickSort(a,0,a.Length-1);
            //TimSort(a, a.Length - 1);
            //Power(b, k);
            //MultiplyMatrixes(a, c);
            //VisitAllCells(d);

            //end algo
            stopwatch.Stop();
            return (int)stopwatch.ElapsedTicks * 10;
        }

        //1
        static void Print(int k)
        {
            Console.WriteLine(k);
        }

        //2
        static void Sum(int[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
                sum += array[i];
        }

        //3
        static void Multiply(int[] array)
        {
            int multiplication = 1;
            for (int i = 0; i < array.Length; i++)
                multiplication *= array[i];
        }

        //4
        static void Polinome(int k,int v)
        {
            double p = 0;
            double x = 1.5;
            for (int j = 1; j <= k; j++)
                p += v * Math.Pow(x, j - 1);
        }

        //5
        static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = i + 1; j < array.Length; j++)
                    if (array[i] > array[j])
                    {
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
        }

        //6
        static void QuickSort(int[] array, int start, int end)
        {
            if (start >= end)
            {
                return;
            }
            int pivot = Partition(array, start, end);
            QuickSort(array, start, pivot - 1);
            QuickSort(array, pivot + 1, end);
        }

        static int Partition(int[] array, int start, int end)
        {
            int temp;
            int marker = start;//divides left and right subarrays
            for (int i = start; i < end; i++)
            {
                if (array[i] < array[end]) //array[end] is pivot
                {
                    temp = array[marker]; 
                    array[marker] = array[i];
                    array[i] = temp;
                    marker += 1;
                }
            }
            //put pivot(array[end]) between left and right subarrays
            temp = array[marker];
            array[marker] = array[end];
            array[end] = temp;
            return marker;
        }

        //7
        public static void TimSort(int[] arr, int n)
        {
            int RUN = 32;
            // Sort individual subarrays of size RUN
            for (int i = 0; i < n; i += RUN)
                InsertionSort(arr, i,
                             Math.Min((i + RUN - 1), (n - 1)));

            // Start merging from size RUN (or 32).
            // It will merge
            // to form size 64, then
            // 128, 256 and so on ....
            for (int size = RUN; size < n;
                                     size = 2 * size)
            {

                // Pick starting point of
                // left sub array. We
                // are going to merge
                // arr[left..left+size-1]
                // and arr[left+size, left+2*size-1]
                // After every merge, we increase
                // left by 2*size
                for (int left = 0; left < n;
                                      left += 2 * size)
                {

                    // Find ending point of left sub array
                    // mid+1 is starting point of
                    // right sub array
                    int mid = left + size - 1;
                    int right = Math.Min((left +
                                        2 * size - 1), (n - 1));

                    // Merge sub array arr[left.....mid] &
                    // arr[mid+1....right]
                    if (mid < right)
                        Merge(arr, left, mid, right);
                }
            }
        }

        public static void InsertionSort(int[] arr, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = arr[i];
                int j = i - 1;
                while (j >= left && arr[j] > temp)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = temp;
            }
        }
        public static void Merge(int[] arr, int l, int m, int r)
        {
            // original array is broken in two parts
            // left and right array
            int len1 = m - l + 1;
            int len2 = r - m;
            int[] left = new int[len1];
            int[] right = new int[len2];
            for (int x = 0; x < len1; x++)
                left[x] = arr[l + x];
            for (int x = 0; x < len2; x++)
                right[x] = arr[m + 1 + x];

            int i = 0;
            int j = 0;
            int k = l;

            // After comparing, we merge those two array
            // in larger sub array
            while (i < len1 && j < len2)
            {
                if (left[i] <= right[j])
                {
                    arr[k] = left[i];
                    i++;
                }
                else
                {
                    arr[k] = right[j];
                    j++;
                }
                k++;
            }

            // Copy remaining elements
            // of left, if any
            while (i < len1)
            {
                arr[k] = left[i];
                k++;
                i++;
            }

            // Copy remaining element
            // of right, if any
            while (j < len2)
            {
                arr[k] = right[j];
                k++;
                j++;
            }
        }

        //8
        static void Power(int x,int k)
        {
            int c = x;
            int n = k;
            int result = 1;
            while (n != 0)
            {
                if (n % 2 == 0)
                {
                    c *= c;
                    n /= 2;
                }
                else
                {
                    result *= c;
                    n--;
                }
            }
        }

        //9
        static void MultiplyMatrixes(int[] array1, int[] array2)
        {
            int[,] result = new int[array1.GetLength(0), array1.GetLength(0)];
            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(0); j++)
                {
                    for (int l = 0; l < array1.GetLength(0); l++)
                    {
                        result[i, j] += result[i, l] * result[l, j];
                    }
                }
            }
        }

        //10
        static void VisitAllCells(string[] maze)
        {
            var map = new State[maze[0].Length, maze.Length];

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    if (maze[y][x] == ' ') map[x, y] = State.Empty;

            var queue = new Queue<Point>();
            queue.Enqueue(new Point { X = 0, Y = 0 });
            while (queue.Count != 0)
            {
                var point = queue.Dequeue();
                if (point.X < 0 || point.X >= map.GetLength(0) || point.Y < 0 || point.Y >= map.GetLength(1)) continue;
                if (map[point.X, point.Y] != State.Empty) continue;
                map[point.X, point.Y] = State.Visited;
                //PrintMaze(map);

                for (var dy = -1; dy <= 1; dy++)
                    for (var dx = -1; dx <= 1; dx++)
                        if (dx != 0 && dy != 0) continue;
                        else queue.Enqueue(new Point { X = point.X + dx, Y = point.Y + dy });
            }
        }

        static int[] PrepareArray(int k)
        {
            int[] array = new int[k];
            for (int i = 0; i < k; i++)
                array[i] = Rnd.Next();
            return array;
        }

        static string[] PrepareMaze(int k)
        {
            string[] maze = new string[k];
            for (int i=0;i<k;i++)
                maze[i] = new string(' ', k);
            return maze;
        }

        static void PrintMaze(State[,] map)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            for (int x = 0; x < map.GetLength(0) + 2; x++)
                Console.Write("X");
            Console.WriteLine();
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Console.Write("X");
                for (int x = 0; x < map.GetLength(0); x++)
                    switch (map[x, y])
                    {
                        case State.Empty: Console.Write(" "); break;
                        case State.Visited: Console.Write("."); break;
                    }
                Console.WriteLine("X");
            }
            for (int x = 0; x < map.GetLength(0) + 2; x++)
                Console.Write("X");
            Console.ReadKey();
        }
    }

    internal class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
