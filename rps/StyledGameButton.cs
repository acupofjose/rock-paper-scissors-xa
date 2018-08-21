using System;
using Xamarin.Forms;

namespace rps
{
    // This is a helper class to keep from repeating yourself (aka, being DRY - "Don't Repeat Yourself")
    // It will function just as a regular button does, it just adds uniform styling to the button
    // when it is instanciated. 
    public class StyledGameButton : Button
    {
        public StyledGameButton()
        {
            TextColor = Color.Black;
            Margin = 5;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            BorderWidth = 1;
            BorderColor = Color.DimGray;
        }

        // Helper method to change the color of the button to an 'Active' state
        public void MarkActive()
        {
            BorderColor = Color.SlateGray;
            BackgroundColor = Color.SlateGray;
            TextColor = Color.White;
        }

        // Helper method to change the color of the button to an 'Inactive' state
        public void MarkInactive()
        {
            BackgroundColor = Color.Transparent;
            TextColor = Color.Black;
        }

        public void MarkPreviouslySelected()
        {
            BorderColor = Color.SlateGray;
            BackgroundColor = Color.Transparent;
            TextColor = Color.SlateGray;
        }

        public void MarkWon()
        {
            BorderColor = Color.SeaGreen;
            BackgroundColor = Color.SeaGreen;
            TextColor = Color.White;
        }

        public void MarkLost()
        {
            BorderColor = Color.IndianRed;
            BackgroundColor = Color.Red;
            TextColor = Color.White;
        }
    }
}
