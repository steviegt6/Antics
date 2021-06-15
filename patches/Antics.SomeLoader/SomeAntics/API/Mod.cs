using Microsoft.Xna.Framework.Input;
using SomeAntics.API.Interfaces;
using System.Reflection;

// TODO: documentation
namespace SomeAntics.API
{
	public abstract class Mod : ILoadable
	{
		public Assembly Code { get; internal set; }

		// null -> vanilla behavior
		public virtual bool? RegisterKeyPress(Keys key) {
			return null;
		}

		public virtual void ModifyIncomingKeyPress(ref Keys key) {
		}

		public virtual bool? RegisterKeyRelease(Keys key) {
			return null;
		}

		public virtual void ModifyIncomingKeyRelease(ref Keys key) {
		}

		public virtual bool? RegisterLeftClick() {
			return null;
		}

		public virtual bool? RegisterRightClick() {
			return null;
		}

		public virtual void Load() {
		}
	}
}