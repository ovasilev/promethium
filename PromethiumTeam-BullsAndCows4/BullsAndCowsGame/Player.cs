﻿namespace BullsAndCowsGame
{
    using System;
    
    /// <summary>
    /// Represents a player
    /// </summary>
    public class Player : IComparable<Player>
    {
        #region Fields

        /// <summary>
        /// Represents validated player's name
        /// </summary>
        private string name;

        /// <summary>
        /// Represents validated player's attempts
        /// </summary>
        private int attempts;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="playerName">Player name</param>
        /// <param name="attempts">Player attempts</param>
        public Player(string playerName, int attempts)
        {
            this.Name = playerName;
            this.Attempts = attempts;
        }

        #endregion 

        #region Properties

        /// <summary>
        /// Gets or sets Player name
        /// </summary>
        public string Name {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Player name can`t be null, empty or only with white spaces");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets Player attempts
        /// </summary>
        public int Attempts 
        {
            get
            {
                return this.attempts;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Player attempts can`t be a negative number");
                }

                this.attempts = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares <see cref="Player"/> instances.
        /// </summary>
        /// <param name="other">An <see cref="Player"/> to compare with this <see cref="Player"/></param>
        /// <returns>Higher than zero if this instance have less attempts,
        /// lower than zero if the other player have less attempts
        /// and zero if the two players have the same number of attempts</returns>
        public int CompareTo(Player other)
        {
            if (other == null)
            {
                return 1;
            }
            
            return (other.Attempts - this.Attempts);
        }

        /// <summary>
        /// Returns the string representations of Player instance.
        /// </summary>
        /// <returns>Formated information about the player's name and attempts</returns>
        public override string ToString()
        {
            return string.Format("Player: \"{0}\", attempts: {1}", this.Name, this.Attempts);
        }

        #endregion
    }
}
