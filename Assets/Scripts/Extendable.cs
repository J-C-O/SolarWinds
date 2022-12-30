using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Extendable : MonoBehaviour
{
    [Tooltip("The prefab to be placed on click.")]
    public GameObject place1;
    public GameObject placePreview1;
    public GameObject place2;
    public GameObject placePreview2;
    public GameObject place3;
    public GameObject placePreview3;
    public GameObject place4;
    public GameObject placePreview4;

    // can be changed during runtime
    private int selectedObject;
    private float rotationAmount = 0;
    private Quaternion placementRotation = new Quaternion(0, 0, 0, 0);

    private GameObject place;
    private Chunk chunk;

    private GameObject placePreview1Object;
    private GameObject placePreview2Object;
    private GameObject placePreview3Object;
    private GameObject placePreview4Object;

    void Start() {
        chunk = GetComponentInParent<Chunk>();

        placePreview1Object = Instantiate(placePreview1, new Vector3(0, 0, 0), placementRotation);
        placePreview2Object = Instantiate(placePreview2, new Vector3(0, 0, 0), placementRotation);
        placePreview3Object = Instantiate(placePreview3, new Vector3(0, 0, 0), placementRotation);
        placePreview4Object = Instantiate(placePreview4, new Vector3(0, 0, 0), placementRotation);

        placePreview1Object.GetComponent<BoxCollider>().enabled = false;

        placePreview1Object.SetActive(true);
        placePreview2Object.SetActive(false);
        placePreview3Object.SetActive(false);
        placePreview4Object.SetActive(false);

        selectedObject = 1;
    }

    void OnMouseUp() {
        placeBlock(false);
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
            // cant place anything, return fast
            return;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        };
        // let p be the direction from the hit point to the center of the GameObject
        // if a cube is not rotated, p always has coordinate with an absolute value 0.5
        // as we use that later we need to remove the rotation
        // this also implies that we need box colliders that resembles a cube
        var dir = Quaternion.Inverse(hit.transform.rotation) * (hit.transform.position - hit.point);
        // Debug.Log("dir: " + dir.ToString() + "center: " + hit.transform.position.ToString() + "hit: " + hit.point.ToString());
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
        // Debug.Log("flatDir: " + flatDir.ToString());
        // to get the new objects position we add flatDir brought into world space
        // to the position of the object hit
        var newPos = (hit.transform.rotation * flatDir) + hit.transform.position;
        // already something placed => do nothing
        if (chunk.GameObjectAt(newPos) != null) {
            return;
        }
        var created = Instantiate(place, newPos, hit.transform.rotation, chunk.transform);
        if (created.GetComponent<Ownable>() == null) {
            created.AddComponent<Ownable>();
            // TODO: set current active player
            created.GetComponent<Ownable>().owner = 0;
        }
        // clear from inventory
        var inventory = InventoryManager.Instance;
        if (inventory != null && !preview) {
            if (inventory.Selected != null) {
                inventory.Remove(inventory.Selected);
            }
            inventory.Selected = null;
        }

        // adjust position height depending on type of object
        // e.g. fans get spawned directly on the ground, while cubes
        // get spawned in mid air to be aligned
        // may need more work depending on used prefabs
        if (selectedObject != 1){
            newPos = new Vector3(newPos.x, newPos.y - 0.25f, newPos.z);
        }

        if (preview) {
            //DestroyImmediate(previewObject);
            //previewObject = Instantiate(placePreview, newPos, placementRotation, chunk.transform);
            placePreview1Object.transform.position = newPos;
            placePreview2Object.transform.position = newPos;
            placePreview3Object.transform.position = newPos;
            placePreview4Object.transform.position = newPos;

            placePreview1Object.transform.rotation = placementRotation;
            placePreview2Object.transform.rotation = placementRotation;
            placePreview3Object.transform.rotation = placementRotation;
            placePreview4Object.transform.rotation = placementRotation;
        }
        else {
            switch (selectedObject) 
            {
                case 1:
                    Instantiate(place1, newPos, placementRotation, chunk.transform);
                    break;
                case 2:
                    Instantiate(place2, newPos, placementRotation, chunk.transform);
                    break;
                case 3:
                    Instantiate(place3, newPos, placementRotation, chunk.transform);
                    break;
                case 4: 
                    Instantiate(place4, newPos, placementRotation, chunk.transform);
                    break;
                default:
                    break;
            }
        }
    }

    // rotates the variable by 90° each time
    void incrementPlacementRotation(){
        if (rotationAmount >= 1.0f){
            rotationAmount = 0.25f;
        } else {
            rotationAmount += 0.25f;
        }
        placementRotation = new Quaternion(0, rotationAmount, 0, 1);
    }

    //remove function on right click (somehow it removes everything that will be right clicked)
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = new RaycastHit();
            if(Physics.Raycast(ray, out hit))
            {
                Destroy(hit.transform.gameObject);
            }
        }
        else{
            placeBlock(true);
        }


        // Input for changing different placement Objects
        // hiding all objects exept the active one
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Debug.Log("1 pressed");
            selectedObject = 1;

            placePreview1Object.SetActive(true);
            placePreview2Object.SetActive(false);
            placePreview3Object.SetActive(false);
            placePreview4Object.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("2 pressed");
            selectedObject = 2;

            placePreview1Object.SetActive(false);
            placePreview2Object.SetActive(true);
            placePreview3Object.SetActive(false);
            placePreview4Object.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            Debug.Log("3 pressed");
            selectedObject = 3;

            placePreview1Object.SetActive(false);
            placePreview2Object.SetActive(false);
            placePreview3Object.SetActive(true);
            placePreview4Object.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            Debug.Log("4 pressed");
            selectedObject = 4;

            placePreview1Object.SetActive(false);
            placePreview2Object.SetActive(false);
            placePreview3Object.SetActive(false);
            placePreview4Object.SetActive(true);
        }
        
        
        //Advance Rotation of placement object
        if (Input.GetKeyDown("r")) {
            Debug.Log("Rotation pressed");
            incrementPlacementRotation();
        }
    }
}
