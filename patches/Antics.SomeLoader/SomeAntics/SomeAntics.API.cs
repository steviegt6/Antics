using Microsoft.Xna.Framework.Input;
using SomeAntics.API;
using Webmilio.Commons.Extensions;

namespace SomeAntics
{
	partial class SomeAntics
	{
		public delegate void InstanceDelegate(SomeAntics game);

		public static event InstanceDelegate PreInitialize;

		public static event InstanceDelegate PostInitialize;

		public static event InstanceDelegate PreLoadContent;

		public static event InstanceDelegate PostLoadContent;

		public static event InstanceDelegate PreUpdateGlobal;

		public static event InstanceDelegate PostUpdateGlobal;

		public static event InstanceDelegate PreDrawGlobal;

		public static event InstanceDelegate PostDrawGlobal;

		public bool JustPressed(Keys key) {
			bool trueValue = do_JustPressed(ref key);
			bool? value = null;

			foreach (Mod mod in ModManager.Mods)
				value = mod.RegisterKeyPress(key);

			return value ?? trueValue;
		}

		public bool JustReleased(Keys key) {
			bool trueValue = do_JustReleased(ref key);
			bool? value = null;

			foreach (Mod mod in ModManager.Mods)
				value = mod.RegisterKeyRelease(key);

			return value ?? trueValue;
		}

		public bool JustLeftClicked() {
			bool trueValue = do_JustLeftClicked();
			bool? value = null;

			foreach (Mod mod in ModManager.Mods)
				value = mod.RegisterLeftClick();

			return value ?? trueValue;
		}

		public bool JustRightClicked() {
			bool trueValue = do_JustRightClicked();
			bool? value = null;

			foreach (Mod mod in ModManager.Mods)
				value = mod.RegisterRightClick();

			return value ?? trueValue;
		}
	}
}
