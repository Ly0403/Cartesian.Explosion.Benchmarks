```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.7623/25H2/2025Update/HudsonValley2)
11th Gen Intel Core i5-1135G7 2.40GHz (Max: 2.42GHz), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.102
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v4 [AttachedDebugger]
  DefaultJob : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v4


```
| Method            | Mean       | Error      | StdDev     | Gen0      | Allocated   |
|------------------ |-----------:|-----------:|-----------:|----------:|------------:|
| WithoutQuerySplit | 648.100 ms | 28.7939 ms | 77.3530 ms | 5000.0000 | 78583.82 KB |
| WithQuerySplit    |   3.854 ms |  0.1770 ms |  0.5078 ms |   39.0625 |   882.67 KB |
