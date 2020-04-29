using System.Windows;
using System.Windows.Forms;
using System.Linq;

namespace Checkpoint4
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(MainWindow owner)
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Person newPerson = new Person { Name = UserNameTextBox.Text, Password = Sha256Tools.GetHash(PasswordTextBox.Password) };
            if (Authentification.Authentify(newPerson))
            {
                using(var context = new ShowContext())
                {
                    UserSingleton.GetInstance.user = context.Persons.Where(x => x.Name == newPerson.Name).FirstOrDefault();
                }
                System.Windows.MessageBox.Show("You have log yourself!");
                this.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Username or password invalid", "User issue!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
