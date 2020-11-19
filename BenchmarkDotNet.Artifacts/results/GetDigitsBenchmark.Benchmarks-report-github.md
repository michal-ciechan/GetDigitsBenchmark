``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-8705G CPU 3.10GHz (Kaby Lake G), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|                         Method |      Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------- |----------:|---------:|---------:|-------:|------:|------:|----------:|
|                          Stack |  89.06 ns | 2.130 ns | 6.179 ns | 0.0592 |     - |     - |     248 B |
|                    SharedArray |  84.64 ns | 1.765 ns | 3.685 ns | 0.0153 |     - |     - |      64 B |
| PreallocateUsingNumberOfDigits |  39.15 ns | 0.861 ns | 2.499 ns | 0.0153 |     - |     - |      64 B |
|                    IEnumerable | 246.53 ns | 4.926 ns | 9.372 ns | 0.0610 |     - |     - |     256 B |
