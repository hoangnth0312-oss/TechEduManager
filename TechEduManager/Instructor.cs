namespace TechEduManager;
//lớp Instructor (Giảng viên) kế thừa Person,
//bổ sung: Department (Khoa/Bộ môn), BaseSalary (Lương cơ bản), TeachingHours (Số giờ dạy).

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
    // Hiển thị thông tin chi tiết của instructor
    public override void DisplayInfo()
    {
        double totalSalary = CalculateMoney();
        Console.WriteLine($"  [GV] ID: {Id}  Tên: {Name}  Tuổi: {Age}  Email: {Email}");
        Console.WriteLine($" Khoa/Bộ môn: {Department}  Lương cơ bản: {BaseSalary}đ | " +
                          $"Giờ dạy: {TeachingHours}h  Tổng lương: {totalSalary}đ");
    }

    // Tính lương = Lương cơ bản + Số giờ dạy * 200,000đ
    
    public double  CalculateMoney()
    {
        return BaseSalary + TeachingHours * HourlyRate;
    }
    
    // Hàm trả về toàn bộ thông tin của giáo viên
    public string InstuctorInfo()
    {
        return $"Instructor|{Id}|{Name}|{Age}|{Email}|{Department}|{BaseSalary}|{TeachingHours}";
    }
    
    // Hàm tạo đối tượng Instructor
    public static Instructor CreateInstructor(string[] InsInfo)
    {
        return new Instructor(
            id: InsInfo[1],
            name: InsInfo[2],
            age: int.Parse(InsInfo[3]),
            email: InsInfo[4],
            department: InsInfo[5],
            baseSalary: double.Parse(InsInfo[6]),
            teachingHours: int.Parse(InsInfo[7])
        );
    }
}