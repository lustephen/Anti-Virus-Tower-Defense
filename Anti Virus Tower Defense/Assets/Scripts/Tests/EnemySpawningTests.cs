using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class EnemySpawnerTests
    {
        [UnityTest]
        public IEnumerator _Instantiates_GameObject_From_Prefab()
        {
            var enemyPrefab = Resources.Load("Tests/dummy");
            var enemySpawner = new GameObject().AddComponent<EnemyManager>();
            enemySpawner.spawnEnemy(enemyPrefab, new Vector3(0,0,0));

            yield return null;

            var spawnedEnemy = GameObject.FindWithTag("Enemy");
            var prefabOfTheSpawnedEnemy = PrefabUtility.GetPrefabParent(spawnedEnemy);
            Debug.Log(prefabOfTheSpawnedEnemy.name);

            Assert.AreEqual(enemyPrefab, prefabOfTheSpawnedEnemy);
        }

        [UnityTest]
        public IEnumerator _Instantiates_GameObject_At_Right_Position()
        {
            var enemyPrefab = Resources.Load("Tests/dummy");
            var enemySpawner = new GameObject().AddComponent<EnemyManager>();
            enemySpawner.spawnEnemy(enemyPrefab, new Vector3(1, 0, 0));

            yield return null;

            var spawnedEnemy = GameObject.FindWithTag("Enemy");
            var expectedPosition = new Vector3(1, 0, 0);

            Assert.AreEqual(expectedPosition, spawnedEnemy.transform.position);
        }

        [TearDown]
        public void AfterEveryTest()
        {
            foreach (var gameObject in GameObject.FindGameObjectsWithTag("Enemy"))
                Object.Destroy(gameObject);
            foreach (var gameObject in Object.FindObjectsOfType<EnemyManager>())
                Object.Destroy(gameObject);
        }
        
    }
}
