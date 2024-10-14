internal class Program
{
    static string[] words = { "elephant", "giraffe", "dolphin", "penguin", "tiger", "zebra" }; // Список слов для угадывания
    static string wordToGuess; // Слово, которое нужно угадать
    static char[] guessedWord; // Текущее состояние отгаданного слова
    static List<char> guessedLetters = new List<char>(); // Список угаданных букв
    static int attemptsLeft = 6;

    private static void Main(string[] args)
    {
        StartGame();
    }

    static void StartGame()
    {
        Random random = new Random();
        wordToGuess = words[random.Next(words.Length)];
        guessedWord = new string('_', wordToGuess.Length).ToCharArray();

        Console.WriteLine("Welcome to Hangman!");
        Console.WriteLine("You need to guess the word by suggesting letters.");

        while (attemptsLeft > 0 && guessedWord.Contains('_'))
        {
            DisplayGameStatus();
            Console.Write("Enter a letter: ");
            char guessedLetter = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (!char.IsLetter(guessedLetter))
            {
                Console.WriteLine("Please enter a valid letter.");
                continue;
            }

            guessedLetter = char.ToLower(guessedLetter); // Приведение к нижнему регистру для корректности сравнения

            if (guessedLetters.Contains(guessedLetter))
            {
                Console.WriteLine("You've already guessed that letter.");
                continue;
            }

            guessedLetters.Add(guessedLetter);

            if (wordToGuess.Contains(guessedLetter))
            {
                UpdateGuessedWord(guessedLetter);
                Console.WriteLine($"Good job! The letter '{guessedLetter}' is in the word.");
            }
            else
            {
                attemptsLeft--;
                Console.WriteLine($"The letter '{guessedLetter}' is not in the word. Attempts left: {attemptsLeft}");
            }
        }

        EndGame();
    }

    static void DisplayGameStatus()
    {
        Console.WriteLine("\nWord to guess: " + new string(guessedWord));
        Console.WriteLine("Guessed letters: " + string.Join(", ", guessedLetters));
        DisplayHangman();
    }

    static void DisplayHangman()
    {
        // Простейшая визуализация виселицы с помощью ASCII-графики
        string[] hangmanStages = new string[]
        {
            "",
            " O ",
            " O \n | ",
            " O \n/| ",
            " O \n/|\\",
            " O \n/|\\\n/ ",
            " O \n/|\\\n/ \\"
        };

        Console.WriteLine(hangmanStages[6 - attemptsLeft]);
    }

    static void UpdateGuessedWord(char guessedLetter)
    {
        for (int i = 0; i < wordToGuess.Length; i++)
        {
            if (wordToGuess[i] == guessedLetter)
            {
                guessedWord[i] = guessedLetter;
            }
        }
    }

    static void EndGame()
    {
        if (attemptsLeft > 0)
        {
            Console.WriteLine("Congratulations! You've guessed the word: " + wordToGuess);
        }
        else
        {
            Console.WriteLine("Game over! The word was: " + wordToGuess);
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}