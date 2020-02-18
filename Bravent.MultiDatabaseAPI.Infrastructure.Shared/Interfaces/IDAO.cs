using System;
namespace Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces
{
    public interface IDAO<TID>
    {
        TID GetId();
        void SetNewId();
    }
}
