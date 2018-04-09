using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace Tests
{

    public class LiveTest
    {


        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode

        /*[UnityTest]
        public IEnumerator Lives_Spawn()
        {
            var livesPrefab = Resources.Load("Test/Lives");
            var livesSpawner = new GameObject().AddComponent<GameState>();

            yield return null;

            var spawnedLives = GameObject.FindWithTag("Lives");
            var spawnedLivesPrefab = PrefabUtility.GetPrefabParent(spawnedLives);

            Assert.AreEqual(spawnedLivesPrefab, livesPrefab);
        }*/

        /*[UnityTest]
        public IEnumerator Lives_Decrease()
        {
            
            var GSTest = new GameObject().AddComponent<GameState>();
            Debug.Log("Initial Lives: ");
            Debug.Log(GSTest.Lives);
            int start_lives = GSTest.Lives + 1;
            for (int x = 0; x < start_lives; x++)
            {
                Debug.Log("Running Lives Test");
                Debug.Log(GSTest.Lives);
                GSTest.Lives--;
            }
            bool result = GSTest.getGameOver();
            Assert.IsTrue(result);


            yield return null;
        }*/

        [Test]
        public void Decreasing_Lives()
        {
            Debug.Log("Testing Decreasing_Lives");
            var test = new GameObject().AddComponent<GameState>();
            Debug.Log(test.Lives);
        }


      
    }
}