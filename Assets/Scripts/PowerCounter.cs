using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PowerCounter : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    public GameObject chunk;

    public void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        int points = chunk.GetComponent<Chunk>().CountPower(0);
        tmp.text = $"Punkte: {points}";
    }
}
