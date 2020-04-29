using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Forms;

namespace Checkpoint4
{
    public partial class UserOrdersWindow : Window
    {
        public UserOrdersWindow(MainWindow owner)
        {
            InitializeComponent();
            this.Owner = owner;
            Person selectedUser = UserSingleton.GetInstance.user;
            UserNameTextBlock.Text = selectedUser.Name;

            List<Order> allOrdersList = DisplayInformation.GetAllDefaultOrders();
            List<Order> selectedUserOrderList = allOrdersList.Where(o => o.Person.PersonId == selectedUser.PersonId).ToList();
            List<Presentation> presList = new List<Presentation>();
            foreach (Order order in selectedUserOrderList)
            {
                Presentation pres = order.Presentation;
                presList.Add(pres);
            }
            List<OrderInfo> presInfoList = new List<OrderInfo>();
            foreach (Presentation pres in presList)
            {
                OrderInfo PresInfoByShow = DisplayInformation.GetOrderInfoByPres(pres);
                presInfoList.Add(PresInfoByShow);
            }
            PresentationInfosListView.ItemsSource = presInfoList;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
