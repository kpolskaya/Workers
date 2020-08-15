using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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

        /// <summary>
        /// Конструктор компании из JSON файла
        /// </summary>
        /// <param name="path"></param>

        public Company(string path)
        {
            string json = File.ReadAllText(path);
            this.deptByName = new Dictionary<string, Department>();
            this.tabNums = new SortedSet<int>();
            this.departments = new List<Department>();
            this.workers = new List<Worker>();

            dynamic parseJson = JsonConvert.DeserializeObject(json);
            string[] r = new string[6];
           // r = parseJson[0][1].staff;
            for (int i = 0; i < parseJson[0].Count; i++)

            {
                string deptName = parseJson[0][i].name; ;
                DateTime crDate = Convert.ToDateTime(parseJson[0][i].date);
                int eCount = Convert.ToInt32(parseJson[0][i].ecount);
                int prCount = Convert.ToInt32(parseJson[0][i].prcount);
                List<string> staff = new List<string>();
                staff= parseJson[0][i].staff.ToObject<List<string>>();
                Department tempDept = new Department(deptName, crDate, eCount, prCount, staff);
                this.departments.Add(tempDept);
                this.deptByName.Add(tempDept.Name, tempDept);

            }


            for (int i = 0; i < parseJson[1].Count; i++)

            {
                int num = Convert.ToInt32(parseJson[1][i].tabnum);
                string firstName = parseJson[1][i].firstname;
                string lastName = parseJson[1][i].Lastname;
                int age = Convert.ToInt32(parseJson[1][i].age);
                string position = parseJson[1][i].position;
                string department = parseJson[1][i].department;
                int salary = Convert.ToInt32(parseJson[1][i].salary);
                int charge = Convert.ToInt32(parseJson[1][i].charge);

                Worker tempWorker = new Worker(num, firstName, lastName, age, position, salary, this.deptByName[department], charge);
                this.workers.Add(tempWorker);
                this.tabNums.Add(tempWorker.Tabnum);

            }
        }

            /// <summary>
            /// Конструктор компании из xml файла
            /// </summary>
            /// <param name="path"></param>
            /// <param name="dummy">фейковый параметр - любая строка (чтобы отличить от другого конструктора из json)</param>
            public Company(string path, string dummy) // потом сделаем общий конструктор, который будет выбирать по расширению файла, какой десериализатор запускать
        {


            this.departments = new List<Department>();
            this.workers = new List<Worker>();
            this.deptByName = new Dictionary<string, Department>();
            this.tabNums = new SortedSet<int>();

            //читаем весь файл
            string xml = File.ReadAllText(path);

            //разбираем его в коллекцию департаментов
            var colOfDepts = XDocument.Parse(xml)
                                       .Descendants("COMPANY")
                                       .Descendants("DEPARTMENT")
                                       .ToList();

            //переписываем атрибуты и элементы каждого департамента в соответствующие поля
            foreach (var item in colOfDepts)
            {
                string deptName = item.Attribute("name").Value;
                DateTime crDate = Convert.ToDateTime(item.Attribute("date").Value);
                int eCount = Convert.ToInt32(item.Attribute("ecount").Value);
                int prCount = Convert.ToInt32(item.Attribute("prcount").Value);

                List<string> staff = new List<string>();
                foreach (string s in item.Elements("Staff"))
                {
                    staff.Add(s);
                }

                //создаем запись в списке департаментов
                Department tempDept = new Department(deptName, crDate, eCount, prCount, staff);
                this.departments.Add(tempDept);
                //и запись в словаре
                this.deptByName.Add(tempDept.Name, tempDept); //вопрос - проверить увольнение после этого!

                int num;
                string firstName;
                string lastName;
                int age;
                string position;
                int salary;
                int charge;

                //переписываем атрибуты каждого worker в соответствующе поля
                foreach (var w in item.Elements("WORKER"))
                {
                    num = Convert.ToInt32(w.Attribute("num").Value);
                    firstName = w.Attribute("firstname").Value;
                    lastName = w.Attribute("lastname").Value;
                    age = Convert.ToInt32(w.Attribute("age").Value);
                    position = w.Attribute("position").Value;
                    salary = Convert.ToInt32(w.Attribute("salary").Value);
                    charge = Convert.ToInt32(w.Attribute("charge").Value);

                    Worker tempWorker = new Worker(num, firstName, lastName, age, position, salary, tempDept, charge);
                    this.workers.Add(tempWorker);
                    this.tabNums.Add(tempWorker.Tabnum);

                }

            }

        }

        #endregion

        #region Сериализация

        /// <summary>
        /// Сериализация JSON
        /// </summary>
        public void SerializeCompanyJSON()
        {
            JArray jArray = new JArray();
            //deptByName = new Dictionary<string, Department>();
            //tabNums = new SortedSet<int>();

            //JObject Jdeptbyname = new JObject();
            JObject Jcompany = new JObject();
            //JObject JTabnum = new JObject();
            //Jcompany["corpname"] = "ACME Corporation";
            //jArray.Add(Jcompany);

            JArray jDepartments = new JArray();

            JArray jWorkers = new JArray();

            for (int i = 0; i < this.departments.Count; i++)
            {
                JObject Jdepartment = new JObject();
                Jdepartment["name"] = this.departments[i].Name;
                Jdepartment["date"] = this.departments[i].CrDate;
                Jdepartment["ecount"] = this.departments[i].ECount;
                Jdepartment["prcount"] = this.departments[i].PrCount;
                //Jdeptbyname["key"] = this.departments[i].Name;

               
                JArray jstaff = new JArray();

                for (int s = 0; s < this.departments[i].Positions.Count; s++)
                {
                    Jdepartment["staff"] = this.departments[i].Positions[s];
                    jstaff.Add(Jdepartment["staff"]);
                }
                Jdepartment["staff"] = jstaff;
                
                jDepartments.Add(Jdepartment);
               
            }
            jArray.Add(jDepartments);

            for (int j = 0; j < this.workers.Count; j++)
            {
                JObject JWorker = new JObject();
                JWorker["tabnum"] = this.workers[j].Tabnum;
                JWorker["firstname"] = this.workers[j].FirstName;
                JWorker["Lastname"] = this.workers[j].LastName;
                JWorker["age"] = this.workers[j].Age;
                JWorker["salary"] = this.workers[j].Salary;
                JWorker["position"] = this.workers[j].Position;
                JWorker["department"] = this.workers[j].Department;
                JWorker["charge"] = this.workers[j].Charge;
                //if (this.workers[j].Department == this.departments[i].Name)
                jWorkers.Add(JWorker);
            }

            jArray.Add(jWorkers);

            //jArray.Add(JTabnum);
            //jArray.Add(Jdeptbyname);
            string json = JsonConvert.SerializeObject(jArray);
            File.WriteAllText("comp1.json", (jArray.ToString()));
        }



        /// <summary>
        /// Сериализация XML
        /// </summary>
        public void SerializeCompanyXML()
        {
            XElement xCompany = new XElement("COMPANY");
            XAttribute xCompanyName = new XAttribute("name", "ACME Corporation");
            xCompany.Add(xCompanyName);



            for (int i = 0; i < this.departments.Count; i++)
            {
                XElement xDepartment = new XElement("DEPARTMENT");
                XAttribute xDeptName = new XAttribute("name", this.departments[i].Name);
                XAttribute xCrDate = new XAttribute("date", this.departments[i].CrDate.ToString("dd.MM.yyyy"));
                XAttribute xEmplCount = new XAttribute("ecount", this.departments[i].ECount);
                XAttribute xPrjCount = new XAttribute("prcount", this.departments[i].PrCount);

                xDepartment.Add(xDeptName);
                xDepartment.Add(xCrDate);
                xDepartment.Add(xEmplCount);
                xDepartment.Add(xPrjCount);

                //XElement xStaff = new XElement("Staff");
                for (int s = 0; s < this.departments[i].Positions.Count; s++)
                {
                    XElement xStaff = new XElement("Staff");
                    xStaff.Add(this.departments[i].Positions[s]);
                    xDepartment.Add(xStaff);
                }

                for (int j = 0; j < this.workers.Count; j++)
                {
                    XElement xWorker = new XElement("WORKER");
                    XAttribute xNum = new XAttribute("num", this.workers[j].Tabnum);
                    XAttribute xFirstName = new XAttribute("firstname", this.workers[j].FirstName);
                    XAttribute xLastName = new XAttribute("lastname", this.workers[j].LastName);
                    XAttribute xAge = new XAttribute("age", this.workers[j].Age);
                    XAttribute xSalary = new XAttribute("salary", this.workers[j].Salary);
                    XAttribute xPosition = new XAttribute("position", this.workers[j].Position);
                    XAttribute xCharge = new XAttribute("charge", this.workers[j].Charge);

                    if (this.workers[j].Department == this.departments[i].Name)
                    {
                        xWorker.Add(xNum);
                        xWorker.Add(xFirstName);
                        xWorker.Add(xLastName);
                        xWorker.Add(xAge);
                        xWorker.Add(xSalary);
                        xWorker.Add(xPosition);
                        xWorker.Add(xCharge);
                        xDepartment.Add(xWorker);
                    }
                }
                xCompany.Add(xDepartment);
            }

            xCompany.Save("_company.xml");
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
            var person = workers.Find(item => item.Tabnum == num);
            deptByName[person.Department].ECount--;                         // минус человек
            deptByName[person.Department].PrCount -= person.Charge;         // минус проекты
            this.workers.Remove(person);
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

        /// <summary>
        /// Выводит на консоль всех работников
        /// </summary>
        public void PrintPanel()
        {
            Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");

            for (int i = 0; i < this.workers.Count; i++)
            {
               PrintPerson(i);
            }
           
        }

        /// <summary>
        /// Выводит на консоль список департаментов со всеми полями
        /// </summary>
        public void PrintDepartments()
        {
            Console.WriteLine($"{"Наименование", 15}{"Дата создания", 15}{"Численность", 18}{"Кол-во проектов", 18}{"Должности", 18}");
            for (int i = 0; i < this.departments.Count; i++)
            {
                PrintDeptInfo(i);
            }
           
        }

        /// <summary>
        /// Сортирует работников по возрасту и зарплате внутри отдела
        /// </summary>
        public void SortByThreeParams() //это нужно сделать покрасивее!
        {
            List<Worker> sortedWorkers = workers.OrderBy(x => x.Department)
                                   .ThenBy(x => x.Age)
                                   .ThenBy(x => x.Salary)
                                   .ToList();
            Console.WriteLine($"{"Таб. номер",10}{"Имя",12} {"Фамилия",15} {"Возраст",10} {"Должность",15} {"Зарплата",10} {"Отдел",10} {"Проектов",10}");

            for (int i = 0; i < sortedWorkers.Count; i++)
            {
                sortedWorkers[i].PrintWorker();
            }
        }

        #endregion



        #region Поля

        /// <summary>
        /// Список отделов
        /// </summary>
        public List<Department> departments;

        /// <summary>
        /// Список работников
        /// </summary>
        public List<Worker> workers;

        /// <summary>
        /// Словарь названий отделов
        /// </summary>
        public Dictionary<string, Department> deptByName;

        /// <summary>
        /// Список занятых табельных номеров
        /// </summary>
        public SortedSet<int> tabNums; 

        #endregion





    }
}
