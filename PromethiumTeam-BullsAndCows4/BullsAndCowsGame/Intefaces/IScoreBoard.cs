﻿//-----------------------------------------------------------------------
// <copyright file="IScoreBoard.cs" company="TelerikAcademy">
//     All rights reserved © Telerik Academy 2012-2013
// </copyright>
//-----------------------------------------------------------------------

namespace BullsAndCowsGame.Intefaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Iterface for Bulls and Cows ScoreBoard
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public interface IScoreBoard<T> : IEnumerable<T>, IEnumerator<T> where T : IComparable<T>
    {
        int Count { get; }
        void Add(T item);
    }
}
