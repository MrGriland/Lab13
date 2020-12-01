using System;
using System.IO;
using System.IO.Compression;

namespace Lab13
{

    class BGVLog 
    {
        public static void WriteLog(string name, string namef, string dir)
        {
            using (StreamWriter st = new StreamWriter(@"BGVlog.txt", true))
            {
                st.WriteLine($"метод -  {name}, файл - {namef}, путь -  {dir}, вызван -  {DateTime.Now}");
            }
        }
        public static void WriteLog(string name, string dir)
        {
            using (StreamWriter st = new StreamWriter(@"BGVlog.txt", true))
            {
                st.WriteLine($"метод -  {name}, путь -  {dir}, вызван -  {DateTime.Now}");
            }
        }
        public static void WriteLog(string name)
        {
            using (StreamWriter st = new StreamWriter(@"BGVlog.txt", true))
            {
                st.WriteLine($"метод -  {name}  , вызван -  {DateTime.Now}");
            }
        }
    }

    static class BGVDiskInfo
    {
        public static void FreeSpace(string DN) 
        {
            BGVLog.WriteLog("FreeSpace");
            var Driveinfo = DriveInfo.GetDrives();
            foreach (var d in Driveinfo)
            {
                if (d.Name == DN)
                {
                    Console.WriteLine($"{ d.Name} - {d.TotalFreeSpace} bytes");
                }
            }
        }
        public static void TypeOfFileSystem(string DN) 
        {
            BGVLog.WriteLog("TypeOfFileSystem");
            var Driveinfo = DriveInfo.GetDrives();
            foreach (var d in Driveinfo)
            {
                if (d.Name == DN)
                {
                    Console.WriteLine($"{d.Name} - {d.DriveFormat}");
                }
            }
        }
        public static void DrivesInfo()
        {
            BGVLog.WriteLog("DrivesInfo");
            var Driveinfo = DriveInfo.GetDrives();
            foreach (var d in Driveinfo)
            {
                if (d.IsReady) Console.WriteLine($"{d.Name} - {d.TotalFreeSpace} bytes / {d.TotalSize} bytes  -- {d.VolumeLabel}");
            } 
        }
    }

    public static class BGVFileInfo 
    {
        public static void FullPath(string Path)
        {
            FileInfo f = new FileInfo(Path);
            BGVLog.WriteLog("FullPath", f.Name, Path);
            Console.WriteLine(f.DirectoryName);
        }
        public static void FullInfo(string Path) 
        {
            FileInfo f = new FileInfo(Path);
            BGVLog.WriteLog("FullInfo", f.Name, Path);
            Console.WriteLine($"{f.Name} - {f.Extension} - {f.Length} ");
        }
        public static void TimeOfCreation(string Path)
        {
            FileInfo f = new FileInfo(Path);
            BGVLog.WriteLog("TimeOfCreation", f.Name, Path);
            Console.WriteLine(f.CreationTime);
        }
    }

    public static class BGVDirInfo 

