﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeon
{
    
    class Game
    { 
        //JJ Debug: after all changes currently 08SEP23 11:56 CST game window opens but does not run or display any options.
        //...need to try something else.
        //Lodis intruction: do not break or change functions. only debug and fix syntax when necessary.
        //JJ Debug: Gave varible types below. Were just text without anything defining them.
        //JJ Debug: variable "input" was several types throughout code and all as local scoped variables. going to define it...
        //as a global variable here and a int for now. Not sure if I'm using the term 'global variable'.
        //JJ Debug: became an issue with 'input' variable on return type being either int or string. decided to separate them into...
        // 'int inputNumber = 0;' and 'string inputText = "";' Plan to impliment these in place of the variable 'input'.
        // This I hope will take away the problem of too many defined local 'input' variables.
       
        //JJ Debug: 11SEP23 changed 'bool gameOver = false;' to 'bool gameOver = true;' . I think the while loop needs something
        //gonna change 'currentArea = -1;' to 'currentArea = 1' hoping this will run the while loop.
        //It works as intended from what I can see at this point. 
        string characterName = "";
        float currentArea = 1;
        bool gameOver = false;
        float health = 20;
        bool playerIsAlive = false;
        int inputNumber = 0;
        string inputText = "";
        /// <summary>
        /// The starting room where the player gives their name, and has their first encounter.
        /// </summary>
        void Room1()
        {
            //JJ debug: Pretty sure Console.Write will only Print 1 character so changed first line to Console.WriteLine.
            //adding readkey so it doesn't skip Hello statement at the Console.Clear.
            ///Get the name from the player
            Console.WriteLine("Please enter your name.");
            health = 20;
            characterName = Console.ReadLine();
            Console.WriteLine("Hello, " + characterName);
            Console.ReadKey(true);
            Console.Clear();
            // JJ Debug: changed initial line to 'string input =' instead of int input =. not sure how to debug yet.
            // JJ Debug: in if statement changed 'if/else if (input =' to 'if/else if (input =='. this seemed to fix error, but...
            //but initial function still errored out. maybe need to define function further back? but GetInput seems to be a...
            //already defined tool thing like Console.Writline etc.
            //JJ Debug: noticed further with some help from classmate that the function 'GetInput' had an output set to 'void'...
            // changed it lower down to 'int' instead so it could output an integer. have yet to run program to see if this works.

            //JJ Debug: changed input to inputText and made it all strings to make the function work. believe using int will not work.
            //JJ Debug flipped yes and no options because they were displaying out of order for the desired result for the player.
            ///Display text for the first encounter, and store the players decision
            string inputText = GetInput("You've been approached by a traveler!! " +
                "\n They offer you a potion. Do you accept?","Yes", "No" );
           
            ///If the player drinks the potion...
            if (inputText == "1")
            {
                ///...kill the player
                Console.WriteLine("It was posion!! Ya dead shuuuunnnnn");
                playerIsAlive = false;
            }
            ///Otherwise if they do not...
            else if (inputText == "2")
            {
                ///...display text to let the player know that they survived the first room
                //JJ Debug: added ReadKey to not auto skip text
                //JJ Debug: added currentArea++ to see if this makes the next room play from the while loop. this worked.
                Console.WriteLine("You decide to follow your gut and decline. You move on to the next area.");
                Console.ReadKey(true);
                currentArea++;
            }
        }

        /// <summary>
        /// The second room where the player is given a riddle to solve.
        /// </summary>
        void Room2()
        {
            //JJ Debug 'int inputNumber = 0' was not being used so changed it to 'string inputText = ""'. 
            int numberOfAttempts = 4;
            string inputText = "";

            ///Loop until the player gets the riddle right or they run out of tries
            //JJ Debug: I think this is syntax error of bad scope placement of '}'. Will move further down to encompass more of the loop and...
            //see what happens
            //JJ Debug: moved '}' to encompass entire 'for' loop. this defined variable 'i'.
            for (int i = 0; i < numberOfAttempts; i--)
            {
                Console.Clear();


                ///Draws monkey character 
                Console.WriteLine("     __\n" +
                                   "w  c(..)o   (\n" +
                                   " \\__(-)   __)\n" +
                                   "    /|   (\n" +
                                   "   /(_)___)\n" +
                                   "  w /|\n" +
                                   "   \\  \n" +
                                   "    m m");

                ///Prints a description of the situation for context
                Console.WriteLine("A very old man with a monkey on his back approaches you." +
                "\n The monkey offers you immortality if you can solve a riddle in " + numberOfAttempts + " attempts.");
                Console.WriteLine("What has to be broken before you can use it?");

                ///Store the amount of attempts the player has remaining
                //JJ Debug: I'm pretty sure 'i' is usually used in reference to 'for loops' will come back to this after looking it up.
                // missed 'for' loop earlier in code.
                int attemptsRemaining = numberOfAttempts + i;

                ///Displays the remaining number of attempts
                Console.WriteLine("Attempts Remaining: " + attemptsRemaining);

                ///Get input for the players guess
                Console.Write("> ");
                inputText = Console.ReadLine();

                ///If the player answered correctly...
                if (inputText == "egg")
                {
                    ///...print text for feedback and break the loop
                    //JJ Debug: changed 'break;' to 'return;' on last line of if statement scope. not sure what break does, but...
                    // I know that return will break a loop which according to provided notes is the intention. hopefully works.
                    Console.WriteLine("Congrats! You've gained immortality!");
                    Console.ReadKey();
                    return;


                }
                //JJ Debug: not sure but think another condition needs to be defined here. added if statement for incorrect answer.
                ///If the player doesn't answer correctly deal damage to them

                if (inputText != "egg")
                {
                    Console.WriteLine("Incorrect! The monkey laughs at you! It hurts..." +
                        "you take 5 points of damage.");
                    Console.ReadKey();
                    health -= 5;
                }

                ///If the player has died after guessing
                if (health <= 0)
                {
                    ///...update the player state and print player feedback to the screen
                    //added return; to break it from the loop and see if this will display gameover sequence
                    playerIsAlive = false;
                    Console.WriteLine("You died...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
            }
        }
        /// <summary>
        /// Prints the menu for restarting or exiting the game
        /// </summary>
        //JJ Debug: changed input to inputText and made it all strings to work with while loop function below after changes made...
        // to get it to stop looping forever.
        void DisplayMainMenu()
        {
            ///Display question and store input
            string inputText = GetInput("Would you like to play again?", "Yes", "No");

            ///If the player decides to restart...
            if (inputText == "1")
            {
                ///...set their current area to be the start and update the player state to be alive
                currentArea = 1;
                gameOver = false;
                playerIsAlive = true;
            }
            ///Otherwise if the player wants to quit...
            else if (inputText == "2")
            {
                //...set game over to be true
                gameOver = true;
            }
        }

        /// <summary>
        /// Prints the text for the test room
        /// </summary>
        //JJ Debug: syntax Error. forgot ';' at the end of 'Console.WriteLine(-text-)' on second line.
        void Room3()
        {
            Console.Clear();
            Console.WriteLine("You've reached the end of your journey!");
        }


        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="option1">The first option the player can choose</param>
        /// <param name="option2">The second option the player can choose</param>
        /// <returns></returns>
        //JJ Debug: unsure if i should define more variables in higher scope or leave local variables such as 'inputReceived' as is.
        // will impliment 'inputText' for now. also unsure if putting 'string inputText' here defines it as a local variable or reference
        // to the global variable I made earlier.
        //Changed int GetInput to string GetInput beacause string makes more sense for the output needed for the function.
        string GetInput(string description, string option1, string option2)
        {
            string inputText = "";
            string inputReceived = "";

            ///While input is not 1 or 2 display the options
            //changed inputReceived from 'int' to 'string'
            //while (!(inputText != "1" && inputText != "2")) changed to while (inputText != "1" && inputText != "2") because
            //unnecessary syntax.
            while (inputText != "1" && inputText != "2")
            {
                ///Print options
                //JJ Debug: changing 'Console.Write'  below for all the options and description to 'Console.WriteLine'...
                // that way all the text doesn't bunch up.
                Console.WriteLine(description);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.Write("> ");

                ///Get input from player
                inputText = Console.ReadLine();
                
                //JJ Debug: changing != for both parts of the initial if statement to == because != doesn't make sense to me.
                //JJ Debug: changed 2nd if statement to else if to connect them or however you say it. 

                ///If player selected the first option...
                if (inputText == "1" || inputText == option1)
                {
                    ///Set input received to be the first option
                    inputReceived = "1";
                }
                ///Otherwise if the player selected the second option...
                else if (inputText == "2" || inputText == option2)
                {
                    ///Set input received to be the second option
                    inputReceived = "2";
                }
                ///If neither are true...
                else
                {
                    ///...display error message
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey();
                }

                Console.Clear();
            }

            return inputReceived;
        }

        public void Run()
        {
            ///Loop while game isn't over
            ///penis
            while (gameOver == false)
            {
                ///Print the current room to the screen
                //JJ Debug changed CurrentArea >= to == hoping that allows it to increment the room number. def 
                if (currentArea == 1)
                {
                    Room1();
                }
                if (currentArea == 2)
                {
                    Room2();
                }
                if (currentArea == 3)
                {
                    Room3();
                }

                ///If the player lost or beat the game...
                if (playerIsAlive == false || currentArea == 3)
                {
                    //...print main menu
                    DisplayMainMenu();
                }
                ///Otherwise if the player is alive and hasn't finished...
               
                else
                {
                    ///...increment the current area
                    currentArea++;
                }
            }
        }   
    }
}
