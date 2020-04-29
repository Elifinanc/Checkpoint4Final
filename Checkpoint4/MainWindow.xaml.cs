using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace Checkpoint4
{
    public partial class MainWindow : Window
    {
        public List<Show> AllShowList { get; set; }
        public List<Presentation> AllPresentationList { get; set; }
        public PresentationInfo PresInfoByShow { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            AllShowList = DisplayInformation.GetAllDefaultShows();
            List<string> showNameList = AllShowList.Select(s => s.Name).ToList();
            ShowListComboBox.ItemsSource = showNameList;

            List<PresentationInfo> presentationInfoList = new List<PresentationInfo>();
            AllPresentationList = DisplayInformation.GetAllPresentationList();
            foreach (Presentation pres in AllPresentationList)
            {
                PresInfoByShow = DisplayInformation.GetPresInfoByPres(pres);
                presentationInfoList.Add(PresInfoByShow);
            }
            PresentationInfosListView.ItemsSource = presentationInfoList;
        }

        private void Research_btn_Click(object sender, RoutedEventArgs e)
        {
            if (ShowListComboBox.SelectedItem != null || (StartDatePicker.SelectedDate != null && EndDatePicker.SelectedDate != null))
            {
                FilterSelectedDisplay();
            }
            else
            {
                System.Windows.MessageBox.Show("You have to select a filter before!");
            }
        }

        private void FilterSelectedDisplay()
        {
            if (IsShowAndPeriodSelected())
            {
                DateTime selectedStartDate = StartDatePicker.SelectedDate.Value.Date;
                DateTime selectedEndDate = EndDatePicker.SelectedDate.Value.Date;
                string selectedShowName = ShowListComboBox.Text;
                Show selectedShow = DisplayInformation.GetShowByName(selectedShowName);
                List<PresentationInfo> presListWithShowFilter = new List<PresentationInfo>();
                List<Presentation> allPresList = DisplayInformation.GetAllPresentationList();
                List<Presentation> selectedPresList = allPresList.Where(p => p.Show.ShowId == selectedShow.ShowId).ToList();
                foreach (Presentation pres in selectedPresList)
                {
                    PresentationInfo presInfoByShow = DisplayInformation.GetPresInfoByPres(pres);
                    presListWithShowFilter.Add(presInfoByShow);
                }
                List<PresentationInfo> presListWithPeriodFilter = presListWithShowFilter.Where(p => p.PresentationDate >= selectedStartDate && p.PresentationDate <= selectedEndDate).ToList();
                PresentationInfosListView.ItemsSource = presListWithPeriodFilter.Distinct();
            }
            else if (IsOnlyShowSelected())
            {
                string selectedShowName = ShowListComboBox.Text;
                Show selectedShow = DisplayInformation.GetShowByName(selectedShowName);
                List<PresentationInfo> presentationInfoList = new List<PresentationInfo>();
                List<Presentation> allPresList = DisplayInformation.GetAllPresentationList();
                List<Presentation> selectedPresList = allPresList.Where(p => p.Show.ShowId == selectedShow.ShowId).ToList();
                foreach (Presentation pres in selectedPresList)
                {
                    PresentationInfo presInfoByShow = DisplayInformation.GetPresInfoByPres(pres);
                    presentationInfoList.Add(presInfoByShow);
                }
                PresentationInfosListView.ItemsSource = presentationInfoList.Distinct();
            }
            else if (IsOnlyPeriodSelected())
            {
                DateTime selectedStartDate = StartDatePicker.SelectedDate.Value.Date;
                DateTime selectedEndDate = EndDatePicker.SelectedDate.Value.Date;
                List<PresentationInfo> presentationInfoList = new List<PresentationInfo>();
                AllPresentationList = DisplayInformation.GetPresListByPeriod(selectedStartDate, selectedEndDate);
                foreach (Presentation pres in AllPresentationList)
                {
                    PresentationInfo presInfoByShow = DisplayInformation.GetPresInfoByPres(pres);
                    presentationInfoList.Add(presInfoByShow);
                }
                PresentationInfosListView.ItemsSource = presentationInfoList.Distinct();
            }
        }

        private bool IsShowAndPeriodSelected()
        {
            if (ShowListComboBox.SelectedItem != null && (StartDatePicker.SelectedDate != null && EndDatePicker.SelectedDate != null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsOnlyShowSelected()
        {
            if (ShowListComboBox.SelectedItem != null && (StartDatePicker.SelectedDate == null && EndDatePicker.SelectedDate == null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsOnlyPeriodSelected()
        {
            if (ShowListComboBox.SelectedItem == null && (StartDatePicker.SelectedDate != null && EndDatePicker.SelectedDate != null))
            {
                return true;
            }
            else { return false; }
        }

        private void Reset_btn_Click(object sender, RoutedEventArgs e)
        {
            PresInfoListDataBinding();
            ShowListComboBox.Text = String.Empty;
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
        }

        private void OrderNowButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserSingleton.GetInstance.user != null)
            {
                OrderWindow orderWindow = new OrderWindow(this);
                orderWindow.Show();
                orderWindow.Closed += (s, eventarg) =>
                {
                    PresInfoListDataBinding();
                    this.Activate();
                };
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("You have to sign in or register you first!", "User issue!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PresInfoListDataBinding()
        {
            PresentationInfosListView.ItemsSource = null;
            DisplayAllPresInfoList();
        }

        private void DisplayAllPresInfoList()
        {
            List<PresentationInfo> presentationInfoList = new List<PresentationInfo>();
            AllPresentationList = DisplayInformation.GetAllPresentationList();
            foreach (Presentation pres in AllPresentationList)
            {
                PresInfoByShow = DisplayInformation.GetPresInfoByPres(pres);
                presentationInfoList.Add(PresInfoByShow);
            }
            PresentationInfosListView.ItemsSource = presentationInfoList;
        }

        private void MyOrders_btn_Click(object sender, RoutedEventArgs e)
        {
            if (UserSingleton.GetInstance.user != null)
            {
                UserOrdersWindow userOrdersWindow = new UserOrdersWindow(this);
                userOrdersWindow.Show();
                userOrdersWindow.Closed += (s, eventarg) =>
                {
                    this.Activate();
                };
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("You have to sign in or register you first!", "User issue!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SignInsButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(this);
            loginWindow.Show();
            loginWindow.Closed += (s, eventarg) =>
            {
                this.Activate();
            };
        }

        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(this);
            registerWindow.Show();
            registerWindow.Closed += (s, eventarg) =>
            {
                this.Activate();
            };
        }
    }
}
