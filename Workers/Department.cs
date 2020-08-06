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

        #endregion

        #region Конструкторы

        public Department ( string Name, DateTime CrDate, int ECount, int PrCount)
        {
            this.Name = Name;
            this.CrDate = CrDate;
            this.ECount = ECount;
            this.PrCount = PrCount;

        }
        #endregion

    }
}
