namespace Modul8.HW.Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\zuevi\\Desktop\\HW";

            if (Directory.Exists(path))
            {
                Clear(path);
            }
            else
            {
                Console.WriteLine("Папка по указанному пути не существует");
            }
        }

        public static void Clear(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            
            DirectoryInfo[] subDirectories = directory.GetDirectories();
            FileInfo[] files = directory.GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.LastAccessTime.AddMinutes(30) < DateTime.Now) // не придумал как по другому проверить время
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine("Файл не найден. Ошибка: " + ex.Message);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка: " + ex.Message);
                    }
                    file.Delete(); 
                }
            }

            foreach  (DirectoryInfo subDirectory in subDirectories)
            {
                if (subDirectory.LastAccessTime.AddMinutes(30) < DateTime.Now)
                {
                    try
                    {
                        subDirectory.Delete(true);
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                        Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Произошла ошибка: " + ex.Message);
                    }
                    
                }
                else
                {
                    string subPath = subDirectory.FullName;
                    Clear(subPath);
                }
            }
        }
    }
}
