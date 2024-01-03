using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAim : MonoBehaviour
{
    public Transform gunMuzzle;

    public Vector3 [] aimsPosition;
    public Quaternion[] aimsRotation;

    public SpriteRenderer flame;

   public void GunDown()
    {
        gunMuzzle.localPosition = aimsPosition[0];
        gunMuzzle.localRotation = aimsRotation[0];
        flame.sortingOrder = 11;
    }

    public void GunDownLeft()
    {
        gunMuzzle.localPosition = aimsPosition[1];
        gunMuzzle.localRotation = aimsRotation[1];
        flame.sortingOrder = 11;
    }
    public void GunLeft()
    {
        gunMuzzle.localPosition = aimsPosition[2];
        gunMuzzle.localRotation = aimsRotation[2];
        flame.sortingOrder = 11;
    }
    public void GunLeftUp()
    {
        gunMuzzle.localPosition = aimsPosition[3];
        gunMuzzle.localRotation = aimsRotation[3];
        flame.sortingOrder = 9;
    }

    public void GunUp()
    {
        gunMuzzle.localPosition = aimsPosition[4];
        gunMuzzle.localRotation = aimsRotation[4];
        flame.sortingOrder = 9;
    }

    public void GunUpRight()
    {
        gunMuzzle.localPosition = aimsPosition[5];
        gunMuzzle.localRotation = aimsRotation[5];
        flame.sortingOrder = 9;
    }
    public void GunRight()
    {
        gunMuzzle.localPosition = aimsPosition[6];
        gunMuzzle.localRotation = aimsRotation[6];
        flame.sortingOrder = 11;
    }
    public void GunRightDown()
    {
        gunMuzzle.localPosition = aimsPosition[7];
        gunMuzzle.localRotation = aimsRotation[7];
        flame.sortingOrder = 11;
    }


}
