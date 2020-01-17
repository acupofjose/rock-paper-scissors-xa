using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RockPaperScissors
{
    public partial class GamePage : ContentPage
    {
        // Map strings to variables
        //  this is done so that if we wanted to change the names of each action we would only
        //  have to change them here rather than everywhere in our code.
        const string ROCK_ACTION = "Rock";
        const string PAPER_ACTION = "Paper";
        const string SCISSORS_ACTION = "Scissors";
        const string STARTING_TEXT = "Choose your weapon.";
        const string ACTION_CHOSEN_TEXT = "Ready?";
        const string GO_TEXT = "Shoot!";

        // Keep a reference to the Description Label
        StyledGameButton SelectedButton;

        public GamePage()
        {
            InitializeComponent();

            // This tells the Navigation page (App.cs:15) what title to put in the navigation bar.
            Title = "XAML : RPS";

            DescriptionLabel.Text = STARTING_TEXT;
            RockButton.Text = ROCK_ACTION;
            PaperButton.Text = PAPER_ACTION;
            ScissorsButton.Text = SCISSORS_ACTION;
            ShootButton.Text = GO_TEXT;


            // Register event handlers when a button is clicked (note that they use the same handler)
            RockButton.Clicked += OnActionButtonClicked;
            PaperButton.Clicked += OnActionButtonClicked;
            ScissorsButton.Clicked += OnActionButtonClicked;

            // Add event handler for when the shoot button is clicked. 
            ShootButton.Clicked += OnShootButtonClicked;
        }

        public void OnActionButtonClicked(object sender, EventArgs args)
        {
            // Cast the sender (object that called the action) into what we know it is to be
            StyledGameButton clicked = (StyledGameButton)sender;

            // We've clicked a button that is currently selected, so let's deselect it
            if (SelectedButton == clicked)
            {
                DescriptionLabel.Text = STARTING_TEXT; // Reset text
                clicked.MarkInactive();
                SelectedButton = null; // Remove our reference
                return;
            }

            // Set all children to their default colors
            foreach (StyledGameButton child in ButtonLayout.Children)
            {
                child.MarkInactive();
            }

            // Since this was the clicked one, make its colors the active colors
            clicked.MarkActive();

            DescriptionLabel.Text = ACTION_CHOSEN_TEXT;

            // Keep the reference so we can use it later (see above early exit and below OnShoot)
            SelectedButton = clicked;
        }

        public void OnShootButtonClicked(object sender, EventArgs args)
        {
            // An option hasn't been clicked (otherwise this wouldn't be null)
            if (SelectedButton == null) return;

            int repetitions = 3;
            TimeSpan span = new TimeSpan(0, 0, 1); // Every second

            // Make a timer to show to the user
            Device.StartTimer(span, () =>
            {
                // After 3 seconds, show the "Shoot" Text
                if (repetitions == 0)
                {
                    DescriptionLabel.Text = GO_TEXT;
                }
                else if (repetitions == -1) // After shoot is displayed, actually run the game logic
                {
                    string computerChoice = GetRandomComputerChoice();

                    bool didComputerTie = SelectedButton.Text == computerChoice;
                    bool didComputerWin = DidComputerWin(SelectedButton.Text, computerChoice);

                    if (didComputerTie)
                    {
                        DescriptionLabel.Text = $"The Computer chose {computerChoice} too\n\nPlay again";
                        SelectedButton.MarkPreviouslySelected();
                    }
                    else if (didComputerWin)
                    {
                        DescriptionLabel.Text = $"The Computer chose {computerChoice}\n\nYou Lost.";
                        SelectedButton.MarkLost();
                    }
                    else
                    {
                        DescriptionLabel.Text = $"The Computer chose {computerChoice}\n\nYou Won.";
                        SelectedButton.MarkWon();
                    }

                    // Prevent the user from pressing the `Shoot` button over and over
                    SelectedButton = null;
                    return false;
                }
                else // Otherwise we're still just diplaying the countdown
                {
                    DescriptionLabel.Text = repetitions.ToString();
                }

                repetitions = repetitions - 1; // or you could do repetitions--;

                return true;
            });
        }

        private string GetRandomComputerChoice()
        {
            Random random = new Random();
            int choice = random.Next(0, 100);

            // With equal probability, choose an action
            if (choice >= 66)
            {
                return ROCK_ACTION;
            }
            else if (choice >= 33 && choice < 66)
            {
                return PAPER_ACTION;
            }
            else
            {
                return SCISSORS_ACTION;
            }
        }

        private bool DidComputerWin(string playerChoice, string computerChoice)
        {
            // Enumerate the states in which the computer won
            if (playerChoice == ROCK_ACTION && computerChoice == PAPER_ACTION) return true;
            if (playerChoice == PAPER_ACTION && computerChoice == SCISSORS_ACTION) return true;
            if (playerChoice == SCISSORS_ACTION && computerChoice == ROCK_ACTION) return true;

            // Otherwise, this is not a winning state, so return false
            return false;
        }
    }
}
