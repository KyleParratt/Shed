using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;

using Xamarin.Forms;

namespace Shed
{
    public partial class RegisterUser : ContentPage
    {   //Initialize User Rgistration Page
        public RegisterUser()
        {
            InitializeComponent();
        }
        async void OnBtnRegisterUserClick(object sender, EventArgs args)
        {
            BtnRegisterUser.Opacity = 0;
            await BtnRegisterUser.FadeTo(1, 500);
            // Code Checks Inputs And Displays An Error If It's Incorrect, Else It Adds User 
            bool GoodInput = true;
            if (EmailEntry.Text == string.Empty)
            {
                GoodInput = false;
            }
            if (NameEntry.Text == string.Empty)
            {
                GoodInput = false;
            }
            if (SurnameEntry.Text == string.Empty)
            {
                GoodInput = false;
            }
            if (PasswordEntry.Text == string.Empty)
            {
                GoodInput = false;
            }
            if (PasswordEntry.Text.Length < 4)
            {
                GoodInput = false;
            }
            if (RePasswpordEntry.Text == string.Empty)
            {
                GoodInput = false;
            }
            if (!(PasswordEntry.Text == RePasswpordEntry.Text))
            {
                GoodInput = false;
            }
            if ((CbAdmin.IsToggled == false) && (CbSupervisor.IsToggled == false) && (CbOperator.IsToggled == false))
            {
                GoodInput = false;
            }
            if (GoodInput == true)
            {
                // Add User To Database
                var realm = Realm.GetInstance();
                string EmpID = UserEmp_ID(NameEntry.Text, SurnameEntry.Text);
                realm.Write(() =>
                {
                    var NewUser = new UsersDB();
                    NewUser.Emp_ID = EmpID;
                    NewUser.Email = EmailEntry.Text;
                    NewUser.Name = NameEntry.Text;
                    NewUser.Surname = SurnameEntry.Text;
                    NewUser.Role = UserRole();
                    NewUser.Password = PasswordEntry.Text;
                    NewUser.assigned = "Assigned";
                    NewUser.Approved = false;
                    realm.Add(NewUser);
                });
                await DisplayAlert("Registration Successful", NameEntry.Text + " has been added with the unique employee ID: " + EmpID, "Great");
                ClearInput();
            }
            else
            {
                await DisplayAlert("Registration Error", "Please ensure that all info is entered correctly", "Okay");
            }
        }
        public void ClearInput()
        {
            EmailEntry.Text = string.Empty;
            NameEntry.Text = string.Empty;
            SurnameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            RePasswpordEntry.Text = string.Empty;
            CbAdmin.IsToggled = false;
            CbSupervisor.IsToggled = false;
            CbOperator.IsToggled = false;
        }

        // Code Below Creates Unique Employee ID
        string UserEmp_ID(string name, string surname)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 1000);
            string id = string.Concat(name.Substring(0, 1), surname.Substring(0, 1));
            id = id + (Convert.ToString(randomNumber));
            return id;
        }
        // Code Below Retrieves Employee's Role
        string UserRole()
        {
            string role = string.Empty;
            if (CbAdmin.IsToggled == true)
            {
                role = "Admin";
            }
            if (CbOperator.IsToggled == true)
            {
                role = "Operator";
            }
            if (CbSupervisor.IsToggled == true)
            {
                role = "Supervisor";
            }
            return role;
        }
        // Code To Ensure That Only One Switch Is Toggled At A Time
        void CbAdminToggle(object sender, EventArgs args)
        {
            CbOperator.IsToggled = false;
            CbSupervisor.IsToggled = false;
        }
        void CbSupervisorToggle(object sender, EventArgs args)
        {
            CbOperator.IsToggled = false;
            CbAdmin.IsToggled = false;
        }
        void CbOperatorToggle(object sender, EventArgs args)
        {
            CbSupervisor.IsToggled = false;
            CbAdmin.IsToggled = false;
        }
    }
}
