using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Scenes
{
    public abstract class Scene : IDisposable
    {
        public string Name { get; set; }

        public bool IsLoaded { get; private set; }

        public Scene(string name)
        {
            this.Name = name;
        }

        public virtual void LoadContent(ContentManager content)
        {
            IsLoaded = true;
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime time, RPGGame game)
        {
            if (!IsLoaded)
                LoadContent(game.Content);
        }

        public virtual void Render(SpriteBatch batch)
        {
            if (!IsLoaded)
                return;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Scene() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
