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
using System.Runtime.CompilerServices;

namespace Workers
{
    /// <summary>
    /// Структура организации с работниками и отделами
    /// </summary>
    public struct Company
    {
        #region Конструкторы

        /// <summary>
        /// Общий конструктор c частично инициализированными полями
        /// </summary>
        /// <param name="AnyInt">Любое целое число (фальшивый параметр)</param>
        public Company(int AnyInt)
        {
            this.departments = new List<Department>();
            this.workers = new List<Worker>();
            this.deptByName = new Dictionary<string, Department>();
            this.tabNums = new SortedSet<int>();
            this.tabNums.Add(0);
            this.wHeader = $"{"Таб. номер",10}{"Имя",12}{"Фамилия",15}{"Возраст",10}{"Должность",18}{"Зарплата",10}{"Отдел",10}{"Проектов",10}";
            this.dHeader = $"{"Наименование",15}{"Дата создания",15}{"Численность",18}{"Кол-во проектов",18}{"Должности",18}";
            this.MaxQ = 1_000_000;
            this.MaxD = 10;
        }

        /// <summary>
        /// Создает компанию в демо-режиме с рандомными сотрудниками
        /// </summary>
        /// <param name="NumOfDepts">Количество создаваемых отделов</param>
        /// <param name="NumOfJacks">Количество создаваемых работников внутри каждого отдела</param>
        public Company(int NumOfDepts, int NumOfJacks) : this (0)
        {
            int max = NumOfDepts < this.MaxD ? NumOfDepts : this.MaxD;
            for (int i = 0; i < max; i++)
            {
                this.departments.Add(new Department(i + 1));
                this.deptByName.Add(this.departments[i].Name, this.departments[i]);
            }

            max = NumOfJacks < this.MaxQ ? NumOfJacks : this.MaxQ;
            for (int d = 0; d < this.departments.Count; d++)
            {
                for (int i = 0; i < max; i++)
                {
                    this.tabNums.Add(this.tabNums.Max + 1);
                    this.workers.Add(new Worker(this.tabNums.Max, this.departments[d]));
                }
            }

        }

