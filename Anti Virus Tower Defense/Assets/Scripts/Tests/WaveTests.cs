using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests
{
    public class WaveTests
    {
        [UnityTest]
        public IEnumerator _Stores_Correct_Enemy_From_Data()
        {
            var dummyObject = Resources.Load("Tests/dummy");
            List<Object> mockPrefabList = new List<Object>() { dummyObject, dummyObject };
            Wave wave = new Wave(1.0f, "0-10 1-11", mockPrefabList);

            yield return null;

            for (int i = 0; i < 10; i++)
            {
                Object obj = wave.getNextEnemy();
                Assert.AreEqual(obj, mockPrefabList[0]);
            }
            for (int i = 0; i < 11; i++)
            {
                Object obj = wave.getNextEnemy();
                Assert.AreEqual(obj, mockPrefabList[1]);
            }
        }

        [UnityTest]
        public IEnumerator _Is_Finished_When_Empty()
        {
            var dummyObject = Resources.Load("Tests/dummy");
            List<Object> mockPrefabList = new List<Object>() { dummyObject };
            Wave wave = new Wave(1.0f, "0-1", mockPrefabList);

            yield return null;

            Assert.False(wave.isFinished());
            wave.getNextEnemy();
            Assert.True(wave.isFinished());
        }
    }
}