using App.Domain.Events;

namespace App.Domain.Events
{
    public record ProductAddedEvent(int ID, string Name, decimal Price): IEventOrMessage;
} //property ve maplemelerin kısa hali