        /// <summary>
        /// Конструктор компании из файла. Нужный парсер выбирается по расширению имени файла.
        /// </summary>
        /// <param name="path">.xml или .json файл</param>
        public Company(string path) : this (0)
        {
            string extn = Path.GetExtension(path);

            if (extn == ".json")
            {
                string json = File.ReadAllText(path);

                dynamic parseJson = JsonConvert.DeserializeObject(json);
                
                for (int i = 0; i < parseJson[1].Count; i++)

                {
                    string deptName = parseJson[1][i].name; ;
                    DateTime crDate = Convert.ToDateTime(parseJson[1][i].date);
                    int eCount = Convert.ToInt32(parseJson[1][i].ecount);
                    int prCount = Convert.ToInt32(parseJson[1][i].prcount);
                    List<string> staff = new List<string>();
                    staff = parseJson[1][i].staff.ToObject<List<string>>();
                    Department tempDept = new Department(deptName, crDate, eCount, prCount, staff);
                    this.departments.Add(tempDept);
                    this.deptByName.Add(tempDept.Name, tempDept);

                }

                for (int i = 0; i < parseJson[2].Count; i++)

                {
                    int num = Convert.ToInt32(parseJson[2][i].tabnum);
                    string firstName = parseJson[2][i].firstname;
                    string lastName = parseJson[2][i].Lastname;
                    int age = Convert.ToInt32(parseJson[2][i].age);
                    string position = parseJson[2][i].position;
                    string department = parseJson[2][i].department;
                    int salary = Convert.ToInt32(parseJson[2][i].salary);
                    int charge = Convert.ToInt32(parseJson[2][i].charge);

                    Worker tempWorker = new Worker(num, firstName, lastName, age, position, salary, this.deptByName[department], charge);
                    this.workers.Add(tempWorker);
                    this.tabNums.Add(tempWorker.Tabnum);
                }
            }

            else if (extn == ".xml")
            {
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
                    this.deptByName.Add(tempDept.Name, tempDept); 

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
           
        }

      
        #endregion

        #region Сериализация

        /// <summary>
        /// Сериализация JSON
        /// </summary>
        public void SerializeCompanyJSON()
        {
            JArray jArray = new JArray();
            JObject Jcompany = new JObject();
            Jcompany["company_name"] = "ACME Corporation Russia";
            jArray.Add(Jcompany);

            JArray jDepartments = new JArray();

            JArray jWorkers = new JArray();

            for (int i = 0; i < this.departments.Count; i++)
            {
                JObject Jdepartment = new JObject();
                Jdepartment["name"] = this.departments[i].Name;
                Jdepartment["date"] = this.departments[i].CrDate;
                Jdepartment["ecount"] = this.departments[i].ECount;
                Jdepartment["prcount"] = this.departments[i].PrCount;
                
                JArray jstaff = new JArray();

                for (int s = 0; s < this.departments[i].Positions.Count; s++)
                {
                   jstaff.Add(this.departments[i].Positions[s]);
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
                jWorkers.Add(JWorker);
            }

            jArray.Add(jWorkers);

            File.WriteAllText("_company.json", jArray.ToString());
        }



        /// <summary>
        /// Сериализация XML
        /// </summary>
        public void SerializeCompanyXML()
        {
            XElement xCompany = new XElement("COMPANY");
            XAttribute xCompanyName = new XAttribute("company_name", "ACME Corporation Russia");
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
        public bool HireRandom()
        {
            List<int> haveFreeChair = new List<int>();
            for (int i =0; i < this.departments.Count; i++)
            {
                if (this.departments[i].ECount < this.MaxQ)
                    haveFreeChair.Add(i);
            }
            if (haveFreeChair.Count == 0) return false;

            Random r = new Random();
            int nextNum = this.tabNums.Max + 1;
            this.tabNums.Add(nextNum);
            this.workers.Add(new Worker(nextNum, this.departments[haveFreeChair[r.Next(0, haveFreeChair.Count)]]));
            return true;
        }

        /// <summary>
        /// Увольняет работника по табельному номеру
        /// </summary>
        /// <param name="num">Табельный номер</param>
        public bool Fire(int num)
        {
            var person = workers.Find(item => item.Tabnum == num);
            if (person == null)
            {
                return false;
            }
            deptByName[person.Department].ECount--;                         // минус человек
            deptByName[person.Department].PrCount -= person.Charge;         // минус проекты
            this.workers.Remove(person);
            return true;
        }

        /// <summary>
        /// Выводит на консоль данные работника по табельному номеру
        /// </summary>
        /// <param name="num">Индекс записи в списке</param>
        public void PrintPerson(int num)
        {
            Console.WriteLine(this.wHeader);

            var person = workers.Find(item => item.Tabnum == num);
            if (person == null)
            {
                Console.WriteLine("Работник с таким табельным номером не найден.");
                return;
            }
            person.PrintWorker();
        }

        /// <summary>
        /// Выводит на консоль информацию об отделе по индексу (номеру) в списке
        /// </summary>
        /// <param name="i">Индекс записи в списке</param>
        public void PrintDeptInfo(int i)
        {
            Console.WriteLine(this.dHeader);
            if (this.departments[i] == null)
                Console.WriteLine("Отдела с таким номером не существует");
            this.departments[i].PrintDepartment();
        }

        /// <summary>
        /// Выводит на консоль всех работников
        /// </summary>
        public void PrintPanel()
        {
            Console.WriteLine(this.wHeader);

            for (int i = 0; i < this.workers.Count; i++)
            {
                this.workers[i].PrintWorker();
            }

        }

        /// <summary>
        /// Выводит на консоль список департаментов со всеми полями
        /// </summary>
        public void PrintDepartments()
        {
            Console.WriteLine(this.dHeader);
            for (int i = 0; i < this.departments.Count; i++)
            {
                this.departments[i].PrintDepartment();
            }

        }

        /// <summary>
        /// Сортирует список работников по одному или нескольким полям
        /// </summary>
        /// <param name="c">Правила сравнения</param>
        public void Sort(Comparison<Worker> c)
        {
            this.workers.Sort(c);
        }
        /// <summary>
        /// Редактирует поля записи конкретного работника
        /// </summary>
        /// <param name="num">табельный номер</param>
        /// <param name="newName">новое имя</param>
        /// <param name="newFamName">новая фамилия</param>
        /// <param name="newSalary">новая зарплата</param>
        /// <param name="newCharge">новая загрузка</param>
        public bool EditWorker(int num, string newName, string newFamName, int newSalary, int newCharge)
        {
            var person = workers.Find(item => item.Tabnum == num);
            if (person == null)
            {
                
                return false;
            }
            int i = this.workers.IndexOf(person);
            this.workers[i].FirstName = newName;
            this.workers[i].LastName = newFamName;
            this.workers[i].Salary = newSalary;
            this.workers[i].Charge = newCharge;
            return true;
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
        /// Список всех использованных табельных номеров по порядку
        /// </summary>
        SortedSet<int> tabNums;

        /// <summary>
        /// Строка заголовков сотрудников
        /// </summary>
        string wHeader;

        /// <summary>
        /// Строка заголовков департаментов
        /// </summary>
        string dHeader;



        #endregion

        #region Свойства
        /// <summary>
        /// Список всех использованных табельных номеров по порядку
        /// </summary>
        public SortedSet<int> TabNums { get { return tabNums; } }

        /// <summary>
        /// Максимальное количество работников в отделе
        /// </summary>
        public int MaxQ { get; }

        /// <summary>
        /// Максимальное количество отделов
        /// </summary>
        public int MaxD { get; }

        #endregion

    }
}
