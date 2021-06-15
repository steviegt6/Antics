using Microsoft.Xna.Framework;
using SomeAntics.Particles;

namespace SomeAntics.API.Core.Types
{
    public class ModParticle : Particle
    {
        public ModParticle(Vector2 position, Color color, string modName) : base(position, color)
        {
            ModName = modName;
        }

        public ModParticle(Vector2 position, Color color, int size, string modName) : base(position, color, size)
        {
            ModName = modName;
        }

        public ModParticle(Vector2 position, Color color, int size, int lifespan, string modName) : base(position, color, size, lifespan)
        {
            ModName = modName;
        }

        public ModParticle(Vector2 position, Vector2 velocity, Color color, int size, float rotation, float rotSpeed, int life, string modName) : base(position, velocity, color, size, rotation, rotSpeed, life)
        {
            ModName = modName;
        }
    }
}