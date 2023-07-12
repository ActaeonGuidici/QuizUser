using System.Xml.Serialization;
public class MainMenu : Quiz, IOpenFiles
{
    public void MainPage(User user)
    {
        Console.WriteLine("1. Начать новую викторину\n2. Результаты прошлых викторин\n3. Рейтинг Топ-20\n4. Редактировать профиль\n5. Выход");
        int choose = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        switch (choose)
        {
            case 1:
                startQuizMenu(user);
                break;
            case 2:
                watchResults(user);
                break;
            case 3:
                watchTOP20(user);
                break;
            case 4:
                profileRed(user);
                break;
            case 5:
                LoginMenu menu = new LoginMenu();
                menu.FirstMenu();
                break;
        }
    }

    public void startQuizMenu(User user)
    {
        IOpenFiles openFiles = new MainMenu();
        List<Quiz> quizs = openFiles.openQuizsData(); //открываю файл с викторинами и записываю их в переменную
        Console.WriteLine("Выберите тему викторины: ");
        Console.WriteLine("\n0. Вернуться в меню");
        for (int i = 0; i < quizs.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {quizs[i].name}");
        }
        int choose = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        if (choose == 0)
        {
            MainPage(user);
        }
        else
        {
            int score = startQuiz(quizs, choose); //начинает викторину и записывает набранные очки по ее завершению
            for (int i = 0; i < user.TotalScores.Count; i++)
            {
                if (user.TotalScores[i].quizName == quizs[choose - 1].name) //нахожу данные по очкам о нужной викторине в массиве TotalScore пользователя
                {
                    user.TotalScores.RemoveAt(i); //удаляю элемент из массива, если он существует. Нельзя просто записать новые данные в поле score, поскольку элемента с нужными данными не будет при первом прохождении
                }
            }
            Score tmp = new Score(user.Login, quizs[choose - 1].name, score); //создаю новую переменную типа Score, которая хранит в себе данные о последнем прохождении викторины
            user.TotalScores.Add(tmp); //и добавляю его в список TotalScores

            List<User> usersNew = openFiles.openUser(); //открываю файл с пользователями и записываю их в переменную
            for (int i = 0; i < usersNew.Count; i++)
            {
                if (user.Login == usersNew[i].Login)
                {
                    usersNew.RemoveAt(i); //нахожу и удаляю данные о пользователе
                }
            }
            usersNew.Add(user); //добавляю новые давнные с обновленным количеством очков
            openFiles.saveToUserFile(usersNew); //записываю новые данные о пользователях в файл
            MainPage(user);
        }
    }

    public void watchResults(User user)
    {
        foreach (var item in user.TotalScores)
        {
            Console.WriteLine($"* {item.quizName} - {item.score} очков.");

        }

        Console.WriteLine("\n0. Вернуться в меню");
        int choose = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        MainPage(user);
    }

    public void watchTOP20(User user)
    {
        IOpenFiles openFiles = new MainMenu();
        List<Quiz> quizs = openFiles.openQuizsData();
        List<User> usersNew = openFiles.openUser();
        Console.WriteLine("Выберите тему викторины: ");
        Console.WriteLine("\n0. Вернуться в меню");
        for (int i = 0; i < quizs.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {quizs[i].name}");
        }
        int choose = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        if (choose == 0)
        {
            MainPage(user);
        }
        else
        {
            List<Score> scores = new List<Score>(); //новый лист, в котором я буду хранить данные о выбранной викторине со всех пользователей
            for (int i = 0; i < usersNew.Count; i++) //перебираю пользователей
            {
                for (int j = 0; j < usersNew[i].TotalScores.Count; j++) //перебираю пройденные викторины у каждого пользователя
                {
                    if (usersNew[i].TotalScores[j].quizName == quizs[choose - 1].name) //если название выбранной для ТОПа викторины совпадает с названием викторины в элементе TotalScores
                    {
                        scores.Add(usersNew[i].TotalScores[j]); //то я добавляю этот элемент в созданный выше массив
                    }
                }
            }
            scores = BubbleSort(scores); //отправляю массив на сортировку по очкам, неотсортированный список сразу заменяю на выходные данные

            int n = 1;
            foreach (var item in scores)
            {
                Console.WriteLine($"{n}. {item.userName} - {item.score}");
                n++;
            }
            Console.WriteLine("\n0. Вернуться в меню");
            int choose1 = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            MainPage(user);
        }
    }

