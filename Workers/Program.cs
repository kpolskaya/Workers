using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Converters;
using System.IO.Pipes;

namespace Workers
{
    class Program
    {
        /// Создать прототип информационной системы, в которой есть возможност работать со структурой организации
        /// В структуре присутствуют департаменты и сотрудники
        /// Каждый департамент может содержать не более 1_000_000 сотрудников.
        /// У каждого департамента есть поля: наименование, дата создания,
        /// количество сотрудников числящихся в нём 
        /// (можно добавить свои пожелания)
        /// 
        /// У каждого сотрудника есть поля: Фамилия, Имя, Возраст, департамент в котором он числится, 
        /// уникальный номер, размер оплаты труда, количество закрепленным за ним.
        ///
        /// В данной информаиционной системе должна быть возможность 
        /// - импорта и экспорта всей информации в xml и json
        /// Добавление, удаление, редактирование сотрудников и департаментов
        /// 
        /// * Реализовать возможность упорядочивания сотрудников в рамках одно департамента 
        /// по нескольким полям, например возрасту и оплате труда
        /// 
        ///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
        ///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
        ///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
        ///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
        ///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
        ///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
        ///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
        ///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
        ///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
        ///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
        /// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
        /// 
        /// 
        /// Упорядочивание по одному полю возраст
        /// 
        ///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
        ///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
        /// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
        ///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
        ///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
        ///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
        ///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
        ///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
        ///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
        ///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
        ///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
        /// 
        ///
        /// Упорядочивание по полям возраст и оплате труда
        /// 
        ///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
        /// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
        ///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
        ///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
        ///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
        ///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
        ///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
        ///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
        ///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
        ///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
        ///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
        /// 
        /// 
        /// Упорядочивание по полям возраст и оплате труда в рамках одного департамента
        /// 
        ///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
        ///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
        ///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
        ///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
        ///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
        ///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
        ///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
        ///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
        /// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
        ///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
        ///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
        /// 

        /// <summary>
        /// Ввод с консоли целого числа с проверкой диапазона значений. Возвращает целое число.
        /// </summary>
        /// <param name="message"> Приглашение к вводу </param>
        /// <param name="min">Минимальное допустимое значение </param>
        /// <param name="max">Максимальное допустимое значение </param>
        /// <returns></returns>
        static int GetNum(string message, int min, int max)
        {
            int num;
            Console.Write($"{message} (от {min} до {max}): ");
            do
            {
                if (int.TryParse(Console.ReadLine(), out num) && num >= min && num <= max)
                    break;
                else
                    Console.Write("Вы ввели недопустимое значение. Попробуйте еще: ");

            } while (true);
            Console.WriteLine();
            return num;
        }

        /// <summary>
        /// Выводит сообщение на консоль и ждет ввода. 
        /// </summary>
        /// <returns>Если нажата клавиша "q" - возвращает false,
        /// в другом случае - true</returns>
        static bool Repeat()
        {
            Console.Write($"Для выхода нажмите Q, для продолжения работы - любую другую клавишу:");
            bool ret = !(Console.ReadKey(true).Key == ConsoleKey.Q);
            Console.WriteLine();
            return ret;
        }
        /// <summary>
        /// Ждет нажатия на клавишу "Y" или "N"
        /// </summary>
        /// <returns>true если нажата "Y" и false если нажата "N"</returns>
        static bool YesNo()
        {
            ConsoleKey ans;
            Console.Write($"Подтвердите действие (y/n):");
            do
                ans = Console.ReadKey(true).Key;
            while (ans != ConsoleKey.Y && ans != ConsoleKey.N);
            Console.WriteLine();
            return ans == ConsoleKey.Y;
        }

        /// <summary>
        /// Ввод с консоли целого числа с проверкой диапазона значений. Возвращает целое число.
        /// </summary>
        /// <param name="message"> Приглашение к вводу </param>
        /// <param name="min">Минимальное допустимое значение </param>
        /// <param name="max">Максимальное допустимое значение </param>
        /// <returns></returns>
        static string GetText(string message)
        {
            string text;
            Console.Write($"{message}: ");
            do
                text = Console.ReadLine();
            while (text == String.Empty);
            return text;
        }



