using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? ConfigurationManager.CMInstance.DefaultTileColorOffset : ConfigurationManager.CMInstance.DefaultTileColorBase;
    }

    public void SetColor(Color color)
    {
        _renderer.color = color;
    }
    public void OnMouseEnter()
    {
        _highlight.SetActive(true);
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        float z = gameObject.transform.position.y;
        Debug.Log(string.Format("X={0} | y={1} | z={2}", x, y, z));
    }

    public void OnMouseExit()
    {
        _highlight.SetActive(false);
    }


}
