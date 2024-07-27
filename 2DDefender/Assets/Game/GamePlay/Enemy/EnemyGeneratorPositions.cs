using UnityEngine;

namespace GamePlay.Enemy
{
    internal sealed class EnemyGeneratorPositions : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;

        [SerializeField] private Transform[] _attackPositions;

        internal Vector3 RandomSpawnPosition() => RandomTransform(_spawnPositions);

        internal Vector2 RandomAttackPosition() => RandomTransform(_attackPositions);

        private Vector3 RandomTransform(Transform[] transforms)
        {
            int index = Random.Range(0, transforms.Length); 
            return transforms[index].position;
        }
    }
}