using System;
using System.Threading;
using Xamarin.Forms;

namespace rps
{
    public class MainPage : ContentPage
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

        // Keep a reference to the button that is currently selected
        StyledGameButton selectedButton;

        // Keep a reference to layouts available to child methods
        StackLayout layout;
        Grid buttonLayout;

        // Keep a reference to the Description Label
        Label descriptionLabel;

        public MainPage()
        {
            // This tells the Navigation page (App.cs:15) what title to put in the navigation bar.
            Title = "Rock Paper Scissors";


            // Create a layout that is the 'root' or 'parent' that can hold everything (as children)
            layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Create a parent layout for the buttons specifically so we can orient them horizontally in their parent
            buttonLayout = new Grid { HorizontalOptions = LayoutOptions.CenterAndExpand, Margin = new Thickness(10, 0) };

            // We want to make a 1x3 grid where the height is determined by its children
            // and the width of each column is equally divided
            buttonLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            buttonLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            buttonLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            buttonLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // We make this a class level variable so we can change the text from other
            // methods later
            descriptionLabel = new Label
            {
                Text = STARTING_TEXT,
                FontSize = 24,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 25)
            };

            // Make the buttons
            Button rockButton = new StyledGameButton { Text = ROCK_ACTION };
            Button paperButton = new StyledGameButton { Text = PAPER_ACTION };
            Button scissorsButton = new StyledGameButton { Text = SCISSORS_ACTION };
            Button shootButton = new Button
            {
                Text = "Shoot",
                Margin = 15,
                BorderWidth = 1,
                BorderColor = Color.SlateGray,
                BackgroundColor = Color.DimGray,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            // Register event handlers when a button is clicked (note that they use the same handler)
            rockButton.Clicked += OnActionButtonClicked;
            paperButton.Clicked += OnActionButtonClicked;
            scissorsButton.Clicked += OnActionButtonClicked;

            // Add event handler for when the shoot button is clicked. 
            shootButton.Clicked += OnShootButtonClicked;

            // Add each button to their layout (as a child)
            buttonLayout.Children.Add(rockButton, 0, 0);
            buttonLayout.Children.Add(paperButton, 1, 0);
            buttonLayout.Children.Add(scissorsButton, 2, 0);

            // Add each child layout (or view) to the root layout
            layout.Children.Add(descriptionLabel);
            layout.Children.Add(buttonLayout);
            layout.Children.Add(shootButton);

            // The content property always has to be set to show anything on a page.
            Content = layout;
        }

        public void OnActionButtonClicked(object sender, EventArgs args)
        {
            // Cast the sender (object that called the action) into what we know it is to be
            StyledGameButton clicked = (StyledGameButton)sender;

            // We've clicked a button that is currently selected, so let's deselect it
            if (selectedButton == clicked)
            {
                descriptionLabel.Text = STARTING_TEXT; // Reset text
                clicked.MarkInactive();
                selectedButton = null; // Remove our reference
                return;
            }

            // Set all children to their default colors
            foreach (StyledGameButton child in buttonLayout.Children)
            {
                child.MarkInactive();
            }

            // Since this was the clicked one, make its colors the active colors
            clicked.MarkActive();

            descriptionLabel.Text = ACTION_CHOSEN_TEXT;

            // Keep the reference so we can use it later (see above early exit and below OnShoot)
            selectedButton = clicked;
        }

        public void OnShootButtonClicked(object sender, EventArgs args)
        {
            // An option hasn't been clicked (otherwise this wouldn't be null)
            if (selectedButton == null) return;

            int repetitions = 3;
            TimeSpan span = new TimeSpan(0, 0, 1); // Every second

            // Make a timer to show to the user
            Device.StartTimer(span, () =>
            {
                // After 3 seconds, show the "Shoot" Text
                if (repetitions == 0)
                {
                    descriptionLabel.Text = GO_TEXT;
                }
                else if (repetitions == -1) // After shoot is displayed, actually run the game logic
                {
                    string computerChoice = GetRandomComputerChoice();

                    bool didComputerTie = selectedButton.Text == computerChoice;
                    bool didComputerWin = DidComputerWin(selectedButton.Text, computerChoice);

                    if (didComputerTie)
                    {
                        descriptionLabel.Text = $"The Computer chose {computerChoice} too\n\nPlay again";
                        selectedButton.MarkPreviouslySelected();
                    }
                    else if (didComputerWin)
                    {
                        descriptionLabel.Text = $"The Computer chose {computerChoice}\n\nYou Lost.";
                        selectedButton.MarkLost();
                    }
                    else
                    {
                        descriptionLabel.Text = $"The Computer chose {computerChoice}\n\nYou Won.";
                        selectedButton.MarkWon();
                    }

                    // Prevent the user from pressing the `Shoot` button over and over
                    selectedButton = null;
                    return false;
                }
                else // Otherwise we're still just diplaying the countdown
                {
                    descriptionLabel.Text = repetitions.ToString(); 
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

