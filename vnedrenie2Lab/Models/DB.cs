using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.VisualBasic.FileIO;

namespace vnedrenie2Lab.Models;

public class DB
{
    private static DB _data;
    
    public List<Contract> Contracts { get; set; } = new List<Contract>();
    public List<Project> Projects { get; set; } = new List<Project>();
    public List<User> Users { get; set; } = new List<User>();
    public List<Task> Tasks { get; set; } = new List<Task>();

    public static DB GetDB()
    {
        if(_data == null)
        {
            _data = new DB();
             _data.LoadContracts();
             _data.LoadProjects();
             _data.LoadUsers();
             _data.LoadTasks();
        }
        return _data;
    }

    private async void LoadUsers()
    {
       
        string filepath = Path.Combine(AppContext.BaseDirectory, "dbUsers.json");
        if (File.Exists(filepath))
        {
            string data1 = await File.ReadAllTextAsync(filepath);
            var users = JsonSerializer.Deserialize<List<User>>(data1);
            Users = users;
            
        }
        else
        {
            Users = new List<User>();
        }
       
    }

    private async void LoadTasks()
    {
        string filepath = Path.Combine(AppContext.BaseDirectory, "dbTasks.json");
        if (File.Exists(filepath))
        {
            string data1 = await File.ReadAllTextAsync(filepath);
            var tasks = JsonSerializer.Deserialize<List<Task>>(data1);
            Tasks = tasks;
            
        }
        else
        {
            Tasks = new List<Task>();
        }
      
    }

    private async void LoadProjects()
    {
        string filepath = Path.Combine(AppContext.BaseDirectory, "dbProjects.json");
        if (File.Exists(filepath))
        {
            string data1 = await File.ReadAllTextAsync(filepath);
            var projects = JsonSerializer.Deserialize<List<Project>>(data1);
            Projects = projects;
            
        }
        else
        {
            Projects = new List<Project>();
        }
      
    }

    private async void LoadContracts()
    {
        string filepath = Path.Combine(AppContext.BaseDirectory, "dbContracts.json");
        if (File.Exists(filepath))
        {
            
            string data1 = await File.ReadAllTextAsync(filepath);
            var contracts = JsonSerializer.Deserialize<List<Contract>>(data1);
            Contracts = contracts;
            
        }
        else
        {
            Contracts = new List<Contract>();
        }
        
    }
    
    public async void SaveUsersAsync()
    {
        string filepath = Path.Combine(AppContext.BaseDirectory, "dbUsers.json");
    
        var options = new JsonSerializerOptions { WriteIndented = true };
        await using FileStream fileStream = File.Create(filepath);
        await JsonSerializer.SerializeAsync(fileStream, Users, options);
    }
    
    public async void SaveTasksAsync()
    {
        string filepath = Path.Combine(AppContext.BaseDirectory, "dbTasks.json");
    
        var options = new JsonSerializerOptions { WriteIndented = true };
        await using FileStream fileStream = File.Create(filepath);
        await JsonSerializer.SerializeAsync(fileStream, Tasks, options);
    }
    
    public async void SaveProjectsAsync()
    {
        string filepath = Path.Combine(AppContext.BaseDirectory, "dbProjects.json");
    
        var options = new JsonSerializerOptions { WriteIndented = true };
        await using FileStream fileStream = File.Create(filepath);
        await JsonSerializer.SerializeAsync(fileStream, Projects, options);
    }
    
    public async void SaveContractsAsync()
    {
        string filepath = Path.Combine(AppContext.BaseDirectory, "dbContracts.json");
    
        var options = new JsonSerializerOptions { WriteIndented = true };
        await using FileStream fileStream = File.Create(filepath);
        await JsonSerializer.SerializeAsync(fileStream, Contracts, options);
    }
    

    
    
    
}