using System;
using System.Collections.Generic;

namespace vnedrenie2Lab.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartedDate { get; set; }
    public int ManagerId { get; set; }
    public List<Task> Tasks { get; set; }
    public List<User> Users { get; set; }
    public int ContractId { get; set; }
    
}