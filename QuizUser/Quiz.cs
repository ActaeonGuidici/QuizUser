public class Quiz
{
    public string name;
    public List<Question> questions;

    public Quiz() { }
    public Quiz(string _name, List<Question> _questions)
    {
        this.name = _name;
        questions = _questions;
    }

    public int startQuiz(List<Quiz> quizs, int choose) //Функция, которая запускает викторину с возвращаемым целочисленным значением, хранящим количество очков
    {
        Random rnd = new Random();
        int score = 0; //переменная для подсчета очков
        while (quizs[choose - 1].questions.Count > 0)
        {
            Question current_question = quizs[choose - 1].questions[rnd.Next(0, quizs[choose - 1].questions.Count)];
            Console.WriteLine(current_question.question);

            if (current_question.correct_answer.SequenceEqual(WriteAndGetAnswer(current_question))) //вызов функции вывода вопросов и ввода ответов, которая возвращает целочисленный массив, который здесь же сравнивается с правильными ответами
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Верный ответ!");
                score += 5; //увеличение очков при правильном ответе
                Console.WriteLine($"Теперь у вас {score} очков!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Неверно! Будьте внимательней. У вас все еще {score} очков!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            quizs[choose - 1].questions.Remove(current_question);
            Console.WriteLine();
        }
        Console.WriteLine("Окончание викторины!");
        Console.WriteLine($"Вы набрали {score} очков на этой викторине!");
        return score; //возврат набранных очков
    }
    static int[] WriteAndGetAnswer(Question tmp) 
    {
        Random rnd = new Random();
        List<string> answer = new List<string>(tmp.answers);
        int len = answer.Count + 1;
        string[] string_ansAfter = new string[answer.Count + 1]; //массив с ответами(текст) после перемешивания, сохранив тот порядок, в котором он отображается на консоли
        for (int i = 1; i < len; i++)
        {
            string str = answer[rnd.Next(0, answer.Count)]; //выбираю случайный ответ из вопроса
            string_ansAfter[i] = str; //записываю его в массив с ответами
            Console.WriteLine($"{i}. {str}");
            answer.Remove(str); //удаляю из первоначального массива с ответами выведенный ответ
        }

        Console.WriteLine("Введите номер ответа: \n(В случае нескольких правильных ответов запишите их через пробел)");
        string input = Console.ReadLine();
        string[] subs = input.Split(" ");
        int[] new_corrects = Array.ConvertAll(subs, int.Parse); //массив с ввеленными цифрами\ответами пользователя

        int[] final_corrects = new int[new_corrects.Length]; //если ответы правильные, то они их цифры будут записываться сюда

        for (int i = 0; i < new_corrects.Length; i++)
        {
            for (int j = 0; j < tmp.correct_answer.Length; j++)
            {
                if (string_ansAfter[new_corrects[i]] == tmp.answers[tmp.correct_answer[j] - 1]) //сравниваю элемент введенного ответа с правильными ответами из изначального массива викторины
                {
                    final_corrects[i] = tmp.correct_answer[j]; //если совпадает, то записывается в массив цифру этого ответа из изначального массива викторины
                }
                else { }
            }

        }
        Array.Sort(final_corrects); //сортирую массив, поскольку при рандомизации порядок непредсказуем(например, введенные пользователем 1 и 2 могут быть правильными, но оказаться 4 и 1 из изначального списка, поэтому его надо перевести в 1 и 4)
        return final_corrects;
    }
}



