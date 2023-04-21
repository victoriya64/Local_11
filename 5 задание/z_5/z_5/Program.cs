using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeNumbers
{
    class Program
    {
        static void Main()
        {
            Console.Write("Данная программа реализует TPL, библиотеку параллельного выполнения задач: ");
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
            List<int> primeNumbers = new List<int>();
            //Parallel.For - метод библиотеки TPL, который позволяет выполнять итерации цикла в несколько потоков. 
            //Здесь мы указываем диапазон итераций, а также опции выполнения, в том числе максимальное количество потоков 
            //MaxDegreeOfParallelism.
            Parallel.For(2, upperBound + 1, new ParallelOptions { MaxDegreeOfParallelism = numThreads },
                i =>
                {
                    bool isPrime = true;
                    //Цикл для проверки, является ли i простым числом.
                    for (int j = 2; j <= Math.Sqrt(i); j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    //Если i простое, оно добавляется в список primeNumbers.
                    if (isPrime)
                    {
                        primeNumbers.Add(i);
                    }
                });
            // Сортируем список primeNumbers по возрастанию.
            primeNumbers.Sort();
            // Вывод результатов
            Console.WriteLine("Простые числа в диапазоне от {0} до {1}:", lowerBound, upperBound);
            foreach (int prime in primeNumbers)
            {
                Console.Write("{0} ", prime);
            }
        }
        // Метод для нахождения простых чисел в заданном диапазоне
        static int[] GetPrimesInRange(int lowerBound, int upperBound)
        {
            List<int> primes = new List<int>();

            for (int number = lowerBound; number <= upperBound; number++)
            {
                if (IsPrime(number))
                {
                    primes.Add(number);
                }
            }

            return primes.ToArray();
        }
        // Метод для проверки, является ли число простым
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