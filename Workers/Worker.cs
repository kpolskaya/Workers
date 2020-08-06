using System;
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
        /// <param name="Position">Должность</param>
        /// <param name="Department">Отдел</param>
        /// <param name="Salary">Оплата труда</param>
        /// <param name="Charge">Количество проектов</param>
        public Worker(int Tabnum, string FirstName, string LastName, string Position, uint Salary, string Department, int Charge)
        {
            this.firstName = FirstName;
            this.lastName = LastName;
            this.position = Position;
            this.department = Department;
            this.salary = Salary;
            this.tabnum = Tabnum;
            this.charge = Charge;

        }

        #endregion

        #region Методы

        public string Print()
        {
            return $"{this.firstName,15} {this.lastName,15} {this.department,15} {this.position,15} {this.salary,10}";
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
        public uint Salary { get { return this.salary; } set { this.salary = value; } }

        /// <summary>
        /// Табельный номер
        /// </summary>
        public int Tabnum { get { return this.tabnum; } set { this.tabnum = value; } }

        /// <summary>
        /// Количество проектов
        /// </summary>
       public int Charge { get { return this.charge; } set { this.charge = value; } }

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

    }
}
