
using console_explorer;
using System;
using System.Collections.Generic;
using System.IO;

internal class Menu
{

    private int selectedIndex = 0;
    private List<Fso> items;

    
    public string text()
    {
        while(true)
        {
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("Мой компьютер *_*");
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("========================================================================================================================");
        }
    }

    public void PrintMenu()
    {
        Console.Clear();

        Console.WriteLine();
        items = Explorer.Dir();
        for (int i = 0; i < items.Count; i++)
        {
            if (i == selectedIndex)
            {
                Console.Write("->");
            }
            Console.WriteLine("\t" + items[i].Name);
        }
        foreach (Fso item in items)
        {
            Console.WriteLine());
           
        }


    }

    public (string, bool) MainLoop()
    {
        while (true)
        {
            PrintMenu();
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("Нажмите F1,чтобы создать папку ");
            Console.SetCursorPosition(90, 4);
            Console.WriteLine("Нажмите F2,чтобы создать файл ");
            Console.SetCursorPosition(90, 5);
            Console.WriteLine("Нажмите F3, чтобы удалить");
            //Меню должно быть стрелочным
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.DownArrow)
            {
                Down();
            }
            if (key.Key == ConsoleKey.UpArrow)
            {
                Up();
            }
            if (key.Key == ConsoleKey.Escape)
            {

                items = Explorer.Up();

            }
            if (key.Key == ConsoleKey.Enter)
            {

                //items = Explorer.GetObjects(items[selectedIndex].Name);
                var selected = items[selectedIndex];
                if (selected.FsoType == FsoType.Folder || selected.FsoType == FsoType.Drive)
                {
                    items = Explorer.Cd(selected.Name);
                }
                if (selected.FsoType == FsoType.File)
                {
                    Explorer.OpenWithDefaultProgram(selected.FullFilename);
                }


            } //при выборе файла через проводник, и при нажатии ф3 он будет удаляться (также и с папкой)
            if (key.Key == ConsoleKey.F3)
            {
                var selected = items[selectedIndex];
                if (selected.FsoType == FsoType.File)
                {
                    File.Delete(selected.FullFilename);

                }
                if (selected.FsoType == FsoType.Folder)
                {
                    Directory.Delete(selected.FullFilename, false);
                }
                items = Explorer.Dir();
                if (key.Key == ConsoleKey.F2)
                {
                    Console.SetCursorPosition(90, 7);
                    Console.WriteLine("Введите имя папки,\n" +
                        "которая будет создана");
                    string imya = Console.ReadLine();
                    Directory.CreateDirectory($"{selected.FullFilename}\\{imya}");
                }

                if (key.Key == ConsoleKey.F1)
                {
                    Console.SetCursorPosition(90, 7);
                    Console.WriteLine("Введите имя файла,\n" +
                        "который будет создан");
                    var file = Console.ReadLine();
                    FileStream file = File.Create($"{selected.FullFilename}");
                }
            }  

        }
    }
   
   

    private void Down()
    {
        selectedIndex = (selectedIndex + 1) % items.Count;
    }
    private void Up()
    {
        if (selectedIndex == 0)
        {
            selectedIndex = items.Count;
        }
        selectedIndex--;
    }

}