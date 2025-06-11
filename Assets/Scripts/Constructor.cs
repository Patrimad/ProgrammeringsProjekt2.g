using System.Collections.Generic;
using UnityEngine;
public class Constructor
{
    public string chosenMessage;

    public Constructor(List<string> messages)
    {
        int randomIndex = Random.Range(0, messages.Count);
        chosenMessage = messages[randomIndex];
    }
}
