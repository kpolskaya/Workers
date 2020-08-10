using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Workers
{   
    /// <summary>
    /// Структура организации с работниками и отделами
    /// </summary>
    public struct Company
    {
        #region Конструкторы

        /// <summary>
        /// Создает компанию в демо-режиме с рандомными сотрудниками
        /// </summary>
        /// <param name="NumOfDepts">Количество создаваемых отделов</param>
        /// <param name="NumOfJacks">Количество создаваемых работников</param>
        public Company(int NumOfDepts, int NumOfJacks)
        {
            this.departments = new List<Department>();
            this.workers = new List<Worker>();
            this.deptByName = new Dictionary<string, Department>();
            this.tabNums = new SortedSet<int>();

            for (int i = 0; i < NumOfDepts; i++)
            {
                this.departments.Add(new Department(i + 1));
                this.deptByName.Add(this.departments[i].Name, this.departments[i]);
            }

            Random r = new Random();
            for (int i = 0; i < NumOfJacks; i++)
            {
                this.tabNums.Add(i + 1);
                this.workers.Add(new Worker(i + 1, this.departments[r.Next(0, NumOfDepts)]));
            }
        }


        #endregion

        #region Методы

        /// <summary>
        /// Нанимает случайно сгенерированного работника
        /// </summary>
        public void HireRandom()
        {
            Random r = new Random();
            int nextNum = this.tabNums.Max + 1;
            this.tabNums.Add(nextNum);
            this.workers.Add(new Worker(nextNum, this.departments[r.Next(0, this.departments.Count)]));
        }

        /// <summary>
        /// Увольняет работника по табельному номеру
        /// </summary>
        /// <param name="num">Табельный номер</param>
        public void Fire(int num)
        {
            var victim = workers.Find(item => item.Tabnum == num);
            deptByName[victim.Department].ECount--;                         // минус человек
            deptByName[victim.Department].PrCount -= victim.Charge;         // минус проекты
            this.workers.Remove(victim);
        }

        /// <summary>
        /// Выводит на консоль данные работника по индексу в списке
        /// </summary>
        /// <param name="i">Индекс записи в списке</param>
        public void PrintPerson(int i)
        {
            this.workers[i].PrintWorker();
        }

        /// <summary>
        /// Выводит на консоль информацию об отделе по индексу в списке
        /// </summary>
        /// <param name="i">Индекс записи в списке</param>
        public void PrintDeptInfo(int i)
        {
            this.departments[i].PrintDepartment();
        }






        #endregion



        #region Поля

        /// <summary>
        /// Список отделов
        /// </summary>
        List<Department> departments;

        /// <summary>
        /// Список работников
        /// </summary>
        List<Worker> workers;

        /// <summary>
        /// Словарь названий отделов
        /// </summary>
        Dictionary<string, Department> deptByName;

        /// <summary>
        /// Список занятых табельных номеров
        /// </summary>
        SortedSet<int> tabNums; // как-нибудь прикрутим и его

        #endregion





    }
}
