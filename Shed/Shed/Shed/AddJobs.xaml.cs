using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
using Xamarin.Forms;

namespace Shed
{
    public partial class AddJobs : ContentPage
    {
        public AddJobs()
        {
            InitializeComponent();
        }

        async void OnBtnOpenJob(object sender, EventArgs args)
        {

            bool GoodInput = true;
            if (titleEntry.Text == string.Empty)
            {
                GoodInput = false;
            }
            if (DescrEntry.Text == string.Empty)
            {
                GoodInput = false;
            }
            if (GoodInput == true)
            {
                var realm = Realm.GetInstance();
                string JobNum = GetJobNum(titleEntry.Text);
                realm.Write(() =>
                {
                    var NewUser = new UserJobs();
                    NewUser.Job_Num = JobNum;
                    NewUser.Job_Title = titleEntry.Text;
                    NewUser.Job_Descr = DescrEntry.Text;
                    NewUser.Job_Status = "Not Started";
                    NewUser.Job_AssignedTo = IDEntry.Text;
                    NewUser.Job_Start = DateTime.Now.ToString();
                    NewUser.Job_End = EndDate.Date.ToString();
                    realm.Add(NewUser);
                });
                await DisplayAlert("Successfully Opened Job","New job been added with the unique job ID: " + JobNum, "Great");
                ClearInput();
            }
           else
            {
                await DisplayAlert("Job Opening Error", "Please ensure that all info is entered correctly", "Okay");
            }
        }

        string GetJobNum(string Title)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 100000);
            string id = string.Concat(Title.Substring(0, 1), (Convert.ToString(randomNumber)));
            return id;
        }

        public void ClearInput()
        {
            titleEntry.Text = string.Empty;
            DescrEntry.Text = string.Empty;
            IDEntry.Text = string.Empty;
        }

    }

}
