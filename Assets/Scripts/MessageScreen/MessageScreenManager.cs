﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageScreenManager : MonoBehaviour
{
    private Dictionary<string, MessageScreen> messageScreensDic = new Dictionary<string, MessageScreen>();

    public delegate void OnMessageScreenButtonPressed();

    // Start is called before the first frame update
    private void Start()
    {
        MessageScreen[] messageScreens = GetComponentsInChildren<MessageScreen>();
        foreach (MessageScreen messageScreen in messageScreens)
        {
            if (messageScreensDic.ContainsKey(messageScreen.ScreenName))
            {
                Debug.LogError("Duplicate screen: " + messageScreen.ScreenName);
            }
            else
            {
                messageScreensDic.Add(messageScreen.ScreenName, messageScreen);
            }
            messageScreen.gameObject.SetActive(false);
        }
        EventAggregator.Instance.Subscribe<MsgShowScreen>(ShowScreen);
        EventAggregator.Instance.Subscribe<MsgHideAllScreens>(HideAllScreens);
    }

    private void HideAllScreens(MsgHideAllScreens msg)
    {
        foreach (KeyValuePair<string, MessageScreen> entry in messageScreensDic)
        {
            entry.Value.ResetAllButtons();
            entry.Value.gameObject.SetActive(false);
        }
    }

    private void ShowScreen(MsgShowScreen msg)
    {
        if (messageScreensDic.ContainsKey(msg.screenName))
        {
            MessageScreen msgScreen = messageScreensDic[msg.screenName];
            msgScreen.gameObject.SetActive(true);
            foreach (Tuple<string, OnMessageScreenButtonPressed> t in msg.listOfActions)
            {
                msgScreen.AddDelegateToButton(t.Item1, t.Item2);
            }

            if (msg.seconds > 0)
            {
                StartCoroutine(DisableScreenOnSeconds(msgScreen, msg.seconds));
            }
        }
    }

    private IEnumerator DisableScreenOnSeconds(MessageScreen messageScreen, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        messageScreen.gameObject.SetActive(false);
    }
}