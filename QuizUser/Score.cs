public class Score
{
    public string userName;
    public string quizName;
    public int score;

    public Score() { }
    public Score(string userName, string quizName, int score)
    {
        this.userName = userName;
        this.quizName = quizName;
        this.score = score;
    }
}