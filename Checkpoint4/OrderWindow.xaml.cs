using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Checkpoint4
{
    public partial class OrderWindow : Window
    {
        int CurrentPresAvailableSeats { get; set; }
        PresentationInfo SelectedPresInfo { get; set; }

        public OrderWindow(MainWindow owner)
        {
            InitializeComponent();
            this.Owner = owner;
            DataContext = owner.PresentationInfosListView.SelectedItem;
            SelectedPresInfo = (PresentationInfo)DataContext;
            string selectedShowName = SelectedPresInfo.ShowName;
            ShowNameTextBlock.Text = selectedShowName;
            string selectedPresDate = SelectedPresInfo.PresentationDate.ToString("MM/dd/yyyy");
            PresentationDateTextBlock.Text = selectedPresDate;
            CurrentPresAvailableSeats = SelectedPresInfo.AvailableSeatsCount;
            AvailableSeatsTextBlock.Text = CurrentPresAvailableSeats.ToString();
        }

        private void BtnValidate_Click(object sender, RoutedEventArgs e)
        {
            if (SeatsQuantityTextBox.Text != "")
            {
                int currentItemQuantity = Convert.ToInt32(SeatsQuantityTextBox.Text);

                if (currentItemQuantity > 0 && currentItemQuantity <= CurrentPresAvailableSeats)
                {
                    using (var context = new ShowContext())
                    {
                        string selectedShowName = ShowNameTextBlock.Text;
                        Show selectedShow = DisplayInformation.GetShowByName(selectedShowName);
                        Presentation presentation = DisplayInformation.GetPresByPresDateAndShow(SelectedPresInfo.PresentationDate, selectedShow);
                        List<Order> presOrder = DisplayInformation.GetOrderListByPresentation(presentation);
                        Order newOrder = new Order()
                        {
                            OrderDate = DateTime.Now,
                            Person = UserSingleton.GetInstance.user,
                            Presentation = presentation,
                            ReservedSeats = Convert.ToInt32(SeatsQuantityTextBox.Text)
                        };
                        presOrder.Add(newOrder);

                        context.Update(newOrder);
                        context.SaveChanges();

                        MessageBox.Show("Your order is validated");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("The quantity has to be between 1 and " + CurrentPresAvailableSeats + " !");
                }
            }
            else
            {
                MessageBox.Show("You must enter a seats quantity to validate!");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
