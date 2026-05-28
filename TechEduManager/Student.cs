namespace TechEduManager;
// lớp Student (Học viên) kế thừa Person,
// bổ sung: CourseName, Score, TuitionFee (Học phí).
public class Student : Person,IFinance
{
    public string CourseName { get; set; }
    private double _score;
    public double Score
    {
        get
        {
            return _score;
        }
        set
        {
            if (value < 0 || value > 10)
                throw new ArgumentOutOfRangeException
                    (nameof(Score), "Điểm đang ngoài khoảng từ 0 đến 10.");
            _score = value;
        }
    }
    public double TuitionFee { get; set; }

    public Student()
    {
        
    }
    
    public Student(string id, string name, int age, string email,
        string courseName, double score, double tuitionFee)
        : base(id, name, age, email)
    {
        CourseName = courseName;
        Score = score;
        TuitionFee = tuitionFee;
    }

    public override void DisplayInfo()
    {
        double actualFee = CalculateMoney();
        string discount = Score >= 8.0 ? " (giảm 10%)" : "";

        Console.WriteLine($"[HV] ID: {Id}  Tên: {Name}  Tuổi: {Age}  Email: {Email}");
        Console.WriteLine($"Khóa học: {CourseName}  Điểm: {Score}  " +
                          $"Học phí: {actualFee}đ{discount}");
    }
    
    //Lớp Student thực thi IFinance để tính học phí
    //(nếu điểm >= 8.0 thì giảm 10% học phí).
    public double CalculateMoney()
    {
        if (Score >= 8.0)
            return TuitionFee * 0.9; // Giảm 10%
        return TuitionFee;
    }
    // Hàm trả về toàn bộ thông tin của học sinh
    public string StudentInfo()
    {
        return $"Student|{Id}|{Name}|{Age}|{Email}|{CourseName}|{Score}|{TuitionFee}";
    }
    
    //Hàm tạo đối tượng Student
    public static Student CreateStudent(string[] parts)
    {
        return new Student(
            id: parts[1],
            name: parts[2],
            age: int.Parse(parts[3]),
            email: parts[4],
            courseName: parts[5],
            score: double.Parse(parts[6]),
            tuitionFee: double.Parse(parts[7])
        );
    }

}