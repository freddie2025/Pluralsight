using System;

namespace NinjaDomain.Classes.Interfaces
{
    public interface IModificationHistory
    {
        DateTime DateModified { get; set; }
        DateTime DateCreated { get; set; }
        bool IsDirty { get; set; }
    }
}
