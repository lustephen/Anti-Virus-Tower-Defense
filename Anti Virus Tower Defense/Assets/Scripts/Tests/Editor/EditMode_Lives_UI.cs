using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


public class EditMode_Lives_UI
{

    [UnityTest]
    public IEnumerator Lives_UI()
    {
        var test = GameObject.Find("Lives");
        Debug.Log("GameObject Test: ");
        Debug.Log((test != null));
        var GSTest = new GameObject().AddComponent<GameState>();
        Debug.Log("Testing Lives_UI");
        Debug.Log(GSTest.Test());
        yield return null;
        Assert.IsTrue(GSTest.Test());
    }

    [UnityTest]
    public IEnumerator Active_Lives()
    {
        Debug.Log("Testing Active_Lives");
        var test = GameObject.Find("Lives");
        yield return null;
        Assert.IsTrue(test.activeInHierarchy);
    }

}

