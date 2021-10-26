﻿using AssetQuote.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Interfaces.Services
{
    public interface IBotService
    {
        public Task<string> StartCommunication(BotThread thread);
    }
}
