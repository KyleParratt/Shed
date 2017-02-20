using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Shed
{
    public class App : Xamarin.Forms.Application
    {
        public App()
        {   //Initializes App And Opens Up The Main Page
            MainPage = new NavigationPage(new ShedMainPage());
        }
    }
}
