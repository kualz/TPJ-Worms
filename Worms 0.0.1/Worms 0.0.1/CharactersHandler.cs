using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worms_0._0._1
{
    class CharactersHandler : Characters
    {
        static public List<Characters> Barracks = new List<Characters>();

        static public void InitList(ContentManager content)
        {   
            Barracks.Add(new Characters("Kualz" ));
            Barracks.Add(new Characters("Phakrumn"));
            Barracks.Add(new Characters("Klipper"));
            Barracks.Add(new Characters("Zjeh"));
            Barracks.Add(new Characters("Saber"));

        }
    }
}
