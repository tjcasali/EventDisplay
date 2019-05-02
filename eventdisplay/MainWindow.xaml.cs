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
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var web = new HtmlWeb();

            var startDate = DateTime.Now.ToString("yyyy-MM-dd");
            //var startDate = DateTime.Now.ToString("2019-05-01");

            var url = @"https://www.longwoodlibrary.org/events/upcoming?keywords=&start_date%5Bdate%5D=" + startDate + "&start_date%5Btime%5D=07%3A15&submit=Apply&form_build_id=form-dUYqRcLJz7dnsnovjYeTpbkFV5yDIKfyuHhJl6Ov_wo&form_id=lc_calendar_upcoming_form";
            var doc = web.Load(url);

            //For each upcoming event in the calendar
            foreach (HtmlNode item in doc.DocumentNode.SelectNodes("//div[@class='lc-event lc-event--upcoming']"))
            {
                EventDetails EventDetails = new EventDetails();

                //Name date and location are all stored in divs/spans that have class names so we can just select the node and grab the innerHtml below
                var eventName = item.SelectSingleNode(".//span[@class='field field--name-title field--type-string field--label-hidden']");
                var eventDate = item.SelectSingleNode(".//div[@class='lc-event-info__item lc-event-info__item--date visually-hidden']");
                var eventLocation = item.SelectSingleNode(".//div[@class='lc-event-info__item lc-event-info__item--categories']");

                //Time and Age Range are buried in a seperate div/span with no class, so we use two select nodes to grab the inner text.
                //We have to do this in a try catch because if an event doesn't have a Time or AgeRange then pointing to a null node crashes the program.
                try
                {
                    var eventTime = item.SelectSingleNode(".//div[@class='lc-event__date']");
                    HtmlNode eventTimeNode = eventTime.SelectSingleNode(".//div[2]");
                    EventDetails.Time = Convert.ToString(eventTimeNode.InnerText);

                    var eventAgeRange = item.SelectSingleNode(".//div[@class='lc-event__age-groups']");
                    HtmlNode eventAgeRangeNode = eventAgeRange.SelectSingleNode(".//span");
                    EventDetails.AgeRange = Convert.ToString(eventAgeRangeNode.InnerText);
                }
                catch
                {
                    //If either Time or AgeRange is null, we don't want to add the event to the EventList.
                    continue;
                }

                //Convert the HtmlNodes to the strings of their innerHtml. We have to trim Room because it has two line breaks in it.
                EventDetails.Name = Convert.ToString(eventName.InnerHtml);
                EventDetails.Room = Convert.ToString(eventLocation.InnerHtml).Trim();
                EventDetails.Date = Convert.ToString(eventDate.InnerHtml);

                
                //If any of the remaining values are null, we don't want to add it to the event list, so we skip the event add.
                if (EventDetails.Name == null || EventDetails.Time == null || EventDetails.Date == null)
                {
                    continue;
                }
                //If the event is cancelled they'll usually just add "***CANCELLED***" to the event name, this skips events with those.
                else if (EventDetails.Name.Contains("CANCELLED") || EventDetails.Name.Contains("Cancelled"))
                {
                    continue;
                }
                else
                {
                    eventList.Add(EventDetails);
                }
            }
            //AllEventsButton_Click(sender, new RoutedEventArgs());
        }


        private void AllEventsButton_Click(object sender, RoutedEventArgs e)
        {
            AllEventsWindow aew = new AllEventsWindow();

            //We want to split our event list into different age groups so we can seperate them and sort them by age range
            List<EventDetails> AdultPrograms = new List<EventDetails>();
            List<EventDetails> TeenPrograms = new List<EventDetails>();
            List<EventDetails> ChildrenPrograms = new List<EventDetails>();

            string todaysDateString = DateTime.Today.ToString("MMMM d, yyyy");
            //string todaysDateString = DateTime.Today.ToString("May 1, 2019");
            aew.AEWDateTB.Text = todaysDateString;

            foreach (EventDetails ev in eventList)
            { 
                //Change the apostraphe hex code back into the apostraphe
                if (ev.Name.Contains("&#039;"))
                    ev.Name = ev.Name.Replace("&#039;", "'");

                //Change the ampersand hex code back into the ampersand
                if (ev.Name.Contains("&amp;"))
                    ev.Name = ev.Name.Replace("&amp;", "&");

                //Change the double quote hex code back into the double quote
                if (ev.Name.Contains("&quot;"))
                    ev.Name = ev.Name.Replace("&quot;", "\"");

                //If the event had the Adult age range and occurs on todays date, add to the AdultPrograms list
                if (ev.AgeRange == "Adult")
                    if(ev.Date == todaysDateString)
                        AdultPrograms.Add(ev);

                //If the event had the Teen age range and occurs on todays date, add to the TeenPrograms list
                if (ev.AgeRange == "Teen")
                    if (ev.Date == todaysDateString)
                        TeenPrograms.Add(ev);

                //There are three different age ranges that correspond to Childrens programs. If the the age range is any of the three listed below, and it's on todays date, add it to ChildrensPrograms
                if (ev.AgeRange == "School Age" || ev.AgeRange == "Infant/Toddler/Preschool" || ev.AgeRange == "Family" || ev.AgeRange == "Infant/Toddler/Preschool, School Age, Family")
                    if (ev.Date == todaysDateString)
                        ChildrenPrograms.Add(ev);
             }

            //Header that will indicate the start of the Children's Programs. Will be bolded in the list view
            ListViewHeaders childHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Children's Programs",
                Room = "Location"
            };

            //Header that will indicate the start of the Teens Programs. Will be bolded in the list view
            ListViewHeaders teenHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Teen Programs",
                Room = "Location"
            };

            //Header that will indicate the start of the Adults Programs. Will be bolded in the list view
            ListViewHeaders adultHeader = new ListViewHeaders
            {
                Time = "Time",
                Name = "Adult Programs",
                Room = "Location"
            };

            //We need a blank item that we can insert at the end of each of the program age ranges.
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

            //If we have no events. We don't want to just display a blank list. So this changes the background image to a welcome message.
            ImageBrush noEventsImageBrush = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), @"/Images/NoEventsBackground.jpg")));

            //If there are no events: Change background, and hide all elements
            if (AdultPrograms.Count == 0 && TeenPrograms.Count == 0 && ChildrenPrograms.Count == 0)
            {
                aew.Background = noEventsImageBrush;
                aew.AEWDateTB.Visibility = Visibility.Hidden;
                aew.AEWTodaysEventsTB.Visibility = Visibility.Hidden;
                aew.AEWListView.Visibility = Visibility.Hidden;
                aew.Show();
            }
            else
            {
                aew.Show();
            }
        }


        private void CommunityRoomEvents_Click(object sender, RoutedEventArgs e)
        {
            CommunityEventsWindow cew = new CommunityEventsWindow();

            List<EventDetails> AdultPrograms = new List<EventDetails>();
            List<EventDetails> TeenPrograms = new List<EventDetails>();
            List<EventDetails> ChildrenPrograms = new List<EventDetails>();

            string todaysDateString = DateTime.Today.ToString("MMMM d, yyyy");
            //string todaysDateString = DateTime.Today.ToString("April 12, 2019");
            cew.CEWDateTB.Text = todaysDateString;

            foreach (EventDetails ev in eventList)
            {
                if (ev.Name.Contains("&#039;"))
                    ev.Name = ev.Name.Replace("&#039;", "'");

                if (ev.Name.Contains("&amp;"))
                    ev.Name = ev.Name.Replace("&amp;", "&");

                if (ev.Name.Contains("&quot;"))
                    ev.Name = ev.Name.Replace("&quot;", "\"");

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
                Name = "Teen Programs",
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

            string todaysDateString = DateTime.Today.ToString("MMMM d, yyyy");
            //string todaysDateString = DateTime.Today.ToString("April 12, 2019");
            kew.KEWDateTB.Text = todaysDateString;

            foreach (EventDetails ev in eventList)
            {
                if (ev.Name.Contains("&#039;"))
                    ev.Name = ev.Name.Replace("&#039;", "'");

                if (ev.Name.Contains("&amp;"))
                    ev.Name = ev.Name.Replace("&amp;", "&");

                if (ev.Name.Contains("&quot;"))
                    ev.Name = ev.Name.Replace("&quot;", "\"");

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
                Name = "Teen Programs",
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

            kew.Show();
        }


        private void ClemensRoomEvents_Click(object sender, RoutedEventArgs e)
        {
            ClemensEventsWindow cew = new ClemensEventsWindow();

            List<EventDetails> AdultPrograms = new List<EventDetails>();
            List<EventDetails> TeenPrograms = new List<EventDetails>();
            List<EventDetails> ChildrenPrograms = new List<EventDetails>();

            string todaysDateString = DateTime.Today.ToString("MMMM d, yyyy");
            //string todaysDateString = DateTime.Today.ToString("April 12, 2019");
            cew.CEWDateTB.Text = todaysDateString;

            foreach (EventDetails ev in eventList)
            {
                if (ev.Name.Contains("&#039;"))
                    ev.Name = ev.Name.Replace("&#039;", "'");

                if (ev.Name.Contains("&amp;"))
                    ev.Name = ev.Name.Replace("&amp;", "&");

                if (ev.Name.Contains("&quot;"))
                    ev.Name = ev.Name.Replace("&quot;", "\"");

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
                Name = "Teen Programs",
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

            cew.Show();
        }

    }
}
