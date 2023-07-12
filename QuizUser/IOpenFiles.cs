using System.Xml.Serialization;
public interface IOpenFiles
{
    public List<User> openUser()
    {
        List<User> usersNew = null;
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
        using (FileStream fs = new FileStream("UsersData.xml", FileMode.OpenOrCreate))
        {
            usersNew = xmlSerializer.Deserialize(fs) as List<User>;
        }
        return usersNew;
    }

    public void saveToUserFile(List<User> usersNew)
    {
        XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
        using (FileStream fs = new FileStream("UsersData.xml", FileMode.Open))
        {
            formatter.Serialize(fs, usersNew);
        }
    }

    public List<Quiz> openQuizsData()
    {
        List<Quiz> quizs = null;
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Quiz>));
        using (FileStream fs = new FileStream("../../../../QuizsData.xml", FileMode.OpenOrCreate))
        {
            quizs = xmlSerializer.Deserialize(fs) as List<Quiz>;
        }
        return quizs;
    }
}
