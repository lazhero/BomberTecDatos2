﻿
using System;
using System.Collections.Generic;

public class DGraph<T> 
{
    public T[] Nodes{ set; get; }
    public float[][] relations{ set; get; }
    private int nodesNumber;
    /**
     * @brief Constructor
     * @param int cuantity of nodes
     */
    public DGraph(int nodesNumber) { 
        
        Nodes = new T[nodesNumber];
        relations=new float[nodesNumber][];
        this.nodesNumber = nodesNumber;
        startAllWith(Int32.MaxValue);

    }

    public void setNodeData(int pos, T data) {
        if(!OnRange(pos))throw new IndexOutOfRangeException();
        Nodes[pos] = data;
    }

    public T getNode(int pos) {
        if (!OnRange(pos)) throw new IndexOutOfRangeException();
        return Nodes[pos];
    }

    public void SetRelationShip(int from,int to,float price) {
        relations[from][to] = price;
    }

    public float GetRelationShip(int from, int to) {
        if (!OnRange(from)||!OnRange(to)) throw new IndexOutOfRangeException();
     
        return relations[from][to];
    }
    public float[] GetRelations(int from) {
      
     
        return relations[from];
    }

    public float[][] GetAllRelations()
    {
        return relations;
    }
    private bool OnRange(int pos) {
        return pos >= 0 && pos < relations.Length;
    }
    /**
     * @brief sets all nodes with a specified value 
     */
    public void startAllWith(float value)
    {
        
        for (var i = 0; i < Nodes.Length; i++) {
            relations[i] = new float[nodesNumber];
            
            for (var j = 0; j < nodesNumber; j++) {
                relations[i][j] = value;
            }

        }
    }


}