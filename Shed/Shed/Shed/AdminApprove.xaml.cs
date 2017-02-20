using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
using Xamarin.Forms;

namespace Shed
{
    public partial class AdminApprove : ContentPage
    {
        public AdminApprove()
        {
            InitializeComponent();
            ShowList();
        }

        public void ShowList()
        {
            var realm = Realm.GetInstance();
            var z = new List<Users>();
            var UserCountList = realm.All<UsersDB>().Where(d => d.Approved == false);
            for (var i = 0; i < UserCountList.Count(); i++)
            {
                var UserList = UserCountList.ElementAt(i);
                string name = UserList.Name;
                string EmpID = UserList.Emp_ID;
                string surname = UserList.Surname;
                string role = UserList.Role;
                string email = UserList.Email;
                string password = UserList.Password;
                string assigned = UserList.assigned;
                z.Add(new Users(EmpID, name, surname, email, role, assigned));
            }
            EmployeeView.ItemsSource = z;
        }

        public async void OnBtnAccepClick(object sender, EventArgs e)
        {
            var mi = ((Button)sender);
            var EmplID = mi.CommandParameter.ToString();
            var realm = Realm.GetInstance();
            EmplID = EmplID.Remove(0, 4);
            var answer = await DisplayActionSheet("Accept Employee?", "Yes", "No");
            if (answer == "Yes")
            {
                var trealm = realm.All<UsersDB>().Where(d => d.Approved == false && d.Emp_ID == EmplID);
                var u = trealm.ElementAt(0);
                string name = u.Name;
                string surname = u.Surname;
                string role = u.Role;
                string email = u.Email;
                string password = u.Password;
                var kristianWithC = new UsersDB
                {
                    Emp_ID = EmplID,
                    Approved = true,
                    assigned = "Unassigned",
                    Name = name,
                    Surname = surname,
                    Email = email,
                    Password = password,
                    Role = role
                };
                realm.Write(() => realm.Add(kristianWithC, update: false));
                var cheeseBook = realm.All<UsersDB>().First(b => b.Emp_ID == EmplID && b.Approved == false);
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(cheeseBook);
                    trans.Commit();
                }
                ShowList();
            }
        }

        public async void OnBtnRejectClick(object sender, EventArgs e)
        {
            var mi = ((Button)sender);
            var EmplID = mi.CommandParameter.ToString();
            var realm = Realm.GetInstance();
            EmplID = EmplID.Remove(0, 4);
            var answer = await DisplayActionSheet("Reject Employee?", "Yes", "No");
            if (answer == "Yes")
            {
                await DisplayAlert("Rejected", "You have rejected this employee's application request", "OK");
                var cheeseBook = realm.All<UsersDB>().First(b => b.Emp_ID == EmplID);
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(cheeseBook);
                    trans.Commit();
                }
                ShowList();
            }


        }
    }

    public class Users
    {
        public string Emp_ID { get; set; }
        public string User_Name { get; set; }
        public string User_Surname { get; set; }
        public string User_Email { get; set; }
        public string User_Role { get; set; }
        public string User_Assigned { get; set; }
        public Users(string ID, string Name, string Surname, string email, string role, string assigned)
        {
            Emp_ID = "ID: "+ ID;
            User_Name = "Applicant Name: " + Name;
            User_Surname = Surname;
            User_Assigned =  assigned;
            User_Role = "Applying For: " + role;
            User_Email = email;
        }
    }
}
