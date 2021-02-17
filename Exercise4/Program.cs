using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace FinalTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "E:\\Хавролев\\Skillfactory\\Обучение\\Модуль 8\\Students.dat";
            ReadFile(path);
            Console.ReadKey();
        }

        static void ReadFile(string path)
        {
            Console.WriteLine("Анализ файла \"{0}\" начат", path);
            Console.WriteLine();

            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не существует");
            }
            else
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        Student[] students = (Student[])formatter.Deserialize(fs);
                        Console.WriteLine("Объект десериализован, длина - {0} байт", students.Length);
                        foreach (Student student in students)
                        { 
                            Console.WriteLine("Студент по имени {0} из группы {1}, дата рождения - {2}", student.Name, student.Group, student.DateOfBirth);
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
        }
    }

    [Serializable]
    class Student
    {
        public string Name;
        public string Group;
        public DateTime DateOfBirth;

        public Student(string name, string group, DateTime date)
        {
            Name = name;
            Group = group;
            DateOfBirth = date;
        }
    }
}
