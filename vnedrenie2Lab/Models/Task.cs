using System;

namespace vnedrenie2Lab.Models;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public TaskStatus Status { get; set; }
    public int UserId { get; set; }
    public int Difficulty { get; set; }
    public int ProjectId { get; set; }
}