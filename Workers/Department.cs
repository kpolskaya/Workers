using System;
using System.Collections.Generic;
using System.Text;

namespace Workers
{
    public class Department
    {

       
        #region Свойства
        /// <summary>
        /// Название отдела
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата создания отдела
        /// </summary>
        public DateTime CrDate { get; set; }

        /// <summary>
        /// Количество сотрудников
        /// </summary>
        public int ECount { get; set; }


        /// <summary>
        /// Количество проектов в департаменте
        /// </summary>
        public int PrCount { get; set; }

        /// <summary>
        /// Список должностей
        /// </summary>
        public string[] Positions { get; set; }


        #endregion

        #region Конструкторы

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Department()
        {

        }

        /// <summary>
        /// Констркутор со всеми полями
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="CrDate"></param>
        /// <param name="ECount"></param>
        /// <param name="PrCount"></param>
        public Department ( string Name, DateTime CrDate, int ECount, int PrCount)
        {
            this.Name = Name;
            this.CrDate = CrDate;
            this.ECount = ECount;
            this.PrCount = PrCount;
            this.Positions = new string[] { "Начальник", "Зам.начальника", "Помощник", "Ведущий инженер", "Инженер", "Стажер" };

        }

        /// <summary>
        /// Конструктор случайного отдела с номером
        /// </summary>
        /// <param name="num">номер отдела</param>
        public Department(int num /*, int q*/)       //q - максимальное количество сотрудников в отделе, для начала 100000
        {
            Random rand;
            DateTime d = Convert.ToDateTime("01.01.1990"); // дата создания организации 
            this.Name = $"Отдел {num}";
            rand = new Random();
            this.CrDate = d.AddDays((num - 1) * rand.Next(15, 31));
            this.ECount = 0;
            this.PrCount = ECount/3;
            this.Positions = new string[] { "Начальник", "Зам. начальника", "Ведущий инженер", "Инженер", "Помощник", "Стажер" };

        }
        #endregion

        #region Методы
        public void PrintDepartment()
        {
            Console.WriteLine($"{this.Name,15}{this.CrDate,25:dd.MM.yyyy} {this.ECount,15} {this.PrCount,10}");
        }
        #endregion

    }
}
