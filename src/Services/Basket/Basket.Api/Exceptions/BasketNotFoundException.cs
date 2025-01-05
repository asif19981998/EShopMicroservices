﻿using BuildingBlocks.Exceptions;

namespace Basket.Api.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string userName) : base($"Basket for user: {userName} was not found.")
        {

        }
    }
}
