using SomeAntics.Objects.Interactables;
using SomeAntics.Words;
using System.Linq;
using WordLibrary;

namespace SomeAntics.API.ModTypes
{
	public abstract class GlobalFunctionalWord : ModLoadable
	{
		public static bool GetPreAffects(FunctionalWord funcWord, ref SomeAntics game, ref Interactable source) {
			bool retVal = true;
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				retVal = global.PreAffect(funcWord, ref game, ref source);

			return retVal;
		}

		public static void GetPostAffects(FunctionalWord funcWord, SomeAntics game, Interactable source) {
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				global.PostAffect(funcWord, game, source);
		}

		public static bool GetPreTransferreds(FunctionalWord funcWord, ref Interactable before,
			ref Interactable after) {
			bool retVal = true;
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				retVal = global.PreTransferred(funcWord, ref before, ref after);

			return retVal;
		}

		public static void GetPostTransferreds(FunctionalWord funcWord, Interactable before, Interactable after) {
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				global.PostTransferred(funcWord, before, after);
		}

		public static bool GetPreTransferredWithWords(FunctionalWord funcWord, ref Interactable before,
			ref Interactable after, ref Word word) {
			bool retVal = true;
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				retVal = global.PreTransferredWithWord(funcWord, ref before, ref after, ref word);

			return retVal;
		}

		public static void GetPostTransferredWithWords(FunctionalWord funcWord, Interactable before, Interactable after,
			Word word) {
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				global.PostTransferredWithWord(funcWord, before, after, word);
		}

		public static bool GetModifiedCanBeTransferredTo(FunctionalWord funcWord, Interactable interactable,
			bool retVal) {
			bool? value = null;
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				value = global.ModifyCanBeTransferredTo(funcWord, interactable, retVal);

			return value ?? retVal;
		}

		public static string GetModifiedWord(string word) {
			foreach (GlobalFunctionalWord global in ModManager.Mods.SelectMany(mod => mod.globalFunctionalWords))
				global.ModifyWord(ref word);

			return word;
		}

		public virtual bool PreAffect(FunctionalWord funcWord, ref SomeAntics game, ref Interactable source) {
			return true;
		}

		public virtual void PostAffect(FunctionalWord funcWord, SomeAntics game, Interactable source) {
		}

		public virtual bool PreTransferred(FunctionalWord funcWord, ref Interactable before, ref Interactable after) {
			return true;
		}

		public virtual void PostTransferred(FunctionalWord funcWord, Interactable before, Interactable after) {
		}

		public virtual bool PreTransferredWithWord(FunctionalWord funcWord, ref Interactable before,
			ref Interactable after, ref Word word) {
			return true;
		}

		public virtual void PostTransferredWithWord(FunctionalWord funcWord, Interactable before, Interactable after,
			Word word) {
		}

		public virtual bool? ModifyCanBeTransferredTo(FunctionalWord funcWord, Interactable interactable, bool retVal) {
			return null;
		}

		public virtual void ModifyWord(ref string word) {
		}
	}
}