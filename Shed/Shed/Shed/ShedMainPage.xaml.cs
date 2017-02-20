using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Shed
{
    public partial class ShedMainPage : ContentPage
    {
        public ShedMainPage()
        {
            InitializeComponent();
            List<Jobs> P = new List<Jobs>();
            P.Add(new Jobs("Fix Tap", "Tap is leaking and it needs to be fixed.", "In Progress", DateTime.Now, DateTime.Now));
            P.Add(new Jobs("Create Spreadsheet", "Corporate needs a spreadsheet for saturday's business pitch", "Not Started", DateTime.Now, DateTime.Now));
            P.Add(new Jobs("Get Uniforms Fitted", "The new uniforms are due soon and so, need to be fitted.", "Completed", DateTime.Now, DateTime.Now));
            P.Add(new Jobs("Order Lunch For Tomorrow", "No lunch has yet been ordered for tomorrow", "In Progress", DateTime.Now, DateTime.Now));
            P.Add(new Jobs(" ", "Tap is leaking and it needs to be fixed.", "In Progress", DateTime.Now, DateTime.Now));
            P.Add(new Jobs("Fix Tap", "Tap is leaking and it needs to be fixed.", "In Progress", DateTime.Now, DateTime.Now));
            P.Add(new Jobs("Fix Tap", "Tap is leaking and it needs to be fixed.", "In Progress", DateTime.Now, DateTime.Now));
            P.Add(new Jobs("Fix Tap", "Tap is leaking and it needs to be fixed.", "In Progress", DateTime.Now, DateTime.Now));
            P.Add(new Jobs("Fix Tap", "Tap is leaking and it needs to be fixed.", "In Progress", DateTime.Now, DateTime.Now));
            P.Add(new Jobs("Fix Tap", "Tap is leaking and it needs to be fixed.", "In Progress", DateTime.Now, DateTime.Now));
            P.Add(new Jobs("Fix Tap", "Tap is leaking and it needs to be fixed.", "In Progress", DateTime.Now, DateTime.Now));
            P.Add(new Jobs("Fix Tap", "Tap is leaking and it needs to be fixed.", "In Progress", DateTime.Now, DateTime.Now));
            JobView.ItemsSource = P;
            HideMenu();
        }

        async void OnBtnMenuClick(object sender, EventArgs args)
        {
            BtnMenu.Opacity = 0;
            await BtnMenu.FadeTo(1, 200);
            if (BtnUers.IsVisible == true)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }

        public void HideMenu()
        {
            BtnMenu.Text = "Menu ►";
            BtnUers.IsVisible = false;
            BtnJobs.IsVisible = false;
        }

        public void ShowMenu()
        {
            BtnMenu.Text = "Menu ▼";
            BtnUers.IsVisible = true;
            BtnJobs.IsVisible = true;
        }

        async void OnBtnUsersClick(object sender, EventArgs args)
        {
            HideMenu();
            await Navigation.PushAsync(new UsersPage());
        }

        async void OnBtnJobsClick(object sender, EventArgs args)
        {
            BtnJobs.Opacity = 0;
            await BtnJobs.FadeTo(1, 200);
            HideMenu();
        }

        public class Jobs
        {
            public string Job_Title { get; set; }
            public string Job_Description { get; set; }
            public string Job_Status { get; set; }
            public DateTime Job_Start { get; set; }
            public DateTime Job_End { get; set; }
            public Jobs(string title, string descr, string status, DateTime start, DateTime end)
            {
                Job_Title = "Title: "+title;
                Job_Description = "Description: "+ descr.Substring(0,40)+"...";
                Job_Status = "Status: "+ status;
                Job_Start = start;
                Job_End = end;
            }
        }
    }
}
