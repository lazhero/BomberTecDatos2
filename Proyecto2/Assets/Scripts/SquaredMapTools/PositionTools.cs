using System.Collections.Generic;
using UnityEngine;

namespace SquaredMapTools
{
    public class PositionTools
    {
        public static Stack<int> getRelatedPositions(int node,int widthAndHeight)
        {
            Stack<int> stack = new Stack<int>();
            if (node + widthAndHeight < widthAndHeight*widthAndHeight)
            {
                stack.Push(node+widthAndHeight);
            }

            if (node - widthAndHeight >= 0)
            {
                stack.Push(node-widthAndHeight);
            }

            if ((int)(node / widthAndHeight) ==(int) ((node + 1) / widthAndHeight))
            {
                if(node+1<widthAndHeight*widthAndHeight)stack.Push(node+1);
            }

            if ((int)(node / widthAndHeight) == (int)((node - 1) / widthAndHeight))
            {
                 if(node-1>0)stack.Push(node-1);
            }

            return stack;
        }
    }
}