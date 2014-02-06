using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GissaTalet.Model
{
    public class SecretNumber
    {
        public SecretNumber()
        {
            _previousGuesses = new List<int>();
            _previousGuesses.Capacity = MaxNumberOfGuesses;
            this.Initialize();
        }

        private int _number;

        private List<int> _previousGuesses;

        public bool CanMakeGuess 
        {
            get
            {
                if (Outcome != Outcome.NoMoreGuesses || Outcome != Outcome.Correct)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int Count 
        {
            get
            {
                return PreviousGuesses.Count();
            }
        }

        public int? Number 
        {
            get
            {
                if (this.Outcome != Outcome.NoMoreGuesses)
                {
                    return null;
                }
                else
                {
                    return _number;
                }
            }
        }

        public Outcome Outcome { get; set; }

        public IEnumerable<int> PreviousGuesses 
        {
            get
            {
                return _previousGuesses.AsReadOnly();
            }
        }

        public const int MaxNumberOfGuesses = 7;

        public void Initialize()
        {
            this._number = new Random().Next(1, 100);
            this.Outcome = Outcome.Indefinite;
            this._previousGuesses.Clear();
        }

        public Outcome MakeGuess(int guess)
        {
            if (guess > 100 || guess < 1)
            {
                this.Outcome = Outcome.Indefinite;
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                foreach (int previousGuess in this._previousGuesses)
                {
                    if (previousGuess == guess)
                    {
                        this.Outcome = Outcome.PreviousGuess;
                        return this.Outcome;
                    }
                }

                this._previousGuesses.Add(guess);

                if (guess > this._number)
                {
                    this.Outcome = Outcome.High;

                    if (this.Count == MaxNumberOfGuesses)
                    {
                        this.Outcome = Outcome.NoMoreGuesses;
                    }
                }
                else if (guess < this._number)
                {
                    this.Outcome = Outcome.Low; 

                    if (this.Count == MaxNumberOfGuesses)
                    {
                        this.Outcome = Outcome.NoMoreGuesses;
                    }
                }
                else
                {
                    this.Outcome = Outcome.Correct;
                }

                return this.Outcome;
            }
        }
    }

    public enum Outcome
    {
        Indefinite,
        Low,
        High,
        Correct,
        NoMoreGuesses,
        PreviousGuess
    }
}