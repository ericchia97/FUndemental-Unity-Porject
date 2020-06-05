using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    //Know what objects are clickable
    public LayerMask clickableLayer;

    //Swap Cursors per object
    public Texture2D pointer; //Normal Pointer
    public Texture2D target; //Cursor for clickable objects like the world
    public Texture2D doorway; //Cursor for doorways
    public Texture2D combat; //Cursor combat actions

    public EventVector3 OnClickEnvironment;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool door = false;
            bool item = false;
            bool enemy = false;
            if (hit.collider.gameObject.tag == "doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else if (hit.collider.gameObject.tag == "item")
            {
                Cursor.SetCursor(combat, new Vector2(16, 16), CursorMode.Auto);
                item = true;
            }
            else if (hit.collider.gameObject.tag == "target")
            {
                Cursor.SetCursor(combat, new Vector2(16, 16), CursorMode.Auto);
                enemy = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if(Input.GetMouseButtonDown(0))
            {
                if(door)
                {
                    Transform doorWay = hit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(doorWay.position);
                    Debug.Log("Door");
                }
                else if (item)
                {
                    Transform treasure = hit.collider.gameObject.transform;

                    OnClickEnvironment.Invoke(treasure.position);
                    Debug.Log("item");
                }
                else if (enemy)
                {
                    Transform NPC = hit.collider.gameObject.transform;

                    OnClickEnvironment.Invoke(NPC.position);
                    Debug.Log("enemy");
                }

                OnClickEnvironment.Invoke(hit.point);
            }
        }

        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

