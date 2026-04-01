using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using People.Models;

namespace People.Services;

public class PersonRepository
{
    private SQLiteConnection _conn;
    private readonly string _dbPath;
    public string StatusMessage { get; private set; } = "";

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    private void Init()
    {
        if (_conn != null)
            return;

        _conn = new SQLiteConnection(_dbPath);
        _conn.CreateTable<Person>();
    }

    public void AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            Init();

            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            result = _conn.Insert(new Person { Name = name });
            StatusMessage = $"{result} row(s) added (Name: {name})";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
        }
    }

    public List<Person> GetAllPeople()
    {
        try
        {
            Init();
            return _conn.Table<Person>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to retrieve data. {ex.Message}";
            return new List<Person>();
        }
    }
}