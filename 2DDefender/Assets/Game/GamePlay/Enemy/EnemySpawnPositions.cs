using UnityEngine;

namespace GamePlay.Enemy
{
    internal sealed class EnemySpawnPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] _positions;

        internal Vector3 RandomSpawnPosition() => RandomTransform(_positions);

        private Vector3 RandomTransform(Transform[] transforms)
        {
            int index = Random.Range(0, transforms.Length); 
            return transforms[index].position;
        }
    }
}