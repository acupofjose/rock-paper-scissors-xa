# Project 1: Rock, Paper, Scissors

**Goal**: To set up a working Xamarin Cross-Platform environment and make a simple text based rock, paper, scissors game.

**Resources**:
The following **are not required to read _in their entirety_** there is a lot of extraneous information that doesn't apply to you in them. However, there will be many items explained in this specification that will be explained in the following documentation links.

- [**Introduction Xamarin Forms**](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/get-started/introduction-to-xamarin-form)
- [Xamarin Forms App Class](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/application-class)
- [Xamarin Forms App Lifecycle](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/app-lifecycle)
- [Xamarin Forms User Interface Views](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/index)
- [Xamarin Forms Samples](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/samples/index)

## Project Specification

**Basic setup:** To begin, you will need to [download and install Visual Studio for Mac](https://docs.microsoft.com/en-us/visualstudio/mac/installation), making sure that you install with it the Xamarin SDKs for: `Android`, `iOS` and `.NET Core`.

After installing, set up a new `Xamarin Forms` project. Name it `RockPaperScissors`, and you can set the company domain to be `example.com` or `{yourname}.com`. We will be making a project targeting `iOS` and `Android`.

On the left side of your solution explorer you should see 3 projects, `RockPaperScissors`, `RockPaperScissors.Android` and `RockPaperScissors.iOS`. All of our code (for now) will be in the `RockPaperScissors` project.

It would be beneficial to use the repo as a means of seeing how a project works. You **should not be copying code from the repo into your project.** If you're going to copy something directly, type it out and analyze what you typed.

### Rules

- Paper beats Rock, Rock Beats Scissors, Scissors Beats Paper
- A user must choose what they will play _before_ being able to play a game
- The computer must choose randomly (with equal probablity) what to play
- The game must show the user what the computer played
- The game must show whether the user won or lost

### Requirements

You will need to provide the following to complete the project:

- A `GameContentPage` to be set as the `MainPage` of your `App`
- The `GameContentPage` to render a layout (or set of layouts) contain:
  - A `Label` containing the choice randomly chosen by the computer.
  - A Play `Button`.
  - 3 `Buttons`, inline _(you will probably need a `Grid` or `StackLayout` here)_ each set to contain `Rock`, `Paper`, `Scissors`.
  - A means of showing feedback to the user _(probably a `Label`)_ as to whether they won or lost.

Please ask questions as you have them, but don't be afraid to search for an answer first!
