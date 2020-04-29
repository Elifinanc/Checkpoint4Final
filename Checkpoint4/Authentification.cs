using System.Linq;

namespace Checkpoint4
{
    public static class Authentification
    {
        public static bool Authentify(Person currentPerson)
        {
            using (var context = new ShowContext())
            {
                var getNameAndPassword = from p in context.Persons
                                         where currentPerson.Name == p.Name && currentPerson.Password == p.Password
                                         select new { p.Name, p.Password };

                if (getNameAndPassword.Count() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
