using System;
using System.Collections.Generic;
using Realms;
using System.Linq;
using Xamarin.Forms;

namespace Shed
{
    public partial class ShedMainPage : ContentPage
    {
        public string UserID { get; set; }
        public string UserRole { get; set; }
        public string UserJobNum { get; set; }

        public ShedMainPage(string ID, string Role)
        {
            InitializeComponent();
            RefreshList();
            HideMenu();
            AssignInputView();
            UserID = ID;
            UserRole = Role;
        }

        public ShedMainPage()
        {
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

        async void OnBtnRefresh(object sender, EventArgs args)
        {
            BtnRefresh.Opacity = 0;
            await BtnRefresh.FadeTo(1, 200);
            RefreshList();
            HideMenu();
            await DisplayAlert("Job List", "Jobs have been refreshed", "Thanks");
        }

        public void RefreshList()
        {
            var realm = Realm.GetInstance();
            var z = new List<Jobs>();
            var UserCountList = realm.All<UserJobs>();
            for (var i = 0; i < UserCountList.Count(); i++)
            {
                var UserList = UserCountList.ElementAt(i);
                string title = UserList.Job_Title;
                string descr = UserList.Job_Descr;
                string status = UserList.Job_Status;
                string id = UserList.Job_Num;
                z.Add(new Jobs(title, descr, status, id));
            }
            JobView.ItemsSource = z;
        }

        public void HideMenu()
        {
            BtnMenu.Text = "Menu ►";
            BtnUers.IsVisible = false;
            BtnJobs.IsVisible = false;
            BtnRefresh.IsVisible = false;
        }

        public void AssignInputView()
        {
            if (InputLabel.IsVisible == true)
            {
                InputLabel.IsVisible = false;
                BtnAssignJob.IsVisible = false;
                IdEntry.IsVisible = false;
            }
            else
            {
                InputLabel.IsVisible = true;
                BtnAssignJob.IsVisible = true;
                IdEntry.IsVisible = true;
            }
        }

        async void OnBtnAssignJob(object sender, EventArgs args)
        {
            string EmpID = IdEntry.Text;
            var realm = Realm.GetInstance();
            var trealm = realm.All<UsersDB>().Where(d => d.Approved == true && d.Emp_ID == EmpID);
            var job = realm.All<UserJobs>().Where(d => d.Job_Num == UserJobNum && d.Job_AssignedTo == "");
            
            if (!(job.Count() == 0))
            {
                var i = job.ElementAt(0);
                if (!(trealm.Count() == 0))
                {
                    var u = trealm.ElementAt(0);
                    if (!(u.assigned == "Assigned"))
                    {
                        string name = u.Name;
                        string surname = u.Surname;
                        string role = u.Role;
                        string email = u.Email;
                        string password = u.Password;
                        string assigned = u.assigned;
                        var kristianWithC = new UsersDB
                        {
                            Emp_ID = EmpID,
                            Approved = true,
                            assigned = "Assigned",
                            Name = name,
                            Surname = surname,
                            Email = email,
                            Password = password,
                            Role = role
                        };
                        realm.Write(() => realm.Add(kristianWithC, update: false));
                        var cheeseBook = realm.All<UsersDB>().First(b => b.Emp_ID == EmpID && b.assigned == "Unassigned");
                        using (var trans = realm.BeginWrite())
                        {
                            realm.Remove(cheeseBook);
                            trans.Commit();
                        }
                        string title = i.Job_Title;
                        string descr = i.Job_Descr;
                        string end = i.Job_End;
                        string start = i.Job_Start;

                        var kristianWith = new UserJobs
                        {
                            Job_Num = UserJobNum,
                            Job_Title = title,
                            Job_Descr = descr,
                            Job_Status = "In Progress",
                            Job_AssignedTo = EmpID,
                            Job_Start = start,
                            Job_End = end,
                        };
                        realm.Write(() => realm.Add(kristianWith, update: false));
                        var cheeseBoo = realm.All<UserJobs>().First(b => b.Job_Num == UserJobNum && b.Job_AssignedTo == "");
                        using (var trans = realm.BeginWrite())
                        {
                            realm.Remove(cheeseBoo);
                            trans.Commit();
                        }
                        RefreshList();
                        AssignInputView();
                    }
                    else
                    {
                        await DisplayAlert("Employee Already Assigned", "This employee is already assigned to a job", "Okay");
                    }
                }
                else
                {
                    await DisplayAlert("Invalid Employee ID", "The employee ID entered is not valid", "Okay");
                }
            }
        }

        public void ShowMenu()
        {
            BtnMenu.Text = "Menu ▼";
            BtnUers.IsVisible = true;
            BtnJobs.IsVisible = true;
            BtnRefresh.IsVisible = true;
        }

        async void OnBtnUsersClick(object sender, EventArgs args)
        {
            HideMenu();
            if (!(UserRole == "Operator"))
            {
                await Navigation.PushAsync(new UsersPage());
            }
            else
            {
                await DisplayAlert("Access Denied", "You are not permitted to access this area", "Okay");
            }
        }

        async void OnBtnJobsClick(object sender, EventArgs args)
        {
            HideMenu();
            if (!(UserRole == "Operator"))
            {
                await Navigation.PushAsync(new AddJobs());
            }
            else
            {
                await DisplayAlert("Access Denied", "You are not permitted to access this area", "Okay");
            }
            
        }

        public class Jobs
        {
            public string Job_Title { get; set; }
            public string Job_Descr { get; set; }
            public string Job_Status { get; set; }
            public string Job_Num { get; set; }
            public Jobs(string Title, string Descr, string Status, string ID)
            {
                Job_Title = "Title: " + Title;
                Job_Descr = "Description: " + Descr.Substring(0,30)+"...";
                Job_Status = "Status: " + Status;
                Job_Num = "ID: "+ID;
            }
        }

        public async void OnBtnViewJob(object sender, EventArgs e)
        {
            var mi = ((Button)sender);
            var EmplID = mi.CommandParameter.ToString();
            var realm = Realm.GetInstance();
            EmplID = EmplID.Remove(0, 4);
            var trealm = realm.All<UserJobs>().Where(d => d.Job_Num == EmplID);
            var u = trealm.ElementAt(0);
            string title = u.Job_Title;
            string assignedTo;
            if (u.Job_AssignedTo == "")
            {
                assignedTo = "NA";
            }
            else
            {
                assignedTo = u.Job_AssignedTo;
            }
            string descr = u.Job_Descr;
            string status = u.Job_Status;
            string started = u.Job_Start;
            string end = u.Job_End;
            string id = u.Job_Num;
            if (assignedTo == "NA")
            {
                UserJobNum = id;
                var x = await DisplayActionSheet( " Title: " + title + "\n Description: " + descr + "\n Status: " + status + "\n Started: " + started + "\n Deadline: " + end + "\n Assigned To: " + assignedTo + "\n Job ID: " + id, "Assign", "Okay");
                    if (x == "Assign")
                    {
                        AssignInputView();
                    }
            }
            else
            {
                     await DisplayAlert("Job Information", " Title: " + title + "\n Description: " + descr + "\n Status: " + status + "\n Started: " + started + "\n Deadline: " + end + "\n Assigned To: " + assignedTo + "\n Job ID: " + id, "Okay");
            }
        }
    }
}
