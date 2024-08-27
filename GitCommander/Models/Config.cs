namespace GitCommander.Models;

public class Config
{
    public List<Repo> Repos {get;set;} = new List<Repo>();
    public List<string> UsersToWatchFor {get;set;} = new List<string>();
    public Token Token { get; set; }
}

public class Token
{
    public string Value { get; set; }
}