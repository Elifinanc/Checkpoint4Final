using System;
using System.Collections.Generic;
using System.Linq;
using Checkpoint4;

namespace Checkpoint4_DataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ShowContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var Show1 = new Show
                {
                    Name = "Lions Show",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30)
                };
                var Show2 = new Show
                {
                    Name = "Elephants Show",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(30)
                };
                var Show3 = new Show
                {
                    Name = "Monkeys Show",
                    StartDate = DateTime.Now.AddDays(15),
                    EndDate = DateTime.Now.AddDays(45)
                };
                var Show4 = new Show
                {
                    Name = "Parrots Show",
                    StartDate = DateTime.Now.AddDays(15),
                    EndDate = DateTime.Now.AddDays(45)
                };
                var Show5 = new Show
                {
                    Name = "Acrobats Show",
                    StartDate = DateTime.Now.AddDays(30),
                    EndDate = DateTime.Now.AddDays(60)
                };
                var Show6 = new Show
                {
                    Name = "Jugglers Show",
                    StartDate = DateTime.Now.AddDays(30),
                    EndDate = DateTime.Now.AddDays(60)
                };

                List<Show> showList = new List<Show>();
                showList.Add(Show1);
                showList.Add(Show2);
                showList.Add(Show3);
                showList.Add(Show4);
                showList.Add(Show5);
                showList.Add(Show6);

                List<Room> roomList = GenerateRoom(2);
                context.AddRange(roomList);

                List<Person> personList = new List<Person>();
                var manyPersons = (from i in Enumerable.Range(0, 3)
                                   select new Person { Name = "Bob" + i, Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" }).ToList();
                personList.AddRange(manyPersons);

                var manyPersons2 = (from i in Enumerable.Range(0, 3)
                                   select new Person { Name = "Emy" + i, Password = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" }).ToList();
                personList.AddRange(manyPersons2);
                context.AddRange(personList);

                List<Presentation> presList = new List<Presentation>();
                for (int i = 0; i < showList.Count(); i++)
                {
                    if (i % 2 == 0)
                    {
                        showList[i].Presentations = GeneratePresentation(showList[i], 6, roomList[0]);
                        presList.AddRange(showList[i].Presentations);
                    }
                    else
                    {
                        showList[i].Presentations = GeneratePresentation(showList[i], 6, roomList[1]);
                        presList.AddRange(showList[i].Presentations);
                    }
                }

                Random random = new Random();
                
                List<Order> orderList = new List<Order>();
                foreach (Person person in personList)
                {
                    int numberRdm = random.Next(0, 35);
                    int numberRdm2 = random.Next(1, 5);
                    List<Order> orderList1 = GenerateOrder(person, 6, numberRdm2);
                    orderList.AddRange(orderList1);
                }

                for (int i = 0; i < orderList.Count; i++)
                {
                    orderList[i].Presentation = presList[i];
                }
                context.AddRange(orderList);

                for (int i = 0; i < presList.Count; i++)
                {
                    presList[i].Orders.Add(orderList[i]);
                }
                context.AddRange(presList);

                context.AddRange(showList);
                context.SaveChanges();
            }
        }

        static List<Room> GenerateRoom(int numberOfRoom)
        {
            List<Room> rooms = new List<Room>();
            Random random = new Random();
            for (int i = 1; i <= numberOfRoom; i++)
            {
                var newRoom = new Room();
                int numberRdm = random.Next(20, 30);
                newRoom.Name = "Room" + i;
                newRoom.TotalSeats = numberRdm + i;
                rooms.Add(newRoom);
            }
            return rooms;
        }

        static List<Presentation> GeneratePresentation(Show show, int numberOfPresentation, Room room)
        {
            List<Presentation> presentationList = new List<Presentation>();
            for (int i = 0; i < numberOfPresentation; i++)
            {
                var newPresentation = new Presentation();
                newPresentation.Room = room;
                newPresentation.Show = show;
                newPresentation.PresentationDate = show.StartDate.AddDays(i * 5);
                presentationList.Add(newPresentation);
            }
            return presentationList;
        }

        static List<Order> GenerateOrder(Person person, int numberOfOrder, int seatCount)
        {
            List<Order> orders = new List<Order>();
            for (int i = 1; i <= numberOfOrder; i++)
            {
                var newOrder = new Order();
                newOrder.OrderDate = DateTime.Today.AddDays(i);
                newOrder.Person = person;
                newOrder.ReservedSeats = seatCount;
                orders.Add(newOrder);
            }
            return orders;
        }
    }
}
