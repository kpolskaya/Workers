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
            return num;
        }

        /// <summary>
        /// Выводит сообщение на консоль и ждет ввода. Если нажата клавиша "q" - возвращает false,
        /// в другом случае - true.
        /// </summary>
        /// <returns></returns>
        static bool Repeat()
        {
            Console.Write($"Для выхода нажмите q, для продолжения редактирования - любую другую клавишу: /n");
            return !(Console.ReadKey(true).Key == ConsoleKey.Q);
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
            text = Console.ReadLine();
            return text;
        }

       

        /// <summary>
        /// Метод десериализации Worker
        /// </summary>
        /// <param name="СoncreteWorker">Экземпляр для сериализации</param>
        /// <param name="Path">Путь к файлу</param>
        static List<Worker> DeserializeWorkerList(string Path)
        {
            List<Worker> tempWorkerCol = new List<Worker>();
            // Создаем сериализатор на основе указанного типа 
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Worker>));

            // Создаем поток для чтения данных
            Stream fStream = new FileStream(Path, FileMode.Open, FileAccess.Read);

            // Запускаем процесс десериализации
            tempWorkerCol = xmlSerializer.Deserialize(fStream) as List<Worker>;

            // Закрываем поток
            fStream.Close();

            // Возвращаем результат
            return tempWorkerCol;
        }

        static void Main(string[] args)
        {
            Company company = new Company(6, 50);
            //Random rand = new Random();

            //List<Department> organization = new List<Department>();

            //for (int i = 0; i < 6; i++)
            //{
            //    organization.Add(new Department(i));

            //}

            //List<Worker> workers = new List<Worker>();

            //for (int i = 1; i <= 50; i++)
            //{
            //   workers.Add(new Worker(i, organization[rand.Next(0, 6)]));
            //}


            company.PrintAll();
            Console.ReadKey();
            company.SortParams();
            Console.ReadKey();

            company.SerializeWorkerList("_listWorker.xml");
          
            /////////////////////////

            //SerializeWorkerList(workers, "_listWorker.xml");

            //List<Worker> workers1 = new List<Worker>();

            //workers1 = DeserializeWorkerList("_listWorker.xml");

            //foreach (var item in workers1)
            //{
            //    Console.WriteLine(item.PrintWorker());
            //}
            //Console.ReadKey();

            ////////////////////////
            //string json = JsonConvert.SerializeObject(workers);
            //File.WriteAllText("_listWorker.json", json);

            //json = File.ReadAllText("_listWorker.json");

            //List<Worker> workers2 = new List<Worker>();

            //workers2 = JsonConvert.DeserializeObject<List<Worker>>(json);
            //foreach (var item in workers2)
            //{
            //    Console.WriteLine(item.PrintWorker());
            //}
            //Console.ReadKey();

            /////////////////////////

            //Console.WriteLine($"{"Отдел",15}{"Дата создания",25} {"Сотрудников",15} {"Тем",10}");

            //foreach (var item in Company.departments)
            //{
            //    Console.WriteLine(item.PrintDepartment());
            //}
            //Console.ReadKey();


            //Сортировка последовательно по отдел-возраст-загрузка

            //var sortedWorkers = from worker in workers
            //                    orderby worker.Department, worker.Age, worker.Charge
            //                    select worker;

            //List<Worker> sortedWorkers = workers.OrderBy(x => x.Department)
            //                        .ThenBy(x => x.Age)
            //                        .ThenBy(x=> x.Charge)
            //                        .ToList();




            //Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");

            //foreach (Worker w in sortedWorkers)
            //{
            //    Console.WriteLine(w.PrintWorker());
            //}
            //Console.ReadKey();



            //Console.WriteLine("Создание нового сотрудника\n");
            //int indx = workers.Count + 1;
            //workers.Add(new Worker(indx, organization[rand.Next(0, 6)]));
            //Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");

            //  Console.WriteLine(workers[indx].PrintWorker());

            //foreach (var item in workers)
            //{
            //    Console.WriteLine(item.PrintWorker());
            //}
            //Console.ReadKey();


            //int d = GetNum("Для удаления сотрудника введите его табельный номер", 1, workers.Count);

            //Worker found = workers.Find(item => item.Tabnum == d);

            //Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");
            //Console.WriteLine(found.PrintWorker());
            //Console.WriteLine();
            //int idx = workers.IndexOf(found);
            //workers.RemoveAt(idx);

            //foreach (var item in workers)
            //{
            //    Console.WriteLine(item.PrintWorker());
            //}
            //Console.ReadKey();


            //do
            //{
            //    Console.WriteLine("РЕДАКТИРОВАНИЕ ДАННЫХ СОТРУДНИКА\n");
            //    Console.WriteLine("для изменения должности и отдела вам необходимо уволить сотрудника и принять его на работу заново \n");
            //    int r = GetNum("Введите табельный номер для редактирования данных сотрудника", 1, workers.Count);
            //    Worker foundr = workers.Find(item => item.Tabnum == r);

            //    Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");
            //    Console.WriteLine(foundr.PrintWorker());

            //    Console.WriteLine("\nДля изменения имени нажмите 1");
            //    Console.WriteLine("Для изменения фамилии нажмите 2");
            //    Console.WriteLine("Для изменения возраста нажмите 3");
            //    Console.WriteLine("Для изменения зарплаты нажмите 4");
            //    Console.WriteLine($"Для изменения количества проектов нажмите 5\n");

            //    //var ans = Console.ReadKey(true).KeyChar;

            //    int ans = GetNum("Выберите нужное действие", 1, 6);
            //    Console.WriteLine();

            //    switch (ans)
            //    {
            //        case 1:

            //            foundr.FirstName = GetText("Введите новое имя");
            //            foundr.LastName = GetText("Введите новую фамилию");
            //            foundr.Age = GetNum("Введите новый возраст", 1, 120);
            //            foundr.Salary = (int)GetNum("Введите новую зарплату", 1, 100000);
            //            foundr.Charge = GetNum("Введите новую загрузку", 0, 100);
            //            Console.WriteLine();
            //            Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");
            //            Console.WriteLine(foundr.PrintWorker());
            //            break;

            //        case 2:

            //            foundr.LastName = GetText("Введите новую фамилию");
            //            Console.WriteLine();
            //            Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");
            //            Console.WriteLine(foundr.PrintWorker());
            //            break;

            //        case 3:

            //            foundr.Age = GetNum("Введите новый возраст",1,120);
            //            Console.WriteLine();
            //            Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");
            //            Console.WriteLine(foundr.PrintWorker());
            //            break;

            //        case 4:

            //            foundr.Salary = (int)GetNum("Введите новую зарплату", 1, 100000);
            //            Console.WriteLine();
            //            Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");
            //            Console.WriteLine(foundr.PrintWorker());
            //            break;

            //        case 5:


            //            Console.WriteLine();
            //            Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");
            //            Console.WriteLine(foundr.PrintWorker());
            //            break;

            //            default:                                                                                          
            //                Console.WriteLine("Выбрано неизвестное действие\n");
            //                break;
            //    }

            //}
            //while (Repeat());

            //Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");

            //foreach (var item in workers)
            //{
            //    Console.WriteLine(item.PrintWorker());
            //}
            Console.ReadKey();

          
        }

    }

}
