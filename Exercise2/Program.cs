using System;
using System.IO;

namespace Exercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "E:\\Хавролев\\Skillfactory\\Обучение\\Модуль 8\\Тест";
            long size = GetDirSize(path);

            Console.ReadKey();
        }

        /// <summary>
        /// Рекурсивный расчёт занимаемого пространства, на выходе long
        /// </summary>
        /// <param name="path">Путь до папки</param>
        /// <returns></returns>
        static long GetDirSize(string path)
        {
            Console.WriteLine("Расчёт размера файлов в папке \"{0}\" начат", path);
            Console.WriteLine();
            long size = 0;

            try
            {
                DirectoryInfo root = new DirectoryInfo(path);
                if (!root.Exists)
                {
                    Console.WriteLine("Папки с путём \"{0}\" не существует", root.FullName);
                    size = -1;
                }
                else
                {
                    size = GetDirSizeFolder(root);
                    Console.WriteLine("Расчёт размера файлов составляет {0} байт", size);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Исключение - \"{0}\"", e.Message);
                size = -1;
            }

            Console.WriteLine();
            Console.WriteLine("Расчёт размера файлов в папке \"{0}\" окончен", path);

            return size;
        }

        /// <summary>
        /// Рекурсивный подсчёт памяти
        /// </summary>
        /// <param name="root">Объект DirectoryInfo</param>
        /// <returns></returns>
        static long GetDirSizeFolder(DirectoryInfo root)
        {
            long size = 0;

            FileInfo[] files = root.GetFiles();

            foreach (FileInfo file in files)
            {
                size += file.Length;
            }

            DirectoryInfo[] folders = root.GetDirectories();

            foreach (DirectoryInfo folder in folders)
            {
                size += GetDirSizeFolder(folder);
            }

            return size;
        }
    }
}
