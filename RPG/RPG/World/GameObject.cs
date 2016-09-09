using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.World
{
    public abstract class GameObject
    {
        public virtual void LoadContent(ContentManager content)
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime time, RPGGame game)
        {

        }

        public virtual void Render(SpriteBatch batch)
        {

        }
    }
}
