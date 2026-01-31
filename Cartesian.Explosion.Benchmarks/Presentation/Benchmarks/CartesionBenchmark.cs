using BenchmarkDotNet.Attributes;
using Bogus;
using Cartesian.Explosion.Benchmarks.Domain.Entities;
using Cartesian.Explosion.Benchmarks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Cartesian.Explosion.Benchmarks.Presentation.Benchmarks;

[MemoryDiagnoser]
public class CartesionBenchmark
{
    private const int TotalPerType = 500;
    private static readonly DateTime StartDate = new(2024, 1, 1);
    private static readonly DateTime EndDate = new(2026, 1, 31);
    private readonly Faker _faker = new ("en");

    [GlobalSetup]
    public async Task GlobalSetupAsync()
    {
        await using var dbContext = new AppDbContext();

        // Ensure DB exists
        await dbContext.Database.EnsureCreatedAsync();

        // If we already have data, skip seeding (so benchmarks are repeatable)
        if (await dbContext.Set<Student>().AnyAsync())
            return;

        // Create two students
        var students = new List<Student>
        {
            new (){ Id = 1, Name = "Alice", Number = 1001 },
            new (){ Id = 2, Name = "Bob",   Number = 1002 }
        };

        dbContext.AddRange(students);

        // Generate grades and homeworks distributed across the two students
        var grades = new List<Grade>(TotalPerType);
        var homeworks = new List<Homework>(TotalPerType);

        var gradeId = 1;
        var hwId = 1;

        for (var i = 0; i < TotalPerType; i++)
        {
            var studentId = (i % students.Count) + 1;

            var score = _faker.Random.Int(0, 100);
            grades.Add(new Grade
            {
                Id = gradeId++,
                Score = score,
                IsFailed = score < 60,
                CompletedAt = _faker.Date.Between(StartDate, EndDate),
                StudentId = studentId
            });

            var isCompleted = _faker.Random.Bool();
            homeworks.Add(new Homework
            {
                Id = hwId++,
                Title = _faker.Lorem.Sentence(3).TrimEnd('.'),
                Description = _faker.Lorem.Paragraph(),
                IsCompleted = isCompleted,
                CompletedAt = isCompleted ? _faker.Date.Between(StartDate, EndDate) : default,
                StudentId = studentId
            });
        }

        dbContext.AddRange(grades);
        dbContext.AddRange(homeworks);

        await dbContext.SaveChangesAsync();
    }

    [Benchmark]
    public async Task<bool> WithoutQuerySplit()
    {
        using var dbContext = new AppDbContext();      

        var students = await dbContext
            .Set<Student>()
            .Include(x => x.Homeworks)
            .Include(x => x.Grades)
            .AsNoTracking()
            .ToListAsync();

        return true;
    }


    [Benchmark]
    public async Task<bool> WithQuerySplit()
    {
        using var dbContext = new AppDbContext();

        var students = await dbContext
            .Set<Student>()
            .Include(x => x.Homeworks)
            .Include(x => x.Grades)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

        return true;
    }
}
