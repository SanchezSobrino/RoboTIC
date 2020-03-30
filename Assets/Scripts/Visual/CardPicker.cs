﻿using TMPro;
using UnityEngine;
using static CardConstants;

public class CardPicker : MonoBehaviour
{
    //Rehacer esta clase con animaciones y esas cosas
    [SerializeField] private TextMeshPro textMesh;

    private Cards selectedCard = Cards.NoCard;

    private bool isLocked = false;

    private void OnMouseDown()
    {
        if (!isLocked)
        {
            if (Cards.IsDefined(typeof(Cards), selectedCard + 1))
            {
                selectedCard++;
            }
            else
            {
                selectedCard = Cards.NoCard;
            }

            textMesh.text = selectedCard.ToString();
          //Inform of card changed
        }
    }

    private void Start()
    {
        textMesh.text = selectedCard.ToString();
    }




    public void Lock()
    {
        isLocked = true;
    }

    public void Unlock()
    {
        isLocked = false;
    }
}