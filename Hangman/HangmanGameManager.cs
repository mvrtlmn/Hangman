using System;

namespace Hangman
{
  class HangmanGameManager
  {

    HangmanLogic logic = new HangmanLogic();
    HangmanUI ui = new HangmanUI();

    // Checking for Window Resize to begin Program
    public void CheckWindowResize()
    {
      // Displays the command to Press Alt + Enter for Fullscreen
      ui.PromptToPressButtons();

      // Save initial Window Size
      int initialWidth = Console.WindowWidth;
      int initialHeight = Console.WindowHeight;

      // Check if initial Window Size has changed
      while (true)
      {
        if (initialHeight != Console.WindowHeight && initialWidth != Console.WindowWidth)
        {
          break;
        }
      }
    }

    // Handles user input (arrow keys and Enter) for main menu navigation
    public bool InterpretMenuKeys(HangmanUI ui, HangmanGameManager gm)
    {
      // Reads user input
      ConsoleKeyInfo key = Console.ReadKey(true);

      // Choose next menu Option
      if (key.Key == ConsoleKey.DownArrow)
      {
        ui.m_iMenuIndex++;
        // Back to first option if end is exceeded
        if (ui.m_iMenuIndex >= ui.m_arrMenuOptions.Length)
        {
          ui.m_iMenuIndex = 0;
        }
      }

      // Choose last menu option
      else if (key.Key == ConsoleKey.UpArrow)
      {
        ui.m_iMenuIndex--;

        // Back to last entry if start is exceeded
        if (ui.m_iMenuIndex < 0)
        {
          ui.m_iMenuIndex = ui.m_arrMenuOptions.Length - 1;
        }
      }

      // Confirm selection
      else if (key.Key == ConsoleKey.Enter)
      {

        // Execute the action of the selected menu option
        RunMenuAction(ui, gm);
      }

      // Indicates that a selection has not been made --> Menu stays active
      return false;

    }

    // Execute the action of the selected menu option
    public void RunMenuAction(HangmanUI ui, HangmanGameManager gm)
    {
      switch (ui.m_iMenuIndex)
      {
        case 0: // "Starten" - Starting the game
          Console.Clear();
          StartGame(gm);
          break;
        case 1: // "Wie Funktioniert Hangman?" - Displays the Rules and the Concept
          Console.Clear();
          ui.ShowRules();
          Console.ReadKey(true);
          ui.StartProgram();
          break;
        case 2: // "Beenden" - Close the Program/Game
          Console.Clear();
          Environment.Exit(0);
          break;
      }
    }

    public string m_strUserInput = "";  // Saves the User Input
    private string m_strCharacter;  // Last Character Input by the User
    public string[] m_arrCharacters; // Saves the allready used Characters
    public int m_iCharacterIndex = 0; // Saves the amount of characters allready tipped

    // Start the Game
    public void StartGame(HangmanGameManager gm)
    {

      // Resets Game Stats from prevoius sessions
      ResetGameState();

      // Starts the Process of choosing a random Word
      logic.ChooseRandomSecretWord();

      while (true)
      {
        gm.AskForCharacters(logic, ui, gm);
      }

    }

    public int m_iRemainingGuesses = 6; // determines the remaining guesses

    // Ask for letters and updates to the current state
    public void AskForCharacters(HangmanLogic logic, HangmanUI ui, HangmanGameManager gm)
    {
      ConsoleKeyInfo key;
      while (true)
      {
        // Displays the Game Visuals
        ui.DisplayGameStartVisuals(logic, gm);

        key = Console.ReadKey(true);

        // Press Escape to go back to Main Menu
        if (key.Key == ConsoleKey.Escape)
        {
          ui.StartProgram();
          break;
        }

        // Checks if the Backspace key was pressend and there is at least 1 character in Input slot to delete
        if (key.Key == ConsoleKey.Backspace && m_iCharacterIndex > 0 && m_strUserInput.Length > 0)
        {
          m_iCharacterIndex--;

          // Removes last character from input string
          m_strUserInput = m_strUserInput.Substring(0, m_strUserInput.Length - 1);
          m_arrCharacters[m_iCharacterIndex] = "";

        }

        // Adds the choosen Character-guess to the Guessed Letter list if the user presses enter after tipping and if the input is not null or empty
        else if (key.Key == ConsoleKey.Enter && !string.IsNullOrEmpty(m_strUserInput))
        {
          
          // If the input lenght is 1 and m_strCharacter is not empty its get send to logic for verification
          if (m_strUserInput.Length == 1 && !string.IsNullOrEmpty(m_strCharacter))
          {
            logic.GuessLetter(m_strCharacter[0], gm, ui, logic);
          }
          else
          {
            //if input equals the random word --> Win
            if (m_strUserInput.ToLower() == logic.m_strSecretWord.ToLower())
            {
              foreach (char letter in logic.m_strSecretWord)
              {
                logic.GuessLetter(letter, gm, ui, logic);
              }
            }
          }

          m_strUserInput = "";  // Clears prevoius User Input from the displayn text
          m_strCharacter = "";  // Clears prevoius safed Character

          // No guesses Left --> Loss
          if (gm.m_iRemainingGuesses == 0)
          {

            Console.Clear();
            ui.ShowLossPage(logic, gm);
            break;
          }

          // If word is Guessed --> Win
          if (logic.IsWordGuessed(gm))
          {
            Console.Clear();
            ui.ShowWinPage(logic, gm);
            break;
          }

          break;
        }

        //Adds the letter to the list only if its an normal key not backspace or enter and if the array has space left
        else if (!char.IsControl(key.KeyChar) && m_iCharacterIndex < m_arrCharacters.Length)
        {
          m_strCharacter = key.KeyChar.ToString();
          m_strUserInput += m_strCharacter;
          m_arrCharacters[m_iCharacterIndex] = m_strCharacter;
          m_iCharacterIndex++;
        }
      }
    }

    // Resets the Game Stats from prevoius game
    public void ResetGameState()
    {
      m_iRemainingGuesses = 6;
      logic.m_lstGuessedLetters.Clear(); // Reset Guessletter List and secret word
      m_strUserInput = "";
      m_strCharacter = "";
      m_arrCharacters = new string[30];
      m_iCharacterIndex = 0;
    }

    // Press ESC to go back to Main Menu or any other button to restart game
    public void ExitOrStartAgain(HangmanGameManager gm)
    {
      ConsoleKeyInfo key = Console.ReadKey(true);
      if (key.Key == ConsoleKey.Escape)
      {
        ui.StartProgram();
      }
      else
      {
        StartGame(gm);
      }
    }
  }
}

