namespace TechEduManager;

public class Instructor : Person, IFinance
{
    public string Department { get; set; }
    public double BaseSalary { get; set; }
    public int TeachingHours { get; set; }
    
    
    private const double HourlyRate = 200_000;

    public Instructor()
    {
        
    }
    public Instructor(string id, string name, int age, string email,
        string department, double baseSalary, int teachingHours)
        : base(id, name, age, email)
    {
        Department = department;
        BaseSalary = baseSalary;
        TeachingHours = teachingHours;
    }

    public override void DisplayInfo()
    {
        double totalSalary = CalculateMoney();
        Console.WriteLine($"  [GV] ID: {Id}  Tên: {Name}  Tuổi: {Age}  Email: {Email}");
        Console.WriteLine($" Khoa/Bộ môn: {Department}  Lương cơ bản: {BaseSalary}đ | " +
                          $"Giờ dạy: {TeachingHours}h  Tổng lương: {totalSalary}đ");
    }

    public double CalculateMoney()
    {
        return BaseSalary + TeachingHours * HourlyRate;
    }
        
}