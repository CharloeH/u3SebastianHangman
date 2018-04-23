/* Sebastian Horton
 * April 22nd 2018
 * u3HangmanSebastian
 * program allows the user to play a game of hangman
 * */
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
using System.Windows.Shapes;
using System.IO;
using System.Net;
using System.Collections;

namespace u3HangmanSebastian
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        string[] wrong = new string[6]; 
        string[] word = new string[25];
        string[] guessed = new string[10];
        int RNG = 0;
        int counter = 5;
        int letters = 0;
        int winnerCheck = 0;
        string wordUsed = null;
        string wordrecreated = null;
        string guessedLetter = null;
        string lblWrong = null;
        string line = null;

        Random random = new Random();
        System.IO.StreamReader streamReader = new System.IO.StreamReader("words.txt");
        public MainWindow()
        {
            InitializeComponent();
           
        }
        //label method
        private void CreateLabel(int i, string content)
        {
            Label wordLabel = new Label();
            wordLabel.Content = content;
            Canvas.SetTop(wordLabel, 92);
            Canvas.SetLeft(wordLabel, (235 + (i * 10)));
            myCanvas.Children.Add(wordLabel);
        }

            private void StartProgram(object sender, RoutedEventArgs e)
        {
             RNG = random.Next(0, 9);//each word is attached to a number
            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                //checking which word is being used
                if (line.Contains(RNG.ToString()))
                {   
                    this.wordUsed = line.Substring(line.IndexOf(RNG.ToString()) + 2, line.Length - 2);//removing the number
                }

            }
            //loop that splits the word into an array of letters
            for (int i = 0; i < wordUsed.Length; i++)
            {
                word[i] = wordUsed.Substring(i, 1);
                
            }
            //loopp that create equally as many underscores
            for (int i = 0; i < wordUsed.Length; i++)
            {
                CreateLabel(i, "_");
            }
          
        }

        private void GuessLetters(object sender, RoutedEventArgs e)
        {
            guessedLetter = txtGuess.Text;
            for (int i = 0; i < wordUsed.Length; i++)
            {
                //check correct letters
                if (guessedLetter == word[i])
                {
                    guessed[i] = guessedLetter;
                    CreateLabel(i, guessedLetter);
                }
                //check for whole word winner
                if (guessedLetter == wordUsed)
                {
                    MessageBox.Show("You win!");
                    i = wordUsed.Length;//ends loop early
                    winnerCheck = 1;//makes sure that there arent two "wins"
                }
                //check for incorrect letters
                if (!wordUsed.Contains(guessedLetter))
                {
                    counter--;
                    wrong[i] = guessedLetter;
                    lblWrong = lblWrong + wrong[i];
                    lblWrongLetters.Content = lblWrong;
                    i = wordUsed.Length;
                    if (counter != 0)
                    {
                        MessageBox.Show("incorrect! you have " + counter.ToString() + " guesses remaining");
                    }
                    //check for loss
                    if (counter == 0)
                    {
                        MessageBox.Show("You lose! The word you were looking for was: " + wordUsed);
                    }
                }
                
            }
            //check for letter winner
            if (winnerCheck == 0)
            {
                wordrecreated = guessed[0] + guessed[1] + guessed[2] + guessed[3] + guessed[4] + guessed[5] + guessed[6] + guessed[7] + guessed[8];
                if (wordrecreated == wordUsed)
                {
                    MessageBox.Show("You win!");
                }
            }




        }

       
        }
    }


