using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    class Program
    {
        static void ShowDirectoryInfo(DirectoryInfo directory,int cursor){
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            FileSystemInfo[] fsis = directory.GetFileSystemInfos();
            
            for(int i=0; i<fsis.Length; i++){
                FileSystemInfo fsi = fsis[i];
                if(i == cursor){
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                else{
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                if(fsi.GetType() == typeof(DirectoryInfo)){
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else{
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                Console.WriteLine(fsi.Name);
            }
        }
        static void Main(string[] args)
        {
            DirectoryInfo dInfo = new DirectoryInfo(@"/home/yerassyl");
            int cursor = 0;
            int n = dInfo.GetFileSystemInfos().Length;
            ShowDirectoryInfo(dInfo,cursor);
            while(true){
                ConsoleKeyInfo kI = Console.ReadKey();
                if(kI.Key == ConsoleKey.DownArrow){
                    cursor++;
                    if(cursor == n){
                        cursor = 0;
                    }
                }
                if(kI.Key == ConsoleKey.UpArrow){
                    cursor--;
                    if(cursor == -1){
                        cursor = n-1;
                    }
                }
                if(kI.Key == ConsoleKey.Enter){
                    if(dInfo.GetFileSystemInfos()[cursor].GetType() == typeof(DirectoryInfo)){
                        dInfo = new DirectoryInfo(dInfo.GetFileSystemInfos()[cursor].FullName);
                        cursor = 0;
                        n = dInfo.GetFileSystemInfos().Length;
                    }
                    else{
                        StreamReader sr = new StreamReader(dInfo.GetFileSystemInfos()[cursor].FullName);
                        string s = sr.ReadToEnd();
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine(s);
                        Console.ReadKey();
                    }
                }
                if(kI.Key == ConsoleKey.Escape){
                    if(dInfo.Parent != null){
                        dInfo = dInfo.Parent;
                        cursor = 0;
                        n = dInfo.GetFileSystemInfos().Length;
                    }
                    else
                        break;
                }
                ShowDirectoryInfo(dInfo, cursor);
            }
        }

    }
}
