using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Extendable))]
public class ExtendFromPlayerInv : MonoBehaviour
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
        if (PlayerInventory.PIInstance != null)
        {
            //var inventory = InventoryManager.Instance;
            var inventory = PlayerInventory.PIInstance;
            if (inventory.SelectedItem != null)
            {
                extendable.SetPlace(inventory.SelectedItem.prefab);
            }
            else
            {
                extendable.place = null;
            }
        }
        
    }
}
