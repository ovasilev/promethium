//-----------------------------------------------------------------------
// <copyright file="IPlayer.cs" company="TelerikAcademy">
//     All rights reserved © Telerik Academy 2012-2013
// </copyright>
//-----------------------------------------------------------------------

namespace BullsAndCowsGame.Intefaces
{
    using System;

    /// <summary>
    /// Interface for Bulls and Cows player
    /// </summary>
    public interface IPlayer : IComparable<IPlayer>
    {
        string Name { get; set; }
        int Attempts { get; set; }
    }
}
