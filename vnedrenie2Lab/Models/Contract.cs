using System;
using System.Collections.Generic;

namespace vnedrenie2Lab.Models;

public class Contract
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; } 
    public ContractStatus Status { get; set; }
    public List<Project> Projects { get; set; }
    public byte [] Scan { get; set; }
}