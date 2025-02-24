using UnityEngine;

//通过接口的方式实现开关门

public interface IDoorController
{
    void OpenDoor();
    void CloseDoor();
}