using UnityEngine;

namespace GameEngine.Bullet
{
    [CreateAssetMenu(
        fileName = "BulletConfigSO",
        menuName = "ShootEmUp/New BulletConfig"
    )]
    public sealed class BulletConfigSO : ScriptableObject
    {
        [SerializeField] internal PhysicsLayer physicsLayer;
        [SerializeField] internal Color color;
        [SerializeField] internal int damage;
        [SerializeField] internal float speed;
    }
}
