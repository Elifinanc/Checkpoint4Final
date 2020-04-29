using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Checkpoint4
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow(MainWindow owner)
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Person newPerson = new Person { Name = UserNameTextBox.Text, Password = Sha256Tools.GetHash(PasswordTextBox.Password) };
            if (Authentification.Authentify(newPerson) && UserNameTextBox.Text != "" && PasswordTextBox.Password != "")
            {
                System.Windows.Forms.MessageBox.Show("You are already register, please use the sign in button", "User issue!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!Authentification.Authentify(newPerson) && UserNameTextBox.Text != "" && PasswordTextBox.Password != "")
            {
                using (var context = new ShowContext())
                {
                    context.Add(newPerson);
                    UserSingleton.GetInstance.user = newPerson;
                    context.SaveChanges();
                    System.Windows.Forms.MessageBox.Show("You are registered now");
                    this.Close();
                }
            }
            else if (UserNameTextBox.Text == "" || PasswordTextBox.Password == "")
            {
                System.Windows.Forms.MessageBox.Show("You have to fill both boxes", "User issue!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
