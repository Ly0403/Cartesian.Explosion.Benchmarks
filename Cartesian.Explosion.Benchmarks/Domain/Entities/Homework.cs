namespace Cartesian.Explosion.Benchmarks.Domain.Entities;

public sealed class Homework
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool IsCompleted{ get; set; }
    public DateTime CompletedAt { get; set; }
    public int StudentId { get; init; }
    public Student? Student { get; init; }
}

