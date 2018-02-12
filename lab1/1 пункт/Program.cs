using System;
using System.Collections.Generic;
namespace с_
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> arr = new List<int> ();      // Создаю новый лист
            for(int i=0;i<args.Length;i++){
                arr.Add(int.Parse(args[i]));       // Добавляю в лист все элементы из массива args
            }

            for(int i=0;i<arr.Count;i++){
                int n = arr[i];                    
                bool ok = true;
                for(int j=2;j*j<=n;j++){           // Проверяю каждый элемент из листа arr на простоту
                    if(n%j==0){
                        ok = false;
                        break;
                    }
                }
                if(ok || n!=1){
                    Console.WriteLine(n);         // Вывожу на терминал
                }
            }
        }
    }
}
