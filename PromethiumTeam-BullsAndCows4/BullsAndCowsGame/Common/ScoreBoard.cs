//-----------------------------------------------------------------------
// <copyright file="NumberManager.cs" company="TelerikAcademy">
//     All rights reserved © Telerik Academy 2012-2013
// </copyright>
//-----------------------------------------------------------------------


namespace BullsAndCowsGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class that manages the score board
    /// </summary>
    //Refactored original class "klasirane"s
    class ScoreBoard<T> : IEnumerable<T>, IEnumerator<T> where T : IComparable<T>
    {
        private readonly T[] data;
        private int count;
        private readonly int maxCount;
        private int position = -1;

        public int Count
        {
            get { return this.count; }
        }

        public T Current
        {
            get { return this.data[this.position]; }
        }
        
        public ScoreBoard() : 
            this(5)
        { }

        public ScoreBoard(int maxCount)
        {
            this.maxCount = maxCount;
            this.data = new T[this.maxCount];
            this.count = 0;
        }

        public void Add(T item)
        {
            if (item.CompareTo(this.data[this.maxCount - 1]) >= 0)
            {
                int tPointer = 0;
                while (item.CompareTo(this.data[tPointer]) < 0)
                {
                    tPointer++;
                }

                for (int i = this.maxCount - 1; i > tPointer; i--)
                {
                    this.data[i] = this.data[i - 1];
                }

                this.data[tPointer] = item;
                if (this.count < this.maxCount)
                {
                    this.count++;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)this;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator<T>)this;
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.data[this.position]; }
        }

        public bool MoveNext()
        {
            if (this.position < this.Count - 1)
            {
                this.position++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            this.position = -1;
        }

        public void Dispose()
        {
            this.Reset();
        }
    }      
}
