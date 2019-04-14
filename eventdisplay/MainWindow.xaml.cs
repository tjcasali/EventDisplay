using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<EventDetails> eventList = new List<EventDetails>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var web = new HtmlWeb();

            var startDate = DateTime.Now.ToString("yyyy-MM-dd");
            //var startDate = DateTime.Now.ToString("2019-04-15");
            var url = @"https://www.longwoodlibrary.org/events/upcoming?keywords=&start_date%5Bdate%5D=" + startDate + "&start_date%5Btime%5D=07%3A15&submit=Apply&form_build_id=form-dUYqRcLJz7dnsnovjYeTpbkFV5yDIKfyuHhJl6Ov_wo&form_id=lc_calendar_upcoming_form";

            var doc = web.Load(url);


            foreach (HtmlNode item in doc.DocumentNode.SelectNodes("//div[@class='lc-event lc-event--upcoming']"))
            {
                EventDetails EventDetails = new EventDetails();

                var eventName = item.SelectSingleNode(".//span[@class='field field--name-title field--type-string field--label-hidden']");
                var eventDate = item.SelectSingleNode(".//div[@class='lc-event-info__item lc-event-info__item--date visually-hidden']");
                var eventTime = item.SelectSingleNode(".//div[@class='lc-event__date']");
                HtmlNode eventTimeNode = eventTime.SelectSingleNode(".//div[2]");
                var eventLocation = item.SelectSingleNode(".//div[@class='lc-event-info__item lc-event-info__item--categories']");
                var eventAgeRange = item.SelectSingleNode(".//div[@class='lc-event__age-groups']");
                HtmlNode eventAgeRangeNode = eventAgeRange.SelectSingleNode(".//span");

                EventDetails.Name = Convert.ToString(eventName.InnerHtml);
                EventDetails.Time = Convert.ToString(eventTimeNode.InnerText);
                EventDetails.Room = Convert.ToString(eventLocation.InnerHtml).Trim();
                EventDetails.AgeRange = Convert.ToString(eventAgeRangeNode.InnerText);
                EventDetails.Date = Convert.ToString(eventDate.InnerHtml);

                eventList.Add(EventDetails);
            }
        }


        private void AllEventsButton_Click(object sender, RoutedEventArgs e)
        {
            AllEventsWindow aew = new AllEventsWindow();

            List<EventDetails> AdultPrograms = new List<EventDetails>();
            List<EventDetails> TeenPrograms = new List<EventDetails>();
            List<EventDetails> ChildrenPrograms = new List<EventDetails>();

            string todaysDateString = DateTime.Today.ToString("MMMM dd, yyyy");
            //string todaysDateString = DateTime.Today.ToString("April 12, 2019");
            aew.AEWDateTB.Text = todaysDateString;

            foreach (EventDetails ev in eventList)
            { 
                if (ev.Name.Contains("&#039;"))
                    ev.Name = ev.Name.Replace("&#039;", "'");

                if (ev.Name.Contains("&amp;"))
                    ev.Name = ev.Name.Replace("&amp;", "&");

                if (ev.AgeRange == "Adult")
                    if(ev.Date == todaysDateString)
                        AdultPrograms.Add(ev);

                if (ev.AgeRange == "Teen")
                    if (ev.Date == todaysDateString)
                        TeenPrograms.Add(ev);
                if (ev.AgeRange == "School Age" || ev.AgeRange == "Infant/Toddler/Preschool" || ev.AgeRange == "Family" || ev.AgeRange == "Infant/Toddler/Preschool, School Age, Family")
                    if (ev.Date == todaysDateString)
                        ChildrenPrograms.Add(ev);
             }

            ListViewHeaders childHeader = new ListViewHeaders();
            childHeader.Time = "Time";
            childHeader.Name = "Children's Programs";
            childHeader.Room = "Location";

            ListViewHeaders teenHeader = new ListViewHeaders();
            teenHeader.Time = "Time";
            teenHeader.Name = "Teens Programs";
            teenHeader.Room = "Location";

            ListViewHeaders adultHeader = new ListViewHeaders();
            adultHeader.Time = "Time";
            adultHeader.Name = "Adult Programs";
            adultHeader.Room = "Location";

            ListViewHeaders blankLine = new ListViewHeaders();
            
            

            if (ChildrenPrograms.Count != 0)
            {
                aew.AEWListView.Items.Add(childHeader);
                foreach (EventDetails ev in ChildrenPrograms)
                {
                    if (ev.Date == todaysDateString)
                    {
                        aew.AEWListView.Items.Add(ev);
                    }
                }
                aew.AEWListView.Items.Add(blankLine);
            }

            if (TeenPrograms.Count != 0)
            {
                aew.AEWListView.Items.Add(teenHeader);
                foreach (EventDetails ev in TeenPrograms)
                {
                    if (ev.Date == todaysDateString)
                    {
                        aew.AEWListView.Items.Add(ev);
                    }
                }
                aew.AEWListView.Items.Add(blankLine);
            }

            if (AdultPrograms.Count != 0)
            {
                aew.AEWListView.Items.Add(adultHeader);
                foreach (EventDetails ev in AdultPrograms)
                {
                    if (ev.Date == todaysDateString)
                    {
                        aew.AEWListView.Items.Add(ev);
                    }
                }
                aew.AEWListView.Items.Add(blankLine);
            }

            aew.Show();
        }


        private void CommunityRoomEvents_Click(object sender, RoutedEventArgs e)
        {
            CommunityEventsWindow cew = new CommunityEventsWindow();

            List<EventDetails> AdultPrograms = new List<EventDetails>();
            List<EventDetails> TeenPrograms = new List<EventDetails>();
            List<EventDetails> ChildrenPrograms = new List<EventDetails>();

            string todaysDateString = DateTime.Today.ToString("MMMM dd, yyyy");
            //string todaysDateString = DateTime.Today.ToString("April 12, 2019");
            cew.CEWDateTB.Text = todaysDateString;

            foreach (EventDetails ev in eventList)
            {
                if (ev.Name.Contains("&#039;"))
                    ev.Name = ev.Name.Replace("&#039;", "'");

                if (ev.Name.Contains("&amp;"))
                    ev.Name = ev.Name.Replace("&amp;", "&");

                if (ev.AgeRange == "Adult" && ev.Date == todaysDateString && ev.Room == "Community Room")
                            AdultPrograms.Add(ev);

                if (ev.AgeRange == "Teen" && ev.Date == todaysDateString && ev.Room == "Community Room")
                        TeenPrograms.Add(ev);

                if (ev.AgeRange == "School Age" || ev.AgeRange == "Infant/Toddler/Preschool" || ev.AgeRange == "Family" || ev.AgeRange == "Infant/Toddler/Preschool, School Age, Family")
                    if (ev.Date == todaysDateString && ev.Room == "Community Room")
                            ChildrenPrograms.Add(ev);
            }

            ListViewHeaders childHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Children's Programs",
                Room = "Location"
            };

            ListViewHeaders teenHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Teens Programs",
                Room = "Location"
            };

            ListViewHeaders adultHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Adult Programs",
                Room = "Location"
            };

            ListViewHeaders blankLine = new ListViewHeaders();



            if (ChildrenPrograms.Count != 0)
            {
                cew.CEWListView.Items.Add(childHeader);
                foreach (EventDetails ev in ChildrenPrograms)
                {
                    if (ev.Date == todaysDateString && ev.Room == "Community Room")
                        cew.CEWListView.Items.Add(ev);
                }
                cew.CEWListView.Items.Add(blankLine);
            }

            if (TeenPrograms.Count != 0)
            {
                cew.CEWListView.Items.Add(teenHeader);
                foreach (EventDetails ev in TeenPrograms)
                {
                    if (ev.Date == todaysDateString && ev.Room == "Community Room")
                            cew.CEWListView.Items.Add(ev);
                }
                cew.CEWListView.Items.Add(blankLine);
            }

            if (AdultPrograms.Count != 0)
            {
                cew.CEWListView.Items.Add(adultHeader);
                foreach (EventDetails ev in AdultPrograms)
                {
                    if (ev.Date == todaysDateString && ev.Room == "Community Room")
                            cew.CEWListView.Items.Add(ev);
                }
                cew.CEWListView.Items.Add(blankLine);
            }

            cew.Show();
        }

        private void KovarikRoomEvents_Click(object sender, RoutedEventArgs e)
        {
            KovarikEventsWindow kew = new KovarikEventsWindow();

            List<EventDetails> AdultPrograms = new List<EventDetails>();
            List<EventDetails> TeenPrograms = new List<EventDetails>();
            List<EventDetails> ChildrenPrograms = new List<EventDetails>();

            string todaysDateString = DateTime.Today.ToString("MMMM dd, yyyy");
            //string todaysDateString = DateTime.Today.ToString("April 12, 2019");
            kew.KEWDateTB.Text = todaysDateString;

            foreach (EventDetails ev in eventList)
            {
                if (ev.Name.Contains("&#039;"))
                    ev.Name = ev.Name.Replace("&#039;", "'");

                if (ev.Name.Contains("&amp;"))
                    ev.Name = ev.Name.Replace("&amp;", "&");

                if (ev.AgeRange == "Adult" && ev.Date == todaysDateString && ev.Room == "Kovarik Room")
                    AdultPrograms.Add(ev);

                if (ev.AgeRange == "Teen" && ev.Date == todaysDateString && ev.Room == "Kovarik Room")
                    TeenPrograms.Add(ev);

                if (ev.AgeRange == "School Age" || ev.AgeRange == "Infant/Toddler/Preschool" || ev.AgeRange == "Family" || ev.AgeRange == "Infant/Toddler/Preschool, School Age, Family")
                    if (ev.Date == todaysDateString && ev.Room == "Kovarik Room")
                        ChildrenPrograms.Add(ev);
            }

            ListViewHeaders childHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Children's Programs",
                Room = "Location"
            };

            ListViewHeaders teenHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Teens Programs",
                Room = "Location"
            };

            ListViewHeaders adultHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Adult Programs",
                Room = "Location"
            };

            ListViewHeaders blankLine = new ListViewHeaders();



            if (ChildrenPrograms.Count != 0)
            {
                kew.KEWListView.Items.Add(childHeader);
                foreach (EventDetails ev in ChildrenPrograms)
                {
                    if (ev.Date == todaysDateString && ev.Room == "Kovarik Room")
                        kew.KEWListView.Items.Add(ev);
                }
                kew.KEWListView.Items.Add(blankLine);
            }

            if (TeenPrograms.Count != 0)
            {
                kew.KEWListView.Items.Add(teenHeader);
                foreach (EventDetails ev in TeenPrograms)
                {
                    if (ev.Date == todaysDateString && ev.Room == "Kovarik Room")
                        kew.KEWListView.Items.Add(ev);
                }
                kew.KEWListView.Items.Add(blankLine);
            }

            if (AdultPrograms.Count != 0)
            {
                kew.KEWListView.Items.Add(adultHeader);
                foreach (EventDetails ev in AdultPrograms)
                {
                    if (ev.Date == todaysDateString && ev.Room == "Kovarik Room")
                        kew.KEWListView.Items.Add(ev);
                }
                kew.KEWListView.Items.Add(blankLine);
            }
        }

        private void ClemensRoomEvents_Click(object sender, RoutedEventArgs e)
        {
            ClemensEventsWindow cew = new ClemensEventsWindow();

            List<EventDetails> AdultPrograms = new List<EventDetails>();
            List<EventDetails> TeenPrograms = new List<EventDetails>();
            List<EventDetails> ChildrenPrograms = new List<EventDetails>();

            string todaysDateString = DateTime.Today.ToString("MMMM dd, yyyy");
            //string todaysDateString = DateTime.Today.ToString("April 12, 2019");
            cew.CEWDateTB.Text = todaysDateString;

            foreach (EventDetails ev in eventList)
            {
                if (ev.Name.Contains("&#039;"))
                    ev.Name = ev.Name.Replace("&#039;", "'");

                if (ev.Name.Contains("&amp;"))
                    ev.Name = ev.Name.Replace("&amp;", "&");

                if (ev.AgeRange == "Adult" && ev.Date == todaysDateString && ev.Room == "Clemens Room")
                    AdultPrograms.Add(ev);

                if (ev.AgeRange == "Teen" && ev.Date == todaysDateString && ev.Room == "Clemens Room")
                    TeenPrograms.Add(ev);

                if (ev.AgeRange == "School Age" || ev.AgeRange == "Infant/Toddler/Preschool" || ev.AgeRange == "Family" || ev.AgeRange == "Infant/Toddler/Preschool, School Age, Family")
                    if (ev.Date == todaysDateString && ev.Room == "Clemens Room")
                        ChildrenPrograms.Add(ev);
            }

            ListViewHeaders childHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Children's Programs",
                Room = "Location"
            };

            ListViewHeaders teenHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Teens Programs",
                Room = "Location"
            };

            ListViewHeaders adultHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Adult Programs",
                Room = "Location"
            };

            ListViewHeaders blankLine = new ListViewHeaders();



            if (ChildrenPrograms.Count != 0)
            {
                cew.CEWListView.Items.Add(childHeader);
                foreach (EventDetails ev in ChildrenPrograms)
                {
                    if (ev.Date == todaysDateString && ev.Room == "Clemens Room")
                        cew.CEWListView.Items.Add(ev);
                }
                cew.CEWListView.Items.Add(blankLine);
            }

            if (TeenPrograms.Count != 0)
            {
                cew.CEWListView.Items.Add(teenHeader);
                foreach (EventDetails ev in TeenPrograms)
                {
                    if (ev.Date == todaysDateString && ev.Room == "Clemens Room")
                        cew.CEWListView.Items.Add(ev);
                }
                cew.CEWListView.Items.Add(blankLine);
            }

            if (AdultPrograms.Count != 0)
            {
                cew.CEWListView.Items.Add(adultHeader);
                foreach (EventDetails ev in AdultPrograms)
                {
                    if (ev.Date == todaysDateString && ev.Room == "Clemens Room")
                        cew.CEWListView.Items.Add(ev);
                }
                cew.CEWListView.Items.Add(blankLine);
            }
        }

    }
}
