//-----------------------------------------------------------------------
// <copyright file="Package.cs" company="FHTW">
//     Copyright (c) FHTW. All rights reserved.
// </copyright>
// <author>Maximilian Moerth</author>
// <summary>Represents a Package of Cards</summary>
//-----------------------------------------------------------------------

using MCTG.Models;
using MCTG.Models.Cards;

namespace MCTG.DAL
{

    public class Package
    {
        public const double PRICE = 5.0d;
        public Guid p_id;
        public ICard[] Cards = new ICard[5];
    }
}