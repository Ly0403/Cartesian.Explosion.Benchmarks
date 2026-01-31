namespace Cartesian.Explosion.Benchmarks.Domain.Entities;

public sealed class Student
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Number { get; init; }
    public List<Homework> Homeworks { get; set; } = [];
    public List<Grade> Grades { get; set; } = [];
}

