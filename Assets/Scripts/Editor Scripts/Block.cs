﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Block
{
    public string Type { get; set; }

    public Block(string type)
    {
        this.Type = type;
    }
}