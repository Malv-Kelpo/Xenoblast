using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BulletBase : MonoBehaviour
{
    public int damage;
    public abstract void Launch(Vector2 direction);
}
