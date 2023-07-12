[Serializable]
public class User
{
    public string Login { get; set; }
    public string Password { get; set; }
    public DateOnly Date { get; set; }
    public List<Score> TotalScores { get; set; }
    public string Status { get; set; } 
    //public SerializableDictionary<string, int> TotalScores { get; set; }

    public User() { }
    public User(string login, string password)
    {
        Login = login;
        Password = password;
    }
    public User(string login, string password, DateOnly dateTime)
    {
        Login = login;
        Password = password;
        this.Date = dateTime;
    }
}
