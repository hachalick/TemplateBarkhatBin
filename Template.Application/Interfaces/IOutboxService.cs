using System;
using System.Collections.Generic;
using System.Text;
using Template.Application.DTOs;

namespace Template.Application.Interfaces
{
    public interface IOutboxService
    {
        Task AddAsync(OutboxMessageDto message);
    }
}
