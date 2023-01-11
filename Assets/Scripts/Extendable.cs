using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Extendable : MonoBehaviour
{
    private float rotationAmount = 0;
    private Quaternion placementRotation = new Quaternion(0, 0, 0, 0);

    [Tooltip("The prefab to be placed on click.")]
    public GameObject place;
    private bool placeChanged = false;
    private Chunk chunk;

    private GameObject preview;

    void Start() {
        chunk = GetComponentInParent<Chunk>();
    }

    void OnMouseUp() {
        placeBlock(false);
    }

    void OnMouseEnter() {
        placeBlock(true);
    }

    void OnMouseExit() {
        DestroyPreview();
    }

    int MaxIndex(float[] absolute) {
        var max = float.NegativeInfinity;
        int index = -1;
        for (int i = 0; i < absolute.Length; i++) {
            var current = absolute[i];
            if (current > max) {
                max = current;
                index = i;
            }
        }
        return index;
    }

    void placeBlock(bool preview) {
        if (place == null) {
            DestroyPreview();
            // cant place anything, return fast
            return;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();
        if (!Physics.Raycast(ray, out hit))
        {
            DestroyPreview();
            return;
        };
        // let p be the direction from the hit point to the center of the GameObject
        // if a cube is not rotated, p always has coordinate with an absolute value 0.5
        // as we use that later we need to remove the rotation
        // this also implies that we need box colliders that resembles a cube
        var dir = Quaternion.Inverse(hit.transform.rotation) * (hit.transform.position - hit.point);
        // determine the coordinate with the greatest absolute value
        var absolute = new float[]{Mathf.Abs(dir.x), Mathf.Abs(dir.y), Mathf.Abs(dir.z)};
        var maxIndex = MaxIndex(absolute);
        // determine on the sign of the coordinate with the greatest absolute value we
        // use -1 or 1 to get a matching direction that is aligned with grid
        var val = 1;
        if (dir[maxIndex] > 0) {
            val = -1;
        }
        // flatDir is the grid aligned direction in local space
        var flatDir = Vector3Int.zero;
        flatDir[maxIndex] = val;
        // to get the new objects position we add flatDir brought into world space
        // to the position of the object hit
        var newPos = (hit.transform.rotation * flatDir) + hit.transform.position;
        // already something placed => do nothing
        var gameObjectAt = chunk.GameObjectAt(newPos);
        if (gameObjectAt != null && gameObjectAt != this.preview.gameObject) {
            Debug.Log("already something placed");
            return;
        }
        
        if (preview) {
            if (this.preview == null) {
                this.preview = Instantiate(this.place, newPos, placementRotation, chunk.transform);
            }
            this.preview.GetComponent<BoxCollider>().enabled = false;
            this.preview.transform.position = newPos;
            this.preview.transform.rotation = placementRotation;
        }
        else {
            DestroyPreview();
            var created = Instantiate(place, newPos, placementRotation, chunk.transform);
            if (created.GetComponent<Ownable>() == null) {
                created.AddComponent<Ownable>();
                //set current active player
                int owner = 0;
                if (PlayerManager.PMInstance != null) {
                    owner = PlayerManager.PMInstance.activePlayer.PlayerID;
                }
                created.GetComponent<Ownable>().owner = owner;
            }
            // clear from inventory
            if(PlayerInventory.PIInstance != null)
            {
                if(PlayerInventory.PIInstance.SelectedItem != null)
                {
                    PlayerInventory.PIInstance.GetActivePlayer().RemoveItem(PlayerInventory.PIInstance.SelectedItem);
                }
                PlayerInventory.PIInstance.SelectedItem = null;
            }
            if(InventoryManager.Instance != null)
            {
                Debug.Log("1");
                var inventory = InventoryManager.Instance;
                if (inventory != null)
                {
                    Debug.Log("2");
                    if (inventory.Selected != null)
                    {
                        Debug.Log("removing");
                        inventory.Remove(inventory.Selected);
                    }
                    inventory.Selected = null;
                }
            }
            
            //finish turn
            if(TurnManager.TMInstance != null)
            {
                TurnManager.TMInstance.NextPlayer();
            }
        }
    }

    public void SetPlace(GameObject prefab) {
        if (prefab != this.place) {
            this.placeChanged = true;
        }
        this.place = prefab;
    }

    public void DestroyPreview() {
        if (this.preview == null) {
            return;
        }
        Destroy(this.preview);
        this.preview = null;
    }

    // rotates the variable by 90° each time
    void incrementPlacementRotation(){
        if (rotationAmount >= 360.0f){
            rotationAmount = 90.0f;
        } else {
            rotationAmount += 90.0f;
        }
        placementRotation = Quaternion.Euler(0, rotationAmount, 0);
        if (this.preview != null) {
            this.preview.transform.localRotation = placementRotation;
        }
    }

    //remove function on right click (somehow it removes everything that will be right clicked)
    void Update()
    {
        if (this.placeChanged) {
            DestroyPreview();
            placeChanged = false;
        }
        //Advance Rotation of placement object
        if (Input.GetKeyDown("r")) {
            incrementPlacementRotation();
        }
    }
}