        static void Main(string[] args)
        {

            Company company = new Company(3, 20);

            
            do
            {
                Console.WriteLine("\nОперации с информационной системой:\n");
               
                Console.WriteLine("Для редактирования данных сотрудника выберите 1");
                Console.WriteLine("Для увольнения сотрудника выберите 2");
                Console.WriteLine("Для найма сотрудника с улицы выберите 3");
                Console.WriteLine("Для записи данных в файл .xml выберите 4");
                Console.WriteLine("Для записи данных в файл .json выберите 5");
                Console.WriteLine("Для чтения данных из файла .xml выберите 6");
                Console.WriteLine("Для чтения данных из файла .json выберите 7");
                Console.WriteLine("Для сортировки списка сотрудников выберите 8");
                Console.WriteLine("Для печати списка сотрудников выберите 9");
                Console.WriteLine("Для печати списка отделов выберите 10\n");

                int x;
                int ans = GetNum("Выберите нужное действие", 1, 10);
               
                switch (ans)
                {
                    case 1:
                        x = GetNum("Введите табельный номер для редактирования данных сотрудника", 1, company.TabNums.Max);
                        company.PrintPerson(x);
                        if (YesNo())
                        {
                            string firstName = GetText("Введите новое имя");
                            string lastName = GetText("Введите новую фамилию");
                            int salary = GetNum("Введите новую зарплату", 1_000, 999_999);
                            int charge = GetNum("Введите новую загрузку", 1, 5);
                            if (company.EditWorker(x, firstName, lastName, salary, charge))
                                company.PrintPerson(x);
                            else
                                Console.WriteLine("Операция не может быть выполнена!");
                        }
                        else
                            Console.WriteLine("Действие отменено!");

                        break;

                    case 2:
                        x = GetNum("Введите табельный номер увольняемого сотрудника", 1, company.TabNums.Max);
                        company.PrintPerson(x);
                        if (YesNo())
                        {
                            if (company.Fire(x))
                                Console.WriteLine("Теперь этот сотрудник у нас никогда не работал.");
                            else
                                Console.WriteLine("Операция не может быть выполнена!");
                        }
                           

                        else
                            Console.WriteLine("Действие отменено!");
                        break;

                    case 3:
                        if (company.HireRandom())
                        {
                            Console.WriteLine("Нанят новый сотрудник:");
                            company.PrintPerson(company.TabNums.Max);
                        }
                        else Console.WriteLine("Невозможно никого нанять - все отделы укомплектованы.");
                        break;

                    case 4:
                        company.SerializeCompanyXML();
                        Console.WriteLine("Данные сохранены в файле _company.xml");
                        break;

                    case 5:
                        company.SerializeCompanyJSON();
                        Console.WriteLine("Данные сохранены в файле _company.json");
                        break;

                    case 6:
                        company = new Company(@"_company.xml");
                        company.PrintPanel();
                        break;

                    case 7:
                        company = new Company(@"_company.json");
                        company.PrintPanel();
                        break;

                    case 8:
                        Console.WriteLine("\nСортировка:\n");

                        Console.WriteLine("Для сортировки сотрудников по возрасту выберите 1");
                        Console.WriteLine("Для сортировки сотрудников по возрасту и заработной плате выберите 2");
                        Console.WriteLine("Для сортировки сотрудников по возрасту и заработной плате внутри отдела выберите 3");
                        Console.WriteLine("Для сортировке по табельным номерам выберите 4");

                        ans = GetNum("Выберите нужное действие", 1, 4);
                        
                        switch (ans)
                        {
                           case 1:
                                company.Sort(Worker.CompareByAge);
                                company.PrintPanel();
                                break;

                            case 2:
                                company.Sort(Worker.CompareByAgeSalary);
                                company.PrintPanel();
                                break;

                            case 3:
                                company.Sort(Worker.CompareByDeptAgeSalary);
                                company.PrintPanel();
                                
                                break;
                            case 4:
                                company.Sort(Worker.CompareByNum);
                                company.PrintPanel();
                                break;
                        }

                        break;

                    case 9:
                        company.PrintPanel();
                        break;

                    case 10:
                        company.PrintDepartments();
                        break;

                    default:
                        Console.WriteLine("Выбрано неизвестное действие\n");
                        break;
                }

            }
            while (Repeat());

           
            Console.ReadKey();

          
        }

    }

}
