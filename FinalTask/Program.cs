using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace FinalTask
{
    class Program
    {
        private const string rootPath = "C:\\Users\\s.khavrolev\\Desktop"; //корневая папка, где будет создаваться папка с файлами
        static void Main(string[] args)
        {
            string path = "E:\\Хавролев\\Skillfactory\\Обучение\\Модуль 8\\Students.dat";
            ReadFile(path);
            Console.ReadKey();
        }

        /// <summary>
        /// Метод читает бинарник, десериализует его и расскладывает по файлам групп имена и даты учеников
        /// </summary>
        /// <param name="path">Путь до бинарника</param>
        static void ReadFile(string path)
        {
            Console.WriteLine("Анализ файла \"{0}\" начат", path);
            Console.WriteLine();

            if (!File.Exists(path) || !Directory.Exists(rootPath))
            {
                Console.WriteLine("Файл не существует или нет папки, куда хотите сохранять данные из файла");
            }
            else
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(rootPath + "\\Students");
                    BinaryFormatter formatter = new BinaryFormatter();
                    Student[] students;

                    if (dir.Exists)
                        dir.Delete(true);

                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        students = (Student[])formatter.Deserialize(fs);
                        Console.WriteLine("Объект десериализован, длина - {0} байт", students.Length);
                    }

                    dir.Create();
                    string dirPath = dir.FullName;

                    foreach (Student student in students)
                    {
                        using (StreamWriter sw = File.AppendText(dirPath + "\\" + student.Group + ".txt"))
                        {
                            string formatedDate = student.DateOfBirth.ToString("dd.MM.yyyy");
                            sw.WriteLine("{0}, {1}", student.Name, formatedDate);
                            Console.WriteLine("Студент по имени {0} из группы {1}, дата рождения - {2}", student.Name, student.Group, formatedDate);
                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Исключение - \"{0}\"", e.Message);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Анализ файла \"{0}\" окончен", path);
            Console.WriteLine("Итоговые данные ищите в папке \"{0}\"", rootPath + "\\Students");
        }
    }

    /// <summary>
    /// Класс для десериализуемого объекта
    /// </summary>
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Student(string name, string group, DateTime date)
        {
            Name = name;
            Group = group;
            DateOfBirth = date;
        }
    }
}
