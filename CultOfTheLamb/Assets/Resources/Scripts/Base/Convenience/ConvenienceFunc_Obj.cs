using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static partial class ConvenienceFunc
{
    public static GameObject FindRootObject(string objName)
    {
        return null;
    }
    public static GameObject FindObject(this GameObject obj_, string objName)
    {

        return null;
    }
    public static GameObject FindObject(this GameObject obj_, int objIdx)
    {
        return null;
    }

    public static float GetAngle(Vector3 from, Vector3 to)
    {
        Vector3 v = to - from;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    //! 특정 좌표를 바라보게 하는 함수
    public static void LookAt2D(this Transform transform, Vector3 target, float rotationSpeed)
    {
        Vector2 direction = new Vector2(
            transform.localPosition.x - target.x,
            transform.localPosition.y - target.y
        );

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle + 90.0f, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotationSpeed * Time.deltaTime);

        transform.rotation = rotation;

    }       // LookAt()
}
