using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
using Xamarin.Forms;

namespace Shed
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        public async void OnBtnLogin(object sender, EventArgs e)
        {
            if (!(userentry.Text == "" && Passentry.Text == ""))
            {
                var mi = ((Button)sender);
                var EmplID = mi.CommandParameter.ToString();
                var realm = Realm.GetInstance();
                var trealm = realm.All<UsersDB>().Where(d => d.Approved == true && d.Emp_ID == EmplID);
                var s = trealm.Count();
                if ( s == 1)
                {
                    var u = trealm.ElementAt(0);
                    string pass = u.Password;
                    string Role = u.Role;
                    if (pass == Passentry.Text)
                    {
                        userentry.Text = string.Empty;
                        Passentry.Text = string.Empty;
                        await Navigation.PushAsync(new ShedMainPage(userentry.Text, Role));
                    }
                    else
                    {
                        await DisplayAlert("Sign In Failed", "Please ensure you are an approved user and that your login credentials are correct", "Okay");
                    }

                }
                else
                {
                    await DisplayAlert("Sign In Failed", "Please ensure you are an approved user and that your login credentials are correct", "Okay");
                }

            }
            else
            {
                await DisplayAlert("Sign In Failed", "Please ensure you are an approved user and that your login credentials are correct", "Okay");
            }
        }
    }
}
