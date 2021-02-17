using System;
using System.IO;

namespace Exercise3
{
    class Program
    {
        private const int minutes = 1; //количество минут разницы, на которое нужно проверять в условии
        static void Main(string[] args)
        {
            string path = "E:\\Хавролев\\Skillfactory\\Обучение\\Модуль 8\\Тест — копия (4) — копия";
            HalfHourErase(path);

            Console.ReadKey();
        }

        /// <summary>
        /// Очищает указанную папку рекурсивно вместе с файлами, по которым последний доступ был более, чем полчаса назад 
        /// </summary>
        /// <param name="path">Путь до папки</param>
        static void HalfHourErase(string path)
        {
            Console.WriteLine("Проверка папки \"{0}\" начата", path);
            Console.WriteLine();

            try
            {
                DirectoryInfo root = new DirectoryInfo(path);

                if (!root.Exists)
                {
                    Console.WriteLine("Папки с путём \"{0}\" не существует", root.FullName);
                }
                else
                {
                    long sizeBefore = GetDirSizeFolder(root);
                    Console.WriteLine("Исходный размер папки - {0} байт", sizeBefore);
                    Console.WriteLine();

                    int deleted = HalfHourEraseFolder(root, true);
                    if (deleted == 0)
                        Console.WriteLine("Нет ни одной папки и файла, доступ к которой осуществлялся более получаса назад");

                    long sizeAfter = GetDirSizeFolder(root);
                    Console.WriteLine();
                    Console.WriteLine("Освобождено - {0} байт", sizeBefore - sizeAfter);
                    Console.WriteLine();
                    Console.WriteLine("Удалено папок и файлов - {0} шт", deleted);
                    Console.WriteLine();
                    Console.WriteLine("Текущий размер папки - {0} байт", sizeAfter);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Исключение - \"{0}\"", e.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Проверка папки \"{0}\" окончена", path);
        }

        /// <summary>
        /// Производит проверку данной папки на старость вложенных папок и файлов
        /// </summary>
        /// <param name="root">Объект типа DirectoryInfo</param>
        /// <param name="flag">Флаг первого запуска зацикливания</param>
        static int HalfHourEraseFolder(DirectoryInfo root, bool flag)
        {
            int counter = 0;

            if (!flag)
            {
                if (HalfHourCompare(root))
                {
                    counter++;
                    root.Delete(true);
                    return counter;
                }
            }

            FileInfo[] files = root.GetFiles();

            foreach (FileInfo file in files)
            {
                if (HalfHourCompare(file))
                {
                    counter++;
                    file.Delete();
                }
            }

            DirectoryInfo[] folders = root.GetDirectories();

            foreach (DirectoryInfo folder in folders)
            {
                int value = HalfHourEraseFolder(folder, false);
                if (value > 0)
                    counter += value;
            }

            return counter;
        }

        /// <summary>
        /// Производит проверку данной папки или файла на старость
        /// </summary>
        /// <param name="file">Объект типа DirectoryInfo или FileInfo</param>
        static bool HalfHourCompare<T>(T obj) where T : FileSystemInfo
        {
            if (DateTime.Now > obj.LastAccessTime.AddMinutes(minutes))
            {
                Console.WriteLine("Последний доступ к \"{0}\" осуществлялся более получаса назад, производится удаление", obj.FullName);
                return true;
            }
            return false;
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
