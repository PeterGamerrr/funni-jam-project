using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using models;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public bool visible = false;
    public Message currentMessage;
    public int endTime;
    public TextMeshProUGUI MessageGameObject;
    public Queue<Message> nextMessageQueue = new Queue<Message>();

    private Dictionary<int,Message> messages = new Dictionary<int, Message>();
    private const string filePath = @"Assets/Resources/npcmessages.csv";

    private void Start()
    {
        ReadFile();
    }

    private void ReadFile()
    {
        using var reader = new StreamReader(filePath);
        reader.ReadLine();
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            Debug.Log(line);
            if (line == null) continue;
            var values = line.Split(';');
            Debug.Log(values[0] + " " + values[1] + " " + values[2] + " " + values[3]);
            messages[int.Parse(values[0])] =
                new Message(int.Parse(values[0]), values[1], int.Parse(values[2]), int.Parse(values[3]));
        }

        Debug.Log("message count: " + messages.Count);
    }

    private void Update()
    {
        UpdateVisibility();

        if (!visible) return;
        if (DateTime.Now.Millisecond < endTime) return;
        if (nextMessageQueue.Count > 0)
        {
            var message = nextMessageQueue.Dequeue();
            currentMessage = message;
            endTime = DateTime.Now.Millisecond + message.time;
        }
    }

    //not so efficient but this way it is always set the same as the variable.
    private void UpdateVisibility()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(visible);
        }

        if (visible && DateTime.Now.Millisecond >= endTime)
        {
            
        }
    }

    

    public void showMessage(int id)
    {
        
    }


}