    {
        public static void GetFiles(string Path)
        {
            DirectoryInfo d = new DirectoryInfo(Path);
            BGVLog.WriteLog("GetFiles", Path);
            foreach (var f in d.GetFiles()) Console.WriteLine(f.FullName);
        }
        public static void GetTime(string Path)
        {
            DirectoryInfo d = new DirectoryInfo(Path);
            BGVLog.WriteLog("GetTime", Path);
            Console.WriteLine(d.CreationTime);
        }
        public static void GetSubDir(string Path)
        {
            DirectoryInfo d = new DirectoryInfo(Path);
            BGVLog.WriteLog("GetSubDir", Path);
            int i = 0;
            foreach (var f in d.GetDirectories())
            {
                Console.WriteLine(f.FullName);
                i++;
            }
            Console.WriteLine(i);
        }
        public static void GetParentDir(string Path)
        {
            DirectoryInfo d = new DirectoryInfo(Path);
            BGVLog.WriteLog("GetParentDir", Path);
            foreach (var f in d.Parent.GetDirectories()) Console.WriteLine(f.FullName);
        }
    }
    public static class BGVFileManager
    {
        public static void FDirMethod(string Path)
        {
            BGVLog.WriteLog("FDirMethod");
            DirectoryInfo d = new DirectoryInfo(Path);
            d.CreateSubdirectory("BGVInspect");
            string p1 = Path + @"\BGVInspect" + @"\BGVDirinfo.txt";
            string p2 = Path + @"\BGVInspect" + @"\BGVDirinfocopy.txt";
            using (StreamWriter st = new StreamWriter(p1, false))
            {
                foreach (var f in d.GetFiles())
                {
                    st.WriteLine($"файл - {f.FullName}");
                    Console.WriteLine(f.FullName);
                }

            }
            using (StreamWriter st = new StreamWriter(p1, true))
            {
                foreach (var f in d.GetDirectories())
                {
                    st.WriteLine($"файл - {f.FullName}");
                    Console.WriteLine(f.FullName);
                }

            }
            File.Copy(p1, p2, true);
            File.Delete(p1);
        }
        public static void SDirMethod(string Path)
        {
            BGVLog.WriteLog("SDirMethod");
            DirectoryInfo d = new DirectoryInfo(Path);
            d.CreateSubdirectory("BGVFiles");
            string p1 = Path + @"\BGVFiles";
            string p2 = Path + @"\BGVInspect\BGVFiles";
            DirectoryInfo d33 = new DirectoryInfo(p2);
            if(d33.Exists)
                d33.Delete(true);
            Console.WriteLine(" ");
            string Path1 = Console.ReadLine();
            DirectoryInfo d1 = new DirectoryInfo(Path1);
            foreach (var f in d1.GetFiles())
            {
                if (f.Extension == ".txt")
                    f.CopyTo(p1 + @"\" + f.Name, true);

            }
            DirectoryInfo d2 = new DirectoryInfo(Path + @"\BGVFiles");
            d2.MoveTo(p2);
        }
        public static void TDirMethod(string Path)
        {
            BGVLog.WriteLog("TDirMethod");
            string p2 = Path + @"\BGVInspect\files.zip";
            FileInfo d4 = new FileInfo(p2);
            if (d4.Exists)
                d4.Delete();
            ZipFile.CreateFromDirectory(Path + @"\BGVInspect\BGVFiles", Path + @"\BGVInspect\files.zip");//делаем архив 
            ZipFile.ExtractToDirectory(Path + @"\BGVInspect\files.zip", Path, true);
        }
        public static void FoDirMethod(string Path)
        {
            BGVLog.WriteLog("FoDirMethod");
            string[] s = File.ReadAllLines(Path + @"\BGVLog.txt");
            for (int i = 0; i < s.Length; i++)
            {
                Console.WriteLine(s[i]);
            }
            Console.WriteLine(s.Length + " Записей");//колво записей влогфайле
            Console.WriteLine("По дате");
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains(DateTime.Now.Day.ToString()))
                    Console.WriteLine(s[i]);
            }
            Console.WriteLine("По времени");
            string pr = Console.ReadLine();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains(pr))
                    Console.WriteLine(s[i]);
            }
            Console.WriteLine("По ключевому слову");
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains("FDirMethod"))
                    Console.WriteLine(s[i]);
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (StreamWriter st = new StreamWriter(@"BGVlog.txt", false)){}
            BGVDiskInfo.FreeSpace("C:\\");
            BGVDiskInfo.TypeOfFileSystem("C:\\");
            BGVDiskInfo.DrivesInfo();
            Console.WriteLine(new string('-', 50));
            BGVFileInfo.FullPath(@"C:\Users\grish\source\repos\Lab13\Lab13\bin\Debug\netcoreapp3.1\ex1.txt");
            BGVFileInfo.FullInfo(@"C:\Users\grish\source\repos\Lab13\Lab13\bin\Debug\netcoreapp3.1\ex1.txt");
            BGVFileInfo.TimeOfCreation(@"C:\Users\grish\source\repos\Lab13\Lab13\bin\Debug\netcoreapp3.1\ex1.txt");
            Console.WriteLine(new string('-', 50));
            BGVDirInfo.GetFiles(@"C:\Users\grish\source\repos\Lab13\Lab13\bin\Debug\netcoreapp3.1");
            BGVDirInfo.GetSubDir(@"C:\Users\grish\source\repos\Lab13\Lab13\bin\Debug\netcoreapp3.1");
            BGVDirInfo.GetTime(@"C:\Users\grish\source\repos\Lab13\Lab13\bin\Debug\netcoreapp3.1");
            BGVDirInfo.GetParentDir(@"C:\Users\grish\source\repos\Lab13\Lab13\bin\Debug\netcoreapp3.1");
            Console.WriteLine(new string('-', 50));
            BGVFileManager.FDirMethod(@"D:\IPCalc");
            BGVFileManager.SDirMethod(@"D:\IPCalc");
            BGVFileManager.TDirMethod(@"D:\IPCalc");
            BGVFileManager.FoDirMethod(@"C:\Users\grish\source\repos\Lab13\Lab13\bin\Debug\netcoreapp3.1");
        }
    }
}
