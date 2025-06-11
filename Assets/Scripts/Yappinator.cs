using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Yappinaotr : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    void Start()
    {
        List<string> messages = new List<string>
        {
            "GET'EM BOYS",
            "Theres one!",
            "Bring the crossbow!",
            "I'm out of holywater!",
            "You'll regret this!",
            "Another Vamp!"
        };
        Constructor msg = new Constructor(messages);
        messageText.text = msg.chosenMessage;
    }


}
