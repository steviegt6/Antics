using SomeAntics.API.Core.Interfaces;

namespace SomeAntics.API.Core
{
    public abstract class Mod : IMod
    {
        public abstract string Name { get; }
    }
}