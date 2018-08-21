using System;
using Xamarin.Forms;

namespace rps
{
    // This class is where the magic happens. Xamarin expects there to be a class that is
    // of type `Application` (subclasses) and this class will determine the actions that
    // take place when a user opens the app, closes it, re-opens it, etc. 
    public class App : Application
    {
        public App()
        {
            // This is really all we need right now. Tell the app to serve the 
            // Content we've generated. 
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }
    }
}
