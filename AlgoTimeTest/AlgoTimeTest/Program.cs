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

            //int[,] mas1 = new int[k,k];
            //int[,] mas2 = new int[k, k];
            //for (int i = 0;i<k;i++)
            //    for (int j = 0; j<k;j++)
            //    {
            //        mas1[i, j] = Rnd.Next();
            //        mas2[i, j] = Rnd.Next();
            //    }
            //int[,] result = new int[mas1.Length, mas1.Length];

            //int sum=0;
            //int multiple = 1;

            //int v = Rnd.Next();
            //var x = 1.5;
            //double p = 0;

            int x = Rnd.Next(1000);
            int c = x;
            int n = k;
            int result=1;
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
            //QuickSort(mas, 0, k-1);

            //7 algo
            //TimSort(mas,mas.Length);

            //8 algo
            while (n !=0)
            {
                if (n%2==0)
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


            //9 algo
            //for (int i = 0; i < mas1.GetLength(0); i++)
            //{
            //    for (int j = 0; j < mas2.GetLength(1); j++)
            //    {
            //        for (int l = 0; k < mas1.GetLength(0); l++)
            //        {
            //            result[i, j] += result[i, l] * result[l, j];
            //        }
            //    }
            //}

            //finish algo


            stopwatch.Stop();

            Console.WriteLine(x+ " "+ k +" "+ result);
            return (int)stopwatch.ElapsedTicks * 10;
        }

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
            int temp;//swap helper
            int marker = start;//divides left and right subarrays
            for (int i = start; i < end; i++)
            {
                if (array[i] < array[end]) //array[end] is pivot
                {
                    temp = array[marker]; // swap
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


        // Iterative Timsort function to sort the
        // array[0...n-1] (similar to merge sort)
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
            int len1 = m - l + 1, len2 = r - m;
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
