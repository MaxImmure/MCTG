//-----------------------------------------------------------------------
// <copyright file="Package.cs" company="FHTW">
//     Copyright (c) FHTW. All rights reserved.
// </copyright>
// <author>Maximilian Moerth</author>
// <summary>Represents a Package of Cards</summary>
//-----------------------------------------------------------------------

//using MCTG.BL;
using MCTG.Models;
using MCTG.Models.Cards;

namespace MCTG.DAL
{

    public static class Package
    {
        public const double PRICE = 5.0d;

        public static ICard[] PurchasePackage(User customer)
        {
            /*if (customer == null) throw new UserNotFoundException(message: $"User not found (User equals {null}!)");
            if (customer.Coins < PRICE)
                throw new NotEnoughCoinsException(message: $"User {customer.Name} has not enough Coins! ({customer.Coins})");
            */
            //ToDo generate 5 random Cards

            return null; //ToDo return 5 random Cards
        }
    }
}