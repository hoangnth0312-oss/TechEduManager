using System;
using System.Collections.Generic;
using System.Linq;

namespace TechEduManager
{
    class Program
    {
        static void Main(string[] args)
        {
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

            static void AddPerson()
            {
                Console.WriteLine("Lựa chọn thêm người: ");
                Console.WriteLine("1. Học viên (Student)");
                Console.WriteLine("2. Giảng viên (Instructor)");
                
            }
        }
    }
}