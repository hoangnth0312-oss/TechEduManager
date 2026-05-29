using System;
using System.Collections.Generic;
using System.Linq;
namespace TechEduManager;

public class TechEduManager
{
    // Danh sách nhân sự chính (chứa cả Student và Instructor)
    private List<Person> _persons = new List<Person>();
    // Tải dữ liệu từ file
     public void Start()
        {
            Console.WriteLine("Đang tải dữ liệu...");
            _persons = TextFileManager.LoadData();

            // Nếu chưa có dữ liệu, thêm dữ liệu mẫu
            if (_persons.Count == 0)
                SeedSampleData();

            // Vòng lặp menu chính
            bool running = true;
            while (running)
            {
                PrintMenu();

                int choice = 0;
                bool validChoice = false;

                // Nhập lựa chọn menu với try-catch
                while (!validChoice)
                {
                    Console.Write("  Vui lòng chọn chức năng (1-7): ");
                    try
                    {
                        choice = int.Parse(Console.ReadLine() ?? "");
                        if (choice < 1 || choice > 7)
                        {
                            Console.WriteLine("  [!] Vui lòng chọn từ 1 đến 7.");
                            continue;
                        }
                        validChoice = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("  [!] Vui lòng nhập một số nguyên hợp lệ.");
                    }
                }

                switch (choice)
                {
                    case 1: AddPerson(); break;
                    case 2: DisplayAllPersons(); break;
                    case 3: SearchPerson(); break;
                    case 4: CalculateFinance(); break;
                    case 5: DisplayExcellentStudents(); break;
                    case 6: SaveData(); break;
                    case 7:
                        running = false;
                        Console.WriteLine("\n  Cảm ơn đã sử dụng TechEdu Manager. Tạm biệt!\n");
                        break;
                }

                if (running)
                {
                    Console.Write("\n  Nhấn Enter để quay lại menu...");
                    Console.ReadLine();
                }
            }
        }
        private bool IsValidName(string name)
        {
            foreach (char c in name)
            {
                // Nếu gặp ký tự số thì tên sẽ không hợp lệ
                //
                if (char.IsDigit(c))
                    return false;
            }
            return true;
        }
        private void SeedSampleData()
        {
            Console.WriteLine("  Nạp dữ liệu mẫu...");

            _persons.AddRange(new List<Person>
            {
                new Student("HV001", "Nguyễn Văn An",     21, "an.nv@email.com",      "C# .NET",    8.5, 5_000_000),
                new Student("HV002", "Trần Thị Bích",     20, "bich.tt@email.com",    "Python AI",  7.2, 4_500_000),
                new Student("HV003", "Lê Quang Cường",    22, "cuong.lq@email.com",   "Web React",  9.1, 6_000_000),
                new Student("HV004", "Phạm Thị Dung",     19, "dung.pt@email.com",    "Java Spring",5.8, 4_000_000),
                new Student("HV005", "Hoàng Minh Đức",    23, "duc.hm@email.com",     "DevOps",     8.0, 7_000_000),

                new Instructor("GV001", "Nguyễn Thị Hoa", 35, "hoa.nt@techedu.vn",   "Lập trình Web",  15_000_000, 80),
                new Instructor("GV002", "Trần Văn Khải",  42, "khai.tv@techedu.vn",  "Khoa học DL",    18_000_000, 60),
                new Instructor("GV003", "Lê Thị Mai",     38, "mai.lt@techedu.vn",   "An ninh mạng",   16_000_000, 70),
            });

            Console.WriteLine($"  Đã nạp {_persons.Count} bản ghi mẫu.\n");
        }
        static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("================ TECHEDU MANAGER ================");
            Console.WriteLine("1. Thêm mới nhân sự (Học viên / Giảng viên)");
            Console.WriteLine("2. Hiển thị danh sách toàn bộ nhân sự");
            Console.WriteLine("3. Tìm kiếm nhân sự theo ID");
            Console.WriteLine("4. Tính lương giảng viên và học phí học viên (IFinance)");
            Console.WriteLine("5. Hiển thị danh sách học viên xuất sắc (Điểm >= 8.0)");
            Console.WriteLine("6. Lưu dữ liệu xuống File");
            Console.WriteLine("7. Thoát chương trình");
            Console.WriteLine("=================================================");
            Console.WriteLine();
        }
        private void AddPerson()
        {

            Console.WriteLine("  Loại nhân sự:");
            Console.WriteLine("    1. Học viên (Student)");
            Console.WriteLine("    2. Giảng viên (Instructor)");

            // Nhập loại nhân sự với try-catch
            int type = 0;
            while (true)
            {
                Console.Write("  Chọn (1-2): ");
                try
                {
                    type = int.Parse(Console.ReadLine() ?? "");
                    if (type < 1 || type > 2)
                    {
                        Console.WriteLine("  [!] Vui lòng chọn 1 hoặc 2.");
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("  [!] Vui lòng nhập số nguyên hợp lệ.");
                }
            }

            // Tạo ID tự động, tránh trùng lặp
            string newId = GenerateId(type == 1 ? "HV" : "GV");
            Console.WriteLine($"\n  ID tự động: {newId}");

            // Nhập họ tên — kiểm tra không để trống
            string name = "";
            while (true)
            {
                Console.Write("  Họ tên: ");
                name = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("  [!] Họ tên không được để trống.");
                    continue;
                }
                if (!IsValidName(name))
                {
                    Console.WriteLine("  [!] Họ tên chỉ được chứa chữ cái, không được nhập số.");
                    continue;
                }
                break;
            }

            // Nhập tuổi với try-catch
            int age = 0;
            while (true)
            {
                Console.Write("  Tuổi: ");
                try
                {
                    age = int.Parse(Console.ReadLine() ?? "");
                    if (age < 16 || age > 100)
                    {
                        Console.WriteLine("  [!] Tuổi phải nằm trong khoảng [16 - 100].");
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("  [!] Vui lòng nhập số nguyên hợp lệ.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("  [!] Số quá lớn. Vui lòng nhập lại.");
                }
            }

            // Nhập email — kiểm tra không để trống
            string email = "";
            while (true)
            {
                Console.Write("  Email: ");
                email = Console.ReadLine()?.Trim() ?? "";
                if (!string.IsNullOrEmpty(email)) break;
                Console.WriteLine("  [!] Email không được để trống.");
            }

            if (type == 1)
            {
                // Nhập tên khóa học
                string courseName = "";
                while (true)
                {
                    Console.Write("  Tên khóa học: ");
                    courseName = Console.ReadLine()?.Trim() ?? "";
                    if (!string.IsNullOrEmpty(courseName)) break;
                    Console.WriteLine("  [!] Tên khóa học không được để trống.");
                }

                // Nhập điểm với try-catch
                double score = 0;
                while (true)
                {
                    Console.Write("  Điểm trung bình (0-10): ");
                    try
                    {
                        string input = Console.ReadLine()?.Replace(',', '.') ?? "";
                        score = double.Parse(input, System.Globalization.CultureInfo.InvariantCulture);
                        if (score < 0 || score > 10)
                        {
                            Console.WriteLine("  [!] Điểm phải nằm trong khoảng [0 - 10].");
                            continue;
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("  [!] Vui lòng nhập số thực hợp lệ (vd: 8.5).");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("  [!] Số quá lớn. Vui lòng nhập lại.");
                    }
                }

                // Nhập học phí với try-catch
                double tuition = 0;
                while (true)
                {
                    Console.Write("  Học phí (đ): ");
                    try
                    {
                        string input = Console.ReadLine()?.Replace(',', '.') ?? "";
                        tuition = double.Parse(input, System.Globalization.CultureInfo.InvariantCulture);
                        if (tuition < 0)
                        {
                            Console.WriteLine("  [!] Học phí không được âm.");
                            continue;
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("  [!] Vui lòng nhập số thực hợp lệ.");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("  [!] Số quá lớn. Vui lòng nhập lại.");
                    }
                }

                var student = new Student(newId, name, age, email, courseName, score, tuition);
                _persons.Add(student);
                Console.WriteLine("\n  [OK] Đã thêm học viên thành công!");
            }
            else
            {
                // Nhập khoa / bộ môn
                string department = "";
                while (true)
                {
                    Console.Write("  Khoa / Bộ môn: ");
                    department = Console.ReadLine()?.Trim() ?? "";
                    if (!string.IsNullOrEmpty(department)) break;
                    Console.WriteLine("  [!] Khoa / Bộ môn không được để trống.");
                }

                // Nhập lương cơ bản với try-catch
                double baseSalary = 0;
                while (true)
                {
                    Console.Write("  Lương cơ bản (đ): ");
                    try
                    {
                        string input = Console.ReadLine()?.Replace(',', '.') ?? "";
                        baseSalary = double.Parse(input, System.Globalization.CultureInfo.InvariantCulture);
                        if (baseSalary < 0)
                        {
                            Console.WriteLine("  [!] Lương không được âm.");
                            continue;
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("  [!] Vui lòng nhập số thực hợp lệ.");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("  [!] Số quá lớn. Vui lòng nhập lại.");
                    }
                }

                // Nhập số giờ dạy với try-catch
                int teachingHours = 0;
                while (true)
                {
                    Console.Write("  Số giờ dạy trong tháng: ");
                    try
                    {
                        teachingHours = int.Parse(Console.ReadLine() ?? "");
                        if (teachingHours < 0 || teachingHours > 744)
                        {
                            Console.WriteLine("  [!] Số giờ dạy phải nằm trong khoảng [0 - 744].");
                            continue;
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("  [!] Vui lòng nhập số nguyên hợp lệ.");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("  [!] Số quá lớn. Vui lòng nhập lại.");
                    }
                }

                var instructor = new Instructor(newId, name, age, email, department, baseSalary, teachingHours);
                _persons.Add(instructor);
                Console.WriteLine("\n  [OK] Đã thêm giảng viên thành công!");
            }
        }
        private void DisplayAllPersons()
        {


            if (_persons.Count == 0)
            {
                Console.WriteLine("  (Chưa có nhân sự nào trong hệ thống.)");
                return;
            }

            var students    = _persons.OfType<Student>().ToList();
            var instructors = _persons.OfType<Instructor>().ToList();

            Console.WriteLine($"\n  === HỌC VIÊN ({students.Count} người) ===\n");
            if (students.Count == 0)
                Console.WriteLine("  (Không có học viên.)");
            else
                foreach (var s in students)
                {
                    s.DisplayInfo();
                    Console.WriteLine();
                }

            Console.WriteLine($"  === GIẢNG VIÊN ({instructors.Count} người) ===\n");
            if (instructors.Count == 0)
                Console.WriteLine("  (Không có giảng viên.)");
            else
                foreach (var i in instructors)
                {
                    i.DisplayInfo();
                    Console.WriteLine();
                }

            Console.WriteLine($"\n  Tổng cộng: {_persons.Count} nhân sự.");
        }
        
        private void SearchPerson()
        {

            Console.WriteLine("  Tìm kiếm theo:");
            Console.WriteLine("    1. Mã ID");
            Console.WriteLine("    2. Tên");

            // Nhập lựa chọn tìm kiếm với try-catch
            int option = 0;
            while (true)
            {
                Console.Write("  Chọn (1-2): ");
                try
                {
                    option = int.Parse(Console.ReadLine() ?? "");
                    if (option < 1 || option > 2)
                    {
                        Console.WriteLine("  [!] Vui lòng chọn 1 hoặc 2.");
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("  [!] Vui lòng nhập số nguyên hợp lệ.");
                }
            }

            List<Person> results;

            if (option == 1)
            {
                string searchId = "";
                while (true)
                {
                    Console.Write("  Nhập Mã ID cần tìm: ");
                    searchId = Console.ReadLine()?.Trim().ToUpper() ?? "";
                    if (!string.IsNullOrEmpty(searchId)) break;
                    Console.WriteLine("  [!] Mã ID không được để trống.");
                }
                results = _persons.Where(p => p.Id.ToUpper() == searchId).ToList();
            }
            else
            {
                string searchName = "";
                while (true)
                {
                    Console.Write("  Nhập Tên cần tìm: ");
                    searchName = Console.ReadLine()?.Trim().ToLower() ?? "";
                    if (!string.IsNullOrEmpty(searchName)) break;
                    Console.WriteLine("  [!] Tên không được để trống.");
                }
                results = _persons.Where(p => p.Name.ToLower().Contains(searchName)).ToList();
            }

            Console.WriteLine();
            if (results.Count == 0)
            {
                Console.WriteLine("  Không tìm thấy nhân sự phù hợp.");
            }
            else
            {
                Console.WriteLine($"  Tìm thấy {results.Count} kết quả:\n");
                foreach (var person in results)
                {
                    person.DisplayInfo();
                    Console.WriteLine();
                }
            }
        }
        
        private void CalculateFinance()
        {

            var students    = _persons.OfType<Student>().ToList();
            var instructors = _persons.OfType<Instructor>().ToList();

            // Tổng lương giảng viên
            double totalSalary = 0;
            Console.WriteLine("\n  === CHI TIẾT LƯƠNG GIẢNG VIÊN ===\n");
            if (instructors.Count == 0)
            {
                Console.WriteLine("  (Không có giảng viên.)");
            }
            else
            {
                foreach (var inst in instructors)
                {
                    double salary = inst.CalculateMoney();
                    totalSalary += salary;
                    Console.WriteLine($"  {inst.Name,-22} | Lương CB: {inst.BaseSalary:N0}đ " +
                                      $"| {inst.TeachingHours}h × 200.000đ = {salary:N0}đ");
                }
            }
            Console.WriteLine($"\n  ► TỔNG LƯƠNG PHẢI TRẢ: {totalSalary:N0}đ");

            // Tổng học phí học viên
            double totalTuition = 0;
            Console.WriteLine("\n  === CHI TIẾT HỌC PHÍ HỌC VIÊN ===\n");
            if (students.Count == 0)
            {
                Console.WriteLine("  (Không có học viên.)");
            }
            else
            {
                foreach (var stu in students)
                {
                    double fee = stu.CalculateMoney();
                    totalTuition += fee;
                    string note = stu.Score >= 8.0 ? "[ưu tú -10%]" : "";
                    Console.WriteLine($"  {stu.Name,-22} | Điểm: {stu.Score:F1} | Phí: {fee:N0}đ {note}");
                }
            }
            Console.WriteLine($"\n  ► TỔNG DOANH THU HỌC PHÍ: {totalTuition:N0}đ");
            Console.WriteLine($"\n  ► CHÊNH LỆCH (Doanh thu - Chi phí lương): {(totalTuition - totalSalary):N0}đ");
        }
        private void DisplayExcellentStudents()
        {
            Console.WriteLine("DANH SÁCH HỌC VIÊN XUẤT SẮC (Điểm >= 8.0)");

            var excellent = _persons
                .OfType<Student>()
                .Where(s => s.Score >= 8.0)
                .OrderByDescending(s => s.Score)
                .ToList();

            if (excellent.Count == 0)
            {
                Console.WriteLine("\n  Hiện không có học viên nào đạt điểm >= 8.0.");
            }
            else
            {
                Console.WriteLine($"\n  Tổng số học viên xuất sắc: {excellent.Count}\n");
                int rank = 1;
                foreach (var student in excellent)
                {
                    Console.WriteLine($"  #{rank++}");
                    student.DisplayInfo();
                    Console.WriteLine();
                }
            }
        }
        private void SaveData()
        {
            Console.WriteLine("LƯU DỮ LIỆU XUỐNG FILE");
            TextFileManager.SaveData(_persons);
        }
        
        /// Tạo ID tự động theo prefix, tránh trùng với ID đã có
        private string GenerateId(string prefix)
        {
            int counter = 1;
            string id;
            do
            {
                id = $"{prefix}{counter:D3}";
                counter++;
            }
            while (_persons.Any(p => p.Id == id));
            return id;
        }
}