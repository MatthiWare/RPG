using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Scenes
{
    public class SceneCollection : KeyedCollection<String, Scene>
    {
        protected override void ClearItems()
        {
            foreach (Scene scene in this)
            {
                scene.Dispose();
            }

            base.ClearItems();
        }

        protected override string GetKeyForItem(Scene item)
        {
            return item.Name;
        }
    }
}
