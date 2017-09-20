using System;

namespace MSLuisLibraries.Interface {
    public interface ILuis {
        string SubscriptionKey { get; set; }

        string BaseAddress { get; set; }
        string AppId { get; set; }
        string VersionId { get; set; }

        Uri Uri { get; set; }

        IModels Models { get; }
    }
}
