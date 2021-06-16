using SomeAntics.API;
using SomeAntics.API.ModTypes;
using WordLibrary;

namespace Antics.Tests.WordTest
{
	public class WordTest : Mod
	{
		public override string ModName => "Word Test";
	}

	public class WordGlobal : GlobalDrawnWord
	{
		public override void ModifyWord(ref Word word) {
			if (word.Text.Equals("you, the"))
				word = new Word("blockaroz-senpai, the");
		}
	}

	public class WordGlobalTwo : GlobalFunctionalWord
	{
		public override void ModifyWord(ref string word) {
			if (word.Equals("you, the"))
				word = "blockaroz-senpai, the";
		}
	}
}