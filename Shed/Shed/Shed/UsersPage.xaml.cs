using System;
using System.Collections.Generic;
using Realms;
using Xamarin.Forms;
using System.Linq;

namespace Shed
{
    public partial class UsersPage : ContentPage
    {
        public UsersPage()
        {   
            //Initializes Page And Loads The Users List
            InitializeComponent();
            HideSort();
            var realm = Realm.GetInstance();
            var z = new List<Users>();
            var UserCountList = realm.All<UsersDB>().Where(d => d.Approved == true && d.Role == "Operator");
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
        // Class That Defines The Users List Fields That Are Then USed To Construct The ListView
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
                Emp_ID = "ID: " +ID;
                User_Name = "Name: "+Name;
                User_Surname = Surname;
                User_Assigned = "Status: "+assigned;
                User_Role = role;
                User_Email = email;
            }
        }
        // Takes User To The User Registration Page
        async void OnBtnAddUserClick(object sender, EventArgs args)
        {
            HideSort();
            await Navigation.PushAsync(new RegisterUser());
        }

        // Button Shows The Sort By Menu
        async void OnBtnUsersSortClick(object sender, EventArgs args)
        {
            BtnUsersSort.Opacity = 0;
            await BtnUsersSort.FadeTo(1, 200);
            if (BtnAddUser.IsVisible == true)
            {
                HideSort();
            }
            else
            {
                ShowSort();
            }
        }

        // Below Code Hides And Shows The Sort By Menu
        public void ShowSort()
        {
            BtnUsersSort.Text = "Menu ▼";
            BtnUsersSortAlpha.IsVisible = true;
            BtnUsersSortAssigned.IsVisible = true;
            BtnApproveUser.IsVisible = true;
            BtnAddUser.IsVisible = true;

        }
        public void HideSort()
        {
            BtnUsersSort.Text = "Menu ►";
            BtnUsersSortAlpha.IsVisible = false;
            BtnUsersSortAssigned.IsVisible = false;
            BtnApproveUser.IsVisible = false;
            BtnAddUser.IsVisible = false;

        }

        public async void OnBtnShowEmployee(object sender, EventArgs e)
        {
            var mi = ((Button)sender);
            var EmplID = mi.CommandParameter.ToString();
            var realm = Realm.GetInstance();
            EmplID = EmplID.Remove(0, 4);  
            var trealm = realm.All<UsersDB>().Where(d => d.Approved == true && d.Emp_ID == EmplID);
            var u = trealm.ElementAt(0);
            string name = u.Name;
            string surname = u.Surname;
            string role = u.Role;
            string email = u.Email;
            string assigned = u.assigned;
            await DisplayAlert("Employee Information", " Name: " + name + "\n Surname: " + surname + "\n Email: " + email + "\n Role: " + role + "\n Status: " + assigned,"Okay");
        }

            async void OnBtnApproveUSersClick(object sender, EventArgs args)
        {
            HideSort();
            await Navigation.PushAsync(new AdminApprove());
        }           
        // Handles The Employee List Sort By Alphabet Request
        async void OnBtnUsersSortAlphaClick(object sender, EventArgs args)
        {
            BtnUsersSortAlpha.Opacity = 0;
            await BtnUsersSortAlpha.FadeTo(1, 500);
            var realm = Realm.GetInstance();
            var z = new List<Users>();
            var UserCountList = realm.All<UsersDB>().Where(d => d.Role == "Operator");
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
                z.Add(new Users(EmpID, name, surname, email, role, "Assigned"));
            }
            var b = z;
            z.Sort((x, y) => string.Compare(x.User_Name, y.User_Surname));
            EmployeeView.ItemsSource = z;
            var answer = await DisplayActionSheet("Employee Directory Has Been Sorted In Alphabetical Order", "Thanks", "Undo");
            if (answer == "Undo")
            {
                EmployeeView.ItemsSource = b;
                await DisplayAlert("Employee Directory", "Changes Undone", "Thanks");
            }
            HideSort();
        }


            // Handles The Employee List Sort By Assigned Request
            async void OnBtnUsersSortAssignedCLick(object sender, EventArgs args)
        {
            BtnUsersSortAssigned.Opacity = 0;
            await BtnUsersSortAssigned.FadeTo(1, 500);
            var realm = Realm.GetInstance();
            var z = new List<Users>();
            var UserCountList = realm.All<UsersDB>().Where(d => d.Role == "Operator");
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
                z.Add(new Users(EmpID, name, surname, email, role, "Assigned"));
            }
            var b = z;
            z.Sort((x, y) => string.Compare(x.User_Assigned, y.User_Assigned));
            EmployeeView.ItemsSource = z;
            var answer = await DisplayActionSheet("Employee Directory Has Been Sorted By Assignment", "Thanks", "Undo");
            if (answer == "Undo")
            {
                EmployeeView.ItemsSource = b;
                await DisplayAlert("Employee Directory", "Changes Undone", "Thanks");
            }
            HideSort();
        }
    }
}

