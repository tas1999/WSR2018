using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
        static ItemTableAdapter itemTableAdapter = new ItemTableAdapter();
        static EventTableAdapter eventTableAdapter = new EventTableAdapter();
        static SpeedTableAdapter speedTableAdapter = new SpeedTableAdapter();
        static GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
        static RunnerTableAdapter runnerTableAdapter = new RunnerTableAdapter();
        static CountryTableAdapter CountryTableAdapter = new CountryTableAdapter();
        static CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
        static DistanceTableAdapter distanceTableAdapter = new DistanceTableAdapter();
        static MarathonTableAdapter marathonTableAdapter = new MarathonTableAdapter();
        static EventRunnerTableAdapter eventRunnerTableAdapter = new EventRunnerTableAdapter();
        static RegistrationTableAdapter registrationTableAdapter = new RegistrationTableAdapter();
        static MarathonSkillsBD.UserDataTable userDataTable = new MarathonSkillsBD.UserDataTable();
        static MarathonSkillsBD.ItemDataTable itemDataTable = new MarathonSkillsBD.ItemDataTable();
        static MarathonSkillsBD.EventDataTable eventDataTable = new MarathonSkillsBD.EventDataTable();
        static MarathonSkillsBD.SpeedDataTable speedDataTable = new MarathonSkillsBD.SpeedDataTable();
        static MarathonSkillsBD.GenderDataTable genderDataTable = new MarathonSkillsBD.GenderDataTable();
        static MarathonSkillsBD.RunnerDataTable runnerDataTable = new MarathonSkillsBD.RunnerDataTable();
        static MarathonSkillsBD.CountryDataTable countryDataTable = new MarathonSkillsBD.CountryDataTable();
        static MarathonSkillsBD.CharityDataTable charityDataTable = new MarathonSkillsBD.CharityDataTable();
        static MarathonSkillsBD.DistanceDataTable distanceDataTable = new MarathonSkillsBD.DistanceDataTable();
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
        static public int GetInventar(Table dataGrid,Table ReportTable)
        {
            registrationTableAdapter.FillByYearHeldArgAndRaceKitOptionIdArg(registrationDataTable, yearMar, "A");
            int selectTypA = registrationDataTable.Count;
            registrationTableAdapter.FillByYearHeldArgAndRaceKitOptionIdArg(registrationDataTable, yearMar, "B");
            int selectTypB = registrationDataTable.Count;
            registrationTableAdapter.FillByYearHeldArgAndRaceKitOptionIdArg(registrationDataTable, yearMar, "C");
            int selectTypC = registrationDataTable.Count;

            itemTableAdapter.Fill(itemDataTable);
            ReportTableCreator(ReportTable, selectTypA, selectTypB, selectTypC);
            InventarTableCreator(dataGrid, selectTypA, selectTypB, selectTypC);
            registrationTableAdapter.FillByYearHeldArg(registrationDataTable, yearMar);
            return registrationDataTable.Count;
        }
        static void ReportTableCreator(Table ReportTable , int selectTypA, int selectTypB, int selectTypC)
        {
            ReportTable.Columns.Add(new TableColumn());
            ReportTable.Columns.Add(new TableColumn());
            ReportTable.Columns.Add(new TableColumn());
            ReportTable.Columns.Add(new TableColumn());
            TableRowGroup tableRowGroup = new TableRowGroup();
            TableRow tableRow1 = new TableRow();
            tableRow1.Cells.Add(new TableCell(new Paragraph(new Run("Наименование")))
            {
                Background = new SolidColorBrush(Colors.Aqua)
            }
                     );
            tableRow1.Cells.Add(new TableCell(new Paragraph(new Run("остаток")))
            {
                Background = new SolidColorBrush(Colors.Aqua)
            }
                     );
            tableRow1.Cells.Add(new TableCell(new Paragraph(new Run("необходимо")))
            {
                Background = new SolidColorBrush(Colors.Aqua)
            }
                     );
            tableRow1.Cells.Add(new TableCell(new Paragraph(new Run("Надо заказать")))
            {
                Background = new SolidColorBrush(Colors.Aqua)
            }
                     );
            tableRowGroup.Rows.Add(tableRow1);
            for (int y = 0; y < 6; y++)
            {
                TableRow tableRow = new TableRow();
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(itemDataTable[y].ItemName))));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(itemDataTable[y].ItemCount.ToString()))));
                if (y < 2) tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypA + selectTypB + selectTypC + ""))));
                else if (y < 4) tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypB + selectTypC + ""))));
                else tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypC + ""))));

                if (y < 2) tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypA + selectTypB + selectTypC - itemDataTable[y].ItemCount + ""))));
                else if (y < 4) tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypB + selectTypC - itemDataTable[y].ItemCount + ""))));
                else tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypC - itemDataTable[y].ItemCount + ""))));

                tableRowGroup.Rows.Add(tableRow);
            }
            ReportTable.RowGroups.Add(tableRowGroup);
        }
        static void InventarTableCreator(Table dataGrid, int selectTypA, int selectTypB, int selectTypC)
        {
            TableRowGroup tableRowGroup1 = new TableRowGroup();
            dataGrid.Columns.Add(new TableColumn());
            dataGrid.Columns.Add(new TableColumn());
            dataGrid.Columns.Add(new TableColumn());
            dataGrid.Columns.Add(new TableColumn());
            dataGrid.Columns.Add(new TableColumn());
            dataGrid.Columns.Add(new TableColumn());
            TableRow tableRow2 = new TableRow();
            tableRow2.Cells.Add(new TableCell(new Paragraph(new Run("Комплект"))));
            tableRow2.Cells.Add(new TableCell(new Paragraph(new Run("Тип А"))));
            tableRow2.Cells.Add(new TableCell(new Paragraph(new Run("Тип B"))));
            tableRow2.Cells.Add(new TableCell(new Paragraph(new Run("Тип C"))));
            tableRow2.Cells.Add(new TableCell(new Paragraph(new Run("Необходимо"))));
            tableRow2.Cells.Add(new TableCell(new Paragraph(new Run("Остаток"))));
            tableRowGroup1.Rows.Add(tableRow2);

            BitmapImage image = new BitmapImage(new Uri(@"C:\Users\WS2018\source\repos\WSR2018\WSR2018\Img\cross-icon.png"));
            for (int y = -1; y < 6; y++)
            {
                TableRow tableRow = new TableRow();
                if (y > -1) tableRow.Cells.Add(new TableCell(new Paragraph(new Run(itemDataTable[y].ItemName))));
                else tableRow.Cells.Add(new TableCell(new Paragraph(new Run("Выбрало данный вариант"))));
                if (y < 2) tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypA.ToString()))));
                else tableRow.Cells.Add(new TableCell(new BlockUIContainer(new Image()
                {
                    Source = image,
                    Height = 30.0,
                    HorizontalAlignment = HorizontalAlignment.Left
                    
                })));
                if (y < 4) tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypB.ToString()))));
                else tableRow.Cells.Add(new TableCell(new BlockUIContainer(new Image()
                {
                    Source = image,
                    Height = 30.0,
                    HorizontalAlignment = HorizontalAlignment.Left
                })));
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypC.ToString()))));
                if (y < 2) tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypA + selectTypB + selectTypC + ""))));
                else if (y < 4) tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypB.ToString(selectTypB + selectTypC + "")))));
                else tableRow.Cells.Add(new TableCell(new Paragraph(new Run(selectTypB.ToString(selectTypC + "")))));
                if (y > -1) tableRow.Cells.Add(new TableCell(new Paragraph(new Run(itemDataTable[y].ItemCount.ToString()))));
                else tableRow.Cells.Add(new TableCell(new Paragraph(new Run("0"))));
                tableRowGroup1.Rows.Add(tableRow);
            }
            dataGrid.RowGroups.Add(tableRowGroup1);
        }
        static public void InventarImport(Table inventarImport)
        {
            itemTableAdapter.Fill(itemDataTable);
            TableRowGroup tableRowGroup = new TableRowGroup();
            TableRow tableRowMain = new TableRow();
            tableRowMain.Cells.Add(new TableCell(new Paragraph(new Run("Наименование"))));
            tableRowMain.Cells.Add(new TableCell(new Paragraph(new Run("Поступление"))));
            tableRowGroup.Rows.Add(tableRowMain);
            for (int y =0; y < itemDataTable.Count; y++)
            {
                TableRow tableRow = new TableRow();
                tableRow.Cells.Add(new TableCell(new Paragraph(new Run(itemDataTable[y].ItemName))));
                tableRow.Cells.Add(new TableCell(new BlockUIContainer(new TextBox()
                {
                    Name = "I" + itemDataTable[y].ItemId
                })));
                tableRowGroup.Rows.Add(tableRow);
            }
            inventarImport.RowGroups.Add(tableRowGroup);
        }
        static public void InventarExsport(Table inventarImport)
        {
            itemTableAdapter.Fill(itemDataTable);
            for (int y = 0; y < itemDataTable.Count; y++)
            {
                var go1 = inventarImport.FindName("I" + itemDataTable[y].ItemId);
                for(int i =1; i < inventarImport.RowGroups[0].Rows.Count; i++)
                {
                    var g = inventarImport.RowGroups[0].Rows[i].Cells[1].Blocks.ToList();
                    var g1 = (BlockUIContainer)g[0];
                    var t = (TextBox)g1.Child;
                    if (t.Name == "I" + itemDataTable[y].ItemId)
                    {
                       string countString = t.Text;
                       if (int.TryParse(countString, out int count))
                            itemDataTable[y].ItemCount = count + itemDataTable[y].ItemCount;
                    }
                }
            }
            if(itemTableAdapter.Update(itemDataTable) > 0)
                MessageBox.Show("Данные внесены");
        }
        static public void HowLongFill(TabControl tab)
        {
            speedTableAdapter.Fill(speedDataTable);
            distanceTableAdapter.Fill(distanceDataTable);
            TabItem speedtabItem = new TabItem();
            speedtabItem.Header = "Speed";
            TabItem distancetabItem = new TabItem();
            distancetabItem.Header = "Distance";
            TabControl speedtabControl = new TabControl();
            speedtabControl.TabStripPlacement = Dock.Left;
            TabControl distancetabControl = new TabControl();
            distancetabControl.TabStripPlacement = Dock.Left;
            foreach (var speedRow in speedDataTable)
            {
                speedtabControl.Items.Add
                (
                    TabItemCreator(
                    speedRow.Image,
                    speedRow.Name,
                    speedRow.Name
                    + " travels at "
                    + speedRow.Top_Speed
                    + " so would complete the marathon in "
                    + yearMar)
                );
            }
            foreach (var distance in distanceDataTable)
            {
                distancetabControl.Items.Add
                (
                    TabItemCreator(
                    distance.Image,
                    distance.Name,
                    distance.Name
                    + " is "
                    + distance.Length
                    + " in length so "
                    + yearMar 
                    + " would fit into the marathon length ")
                );
            }
            speedtabItem.Content = speedtabControl;
            distancetabItem.Content = distancetabControl;
            tab.Items.Add(speedtabItem);
            tab.Items.Add(distancetabItem);
        }
        static public TabItem TabItemCreator(string Image, string Name, string Text )
        {
                BitmapImage bitmapImage = new BitmapImage(
                    new Uri(@"C:\Users\WS2018\source\repos\WSR2018\WSR2018\Img\how-long-is-a-marathon-images\"
                            + Image));
                TabItem tabItem = new TabItem();
                StackPanel hederStackPanel = new StackPanel();
                hederStackPanel.Orientation = Orientation.Horizontal;
                hederStackPanel.Children.Add(new Image()
                {
                    Source = bitmapImage,
                    Height = 50,
                    Width = 50
                });
                hederStackPanel.Children.Add(new TextBlock()
                {
                    Text = Name
                });
                tabItem.Header = hederStackPanel;
                StackPanel contentStackPanel = new StackPanel();
                contentStackPanel.Orientation = Orientation.Vertical;
                contentStackPanel.Children.Add(new TextBlock()
                {
                    Text = Name,
                    Style = (Style)Application.Current.Resources["TittleStyle"]
                });
                contentStackPanel.Children.Add(new Image()
                {
                    Source = bitmapImage,
                    Height = 200,
                    Width = 200
                });
                contentStackPanel.Children.Add(new TextBlock()
                {
                    Text = Text,
                    Style = (Style)Application.Current.Resources["TextWrapStyle"]
                });
                tabItem.Content = contentStackPanel;
            return tabItem;
        }
    }
}
