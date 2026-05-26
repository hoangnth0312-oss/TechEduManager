namespace TechEduManager;
//cha Person chứa các thuộc tính chung:
//Id, Name, Age, Email.

public abstract class Person
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    protected Person()
    {
        
    }
    protected Person(string id, string name, int age, string email)
    {
        Id = id;
        Name = name;
        Age = age;
        Email = email;
    }
    //Lớp Person phải là một Abstract Class có chứa
    //abstract method DisplayInfo().
    public abstract void DisplayInfo();
}