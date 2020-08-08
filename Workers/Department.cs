using System;
using System.Collections.Generic;
using System.Text;

namespace Workers
{
    struct Department
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

        public Department ( string Name, DateTime CrDate, int ECount, int PrCount)
        {
            this.Name = Name;
            this.CrDate = CrDate;
            this.ECount = ECount;
            this.PrCount = PrCount;
            this.Positions = new string[] { "Начальник", "Зам.начальника", "Помощник", "Ведущий инженер", "Инженер", "Стажер" };

        }

        public Department(int num/*, int q*/)       //q - максимальное количество сотрудников в отделе, для начала 100000
        {
            Random rand;
            DateTime d = Convert.ToDateTime("01.01.1990");
            this.Name = $"Отдел  {num}";
            rand = new Random();
            this.CrDate = d.AddDays (rand.Next(0, 500));
            this.ECount = 0;
            this.PrCount = ECount/3;
            this.Positions = new string[] { "Начальник", "Зам.начальника", "Помощник", "Ведущий инженер", "Инженер", "Стажер" };

        }

        public string PrintDepartment()
        {
            return $"{this.Name,15}{this.CrDate,25} {this.ECount,15} {this.PrCount,10}";
        }
        #endregion

    }
}
