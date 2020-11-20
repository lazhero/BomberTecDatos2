using System;
using UnityEditor.Experimental.GraphView;

public class DGraph<T> where T:IComparable
{
    private T[] Nodes;
    private float[,] relations;
    

    DGraph(int nodesNumber)
    {
        if (!PositiveVerification(nodesNumber)) nodesNumber = 0;
        Nodes = new T[nodesNumber];
        relations=new float[nodesNumber,nodesNumber];

    }

    public void setNode(int pos, T data)
    {
        if(!OnRange(pos))throw new IndexOutOfRangeException();
        Nodes[pos] = data;
    }

    public T getNode(int pos)
    {
        if (!OnRange(pos)) throw new IndexOutOfRangeException();
        return Nodes[pos];
    }

    public void SetRelationShip(int from,int to,float price)
    {
        if(!OnRange(from))throw new IndexOutOfRangeException();
        if(!OnRange(to))throw  new IndexOutOfRangeException();
        relations[from, to] = price;
    }

    public float GetRelationShip(int from, int to)
    {
        if (!OnRange(from)) throw new IndexOutOfRangeException();
        if(!OnRange(to))throw new IndexOutOfRangeException();
        return relations[from, to];
    }
    private bool PositiveVerification(int pos)
    {
        return pos >= 0;
    }

    private bool OnRange(int pos)
    {
        return PositiveVerification(pos) && pos < Nodes.Length;
    }

    public void startAllWith(float value)
    {
        for (int i = 0; i < relations.Length; i++)
        {
            for (int j = 0; j < relations.Length; j++)
            {
                relations[i, j] = value;
            }
        }
    }
    
}