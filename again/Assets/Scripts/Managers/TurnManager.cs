﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    private int turnCounter = 1;
    public Text text;
    public Side left;
    public Side right;
    public SelectionManager selectionManager;

    void Start()
    {
        AssaingSide();
    }



    public int TurnCounter
    {
        get
        {
            return turnCounter;
        }
    }
    public void AssaingSide()
    {
        if(turnCounter % 2 == left.turn)
        {
            selectionManager.currentSide = left;
        }
        else if(turnCounter % 2 == right.turn)
        {
            selectionManager.currentSide = right;
        }
        else
        {
            throw new System.Exception("turn, side ? wrong");
        }
    }
    public void AddTurn()
    {
        turnCounter++;
        text.text = turnCounter.ToString();
        left.selectedToAdd = null;
        right.selectedToAdd = null;
        AssaingSide();
    }
}
