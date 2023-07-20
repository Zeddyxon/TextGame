using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame.Object
{
    public enum TypeOfObject
    {
        NONE,
        ENTRANCE,
        EXIT,
        ITEM
    }
    public class ObjectInTile
    {
        public TypeOfObject typeOfObject { get; }

        public ObjectInTile(TypeOfObject typeOfObject)
        {
            this.typeOfObject = typeOfObject;
        }

    }
}
