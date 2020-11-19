using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace GetDigitsBenchmark
{
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.StackOverflow]
    [RPlotExporter]
    public class Benchmarks
    {
        public const int digit = Int32.MaxValue;
        
        [Benchmark]
        public int[] Stack()
        {
            var list = new Stack<int>(32);
            var remainder = digit;
            do
            {
                list.Push(remainder % 10);
                remainder /= 10;
            } while (remainder != 0);

            return list.ToArray();
        }
        
        public static int[] GetDigits_SharedPool(int n)
        {
            if (n == 0)
                return new[] {0};
            
            var x = Math.Abs(n);
            
            var pool = ArrayPool<int>.Shared.Rent(11);
            var count = 0;

            while (x > 0)
            {
                pool[count++] = x % 10;
                
                x /= 10;
            }

            var res = new int[count];
            
            Array.Copy(pool, res, count);
            
            Array.Reverse(res);
            
            ArrayPool<int>.Shared.Return(pool);

            return res;
        }
        
        
        public static int[] GetDigits(int n)
        {
            if (n == 0)
                return new[] {0};
            
            var x = Math.Abs(n);

            var numDigits = NumberOfDigits(x);

            var res = new int[numDigits];
            var count = 0;

            while (x > 0)
            {
                res[count++] = x % 10;
                
                x /= 10;
            }
            
            Array.Reverse(res);

            return res;
        }
        
        public static int NumberOfDigits(int n)
        {
            if (n >= 0)
            {
                if (n < 10) return 1;
                if (n < 100) return 2;
                if (n < 1000) return 3;
                if (n < 10000) return 4;
                if (n < 100000) return 5;
                if (n < 1000000) return 6;
                if (n < 10000000) return 7;
                if (n < 100000000) return 8;
                if (n < 1000000000) return 9;
                return 10;
            }
            else
            {
                if (n > -10) return 2;
                if (n > -100) return 3;
                if (n > -1000) return 4;
                if (n > -10000) return 5;
                if (n > -100000) return 6;
                if (n > -1000000) return 7;
                if (n > -10000000) return 8;
                if (n > -100000000) return 9;
                if (n > -1000000000) return 10;
                return 11;
            }
        }

        [Benchmark]
        public int[] SharedArray()
        {
            return GetDigits_SharedPool(digit);
        }

        [Benchmark]
        public int[] PreallocateUsingNumberOfDigits()
        {
            return GetDigits(digit);
        }

        [Benchmark]
        public int[] IEnumerable()
        {
            return Digits_IEnumerable(digit).ToArray();
        }
        public static IEnumerable<int> Digits_IEnumerable(int number)
        {
            do
            {
                yield return number % 10;
                number /= 10;
            } while (number > 0);
        }
    }
}
