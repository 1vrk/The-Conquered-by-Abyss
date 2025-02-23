using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Room currRoom;
    public float move_speed_when_room_change;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }


    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (currRoom == null)
        {
            return;
        }

        Vector3 target_pos = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, target_pos, Time.deltaTime * move_speed_when_room_change);
    }

    Vector3 GetCameraTargetPosition()
    {
        if(currRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 target_pos = currRoom.GetRoomCentre();
        target_pos.z = transform.position.z;

        return target_pos;

    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }

}
