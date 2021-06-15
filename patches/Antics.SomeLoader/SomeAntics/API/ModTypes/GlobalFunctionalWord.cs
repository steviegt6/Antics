using SomeAntics.Objects.Interactables;
using System.Linq;
using WordLibrary;

namespace SomeAntics.API.ModTypes
{
	public abstract class GlobalFunctionalWord : ModLoadable
	{
		public static bool GetPreAffects(ref SomeAntics game, ref Interactable source) {
			bool retVal = true;
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				retVal = global.PreAffect(ref game, ref source);

			return retVal;
		}

		public static void GetPostAffects(SomeAntics game, Interactable source) {
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				global.PostAffect(game, source);
		}

		public static bool GetPreTransferreds(ref Interactable before, ref Interactable after) {
			bool retVal = true;
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				retVal = global.PreTransferred(ref before, ref after);

			return retVal;
		}

		public static void GetPostTransferreds(Interactable before, Interactable after) {
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				global.PostTransferred(before, after);
		}

		public static bool GetPreTransferredWithWords(ref Interactable before, ref Interactable after, ref Word word) {
			bool retVal = true;
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				retVal = global.PreTransferredWithWord(ref before, ref after, ref word);

			return retVal;
		}

		public static void GetPostTransferredWithWords(Interactable before, Interactable after, Word word) {
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				global.PostTransferredWithWord(before, after, word);
		}

		public static bool GetModifiedCanBeTransferredTo(Interactable interactable, bool retVal) {
			bool? value = null;
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				value = global.ModifyCanBeTransferredTo(interactable, retVal);

			return value ?? retVal;
		}

		public virtual bool PreAffect(ref SomeAntics game, ref Interactable source) {
			return true;
		}

		public virtual void PostAffect(SomeAntics game, Interactable source) {
		}

		public virtual bool PreTransferred(ref Interactable before, ref Interactable after) {
			return true;
		}

		public virtual void PostTransferred(Interactable before, Interactable after) {
		}

		public virtual bool PreTransferredWithWord(ref Interactable before, ref Interactable after, ref Word word) {
			return true;
		}

		public virtual void PostTransferredWithWord(Interactable before, Interactable after, Word word) {
		}

		public virtual bool? ModifyCanBeTransferredTo(Interactable interactable, bool retVal) {
			return null;
		}
	}
}