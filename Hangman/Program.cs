using System;


namespace Hangman
{
  class Program
  {

    static void Main(string[] args)
    {

      Console.CursorVisible = false;

      // Checking for Window Resize to begin Program
      HangmanGameManager gm = new HangmanGameManager();
      gm.CheckWindowResize();

      // Starting the Program
      HangmanUI ui = new HangmanUI();
      ui.StartProgram();

    }
  }
}
