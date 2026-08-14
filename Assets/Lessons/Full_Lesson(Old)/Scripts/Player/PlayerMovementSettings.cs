using UnityEngine;
using System;

namespace EspartanosGameDev.Lessons.FullLessonOld
{
[Serializable]
public class PlayerMovementSettings
{
    public float xSpeed = 5f;
    public float jumpForce = 10f;
    public float checkDistance = 0.1f;
    public float minGroundNormalY = 0.65f;
    public ContactFilter2D contactFilter;
    public float knockbackDuration = 0.5f;

}
}