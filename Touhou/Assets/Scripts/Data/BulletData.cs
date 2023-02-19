using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "BulletData", order = 10)]
public class BulletData : ScriptableObject
{
    public string BulletName;
    public int damage;
    public Image BulletImg;
    public GameObject bulletPrefab;
}
