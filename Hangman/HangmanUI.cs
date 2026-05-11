using System;


namespace Hangman
{
  class HangmanUI
  {
    // Displays the prompt to Press Alt + Enter for Fullscreen
    public void PromptToPressButtons()
    {
      // Moves the text down
      Spacer(12);

      Console.ForegroundColor = ConsoleColor.Gray;
      ShowText(@"
┌─────────────────────────────────────────────────────────────────────────────┐
│  _____  _                                                          _ _      │
│ |  __ \| |                                                   /\   | | |     │
│ | |__) | | ___  __ _ ___  ___   _ __  _ __ ___  ___ ___     /  \  | | |_    │
│ |  ___/| |/ _ \/ _` / __|/ _ \ | '_ \| '__/ _ \/ __/ __|   / /\ \ | | __|   │
│ | |    | |  __/ (_| \__ \  __/ | |_) | | |  __/\__ \__ \  / ____ \| | |_    │
│ |_|    |_|\___|\__,_|___/\___| | .__/|_|  \___||___/___/ /_/    \_\_|\__|   │
│                                | |                                          │
│                                |_|                                          │
│          ______       _               _          ______       _             │
│    _    |  ____|     | |             | |        |  ____|     | |            │
│  _| |_  | |__   _ __ | |_ ___ _ __   | |_ ___   | |__   _ __ | |_ ___ _ __  │
│ |_   _| |  __| | '_ \| __/ _ \ '__|  | __/ _ \  |  __| | '_ \| __/ _ \ '__| │
│   |_|   | |____| | | | ||  __/ |     | || (_) | | |____| | | | ||  __/ |    │
│         |______|_| |_|\__\___|_|      \__\___/  |______|_| |_|\__\___|_|    │
│                                                                             │
│                                                                             │
│  _   _                                                                      │
│ | | | |                                                                     │
│ | |_| |__   ___    __ _  __ _ _ __ ___   ___                                │
│ | __| '_ \ / _ \  / _` |/ _` | '_ ` _ \ / _ \                               │
│ | |_| | | |  __/ | (_| | (_| | | | | | |  __/_                              │
│  \__|_| |_|\___|  \__, |\__,_|_| |_| |_|\___(_)                             │
│                    __/ |                                                    │
│                   |___/                                                     │
└─────────────────────────────────────────────────────────────────────────────┘");
      Console.ResetColor();
    }

    public int m_iMenuIndex = 0;
    public string[] m_arrMenuOptions = { "Starten", "Wie Funktioniert Hangman?", "Beenden" };

    // Displays the main menu and allows the user to naviagte through the options
    public void StartProgram()
    {
      Console.CursorVisible = false;
      HangmanLogic logic = new HangmanLogic();
      HangmanGameManager gm = new HangmanGameManager();

      while (true)
      {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Gray;

        // Moves the text down
        Spacer(15);

        ShowFrequentConsoleDesign(@"
 __          ___ _ _ _                                              _          _       _    _                                         
 \ \        / (_) | | |                                            | |        (_)     | |  | |                                        
  \ \  /\  / / _| | | | _____  _ __ ___  _ __ ___   ___ _ __       | |__   ___ _      | |__| | __ _ _ __   __ _ _ __ ___   __ _ _ __  
   \ \/  \/ / | | | | |/ / _ \| '_ ` _ \| '_ ` _ \ / _ \ '_ \      | '_ \ / _ \ |     |  __  |/ _` | '_ \ / _` | '_ ` _ \ / _` | '_ \ 
    \  /\  /  | | | |   < (_) | | | | | | | | | | |  __/ | | |     | |_) |  __/ |     | |  | | (_| | | | | (_| | | | | | | (_| | | | |
     \/  \/   |_|_|_|_|\_\___/|_| |_| |_|_| |_| |_|\___|_| |_|     |_.__/ \___|_|     |_|  |_|\__,_|_| |_|\__, |_| |_| |_|\__,_|_| |_|
                                                                                                           __/ |                      
                                                                                                          |___/                       
");

        // Loop through all menu options to display them
        for (int i = 0; i < m_arrMenuOptions.Length; i++)
        {
          // Highlight currently selected option
          if (i == m_iMenuIndex)
          {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine(" > {0}", m_arrMenuOptions[i]);
            Console.ResetColor();
          }
          else
          {
            // Disply unselected options with default design
            Console.WriteLine("  {0}", m_arrMenuOptions[i]);
          }

        }
        Spacer(1);
        Console.BackgroundColor = ConsoleColor.Gray;
        for (int i = 0; i < 134; i++) Console.Write(" ");
        Console.ResetColor();

        // Checks what key was pressed and if the game should stop
        bool shouldExit = gm.InterpretMenuKeys(this, gm);

        if (shouldExit)
        {
          break;
        }
      }
    }

    // Text for How Hangman works
    public void ShowRules()
    {
      //Moves the text down
      Spacer(15);

      ShowFrequentConsoleDesign(@"
 __          ___            ______           _    _   _             _           _         _    _                                        ___  
 \ \        / (_)          |  ____|         | |  | | (_)           (_)         | |       | |  | |                                      |__ \ 
  \ \  /\  / / _  ___      | |__ _   _ _ __ | | _| |_ _  ___  _ __  _  ___ _ __| |_      | |__| | __ _ _ __   __ _ _ __ ___   __ _ _ __   ) |
   \ \/  \/ / | |/ _ \     |  __| | | | '_ \| |/ / __| |/ _ \| '_ \| |/ _ \ '__| __|     |  __  |/ _` | '_ \ / _` | '_ ` _ \ / _` | '_ \ / / 
    \  /\  /  | |  __/     | |  | |_| | | | |   <| |_| | (_) | | | | |  __/ |  | |_      | |  | | (_| | | | | (_| | | | | | | (_| | | | |_|  
     \/  \/   |_|\___|     |_|   \__,_|_| |_|_|\_\\__|_|\___/|_| |_|_|\___|_|   \__|     |_|  |_|\__,_|_| |_|\__, |_| |_| |_|\__,_|_| |_(_)  
                                                                                                              __/ |                          
                                                                                                             |___/                           
");

      ShowText("Ziel des Spiels:");
      ShowText("Errate das geheime Wort, bevor das Hangman komplett gezeichnet ist.");
      Spacer(1);
      ShowText("So funktioniert's:");
      ShowText("1. Das Spiel wählt ein geheimes Wort.");
      ShowText("2. Du siehst Platzhalter (_) für jeden Buchstaben im Wort.");
      ShowText("3. Gib einzelne Buchstaben ein, die du im Wort vermutest oder direkt das ganze Wort");
      ShowText("4. Richtige Buchstaben werden im Wort angezeigt.");
      ShowText("5. Falsche Buchstaben führen dazu, dass ein Teil des Hängemännchens gezeichnet wird.");
      ShowText("6. Du hast nur eine begrenzte Anzahl an Fehlversuchen (6 Stück).");
      ShowText("7. Du gewinnst, wenn du das ganze Wort errätst, bevor das Hangman fertig ist.");
      Spacer(1);
      ShowFrequentConsoleDesign("\nDrücke eine Taste, um zurückzugehen...");
    }

    int m_iStatesIndex; //Saves current Hangman state

    //Displays the Visuals of the Game while Running
    public void DisplayGameStartVisuals(HangmanLogic logic, HangmanGameManager gm)
    {
      Console.Clear();

      // Moves the text down
      Spacer(10);

      ShowFrequentConsoleDesign(@"
  _    _                                                     
 | |  | |                                                    
 | |__| |   __ _   _ __     __ _   _ __ ___     __ _   _ __  
 |  __  |  / _` | | '_ \   / _` | | '_ ` _ \   / _` | | '_ \ 
 | |  | | | (_| | | | | | | (_| | | | | | | | | (_| | | | | |
 |_|  |_|  \__,_| |_| |_|  \__, | |_| |_| |_|  \__,_| |_| |_|
                            __/ |                            
                           |___/                             
");

      Spacer(2);

      // Shows the current state of the secretWord whats letters have been guessed
      ShowText(@"┌──────────────────────────────────────────────");
      ShowText(logic.DisplayWordWithGuesses());
      ShowText(@"└──────────────────────────────────────────────");

      // Calculates the Index based on the RemainingGuesses
      m_iStatesIndex = 0 + (6 - gm.m_iRemainingGuesses);

      // Displays the Current Hangman State
      Console.Write(m_arrHangmanStates[m_iStatesIndex]);

      Spacer(2);

      // Display the current User Input
      Console.WriteLine("  Deine Eingabe >  {0}  ", gm.m_strUserInput);
      ShowText(@"└──────────────────────────────────────────────");
      ShowFrequentConsoleDesign("Rate einen Buchstaben oder versuche gleich das ganze Wort!");
      Spacer(2);
      Console.BackgroundColor = ConsoleColor.DarkRed;

      // Displays the remaining Guesses of the User
      Console.WriteLine("Verbleibende Versuche > {0}", gm.m_iRemainingGuesses);
      Spacer(1);
      Console.ResetColor();

      // ESC to Exit
      ShowFrequentConsoleDesign("\nDrücke ESC, um ins Main Menu zurückzugehen...");
      Spacer(1);

      // Temporary Cheat - Shows the Solution if Input is "3141" - Hides when Input of "3141" is removed
      if (gm.m_strUserInput == "3141")
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Lösung > {0}", logic.m_strSecretWord);
        Console.ResetColor();
      }

    }

    // Displays the Win page
    public void ShowWinPage(HangmanLogic logic, HangmanGameManager gm)
    {
      Console.Clear();
      Spacer(10);
      ShowFrequentConsoleDesign(@"
  _____              _               _                                                         _ 
 |  __ \            | |             | |                                                       | |
 | |  | |_   _      | |__   __ _ ___| |_        __ _  _____      _____  _ __  _ __   ___ _ __ | |
 | |  | | | | |     | '_ \ / _` / __| __|      / _` |/ _ \ \ /\ / / _ \| '_ \| '_ \ / _ \ '_ \| |
 | |__| | |_| |     | | | | (_| \__ \ |_      | (_| |  __/\ V  V / (_) | | | | | | |  __/ | | |_|
 |_____/ \__,_|     |_| |_|\__,_|___/\__|      \__, |\___| \_/\_/ \___/|_| |_|_| |_|\___|_| |_(_)
                                                __/ |                                            
                                               |___/                                             
");
      // Displays the Hangman State
      Console.WriteLine(m_arrHangmanStates[m_iStatesIndex]);
      ShowText(@"───────────────────────────────────");

      Console.Write("Dein erratendes Wort > ");

      Console.ForegroundColor = ConsoleColor.DarkRed;
      Console.Write(logic.m_strSecretWord);
      Spacer(1);
      Console.ResetColor();
      ShowText(@"───────────────────────────────────");
      Console.BackgroundColor = ConsoleColor.DarkRed;

      // Displays remaining guesses
      Console.WriteLine("Verbliebende Versuche > {0}", gm.m_iRemainingGuesses);
      Console.ResetColor();

      ShowFrequentConsoleDesign("\nDrücke ESC, um ins Main Menu zurückzugehen...");
      ShowFrequentConsoleDesign("\nDrücke eine beliebige andere Taste, um erneut zu Spielen...");

      // ESC to Exit or any other button to play again
      gm.ExitOrStartAgain(gm);
    }

    // Displays the loss page
    public void ShowLossPage(HangmanLogic logic, HangmanGameManager gm)
    {
      Console.Clear();
      Spacer(10);
      ShowFrequentConsoleDesign(@"
  _____              _               _                        _                      _ 
 |  __ \            | |             | |                      | |                    | |
 | |  | |_   _      | |__   __ _ ___| |_      __   _____ _ __| | ___  _ __ ___ _ __ | |
 | |  | | | | |     | '_ \ / _` / __| __|     \ \ / / _ \ '__| |/ _ \| '__/ _ \ '_ \| |
 | |__| | |_| |     | | | | (_| \__ \ |_       \ V /  __/ |  | | (_) | | |  __/ | | |_|
 |_____/ \__,_|     |_| |_|\__,_|___/\__|       \_/ \___|_|  |_|\___/|_|  \___|_| |_(_)
                                                                                       
                                                                                       
");
      Console.WriteLine(m_arrHangmanStates[6]);
      ShowText(@"───────────────────────────────────");
      Console.Write("Das gesuchte Wort war > ");
      Console.ForegroundColor = ConsoleColor.DarkRed;
      Console.Write(logic.m_strSecretWord);
      Console.ResetColor();
      Spacer(1);
      ShowText(@"───────────────────────────────────");
      ShowText("Try again  - vielleicht klappt es beim nächsten Mal!");

      ShowFrequentConsoleDesign("\nDrücke ESC, um ins Main Menu zurückzugehen...");
      ShowFrequentConsoleDesign("\nDrücke eine beliebige andere Taste, um erneut zu Spielen...");

      // ESC to Exit or any other button to play again
      gm.ExitOrStartAgain(gm);
    }

    // Arrays that contains the different states of Hangman 
    string[] m_arrHangmanStates = new string[7]
{
                                               // Try 7 (no mistakes)
    @"
                   +---+
                   |   |
                       |
                       |
                       |
                       |
                  =========",

                                                // Try 6 (Head)
    @"
                   +---+
                   |   |
                   O   |
                       |
                       |
                       |
                  =========",

                                                // Try 5 (Head + Body)
    @"
                   +---+
                   |   |
                   O   |
                   |   |
                       |
                       |
                  =========",

                                                 // Try 4 (left arm)
    @"
                   +---+
                   |   |
                   O   |
                  /|   |
                       |
                       |
                  =========",

                                                 // Try 3 (both arms)
    @"
                   +---+
                   |   |
                   O   |
                  /|\  |
                       |
                       |
                  =========",

                                                 // Try 2 (left leg)
    @"
                   +---+
                   |   |
                   O   |
                  /|\  |
                  /    |
                       |
                  =========",

                                                  // Try 1 (both legs)
    @"
                   +---+
                   |   |
                   O   |
                  /|\  |
                  / \  |
                       |
                  =========",
   };

    // Default design for some text
    private void ShowFrequentConsoleDesign(string designText)
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Console.ForegroundColor = ConsoleColor.Black;
      Console.BackgroundColor = ConsoleColor.Gray;
      Console.WriteLine(designText);
      Console.ResetColor();
    }

    // Creates Spaces in the console
    private void Spacer(int amount)
    {
      for (int i = 0; i < amount; i++) Console.WriteLine();
    }

    // Shortcut for Console.WriteLine();
    private void ShowText(string text)
    {
      Console.WriteLine(text);
    }
  }
}
