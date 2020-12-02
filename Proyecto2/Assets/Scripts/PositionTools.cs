using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionTools
{
  

    public static int[] Up { get; }  = {0, -1};
    public static int[] Down{ get; }  = {0, 1};
    public static int[] Right{ get; } = {1, 0};
    public static int[] Left { get; } = {-1, 0};

 
    public static Stack<int> GetRelatedPositions(int node,int widthAndHeight)
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
            stack.Push(node+1);
        }

        if ((int)(node % widthAndHeight) == (int)((node - 1) % widthAndHeight))
        {
            stack.Push(node-1);
        }

        return stack;
    }
    
    

    /// <summary>
    /// Determines the forgiven Positions for n*n matrix
    /// </summary>
    public static int[] DetermineForgivenPositions(int n) {
        var n2 = n * n;
        var ForgivenPositions = new int[28];

        ForgivenPositions[0] = n + 1;
        ForgivenPositions[1] = n + 2;
        ForgivenPositions[2] = 2*n + 1;

        ForgivenPositions[3] = 2*n  -2;
        ForgivenPositions[4] = 2*n  -3;
        ForgivenPositions[5] = 3*n +-2;

        ForgivenPositions[6] = n2-2*n + 1;
        ForgivenPositions[7] = n2-2*n + 2;
        ForgivenPositions[8] = n2-3*n + 1;

        ForgivenPositions[9] = n2- n -2 ;
        ForgivenPositions[10] = n2-2*n -2 ;
        ForgivenPositions[11] = n2-n -   3;

        var c = n / 2 + n;
        ForgivenPositions[12] = c + 1;
        ForgivenPositions[13] = c - 1;
        ForgivenPositions[14] = c + n;
        ForgivenPositions[15] = c;

        c = (n2 - 1) - n/2 - n;
        ForgivenPositions[16] = c-n;
        ForgivenPositions[17] = c-1;
        ForgivenPositions[18] = c+1;
        ForgivenPositions[19] = c;

        c = n2 /2 - n/2 + 1;
        ForgivenPositions[20] = c-n;
        ForgivenPositions[21] = c+n;
        ForgivenPositions[22] = c+1;
        ForgivenPositions[23] = c;

        c = c+n-3;
        ForgivenPositions[24] = c-n;
        ForgivenPositions[25] = c+n;
        ForgivenPositions[26] = c-1;
        ForgivenPositions[27] = c;

        return ForgivenPositions;
        
            
        }
    /// <summary>
    /// Determines center and height that camera must be
    /// </summary>
    /// <returns></returns>
    public static Vector3 DeterminesCameraPosition( GameObject[] nodes)
    {
        var totalX = 0f;
        var totalZ = 0f;
        foreach (var blokc in nodes)
        {
            var position = blokc.transform.position;
            totalX += position.x;
            totalZ += position.z;
        }

        var len = nodes.Length;
        var centerX = totalX / len;
        var centerZ = totalZ / len;
        var height = Mathf.Sqrt(len) * 1.5f;
        return new Vector3(centerX , height,centerZ*2);
    }
        
    /// <summary>
    /// this calculates who is up, down , left, or right depending on a Vector2 (x,y) return -1 if the direction is not possible
    /// (1,0) is right (0,1) is up
    /// </summary>
    public static int WhoIs(int[] direction, int length,int WidthAndHeight) {
        return WhoIs(length, direction,WidthAndHeight);
    }

    /// <summary>
    /// this calculates who is up, down , left, or right depending on a Vector2 (x,y) return -1 if the direction is not possible
    /// (1,0) is right (0,1) is up
    /// </summary>
    private static  int WhoIs(int blockNumber, int[] direction, int widthAndHeight) {
        if (!ValidateDirection(direction, blockNumber, widthAndHeight )) return -1;

        return Convert.ToInt32(blockNumber + direction[0] + direction[1] * widthAndHeight);

    }
    public static int DetectWalkable(int blockNumber, int[] direction,int widthAndHeight)
    {
        var response = WhoIs(blockNumber, direction,widthAndHeight);

        if (IsSide(response,widthAndHeight)) return -1;
        
        return response;

    }
        
    /// <summary>
    /// validates certain direction from given block number, THIS IS ONLY FOR MAP GENERATION
    /// you dont need to know how it works
    /// </summary>
    private static bool ValidateDirection(int[] direction, int blockNumber, int widthAndHeight)
    {

        //si estoy en un borde izquierdo y me preguntan por su izquierdo
        //si estoy en un borde derecho y me preguntan por su derecho        
        //si estoy en el borde superior y me preguntan por el de arriba
        //si estoy en el borde inferior y me preguntan por el de abajo

        if (blockNumber % widthAndHeight == 0 && direction[0] < 0) return false;

        if ((blockNumber + 1) % widthAndHeight == 0 && direction[0] > 0) return false;

        if (blockNumber - widthAndHeight < 0 && direction[1] < 0) return false;

        if (blockNumber + widthAndHeight > widthAndHeight * widthAndHeight && direction[1] > 0) return false;

        return true;

    }

    /// <summary>
    /// Determines if the given block is a side of the map, it also verifies that the given parameter is a int
    /// </summary>
    /// <param name="blockNumberAux"></param>
    /// <param name="widthAndHeight"></param>
    /// <returns> bool</returns>
    public static bool IsSide(object blockNumberAux, int widthAndHeight) {
        int blockNumber;

        if (blockNumberAux is string)
            blockNumber = Convert.ToInt32(blockNumberAux);
        else
            blockNumber = (int) blockNumberAux;

        return blockNumber % widthAndHeight == 0 || (blockNumber + 1) % widthAndHeight == 0 ||blockNumber - widthAndHeight < 0 || blockNumber + widthAndHeight > widthAndHeight * widthAndHeight;
    }

    /// <summary>
    ///  Returns if a specified block is a corner can accept int or string 
    /// </summary>
    /// <param name="blockNumberAux"></param>
    /// <param name="forgivenPositions"></param>
    /// <returns></returns>
    public static bool IsCorner(object blockNumberAux, int[] forgivenPositions){
        int blockNumber;

        if (blockNumberAux is string)
            blockNumber = Convert.ToInt32(blockNumberAux);
        else
            blockNumber = (int) blockNumberAux;
        return forgivenPositions.Contains(blockNumber);
    }
    
  
}