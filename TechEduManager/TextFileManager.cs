using System;
using System.Collections.Generic;
using System.IO;

namespace TechEduManager;
//Viết hàm lưu dữ liệu và đọc dữ liệu
public static class TextFileManager
{
    private const string DataFilePath = "techedu_data.txt";
    
    //logic của hàm SaveData
    //tạo ra một danh sách(list) rỗng để lưu trữ thông tin của giao vien và học sinh
    //sau đó sử dụng vòng lặp foreach để xử lí tất cả dữ liệu đã nhập thảnh chuỗi
    // cuối cùng là ghi tất cả các chuỗi vừa được xử lí vào file.
    
    
    public static void SaveData(List<Person> persons)
    {
        try
        {
            // Tạo danh sách các dòng cần ghi
            List<string> lines = new List<string>();

            foreach (var person in persons)
            {
                // FileManager tự xử lý chuyển đổi dữ liệu thành chuỗi
                if (person is Student s)
                    lines.Add(s.StudentInfo());
                else if (person is Instructor i)
                    lines.Add(i.InstuctorInfo());
            }

            // Ghi tất cả xuống file 1 lần — tự tạo file nếu chưa có, tự đóng sau khi ghi
            // logic hoạt động của dòng lưu file này:
            //Truy cập vào class có sẵn của c# để sử dụng file
            //Sau đó ghi lại tất cả các dòng
            // cuối cùng là truyền vào tham số là file sử dụng để ghi dữ liệu và ghi tất cả các dòng có trong list
            File.WriteAllLines(DataFilePath, lines);

            
            Console.WriteLine($"\n Đã lưu {persons.Count} bản ghi vào file '{DataFilePath}'.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"\n Không thể ghi file: {ex.Message}");
        }
    }
    
    public static List<Person> LoadData()
        {
            
            //tạo ra một danh sách rỗng
            var persons = new List<Person>();

            //Kiểm tra xem có đang tồn tại một file nào không
            //nếu file không tồn tại thì sẽ tạo ra một danh sách rỗng mới
            if (!File.Exists(DataFilePath))
            {
                Console.WriteLine(" Chưa có file dữ liệu. Bắt đầu với danh sách rỗng.");
                return persons;
            }

            try
            {
                string[] lines = File.ReadAllLines(DataFilePath);
                int loaded = 0;

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('|');
                    if (parts.Length < 2) continue;

                    try
                    {
                        // FileManager tự xử lý tạo object từ chuỗi đọc được
                        if (parts[0] == "Student" && parts.Length == 8)
                        {
                            persons.Add(Student.CreateStudent(parts));
                            loaded++;
                        }
                        else if (parts[0] == "Instructor" && parts.Length == 8)
                        {
                            
                            persons.Add(Instructor.CreateInstructor(parts));
                            loaded++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" Bỏ qua dòng lỗi: {line} ({ex.Message})");
                    }
                }

                Console.WriteLine($" Đã tải {loaded} bản ghi từ file '{DataFilePath}'.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"\n Không thể đọc file: {ex.Message}");
            }

            return persons;
        }
    
    
}