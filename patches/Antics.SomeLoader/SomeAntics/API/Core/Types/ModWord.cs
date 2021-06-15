using SomeAntics.API.Core.Interfaces;
using WordLibrary;

namespace SomeAntics.API.Core.Types
{
    public class ModWord : Word, IModIdentity
    {
        public string ModName { get; }

        protected ModWord() : base("")
        {
            ModName = "Antics";
        }

        internal ModWord(string text) : base(text)
        {
            ModName = "Antics";
        }

        public ModWord(string text, string modName) : base(text)
        {
            ModName = modName;
        }
    }
}