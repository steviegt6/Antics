using Microsoft.Xna.Framework;
using SomeAntics.Map.Levels;
using System;

namespace SomeAntics.API.Internal
{
	public class InternalMod : Mod
	{
		internal InternalMod(Version version) : base("Internal Systems", version) {
			SomeAntics.PostDrawGlobal += DrawModsAndVersion;
		}

		private void DrawModsAndVersion(SomeAntics game) {
			if (game.World.CurrentLevel is not MainMenu) {
				return;
			}

			DrawVersion();
			DrawMods();
		}

		private void DrawVersion() {
			const int offset = 6;
			Vector2 position = new Vector2(offset, SomeAntics.ResHeight - offset);
			Vector2 posOffset = new(0, SomeAntics.wordFont.MeasureString("A").Y);

			SomeAntics.spriteBatch.DrawString(SomeAntics.wordFont,
				$"SomeAntics {typeof(SomeAntics).Assembly.GetName().Version}", position - posOffset, Color.White);
			SomeAntics.spriteBatch.DrawString(SomeAntics.wordFont, $"Antics.SomeLoader {Version}",
				position - (posOffset * 2), Color.White);
		}

		private void DrawMods() {
			const int offset = 6;
			Vector2 position = new Vector2(offset, 0);

			void Draw(string text) {
				SomeAntics.spriteBatch.DrawString(SomeAntics.wordFont, text, position, Color.White);
				position += new Vector2(0, SomeAntics.wordFont.MeasureString(text).Y);
			}

			Draw("Loaded mods:");

			foreach (Mod mod in ModManager.Mods)
				Draw($"{mod.ModName} - v{mod.Version}");
		}
	}
}