using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugMonitor : MonoBehaviour
{
    public static DebugMonitor Monitor;
    public List<PowerConsumer> Consumers = new List<PowerConsumer>();
    public GameObject ParentChunk;
    public Text DebugText;

    private void Awake()
    {
        Monitor = this;
    }

    public void AddConsumer(PowerConsumer item)
    {
        Consumers.Add(item);
    }
    public void RemoveConsumer(PowerConsumer item)
    {
        Consumers.Remove(item);
    }

    public void GetChildConsumer()
    {
        ClearConsumer();
        Consumers.AddRange(ParentChunk.GetComponentsInChildren<PowerConsumer>());
    }

    public void ClearConsumer()
    {
        Consumers.Clear();
    }

    public string GetIsPowered(PowerConsumer consumer)
    {
        return "[" + consumer.gameObject.name + "] IsPowered:" + consumer.powerPoints.ToString();
    }

    public void SetDebugText()
    {
        string debugMessage = "";

        if(Consumers.Count > 0)
        {
            for(int i = 0; i < Consumers.Count; i++)
            {
                if (Consumers[i].bringsPoints)
                {
                    if(i == 0)
                    {
                        debugMessage = GetIsPowered(Consumers[i]);
                    }
                    else
                    {
                        debugMessage += "\n" + GetIsPowered(Consumers[i]);
                    }
                }
            }
        }
        DebugText.text = debugMessage;
    }

    private void Update()
    {
        GetChildConsumer();
        SetDebugText();

    }
}
