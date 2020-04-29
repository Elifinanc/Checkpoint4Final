using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkpoint4
{
    public static class DisplayInformation
    {
        public static List<Show> GetAllDefaultShows()
        {
            using (var context = new ShowContext())
            {
                var defaultShowList = (from s in context.Shows
                                      select s).ToList();
                return defaultShowList;
            }
        }

        public static List<Order> GetAllDefaultOrders()
        {
            using (var context = new ShowContext())
            {
                var orders = (from o in context.Orders
                                  select o).ToList();
                var users = (from u in context.Persons
                             select u).ToList();
                var presentations = (from p in context.Presentations
                                     select p).ToList();
                var orderList = (from o in orders
                                 join u in users on o.Person equals u
                                 join p in presentations on o.Presentation equals p
                                 select o).ToList();
                return orderList;
            }
        }

        public static List<Presentation> GetAllPresentationList()
        {
            using (var context = new ShowContext())
            {
                var defaultPresentationListTemp = (from p in context.Presentations
                                              select p).ToList();
                var rooms = (from r in context.Rooms
                            select r).ToList();
                var shows = (from s in context.Shows
                             select s).ToList();
                var orders = (from o in context.Orders
                              select o).ToList();

                List<Presentation> defaultPresentationList = (from d in defaultPresentationListTemp
                                                              join r in rooms on d.Room equals r
                                                              join s in shows on d.Show equals s
                                                              join o in orders on d.Orders.ElementAt(0) equals o
                                                              select d).ToList();
                return defaultPresentationList;
            }
        }

        public static int GetAllOrdersSeatsCountByPresentation(Presentation presentation)
        {
            using (var context = new ShowContext())
            {
                var orderList = (from o in context.Orders
                                 where o.Presentation == presentation
                                 select o.ReservedSeats).Count();
                return orderList;
            }
        }

        public static int GetAllTotalSeatsCountByPresentation(Presentation presentation)
        {
            using (var context = new ShowContext())
            {
                var orderList = (from r in context.Rooms
                                 join p in context.Presentations
                                 on r.RoomId equals presentation.Room.RoomId
                                 select r.TotalSeats).FirstOrDefault();
                return orderList;
            }
        }

        public static Show GetShowByName(string showName)
        {
            using (var context = new ShowContext())
            {
                var selectedShowTemp = (from s in context.Shows
                                       select s).ToList();
                var presentations = (from p in context.Presentations
                                    select p).ToList();
                var selectedShow = (from p in presentations
                                    join s in selectedShowTemp on p.Show equals s
                                    where s.Name == showName
                                    select s).FirstOrDefault();
                return selectedShow;
            }
        }

        public static List<Presentation> GetPresListByPeriod(DateTime startDate, DateTime endDate)
        {
            using (var context = new ShowContext())
            {
                var presListByPeriod= (from p in context.Presentations
                                      where p.PresentationDate >= startDate && p.PresentationDate <= endDate
                                      select p).ToList();
                return presListByPeriod;
            }
        }

        public static List<Presentation> GetPresListByAvailableSeatCount(int seatCount)
        {
            using (var context = new ShowContext())
            {
                var presListBySeatCountTemp = (from p in context.Presentations
                                              select p).ToList();
                var presInfos = (from pi in context.PresentationInfos
                                 select pi).ToList();
                var presListBySeatCount = (from p in presListBySeatCountTemp
                                           join pi in presInfos on p.PresentationId.ToString() equals pi.PresentationId
                                           where pi.AvailableSeatsCount == seatCount
                                           select p).ToList();
                return presListBySeatCount;
            }
        }

        public static Presentation GetPresByPresDateAndShow(DateTime presDate, Show show)
        {
            using (var context = new ShowContext())
            {
                var presentation = (from p in context.Presentations
                                        join s in context.Shows
                                        on p.Show.ShowId equals show.ShowId
                                        where p.PresentationDate == presDate
                                        select p).FirstOrDefault();
                return presentation;
            }
        }

        public static List<Order> GetOrderListByPresentation(Presentation pres)
        {
            using (var context = new ShowContext())
            {
                var orderListByPres = (from o in context.Orders
                                    join p in context.Presentations
                                    on o.Presentation.PresentationId equals pres.PresentationId
                                    select o).ToList();
                return orderListByPres;
            }
        }

        public static PresentationInfo GetPresInfoByPres(Presentation pres)
        {
            using (var context = new ShowContext())
            {
                List<Presentation> presentations = (from p in context.Presentations
                                                    select p).ToList();
                List<Show> shows = (from s in context.Shows
                                    select s).ToList();
                List<Order> orders = (from o in context.Orders
                                      select o).ToList();
                List<Room> rooms = (from r in context.Rooms
                                    select r).ToList();
                var presInfo = (from p in presentations
                                join s in shows on p.Show.ShowId equals s.ShowId
                                join o in orders on p.PresentationId equals o.Presentation.PresentationId
                                join r in rooms on p.Room.RoomId equals r.RoomId
                                where p.PresentationId == pres.PresentationId
                                select new { s.Name, p.PresentationDate, o.ReservedSeats, r.TotalSeats }).ToList();

                PresentationInfo presInfosLine = presInfo.GroupBy(x => x.Name)
                                                .Select(s => new PresentationInfo
                                                {
                                                    ShowName = s.First().Name,
                                                    PresentationDate = s.First().PresentationDate,
                                                    ReservedSeatCount = s.Sum(o => o.ReservedSeats),
                                                    TotalSeatCount = s.First().TotalSeats,
                                                    AvailableSeatsCount = s.First().TotalSeats - s.Sum(o => o.ReservedSeats)
                                                }).FirstOrDefault();
                return presInfosLine;
            }
        }

        public static OrderInfo GetOrderInfoByPres(Presentation presentation)
        {
            using (var context = new ShowContext())
            {
                List<Presentation> presentations = (from p in context.Presentations
                                                    select p).ToList();
                List<Show> shows = (from s in context.Shows
                                    select s).ToList();
                List<Order> orders = (from o in context.Orders
                                        select o).ToList();

                var orderInfo = (from p in presentations
                                    join s in shows on p.Show.ShowId equals s.ShowId
                                    join o in orders on p.PresentationId equals o.Presentation.PresentationId
                                    where p.PresentationId == presentation.PresentationId
                                    select new { s.Name, p.PresentationDate, o.ReservedSeats }).ToList();

                OrderInfo orderInfosLine = orderInfo.GroupBy(x => x.Name)
                                                .Select(s => new OrderInfo
                                                {
                                                    ShowName = s.First().Name,
                                                    PresentationDate = s.First().PresentationDate,
                                                    ReservedSeats = s.First().ReservedSeats,
                                                }).FirstOrDefault();
                return orderInfosLine;
            }
        }

        public static List<Order> GetOrdersByLogin(string login)
        {
            var context = new ShowContext();
            List<Person> persons = (from p in context.Persons
                                    select p).ToList();
            Person currentPerson = context.Persons.Where(p => p.Name == login).FirstOrDefault();
            List<Order> resultOrders = context.Orders.Where(o => o.Person == currentPerson).ToList();
            return resultOrders;
        }
    }
}
