using System;
using System.Collections.Generic;
using System.Resources;
using System.Reflection;
using System.Linq;

namespace Hangman
{
  class HangmanLogic
  {
    string m_strDisplayWord;
    public string m_strSecretWord;  // Saves the Randomly selected Secret Word
    private int m_iSecretWordLength;
    private char[] m_arrSecretWordOriginal; // Saves each character of the Secret Word
    private string[] m_arrSecretWord; // Saves each character of the Secret Word as a string in the array with lower cases
    private string[] m_arrSecretWordB; // Saves each character of the Secret Word as a string in the array with Upper Cases

    // Saves the allready Guessed Letters
    public List<char> m_lstGuessedLetters = new List<char>();

      // Chooses a Random Word from the Word List
      public void ChooseRandomSecretWord()
      {
        Random rnd = new Random();
        int index = rnd.Next(1, 102);

        ResourceManager rm = new ResourceManager("Hangman.Resources.WordList", Assembly.GetExecutingAssembly());
        m_strSecretWord = rm.GetString("String" + index);

        m_iSecretWordLength = m_strSecretWord.Length;

        ConvertSecretWordToArray();
      }

    // Converts the secret word into a string array for individual character access
    public void ConvertSecretWordToArray()
    {
      m_arrSecretWord = new string[m_strSecretWord.Length];
      m_arrSecretWordB = new string[m_strSecretWord.Length];
      m_arrSecretWordOriginal = new char[m_strSecretWord.Length];

      for (int i = 0; i < m_strSecretWord.Length; i++)
      {
        // Stores each character as a string in the array
        m_arrSecretWordOriginal[i] = m_strSecretWord[i];
        m_arrSecretWord[i] = m_strSecretWord[i].ToString().ToLower(); // [b, a, u, m]
        m_arrSecretWordB[i] = m_strSecretWord[i].ToString().ToUpper(); // [B, A, U, M]
      }
    }

    // Displays the current state of the word with guessed letters and shows the rest hidden
    public string DisplayWordWithGuesses()
    {
      m_strDisplayWord = "│ Zu erratenes Wort > ";

      for (int i = 0; i < m_arrSecretWord.Length || i < m_arrSecretWordB.Length; i++)
      {

        string currentChar = m_arrSecretWord[i];
        string currentCharB = m_arrSecretWordB[i];

        // If the character has been guessed, display it. Else show an underscore ("_")
        if (m_lstGuessedLetters.Contains(currentChar[0]) || m_lstGuessedLetters.Contains(currentCharB[0]))
        {

          m_strDisplayWord += m_arrSecretWordOriginal[i];
        }
        else
        {
          m_strDisplayWord += "_";
        }
      }
      return (m_strDisplayWord); // Output: "_a__"
    }

    // Adds a guessed letter to the list if it hasn't been guessed yet,
    public void GuessLetter(char letter, HangmanGameManager gm, HangmanUI ui, HangmanLogic logic)
    {
      //If the letter has not been guessed before and is not in the secret word: add the letter and reduce the remaining guesses
      if (!m_lstGuessedLetters.Contains(letter) && !m_arrSecretWord.Contains(letter.ToString()) && !m_arrSecretWordB.Contains(letter.ToString()))
      {
        m_lstGuessedLetters.Add(letter);
        gm.m_iRemainingGuesses--;
      }
      // If letter was not guessed before and in the secret word - add to list
      else if (!m_lstGuessedLetters.Contains(letter))
      {
        m_lstGuessedLetters.Add(letter);
      }
      // If the letter was already guessed and is incorrect, reduce by one try
      else if (m_lstGuessedLetters.Contains(letter) && !m_arrSecretWord.Contains(letter.ToString()) && !m_arrSecretWordB.Contains(letter.ToString()))
      {
        gm.m_iRemainingGuesses--; ;
      }
    }

    // Checks if all letters in the secret Word have been guessed and returns true if so
    public bool IsWordGuessed(HangmanGameManager gm)
    {
      foreach (char letter in m_strSecretWord)
      {
        // If any letter is not in the guessed letters list, return false
        if (!m_lstGuessedLetters.Contains(char.ToLower(letter)) && !m_lstGuessedLetters.Contains(char.ToUpper(letter)))
        {
          return false;
        }
      }
      return true;
    }
  }
}
