﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Workers
{
    public class Worker
    {
        #region Конструкторы
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Worker()
        {
        }

        /// <summary>
        /// Создание сотрудника со всеми полями
        /// </summary>
        /// <param name="Tabnum">Табельный номер</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Position">Должность</param>
        /// <param name="Department">Отдел</param>
        /// <param name="Salary">Оплата труда</param>
        /// <param name="Charge">Количество проектов</param>
        public Worker(int Tabnum, string FirstName, string LastName, int Age, string Position, int Salary, Department Department, int Charge)
        {
            this.firstName = FirstName;
            this.lastName = LastName;
            this.position = Position;
            this.department = Department.Name;
            this.salary = Salary;
            this.tabnum = Tabnum;
            this.charge = Charge;
            this.age = Age;
        }

        /// <summary>
        /// Создание рандомного сотрудника в департаменте
        /// </summary>
        /// <param name="TubNum"></param>
        /// <param name="Department"></param>
        public Worker(int TabNum, Department Department)
        {
            this.tabnum = TabNum;
            Random rand = new Random();
           
            // Размещение женских имен в базе данных firstNamesF
            string[] firstNamesF = new string[] {
                "Агата",
                "Агния",
                "Аделина",
                "Аида",
                "Аксинья",
                "Александра",
                "Алена",
                "Алина",
                "Алиса",
                "Алия",
                "Алла",
                "Альбина",
                "Амелия",
                "Амина",
                "Анастасия",
                "Ангелина",
                "Анна",
                "Антонина",
                "Ариана",
                "Арина",
                "Валентина",
                "Валерия",
                "Варвара",
                "Василина",
                "Василиса",
                "Вера",
                "Вероника",
                "Виктория",
                "Виолетта",
                "Владислава",
                "Галина",
                "Дарина",
                "Дарья",
                "Диана",
                "Дина",
                "Ева",
                "Евангелина",
                "Евгения",
                "Екатерина",
                "Елена",
                "Елизавета",
                "Есения",
                "Жанна",
                "Зарина",
                "Злата",
                "Илона",
                "Инна",
                "Ирина",
                "Камилла",
                "Карина",
                "Каролина",
                "Кира",
                "Клавдия",
                "Кристина",
                "Ксения",
                "Лариса",
                "Лейла",
                "Лиана",
                "Лидия",
                "Лилия",
                "Лина",
                "Лия",
                "Любовь",
                "Людмила",
                "Майя",
                "Маргарита",
                "Марианна",
                "Марина",
                "Мария",
                "Мелания",
                "Мила",
                "Милана",
                "Милена",
                "Мирослава",
                "Надежда",
                "Наталья",
                "Нелли",
                "Ника",
                "Нина",
                "Оксана",
                "Олеся",
                "Ольга",
                "Полина",
                "Регина",
                "Сабина",
                "Светлана",
                "София",
                "Стефания",
                "Таисия",
                "Тамара",
                "Татьяна",
                "Ульяна",
                "Эвелина",
                "Элина",
                "Эльвира",
                "Эльмира",
                "Эмилия",
                "Юлия",
                "Яна",
                "Ярослава"

                };
            // Размещение мужских имен в базе данных firstNamesM
            string[] firstNamesM = new string[] {
                "Алан",
                "Александр",
                "Алексей",
                "Альберт",
                "Анатолий",
                "Андрей",
                "Антон",
                "Арсен",
                "Арсений",
                "Артем",
                "Артемий",
                "Артур",
                "Богдан",
                "Борис",
                "Вадим",
                "Валентин",
                "Валерий",
                "Василий",
                "Виктор",
                "Виталий",
                "Владимир",
                "Владислав",
                "Всеволод",
                "Вячеслав",
                "Геннадий",
                "Георгий",
                "Герман",
                "Глеб",
                "Гордей",
                "Григорий",
                "Давид",
                "Дамир",
                "Даниил",
                "Демид",
                "Демьян",
                "Денис",
                "Дмитрий",
                "Евгений",
                "Егор",
                "Елисей",
                "Захар",
                "Иван",
                "Игнат",
                "Игорь",
                "Илья",
                "Ильяс",
                "Камиль",
                "Карим",
                "Кирилл",
                "Клим",
                "Константин",
                "Лев",
                "Леонид",
                "Макар",
                "Максим",
                "Марат",
                "Марк",
                "Марсель",
                "Матвей",
                "Мирон",
                "Мирослав",
                "Михаил",
                "Назар",
                "Никита",
                "Николай",
                "Олег",
                "Павел",
                "Петр",
                "Платон",
                "Прохор",
                "Рамиль",
                "Ратмир",
                "Ринат",
                "Роберт",
                "Родион",
                "Роман",
                "Ростислав",
                "Руслан",
                "Рустам",
                "Савва",
                "Савелий",
                "Святослав",
                "Семен",
                "Сергей",
                "Станислав",
                "Степан",
                "Тамерлан",
                "Тимофей",
                "Тимур",
                "Тихон",
                "Федор",
                "Филипп",
                "Шамиль",
                "Эдуард",
                "Эльдар",
                "Эмиль",
                "Эрик",
                "Юрий",
                "Ян",
                "Ярослав"

                };
            // Размещение фамилий в базе данных lastNames
            string[] lastNames = new string[]
            {
                "Смирнов",
                "Иванов",
                "Кузнецов",
                "Соколов",
                "Попов",
                "Лебедев",
                "Козлов",
                "Новиков",
                "Морозов",
                "Петров",
                "Волков",
                "Соловьёв",
                "Васильев",
                "Зайцев",
                "Павлов",
                "Семёнов",
                "Голубев",
                "Виноградов",
                "Богданов",
                "Воробьёв",
                "Фёдоров",
                "Михайлов",
                "Беляев",
                "Тарасов",
                "Белов",
                "Комаров",
                "Орлов",
                "Киселёв",
                "Макаров",
                "Андреев",
                "Ковалёв",
                "Ильин",
                "Гусев",
                "Титов",
                "Кузьмин",
                "Кудрявцев",
                "Баранов",
                "Куликов",
                "Алексеев",
                "Степанов",
                "Яковлев",
                "Сорокин",
                "Сергеев",
                "Романов",
                "Захаров",
                "Борисов",
                "Королёв",
                "Герасимов",
                "Пономарёв",
                "Григорьев",
                "Лазарев",
                "Медведев",
                "Ершов",
                "Никитин",
                "Соболев",
                "Рябов",
                "Поляков",
                "Цветков",
                "Данилов",
                "Жуков",
                "Фролов",
                "Журавлёв",
                "Николаев",
                "Крылов",
                "Максимов",
                "Сидоров",
                "Осипов",
                "Белоусов",
                "Федотов",
                "Дорофеев",
                "Егоров",
                "Матвеев",
                "Бобров",
                "Дмитриев",
                "Калинин",
                "Анисимов",
                "Петухов",
                "Антонов",
                "Тимофеев",
                "Никифоров",
                "Веселов",
                "Филиппов",
                "Марков",
                "Большаков",
                "Суханов",
                "Миронов",
                "Ширяев",
                "Александров",
                "Коновалов",
                "Шестаков",
                "Казаков",
                "Ефимов",
                "Денисов",
                "Громов",
                "Фомин",
                "Давыдов",
                "Мельников",
                "Щербаков",
                "Блинов",
                "Колесников",
                "Карпов",
                "Афанасьев",
                "Власов",
                "Маслов",
                "Исаков",
                "Тихонов",
                "Аксёнов",
                "Гаврилов",
                "Родионов",
                "Котов",
                "Горбунов",
                "Кудряшов",
                "Быков",
                "Зуев",
                "Третьяков",
                "Савельев",
                "Панов",
                "Рыбаков",
                "Суворов",
                "Абрамов",
                "Воронов",
                "Мухин",
                "Архипов",
                "Трофимов",
                "Мартынов",
                "Емельянов",
                "Горшков",
                "Чернов",
                "Овчинников",
                "Селезнёв",
                "Панфилов",
                "Копылов",
                "Михеев",
                "Галкин",
                "Назаров",
                "Лобанов",
                "Лукин",
                "Беляков",
                "Потапов",
                "Некрасов",
                "Хохлов",
                "Жданов",
                "Наумов",
                "Шилов",
                "Воронцов",
                "Ермаков",
                "Дроздов",
                "Игнатьев",
                "Савин",
                "Логинов",
                "Сафонов",
                "Капустин",
                "Кириллов",
                "Моисеев",
                "Елисеев",
                "Кошелев",
                "Костин",
                "Горбачёв",
                "Орехов",
                "Ефремов",
                "Исаев",
                "Евдокимов",
                "Калашников",
                "Кабанов",
                "Носков",
                "Юдин",
                "Кулагин",
                "Лапин",
                "Прохоров",
                "Нестеров",
                "Харитонов",
                "Агафонов",
                "Муравьёв",
                "Ларионов",
                "Федосеев",
                "Зимин",
                "Пахомов",
                "Шубин",
                "Игнатов",
                "Филатов",
                "Крюков",
                "Рогов",
                "Кулаков",
                "Терентьев",
                "Молчанов",
                "Владимиров",
                "Артемьев",
                "Гурьев",
                "Зиновьев",
                "Гришин",
                "Кононов",
                "Дементьев",
                "Ситников",
                "Симонов",
                "Мишин",
                "Фадеев",
                "Комиссаров",
                "Мамонтов",
                "Носов",
                "Гуляев",
                "Шаров",
                "Устинов",
                "Вишняков",
                "Евсеев",
                "Лаврентьев",
                "Брагин",
                "Константинов",
                "Корнилов",
                "Авдеев",
                "Зыков",
                "Бирюков",
                "Шарапов",
                "Никонов",
                "Щукин",
                "Дьячков",
                "Одинцов",
                "Сазонов",
                "Якушев",
                "Красильников",
                "Гордеев",
                "Самойлов",
                "Князев",
                "Беспалов",
                "Уваров",
                "Шашков",
                "Бобылёв",
                "Доронин",
                "Белозёров",
                "Рожков",
                "Самсонов",
                "Мясников",
                "Лихачёв",
                "Буров",
                "Сысоев",
                "Фомичёв",
                "Русаков",
                "Стрелков",
                "Гущин",
                "Тетерин",
                "Колобов",
                "Субботин",
                "Фокин",
                "Блохин",
                "Селиверстов",
                "Пестов",
                "Кондратьев",
                "Силин",
                "Меркушев",
                "Лыткин",
                "Туров"

            };

            int sex = rand.Next(0, 2); // 0 - male, 1 - female ¯\_(ツ)_/¯
            if (sex==0)
            {
                this.firstName = firstNamesM[rand.Next(0, 100)];
                this.lastName = lastNames[rand.Next(0, 250)];
            }
            else
            {
                this.firstName = firstNamesF[rand.Next(0, 100)];
                this.lastName = lastNames[rand.Next(0, 250)]+'а';
            }

            this.salary = 0;
            this.position = Department.Positions[0];
            this.department = Department.Name;
           
            this.charge = rand.Next(1, 4);
            this.age = rand.Next(20, 67);
            
            int hat = HatFitsSenka(this.Age, Department.Positions.Count);
            
            this.position = Department.Positions[hat];
            this.salary = (100 / (hat + 1)) * 1000 + (6 / rand.Next(1, 5) * 100);
            Department.ECount++;
            Department.PrCount += this.charge;
        }


        #endregion

        #region Методы
        /// <summary>
        /// печать информации о сотруднике
        /// </summary>
        /// <returns></returns>
        public void PrintWorker()
        {
            Console.WriteLine($"{this.tabnum,10}{this.firstName,12}{this.lastName,15}{this.age,10}{this.position,18}{this.salary,10 : ### ##0}{this.department,10}{this.charge,10}");
        }

        /// <summary>
        /// Сравнивает работников по табельному номеру
        /// </summary>
        /// <param name="x">первый работник</param>
        /// <param name="y">второй работник</param>
        /// <returns>-1 если у первого номер меньше, 1 если у первого номер больше, 0 если одиноковые</returns>
        public static int CompareByNum(Worker x, Worker y)
        {
            return x.Tabnum.CompareTo(y.Tabnum);
        }

        /// <summary>
        /// Сравнивает работников по возрасту
        /// </summary>
        /// <param name="x">первый работник</param>
        /// <param name="y">второй работник</param>
        /// <returns>-1 если первый младше, 1 если первый старше, 0 если одного возраста</returns>
        public static int CompareByAge(Worker x, Worker y)
        {
          
            return x.Age.CompareTo(y.Age);
        }
        /// <summary>
        /// Сравнивает работников по возрасту и зарплате 
        /// </summary>
        /// <param name="x">превый работник</param>
        /// <param name="y">второй работник</param>
        /// <returns>-1 если первый младше или того же возраста но меньше получает,
        /// 1 если первый старше или того же возраста но больше получает,
        /// 0 если все эти параметры совпадают</returns>
        public static int CompareByAgeSalary(Worker x, Worker y)
        {
            int ret = CompareByAge(x, y);
            if (ret != 0) return ret;
            return x.Salary.CompareTo(y.Salary);
        }

        /// <summary>
        /// Сравнивает работников по возрасту и зарплате в пределах департамента
        /// </summary>
        /// <param name="x">превый работник</param>
        /// <param name="y">второй работник</param>
        /// <returns>Если работники в одном департаменте: 
        /// -1 если первый младше или того же возраста но меньше получает,
        /// 1 если первый старше или того же возраста но больше получает,
        /// 0 если все эти параметры совпадают.
        /// Если работники из разных департаментов: -1 если у первого работника номер
        /// департамента меньше, 1 если у первого номер департамента больше (названия департаментов
        /// сравниваются как стандартные строки).</returns>
        public static int CompareByDeptAgeSalary(Worker x, Worker y)
        {
            int ret = String.Compare(x.Department, y.Department);
            if (ret != 0) return ret;
            return CompareByAgeSalary(x, y);
        }

        #endregion

        #region Частные методы

        /// <summary>
        /// Распределяет должности в отделе
        /// </summary>
        /// <param name="age">Возраст</param>
        /// <param name="max">Количество должностей в отделе</param>
        /// <returns></returns>
        private int HatFitsSenka(int age, int max)
        {
            Random r = new Random();
            int hat;

            if (age < 28)
                return r.Next(max/2 + 1, max);

            else if (age >= 55)
                return r.Next(1, max/2 + 1);

            else hat = r.Next(0, max/2 + 1);

            if (hat == 0)
            {
                hat += r.Next(0, max/3 + 1);
            }

            return hat;
        }


        #endregion

        #region Свойства

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get { return this.lastName; } set { this.lastName = value; } }

        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get { return this.position; } set { this.position = value; } }

        /// <summary>
        /// Отдел
        /// </summary>
        public string Department { get { return this.department; } set { this.department = value; } }

        /// <summary>
        /// Оплата труда
        /// </summary>
        public int Salary { get { return this.salary; } set { this.salary = value; } }

        /// <summary>
        /// Табельный номер
        /// </summary>
        public int Tabnum { get { return this.tabnum; } set { this.tabnum = value; } }

        /// <summary>
        /// Количество проектов
        /// </summary>
       public int Charge { get { return this.charge; } set { this.charge = value; } }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get { return this.age; } set { this.age = value; } }

        #endregion

        #region Поля

        /// <summary>
        /// Поле "Имя"
        /// </summary>
        private string firstName;

        /// <summary>
        /// Поле "Фамилия"
        /// </summary>
        private string lastName;

        /// <summary>
        /// Поле "Возраст"
        /// </summary>
        private int age;

        /// <summary>
        /// Поле "Должность"
        /// </summary>
        private string position;

        /// <summary>
        /// Поле "Отдел"
        /// </summary>
        private string department;

        /// <summary>
        /// Поле "Оплата труда"
        /// </summary>
        private int salary;

        /// <summary>
        /// Поле "Табельный номер"
        /// </summary>
        private int tabnum;

        /// <summary>
        /// Поле "Количество проектов"
        /// </summary>
        private int charge;

        #endregion

       

    }
}
