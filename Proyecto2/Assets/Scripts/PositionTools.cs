using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionTools
{
    private int WidthAndHeight { set; get; } 
    private int[] ForgivenPositions { set; get; }

    public  int[] Up { get; }  = {0, -1};
    public  int[] Down{ get; }  = {0, 1};
    public  int[] Right{ get; } = {1, 0};
    public  int[] Left { get; } = {-1, 0};

    public PositionTools(int n)
    {
        WidthAndHeight = n;
    }
    
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
    public  int[] DetermineForgivenPositions(int n) {
        var n2 = n * n;
        ForgivenPositions = new int[12];
             
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

        return ForgivenPositions;
    }
        
        
    /// <summary>
    /// Determines center and height that camera must be
    /// </summary>
    /// <returns></returns>
    public Vector3 DeterminesCameraPosition( GameObject[] nodes)
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
    public int WhoIs(int[] direction, int length) {
        return WhoIs(length, direction);
    }

    /// <summary>
    /// this calculates who is up, down , left, or right depending on a Vector2 (x,y) return -1 if the direction is not possible
    /// (1,0) is right (0,1) is up
    /// </summary>
    private  int WhoIs(int blockNumber, int[] direction) {
        if (!ValidateDirection(direction, blockNumber )) return -1;

        return Convert.ToInt32(blockNumber + direction[0] + direction[1] * WidthAndHeight);

    }
    public int DetectWalkable(int blockNumber, int[] direction)
    {
        var response = WhoIs(blockNumber, direction);

        if (IsSide(response)) return -1;
        
        return response;

    }
        
    /// <summary>
    /// validates certain direction from given block number, THIS IS ONLY FOR MAP GENERATION
    /// you dont need to know how it works
    /// </summary>
    private bool ValidateDirection(int[] direction, int blockNumber)
    {

        //si estoy en un borde izquierdo y me preguntan por su izquierdo
        //si estoy en un borde derecho y me preguntan por su derecho        
        //si estoy en el borde superior y me preguntan por el de arriba
        //si estoy en el borde inferior y me preguntan por el de abajo

        if (blockNumber % WidthAndHeight == 0 && direction[0] < 0) return false;

        if ((blockNumber + 1) % WidthAndHeight == 0 && direction[0] > 0) return false;

        if (blockNumber - WidthAndHeight < 0 && direction[1] < 0) return false;

        if (blockNumber + WidthAndHeight > WidthAndHeight * WidthAndHeight && direction[1] > 0) return false;

        return true;

    }
        
    /// <summary>
    /// Determines if the given block is a side of the map, it also verifies that the given parameter is a int
    /// </summary>
    /// <param name="blockNumberAux"></param>
    /// <returns> bool</returns>
    public bool IsSide(object blockNumberAux) {
        int blockNumber;

        if (blockNumberAux is string)
            blockNumber = Convert.ToInt32(blockNumberAux);
        else
            blockNumber = (int) blockNumberAux;

        return blockNumber % WidthAndHeight == 0 || (blockNumber + 1) % WidthAndHeight == 0 ||
               blockNumber - WidthAndHeight < 0 || blockNumber + WidthAndHeight > WidthAndHeight * WidthAndHeight;
    }

    /// <summary>
    ///  Returns if a specified block is a corner can accept int or string 
    /// </summary>
    /// <param name="blockNumberAux"></param>
    /// <returns></returns>
    public bool IsCorner(object blockNumberAux){
        int blockNumber;

        if (blockNumberAux is string)
            blockNumber = Convert.ToInt32(blockNumberAux);
        else
            blockNumber = (int) blockNumberAux;
        return ForgivenPositions.Contains(blockNumber);
    }
    
    public void LinkGraph(DGraph<GameObject > Graph,int widthAndHeight)
    {
        for (var i = 0; i < widthAndHeight*widthAndHeight; i++)
        {
            if (i + widthAndHeight < widthAndHeight*widthAndHeight)
            {
                Graph.SetRelationShip(i,i+widthAndHeight,10);
            }

            if (i - widthAndHeight >= 0)
            {
                Graph.SetRelationShip(i,i-widthAndHeight,10);
            }

            if ((int)(i / widthAndHeight) ==(int) ((i + 1) / widthAndHeight))
            {
                Graph.SetRelationShip(i,i+1,10);
            }

            if ((int)(i % widthAndHeight) == (int)((i - 1) % widthAndHeight))
            {
                Graph.SetRelationShip(i,i-1,10);
            }
        }
    }
    
}