using Microsoft.Xna.Framework;
using System.Linq;
using WordLibrary;

namespace SomeAntics.API.ModTypes
{
	public abstract class GlobalDrawnWord : ModLoadable
	{
		public static Word GetModifiedWord(Word word) {
			foreach (GlobalDrawnWord global in ModManager.Mods.SelectMany(mod => mod.globalDrawnWords))
				global.ModifyWord(ref word);

			return word;
		}

		public static Rectangle[] GetModifiedSurroundingBoxes(Rectangle[] surroundingBoxes) {
			foreach (GlobalDrawnWord global in ModManager.Mods.SelectMany(mod => mod.globalDrawnWords))
				global.ModifySurroundingBoxes(ref surroundingBoxes);

			return surroundingBoxes;
		}

		public static bool HitboxContains(Vector2 position, bool retVal) => ModManager.Mods
			.SelectMany(mod => mod.globalDrawnWords)
			.Aggregate(retVal, (current, global) => global.HitboxContains(ref position, current));

		public virtual void ModifyWord(ref Word word) {
		}

		public virtual void ModifySurroundingBoxes(ref Rectangle[] surroundingBoxes) {
		}

		public virtual bool HitboxContains(ref Vector2 position, bool retVal) {
			return retVal;
		}
	}
}