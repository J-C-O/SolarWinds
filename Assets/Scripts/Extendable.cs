using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Extendable : MonoBehaviour
{
    [Tooltip("The prefab to be placed on click.")]
    public GameObject place;

    private Chunk chunk;

    void Start() {
        chunk = GetComponentInParent<Chunk>();
    }

    void OnMouseUp() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();
        Physics.Raycast(ray, out hit);
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
        Instantiate(place, newPos, hit.transform.rotation, chunk.transform);
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
}
