using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject Player;
    bool connected;

    private void Start()
    {
        connected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (connected)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
        }
    }

    public void Unconnect()
    {
        connected = false;
    }

    public void Reconnect()
    {
        connected = true;
    }

    public bool IsConnected()
    {
        return connected;
    }
}
