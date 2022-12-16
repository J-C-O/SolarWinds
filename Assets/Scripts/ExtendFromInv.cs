using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Extendable))]
public class ExtendFromInv : MonoBehaviour
{
    private Extendable extendable;
    // Start is called before the first frame update
    void Start()
    {
        extendable = this.GetComponent<Extendable>();
    }

    // Update is called once per frame
    void Update()
    {
        var inventory = InventoryManager.Instance;
        if (inventory.Selected != null) {
            extendable.place = inventory.Selected.prefab;
        } else {
            extendable.place = null;
        }
    }
}
