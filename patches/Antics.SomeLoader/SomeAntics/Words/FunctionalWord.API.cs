using SomeAntics.API.ModTypes;
using SomeAntics.Objects.Interactables;
using WordLibrary;

namespace SomeAntics.Words
{
	partial class FunctionalWord
	{
		public void do_Affect(SomeAntics game, Interactable source) {
			if (GlobalFunctionalWord.GetPreAffects(ref game, ref source)) 
				Affect(game, source);
			GlobalFunctionalWord.GetPostAffects(game, source);
		}

		public virtual void do_OnTransferred(Interactable before, Interactable after) {
			if (GlobalFunctionalWord.GetPreTransferreds(ref before, ref after)) 
				OnTransferred(before, after);
			GlobalFunctionalWord.GetPostTransferreds(before, after);
		}

		public virtual void do_OnTransferredWithWord(Interactable before, Interactable after, Word word) {
			if (GlobalFunctionalWord.GetPreTransferredWithWords(ref before, ref after, ref word)) 
				OnTransferred(before, after, word);
			GlobalFunctionalWord.GetPostTransferredWithWords(before, after, word);
		}

		public bool do_CanBeTransferredTo(Interactable interactable) {
			return GlobalFunctionalWord.GetModifiedCanBeTransferredTo(interactable, CanBeTransferredTo(interactable));
		}
	}
}
