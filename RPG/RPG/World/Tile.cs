using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.World
{
    public class Tile : GameObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width, Height;
        public int Id { get; set; }
        public float Alpha = 1f;
        public float Scale = 1f;
        public float Rotation = 1f;
        private Texture2D texture;

        public override void Render(SpriteBatch batch)
        {
            base.Render(batch);

            
        }
    }
}
