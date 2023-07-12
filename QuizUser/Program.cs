using System.Xml.Serialization;
LoginMenu menu = new LoginMenu();
menu.FirstMenu();


//List<User> users = new List<User>()
//{
//    new User("Oguz", "aguz123"),
//    new User("Test", "testpass")
//};

//XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
//using (FileStream fs = new FileStream("UsersData.xml", FileMode.Create))
//{
//    formatter.Serialize(fs, users);
//}
//Console.WriteLine("ОТлично");