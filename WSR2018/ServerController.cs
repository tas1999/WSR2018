using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WSR2018.MarathonSkillsBDTableAdapters;

namespace WSR2018
{
    
    class ServerController
    {
        static int runnerId = 0;
        static string email = null;
        static public string Email { get => email; }
        static short yearMar = 2015;
        static UserTableAdapter userTableAdapter = new UserTableAdapter();
        static EventTableAdapter eventTableAdapter = new EventTableAdapter();
        
        static GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
        static RunnerTableAdapter runnerTableAdapter = new RunnerTableAdapter();
        static ItemTableAdapter itemTableAdapter = new ItemTableAdapter();
        static CountryTableAdapter CountryTableAdapter = new CountryTableAdapter();
        static CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
        static MarathonTableAdapter marathonTableAdapter = new MarathonTableAdapter();
        static EventRunnerTableAdapter eventRunnerTableAdapter = new EventRunnerTableAdapter();
        static RegistrationTableAdapter registrationTableAdapter = new RegistrationTableAdapter();
        static MarathonSkillsBD.UserDataTable userDataTable = new MarathonSkillsBD.UserDataTable();
        static MarathonSkillsBD.EventDataTable eventDataTable = new MarathonSkillsBD.EventDataTable();
        static MarathonSkillsBD.GenderDataTable genderDataTable = new MarathonSkillsBD.GenderDataTable();
        static MarathonSkillsBD.RunnerDataTable runnerDataTable = new MarathonSkillsBD.RunnerDataTable();
        static MarathonSkillsBD.ItemDataTable itemDataTable = new MarathonSkillsBD.ItemDataTable();
        static MarathonSkillsBD.CountryDataTable countryDataTable = new MarathonSkillsBD.CountryDataTable();
        static MarathonSkillsBD.CharityDataTable charityDataTable = new MarathonSkillsBD.CharityDataTable();
        static MarathonSkillsBD.MarathonDataTable marathonDataTable = new MarathonSkillsBD.MarathonDataTable();
        static MarathonSkillsBD.EventRunnerDataTable eventRunnerDataTableIdRunner = new MarathonSkillsBD.EventRunnerDataTable();
        static MarathonSkillsBD.EventRunnerDataTable eventRunnerDataTableAllRunnre = new MarathonSkillsBD.EventRunnerDataTable();
        static MarathonSkillsBD.RegistrationDataTable registrationDataTable = new MarathonSkillsBD.RegistrationDataTable();
        /// <summary>
        /// Метод для аунтификаци пользователя в системе
        /// </summary>
        /// <param name="Email">Адресс электроной почты пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        /// <returns></returns>
        static public string Connect(string Email, string Password)
        {
            userTableAdapter.Fill(userDataTable);
            var userRows = userDataTable.Where(u => u.Email == Email && u.Password == Password).ToList();
            if (userRows.Count > 0)
            {
                runnerTableAdapter.Fill(runnerDataTable);
                if(userRows[0].RoleId == "R")
                    runnerId = runnerDataTable.Where(r => r.Email == userRows[0].Email).ToList()[0].RunnerId;
                email = Email;
                return userRows[0].RoleId;
            }
            return null;
        }
        static public bool CheckEmail(string Email)
        {
            userTableAdapter.Fill(userDataTable);
            return userDataTable.Where(u => u.Email.ToLower() == Email.ToLower()).ToList().Count == 0;
        }
        static public bool CheckPassword(string Password)
        {
            userTableAdapter.Fill(userDataTable);
            return userDataTable.Where(u => u.Password.ToLower() == Password.ToLower()).ToList().Count == 0;
        }
        /// <summary>
        /// Метод для вывода списка всех пользователей и их стран
        /// </summary>
        /// <returns>Список всех пользователей и их стран</returns>
        static public List<RunnerAndCountry> RunnerList()
        {
            runnerTableAdapter.FillByYearHeldArg(runnerDataTable, yearMar);
            userTableAdapter.Fill(userDataTable);
            CountryTableAdapter.Fill(countryDataTable);
            
            List<RunnerAndCountry> runners = new List<RunnerAndCountry>();
            foreach(var runner in runnerDataTable)
            {
                var user = userDataTable.Where(u => u.Email == runner.Email).ToList()[0];
                var country = countryDataTable.Where(c => c.CountryCode == runner.CountryCode).ToList()[0];
                charityTableAdapter.FillByRunnerIdArg(charityDataTable, runner.RunnerId);
                RunnerAndCountry runnerC = new RunnerAndCountry()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CountryCode = runner.CountryCode,
                    CountryName = country.CountryName,
                    CharityName = charityDataTable[0].CharityName,
                    CharityDescription = charityDataTable[0].CharityDescription,
                    CharityLogo = charityDataTable[0].CharityLogo
                };
                runners.Add(runnerC);
            }
            return runners;
        }
        static public MarathonSkillsBD.CharityDataTable CharityList()
        {
            charityTableAdapter.Fill(charityDataTable);
            return charityDataTable;
        }
        /// <summary>
        /// Метод регистрации пользователей
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="Pasword">Пароль</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Gender">Пол</param>
        /// <param name="DateOfBirth">Дата Рождения</param>
        /// <param name="CountryCode">Код Страны</param>
        /// <param name="photo">Ссылка на фото</param>
        static public void RegistrationRunner(
            string Email, 
            string Pasword, 
            string FirstName, 
            string LastName, 
            string Gender, 
            DateTime DateOfBirth, 
            string CountryCode,
            string photo)
        {
            userTableAdapter.Fill(userDataTable);
            var User = userDataTable.NewUserRow();
            User["Email"] = Email;
            User["Password"] = Pasword;
            User["FirstName"] = FirstName;
            User["LastName"] = LastName;
            User["RoleId"] = "R";
            userDataTable.AddUserRow(User);
            userTableAdapter.Update(userDataTable);

            runnerTableAdapter.Fill(runnerDataTable);
            var runner = runnerDataTable.NewRunnerRow();
            runner["Email"] = Email;
            runner["Gender"] = Gender;
            runner["DateOfBirth"] = DateOfBirth;
            runner["CountryCode"] = CountryCode;
            runnerDataTable.AddRunnerRow(runner);
            runnerTableAdapter.Update(runnerDataTable);
            runnerId = runner.RunnerId;
            email = Email;
        }
        /// <summary>
        /// Метод изменения данных пользователя
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="Pasword">Пароль</param>
        /// <param name="FirstName">Имя</param>
        /// <param name="LastName">Фамилия</param>
        /// <param name="Gender">Пол</param>
        /// <param name="DateOfBirth">Дата Рождения</param>
        /// <param name="CountryCode">Код Страны</param>
        /// <param name="photo">Ссылка на фото</param
        static public void EditRunner(
            string Email, 
            string Pasword, 
            string FirstName, 
            string LastName, 
            string Gender, 
            DateTime DateOfBirth, 
            string CountryCode, 
            string photo)
        {
            userTableAdapter.Fill(userDataTable);
            var User = userDataTable.Where(u => u.Email == Email).ToList()[0];
            User["Email"] = Email;
            if(!string.IsNullOrWhiteSpace(Pasword))
                User["Password"] = Pasword;
            User["FirstName"] = FirstName;
            User["LastName"] = LastName;
            User["RoleId"] = "R";
            userTableAdapter.Update(userDataTable);

            runnerTableAdapter.Fill(runnerDataTable);
            var runner = runnerDataTable.Where(r => r.Email == Email).ToList()[0];
            runner["Email"] = Email;
            runner["Gender"] = Gender;
            runner["DateOfBirth"] = DateOfBirth;
            runner["CountryCode"] = CountryCode;
            runnerTableAdapter.Update(runnerDataTable);
        }
        /// <summary>
        /// Метод регистрация пользователя на марафон
        /// </summary>
        /// <param name="RaceKitOptionId">Выбраный комплект</param>
        /// <param name="Cost">Взнос</param>
        /// <param name="CharityId">Id благотворительного фонда</param>
        /// <param name="SponsorshipTarget">Сумма спонсорства</param>
        static public void RegistrationRunnerOnMarafon(string RaceKitOptionId, double Cost, int CharityId, double SponsorshipTarget)
        {
            registrationTableAdapter.Fill(registrationDataTable);
            var registration = registrationDataTable.NewRegistrationRow();
            registration["RunnerId"] = runnerId;
            registration["RegistrationDateTime"] = DateTime.Now;
            registration["RaceKitOptionId"] = RaceKitOptionId;
            registration["RegistrationStatusId"] = 1;
            registration["Cost"] = Cost;
            registration["CharityId"] = CharityId;
            registration["SponsorshipTarget"] = SponsorshipTarget;
            registrationDataTable.AddRegistrationRow(registration);
            registrationTableAdapter.Update(registrationDataTable);
        }
        static public MarathonSkillsBD.CountryDataTable CountriesList()
        {
            CountryTableAdapter.Fill(countryDataTable);
            return countryDataTable;
        }
        static public MarathonSkillsBD.GenderDataTable GenderList()
        {
            genderTableAdapter.Fill(genderDataTable);
            return genderDataTable;
        }
        static public void Logout()
        {
            runnerId = 0;
            email = null;
        }
        /// <summary>
        /// Вывод статистики по марафону выбраного пользавателя
        /// </summary>
        /// <returns>Статистики по марафону выбраного пользавателя</returns>
        static public List<RunnnerResult> RunnerMarafonStatistic()
        {
            List<RunnnerResult> runnnerResults = new List<RunnnerResult>();
            eventRunnerTableAdapter.FillByRunnerIdArg(eventRunnerDataTableIdRunner, runnerId);
            
            foreach(var eventer in eventRunnerDataTableIdRunner)
            {
                int numberAll = 1;
                int numberC = 1;
                eventRunnerTableAdapter.FillByEventIdArg(eventRunnerDataTableAllRunnre,eventer.EventId);
                foreach(var evenAll in eventRunnerDataTableAllRunnre)
                {
                    if(evenAll.RaceTime < eventer.RaceTime)
                    {
                        numberAll++;
                        if(OldStatus(evenAll.DateOfBirth.Year) == OldStatus(eventer.DateOfBirth.Year) && evenAll.Gender == eventer.Gender)
                        {
                            numberC++;
                        }
                    }
                    
                }
                runnnerResults.Add(new RunnnerResult()
                {
                    MarathonName = eventer.MarathonName,
                    Distance = eventer.EventTypeName,
                    RaceTime = TimeSpan.FromSeconds(eventer.RaceTime).ToString(),
                    TopNumberAll ="#"+numberAll,
                    TopNumberCategority ="#"+numberC
                });
            }
            return runnnerResults;
        }
        /// <summary>
        /// Метод для расчёта возрастной категории бегуна
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        static string OldStatus(int Year)
        {
            if(Year < 18)
            {
                return "до 18";
            }
            else if(Year >=18 && Year < 29)
            {
                return "от 18 до 29";
            }
            else if (Year >= 30 && Year < 39)
            {
                return "от 30 до 39";
            }
            else if (Year >= 40 && Year < 55)
            {
                return "от 40 до 55";
            }
            else if (Year >= 56 && Year < 70)
            {
                return "от 56 до 70";
            }
            else 
            {
                return "более 70";
            }
        }
        static public DataGrid GetInventar(DataGrid dataGrid)
        {
            registrationTableAdapter.FillByYearHeldArgAndRaceKitOptionIdArg(registrationDataTable, yearMar, "A");
            int selectTypA = registrationDataTable.Count;
            registrationTableAdapter.FillByYearHeldArgAndRaceKitOptionIdArg(registrationDataTable, yearMar, "B");
            int selectTypB = registrationDataTable.Count;
            registrationTableAdapter.FillByYearHeldArgAndRaceKitOptionIdArg(registrationDataTable, yearMar, "C");
            int selectTypC = registrationDataTable.Count;
            itemTableAdapter.Fill(itemDataTable);
            
            Binding binding = new Binding("[0]");

            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Комплект",
                Binding = new Binding("[0]"),
                
            });
            dataGrid.Columns.Add(new DataGridTemplateColumn()
            {
                Header = "Тип А",
                CellTemplate = ColumnDataTemplate("[1]")
            });
            dataGrid.Columns.Add(new DataGridTemplateColumn()
            {
                Header = "Тип B",
                CellTemplate = ColumnDataTemplate("[2]")
            });
            dataGrid.Columns.Add(new DataGridTemplateColumn()
            {
                Header = "Тип C",
                CellTemplate = ColumnDataTemplate("[3]")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Необходимо",
                Binding = new Binding("[4]")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Остаток",
                Binding = new Binding("[5]")

            });
            dataGrid.Items.Add(new string[]
            {
                "Выбрало данный вариант",
                selectTypA.ToString(),
                selectTypB.ToString(),
                selectTypC.ToString(),
                selectTypA + selectTypB + selectTypC + "",
                "0"
            });
            dataGrid.Items.Add(new string[]
            {
                itemDataTable[0].ItemName,
                selectTypA.ToString(),
                selectTypB.ToString(),
                selectTypC.ToString(),
                selectTypA + selectTypB + selectTypC + "",
                itemDataTable[0].ItemCount.ToString()
            });
            dataGrid.Items.Add(new string[]
            {
                itemDataTable[1].ItemName,
                selectTypA.ToString(),
                selectTypB.ToString(),
                selectTypC.ToString(),
                selectTypA + selectTypB + selectTypC + "",
                itemDataTable[1].ItemCount.ToString()
            });
            dataGrid.Items.Add(new string[]
            {
                itemDataTable[2].ItemName,
                "ErrorImg",
                selectTypB.ToString(),
                selectTypC.ToString(),
                selectTypA + selectTypB + selectTypC + "",
                itemDataTable[2].ItemCount.ToString()
            });
            dataGrid.Items.Add(new string[]
            {
                itemDataTable[3].ItemName,
                "ErrorImg",
                selectTypB.ToString(),
                selectTypC.ToString(),
                selectTypA + selectTypB + selectTypC + "",
                itemDataTable[3].ItemCount.ToString()
            });
            dataGrid.Items.Add(new string[]
            {
                itemDataTable[4].ItemName,
                "ErrorImg",
                "ErrorImg",
                selectTypC.ToString(),
                selectTypA + selectTypB + selectTypC + "",
                itemDataTable[4].ItemCount.ToString()
            });
            dataGrid.Items.Add(new string[]
            {
                itemDataTable[5].ItemName,
                "ErrorImg",
                "ErrorImg",
                selectTypC.ToString(),
                selectTypA + selectTypB + selectTypC + "",
                itemDataTable[5].ItemCount.ToString()
            });
            return dataGrid;
        }
        static public DataTemplate ColumnDataTemplate(string path)
        {

            Binding bindDateLecture = new Binding()
            {
                Path = new PropertyPath(path)
            };

            var DateDayWithDateLecture = new FrameworkElementFactory(typeof(TextBlock));
            DateDayWithDateLecture.SetBinding(TextBlock.TextProperty, bindDateLecture);
            DateDayWithDateLecture.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);

            DateDayWithDateLecture.SetValue(TextBlock.BackgroundProperty, new SolidColorBrush(Colors.White));
            DateDayWithDateLecture.SetValue(TextBlock.HeightProperty, 40.0);
            DateDayWithDateLecture.SetValue(TextBlock.WidthProperty, 100.0);
            Style style = new Style();
            
            Trigger trigger = new Trigger()
            {
                Property = TextBlock.TextProperty,
                Value = "ErrorImg"
            };
            trigger.Setters.Add(new Setter()
            {
                Property = TextBlock.VisibilityProperty,
                Value = Visibility.Collapsed
            });
            style.Triggers.Add(trigger);

            DateDayWithDateLecture.SetValue(TextBlock.StyleProperty, style);

            var DateMonthWithNumberLectur = new FrameworkElementFactory(typeof(Image));
            DateMonthWithNumberLectur.SetValue(Image.SourceProperty, 
                new BitmapImage(new Uri(@"C:\Users\WS2018\source\repos\WSR2018\WSR2018\Img\cross-icon.png")));
            DateMonthWithNumberLectur.SetValue(Image.HeightProperty, 30.0);
            DateDayWithDateLecture.SetValue(Image.WidthProperty, 30.0);


            var grid2FactoryField = new FrameworkElementFactory(typeof(Grid));
            grid2FactoryField.AppendChild(DateMonthWithNumberLectur);
            grid2FactoryField.AppendChild(DateDayWithDateLecture);
            
            DataTemplate gridFieldTemplate = new DataTemplate();
            gridFieldTemplate.VisualTree = grid2FactoryField;
            return gridFieldTemplate;
        }

    }
}
