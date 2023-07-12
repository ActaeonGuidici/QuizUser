public class Question
{
    public string question;
    public string[] answers;
    public int[] correct_answer;

    public Question() { }
    public Question(string question, string[] answers, int[] correct_answer)
    {
        this.question = question;
        this.answers = answers;
        this.correct_answer = correct_answer;
    }
}
