using System;
using Cards2;

public class Dice
{
    private int sides;
    private int topSide;
    private static Random rand = new Random();

    // Default constructor
    public Dice()
    {
        sides = 6;
    }

    // Constructor with specified number of sides
    public Dice(int sides)
    {
        if (sides < 4)
        {
            throw new ArgumentOutOfRangeException("sides", "Number of sides must be at least 4.");
        }
        else
        {
            this.sides = sides;
        }
    }

    public void Roll()
    {
        topSide = rand.Next(1, sides + 1);
    }

    public int TopSide
    {
        get { return topSide; }
    }

    public int Sides
    {
        get { return sides; }
    }
}

public class Game
{
    public static void Main(string[] args)
    {
        List<Card>[] players = new List<Card>[4];
        for (int i = 0; i < 4; i++)
        {
            players[i] = new List<Card>();
        }

        Dice dice = new Dice();
        int[] rolls = new int[4];
        bool dealerChosen = false;
        int dealer = 0;

        while (!dealerChosen)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (rolls[i] == 0)
                {
                    dice.Roll();
                    rolls[i] = dice.TopSide;
                    Console.WriteLine($"Player {i + 1} rolls {dice.TopSide}");
                }
            }


            int highestRoll = rolls.Max();
            List<int> tied = new List<int>();

            for (int i = 0; i < rolls.Length; i++)
            {
                if (rolls[i] == highestRoll)
                {
                    tied.Add(i);
                }
            }

            if (tied.Count == 1)
            {
                dealer = tied[0];
                Console.WriteLine($"Player {dealer + 1} is the dealer.");
                dealerChosen = true;
            }
            else
            {
                Console.WriteLine("There is a tie. Re-rolling for tied players.");
                foreach (int index in tied)
                {
                    rolls[index] = 0;
                }
            }

        }

        Deck deck = new Deck();
        deck.Shuffle();

        while (!deck.Empty)
        {
                players[dealer].Add(deck.TakeTopCard());
                dealer = (dealer + 1) % players.Length;
            
        }

        for (int i = 0; i < players.Length; i++)
        {
            Console.WriteLine($"Player {i + 1}:");
            foreach (Card card in players[i])
            {
                Console.WriteLine($"{card.Rank} of {card.Suit}");
            }
            Console.WriteLine();
        }
        
    }
}