    public void profileRed(User user)
    {
        Console.WriteLine("0. Вернуться в главное меню\n1. Сменить пароль\n2. Сменить день рождения");
        int choose = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        switch (choose)
        {
            case 0:
                MainPage(user);
                break;
            case 1:
                changePass(user);
                break;
            case 2:
                changeDate(user);
                break;
        }
    }
    public void changePass(User user)
    {
        IOpenFiles openFiles = new MainMenu();
        List<User> usersNew = openFiles.openUser();
        Console.WriteLine("Введите старый пароль: ");
        string pass = Console.ReadLine();
        if (user.Password != pass)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Пароль неверный. Повторите заново.");
            Console.ForegroundColor = ConsoleColor.White;
            changePass(user);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Введите новый пароль: ");
            pass = Console.ReadLine();
            if (user.Password == pass)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Новый пароль не может быть идентичен старому. Повторите заново.");
                Console.ForegroundColor = ConsoleColor.White;
                changePass(user);
            }
            else if(pass.Length < 6)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Длина пароля не может быть меньше шести символов. Повторите попытку заново.");
                Console.ForegroundColor = ConsoleColor.White;
                changePass(user);
            }
            else
            {
                Console.WriteLine("Повторите новый пароль: ");
                string pass2 = Console.ReadLine();
                if (pass2 != pass)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введенные данные не совпадают. Повторите заново.");
                    Console.ForegroundColor = ConsoleColor.White;
                    changePass(user);
                }
                else
                {
                    user.Password = pass2;
                    for (int i = 0; i < usersNew.Count; i++)
                    {
                        if (user.Login == usersNew[i].Login)
                        {
                            usersNew.RemoveAt(i); //нахожу и удаляю данные о пользователе
                        }
                    }
                    Console.Clear();
                    usersNew.Add(user); //добавляю новые давнные с обновленным паролем
                    openFiles.saveToUserFile(usersNew); //записываю новые данные о пользователях в файл
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Пароль успешно изменен");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        Console.WriteLine("\n0. Вернуться в меню");
        int choose = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        MainPage(user);
    }
    
    public void changeDate(User user)
    {
        IOpenFiles openFiles = new MainMenu();
        List<User> usersNew = openFiles.openUser();
        Console.WriteLine($"Нынешняя дата вашего рождения - {user.Date}\nВведите новую дату по типу 'год.месяц.день': ");
        string Date = Console.ReadLine();
        DateOnly newDate = DateOnly.Parse(Date);
        user.Date = newDate;
        for (int i = 0; i < usersNew.Count; i++)
        {
            if (user.Login == usersNew[i].Login)
            {
                usersNew.RemoveAt(i); //нахожу и удаляю данные о пользователе
            }
        }
        Console.Clear();
        usersNew.Add(user); //добавляю новые давнные с обновленнрй датой
        openFiles.saveToUserFile(usersNew); //записываю новые данные о пользователях в файл
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Дата рождения успешно изменена");
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("\n0. Вернуться в меню");
        int choose = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        MainPage(user);
    }
    public List<Score> BubbleSort(List<Score> scoreList)
    {
        int left = 0;
        int right = scoreList.Count - 1;
        while (left <= right)
        {
            for (int i = right; i > left; --i)
           {
                if (scoreList[i - 1].score > scoreList[i].score)
               {
                    Score tmp = scoreList[i - 1];
                    scoreList[i - 1] = scoreList[i];
                    scoreList[i] = tmp;
                }
            }
            ++left;
            for (int i = left; i < right; ++i)
           {
                if (scoreList[i].score > scoreList[i + 1].score)
                {
                    Score tmp = scoreList[i + 1];
                    scoreList[i + 1] = scoreList[i];
                    scoreList[i] = tmp;
                }
           }
            --right;
        }
        return scoreList;
    }
} 

