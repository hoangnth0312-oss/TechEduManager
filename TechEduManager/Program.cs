using System;
using System.Collections.Generic;
using System.Linq;

namespace TechEduManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "TechEdu Manager";

            // Tạo và khởi động hệ thống
            TechEduManager app = new TechEduManager();
            app.Start();
        }
    }
}