﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Workers
{
    struct Worker
    {
        #region Конструкторы

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="Tabnum">Табельный номер</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Age">Возраст</param>
        /// <param name="Position">Должность</param>
        /// <param name="Department">Отдел</param>
        /// <param name="Salary">Оплата труда</param>
        /// <param name="Charge">Количество проектов</param>
        public Worker(int Tabnum, string FirstName, string LastName, int Age, string Position, uint Salary, Department Department, int Charge)
        {
            this.firstName = FirstName;
            this.lastName = LastName;
            this.position = Position;
            this.department = Department;
            this.salary = Salary;
            this.tabnum = Tabnum;
            this.charge = Charge;
            this.age = Age;
        }

        public Worker(int TubNum, Department Department)
        {
            this.tabnum = TubNum;
            Random rand = new Random();
           
            // Размещение женских имен в базе данных firstNames
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
            // Размещение мужских имен в базе данных firstNames
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

            int sex = rand.Next(0, 2);
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
            this.department = Department;
           
            this.charge = rand.Next(1, 10);
            this.age = rand.Next(20, 67);

            int numPos = PosCount(5);
            this.position = Department.Positions[numPos];
            this.salary = (uint)(100000 / (numPos + 1));
           
        }


        #endregion

        #region Методы
        public string PrintWorker()
        {
            return $"{this.tabnum,10}{this.firstName,12} {this.lastName,15} {this.age,10}  {this.position,15}  {this.salary,10} {this.department.Name,10} {this.charge,10}";
        }


        /// <summary>
        /// минимизирует вероятность использования значения 0
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        private int PosCount(int max)
        {
            Random r = new Random();
            int t = r.Next(0, max + 1);
            if (t == 0)
            {
                t += r.Next(0, max + 1);
            }
            return t;
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
        public Department Department { get { return this.department; } set { this.department = value; } }

        /// <summary>
        /// Оплата труда
        /// </summary>
        public uint Salary { get { return this.salary; } set { this.salary = value; } }

        /// <summary>
        /// Табельный номер
        /// </summary>
        public int Tabnum { get { return this.tabnum; } }

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
        private Department department;

        /// <summary>
        /// Поле "Оплата труда"
        /// </summary>
        private uint salary;

        /// <summary>
        /// Поле "Табельный номер"
        /// </summary>
        private int tabnum;

        /// <summary>
        /// Поле "Количество проектов"
        /// </summary>
        private int charge;

        #endregion

        #region Частные методы
        



        #endregion

    }
}
