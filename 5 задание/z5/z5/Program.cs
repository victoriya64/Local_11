using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace z_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Данная программа реализует TPL, библиотеку параллельного выполнения задач: \n");
            int lowerBound, upperBound, numThreads;
            // Считываем нижнюю границу диапазона
            Console.Write("Введите нижнюю границу диапазона: ");
            while (!int.TryParse(Console.ReadLine(), out lowerBound) || lowerBound <= 0)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                Console.Write("Введите нижнюю границу диапазона: ");
            }

            // Считываем верхнюю границу диапазона
            Console.Write("Введите верхнюю границу диапазона: ");
            while (!int.TryParse(Console.ReadLine(), out upperBound) || upperBound <= lowerBound)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                Console.Write("Введите верхнюю границу диапазона: ");
            }

            // Считываем количество потоков
            Console.Write("Введите количество потоков для работы: ");
            while (!int.TryParse(Console.ReadLine(), out numThreads) || numThreads <= 0 || numThreads > 10)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                Console.Write("Введите количество потоков для работы: ");
            }
            //Создается список primeNumbers, который будет содержать простые числа в заданном диапазоне.
            List<int> primeNumbers = PrimeFinder.GetPrimesInRange(lowerBound, upperBound, numThreads);
            // Вывод результатов
            Console.WriteLine("Простые числа в диапазоне от {0} до {1}:", lowerBound, upperBound);
            foreach (int prime in primeNumbers)
            {
                Console.Write("{0} ", prime);
            }
            Console.WriteLine("Нажмите любую кнопку для выхода...");
            Console.ReadKey();
        }
    }
    public class PrimeFinder
    {
        public static List<int> GetPrimesInRange(int lowerBound, int upperBound, int numThreads)
        {
            if (upperBound <= lowerBound)
            {
                throw new ArgumentException("Верхняя граница не может быть меньше нижней границы.");
            }

            if (lowerBound <= 0)
            {
                throw new ArgumentException("Верхняя граница не может быть равной 0 или отрицательным числом.");
            }

            if (numThreads <= 0 || numThreads > 10)
            {
                throw new ArgumentException("Количество потоков не может быть равным 0, отрицательным числом или больше 10.");
            }

            List<int> primeNumbers = new List<int>();

            Parallel.For(3, upperBound + 1, new ParallelOptions { MaxDegreeOfParallelism = numThreads },
                i =>
                {
                    bool isPrime = true;
                    // Цикл для проверки, является ли i простым числом.
                    for (int j = 2; j <= Math.Sqrt(i); j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    // Если i простое, оно добавляется в список primeNumbers.
                    if (isPrime)
                    {
                        // Выводим номер потока и число
                        Console.WriteLine("Поток {0}: {1}", Thread.CurrentThread.ManagedThreadId, i);
                        primeNumbers.Add(i);
                    }
                });
            // Сортируем список primeNumbers по возрастанию.
            primeNumbers.Sort();
            return primeNumbers;
        }

        static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            int boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 2; i <= boundary; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
