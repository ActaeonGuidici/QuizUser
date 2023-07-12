using System.Xml.Serialization;

public class LoginMenu : MainMenu, IOpenFiles
{
    public void FirstMenu()
    {
        Console.WriteLine("1. Войти\n2. Зарегистрироваться");
        int tmp = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        switch (tmp)
        {
            case 1:
                SignIn();
                break;
            case 2:
                SignUp();
                break;
        }
    }
    public void SignIn()
    {
        IOpenFiles openFiles = new LoginMenu();
        List<User> usersNew = openFiles.openUser();

        Console.Write("Введите логин: ");
        string tmpLog = Console.ReadLine();
        int n = 0;
        bool re = false;
        for (int i = 0; i < usersNew.Count; i++)
        {
            if (tmpLog == usersNew[i].Login)
            {
                re = true;
                n += i;
            }
        }
        if (re == true)
        {
            Console.Write("Введите пароль: ");
            string tmpPass = Console.ReadLine();

            if (tmpPass == usersNew[n].Password)
            {
                if (usersNew[n].Status == "Admin")
                {
                    Console.Clear();
                    Console.WriteLine("Зайти в меню администрации или пользователя?");
                    Console.WriteLine("1. Меню администрации\n2. Меню пользователя");
                    int adminChoose = Convert.ToInt32(Console.ReadLine());
                    switch (adminChoose)
                    {
                        case 1:

                            break;
                        case 2:
                            MainPage(usersNew[n]);
                            break;
                    }
                   
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Вход успешно выполнен.");
                    MainPage(usersNew[n]);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Пароль неверный. Повторите попытку заново.");
                SignIn();
            }
        }
        else if (re == false)
        {
            Console.Clear();
            Console.WriteLine("Пользователя с указанным логином не существует.");
            SignIn();
        }
    }
    public void SignUp()
    {
        List<User> usersNew = null;
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
        using (FileStream fs = new FileStream("UsersData.xml", FileMode.OpenOrCreate))
        {
            usersNew = xmlSerializer.Deserialize(fs) as List<User>;
        }
        Console.Write("Задайте логин: ");
        string tmpLog = Console.ReadLine();
        bool re = false;
        foreach (User user in usersNew)
        {
            if (tmpLog == user.Login)
            {
                re = true;
            }
        }
        if (re)
        {
            Console.Clear();
            Console.WriteLine("Заданный логин уже используется. Повторите попытку заново.");
            SignUp();
        }
        else if (tmpLog.Length < 3 || tmpLog.Length > 10)
        {
            Console.Clear();
            Console.WriteLine("Длина логина меньше 3 символов или больше 10-ти. Повторите попытку заново.");
            SignUp();
        }

        Console.Write("Задайте пароль: ");
        string tmpPass = Console.ReadLine();
        if (tmpPass.Length < 6)
        {
            Console.Clear();
            Console.WriteLine("Длина пароля не может быть меньше шести символов. Повторите попытку заново.");
            SignUp();
        }
        Console.Write("Введите дату вашего рождения по типу 'год.месяц.день': ");
        string tmpDate = Console.ReadLine();
        string[] subs = tmpDate.Split(".", 3);
        int year = int.Parse(subs[0]);
        int month = int.Parse(subs[1]);
        int day = int.Parse(subs[2]);
        DateOnly date = new DateOnly(year, month, day);
        User newUser = new User(tmpLog, tmpPass, date);
        usersNew.Add(newUser);
        IOpenFiles openFiles = new LoginMenu();
        openFiles.saveToUserFile(usersNew);
        FirstMenu();
    }
}
