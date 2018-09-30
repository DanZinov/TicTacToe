using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace TicTacToe
{
    /// <summary>
    /// Constructor
    /// </summary>
    public partial class MainWindow : Window
    {
        private MarkType[] mResults;
        private bool mPlayer1Turn;
        private bool mGameEnded;

        public MainWindow()
        {
            InitializeComponent();
            // Start the game
            NewGame();
        }

        private void NewGame()
        {
            // Set the array size of 9
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }
            mPlayer1Turn = true;
            // Iterate through every button
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                // Change background and content of each button to be empty at the start of the game
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Makes the game not finished at the start
            mGameEnded = false;
        }
        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The event when the button is clicked</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the button to type Button
            var button = (Button)sender;

            // Get row and column of the current button
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            // Get the index of the current button in the array of all buttons
            var index = column + (row * 3);

            // Nothing has to be done if a value is already there
            if (mResults[index] != MarkType.Free)
            {
                return;
            }
            // Set the value of the button as cross or circles
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Zero;

            // Change circles to green
            if (!mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            // Put the cross or circles in the grid
            button.Content = mPlayer1Turn ? "X" : "O";

            // Switch the turns of the players
            mPlayer1Turn ^= true;

            CheckForWinner();
        }
        /// <summary>
        /// Check for winner
        /// </summary>
        private void CheckForWinner()
        {
            // Check if the Row 0 has all circles or crosses
            if(mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight the winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            // Check if the Row 1 has all circles or crosses
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                mGameEnded = true;

                // Highlight the winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            // Check if the Row 2 has all circles or crosses
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight the winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            // Check if the Column 0 has all circles or crosses
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight the winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            // Check if the Column 1 has all circles or crosses
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                mGameEnded = true;

                // Highlight the winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            // Check if the Column 2 has all circles or crosses
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight the winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            // Check if the Diagonal 0 has all circles or crosses
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight the winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            // Check if the Diagonal 1 has all circles or crosses
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight the winning cells in green
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
            }


            // Check if all of the cells are filled and therefore the game is a draw
            if (!mResults.Any(result => result == MarkType.Free))
            {
                // Finish the game
                mGameEnded = true;

                // Change all cells to orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
        }
    }

}
