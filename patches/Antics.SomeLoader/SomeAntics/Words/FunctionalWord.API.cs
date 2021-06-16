using SomeAntics.API.ModTypes;
using SomeAntics.Objects.Interactables;
using WordLibrary;

namespace SomeAntics.Words
{
	partial class FunctionalWord
	{
		private string _text;

		public virtual string FunctionalText {
			get => _text;
			set => _text = value;
		}

		public override string Text => FunctionalText;

		public void do_Affect(SomeAntics game, Interactable source) {
			FunctionalWord @this = this;
			if (GlobalFunctionalWord.GetPreAffects(this, ref game, ref source)) 
				Affect(game, source);
			GlobalFunctionalWord.GetPostAffects(this, game, source);
		}

		public virtual void do_OnTransferred(Interactable before, Interactable after) {
			if (GlobalFunctionalWord.GetPreTransferreds(this, ref before, ref after)) 
				OnTransferred(before, after);
			GlobalFunctionalWord.GetPostTransferreds(this, before, after);
		}

		public virtual void do_OnTransferredWithWord(Interactable before, Interactable after, Word word) {
			if (GlobalFunctionalWord.GetPreTransferredWithWords(this, ref before, ref after, ref word)) 
				OnTransferred(before, after, word);
			GlobalFunctionalWord.GetPostTransferredWithWords(this, before, after, word);
		}

		public bool do_CanBeTransferredTo(Interactable interactable) {
			return GlobalFunctionalWord.GetModifiedCanBeTransferredTo(this, interactable, CanBeTransferredTo(interactable));
		}
	}
}
