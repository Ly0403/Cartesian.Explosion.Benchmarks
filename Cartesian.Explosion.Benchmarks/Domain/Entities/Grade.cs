namespace Cartesian.Explosion.Benchmarks.Domain.Entities;

public sealed class Grade
{
    public int Id { get; init; }
    public int Score { get; init; }
    public bool IsFailed { get; set; }
    public DateTime CompletedAt { get; set; }
    public int StudentId { get; init; }
    public Student? Student { get; init; }
}

