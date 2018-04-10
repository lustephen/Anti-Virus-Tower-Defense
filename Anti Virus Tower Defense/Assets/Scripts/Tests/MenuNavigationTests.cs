using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace Tests {
	public class MenuNavigationTests {

		[UnityTest]
		public IEnumerator _Loads_Menu_Scene() {
			SceneManager.LoadScene(0);
			yield return null;
			Assert.AreEqual(SceneManager.GetActiveScene().name, "Menu");
		}

		[UnityTest]
		public IEnumerator _HighScorePanel_Navigation() {
			SceneManager.LoadScene(0);
			yield return null;
			GameObject Canvas = GameObject.Find("Canvas");
			GameObject MainMenuPanel = Canvas.transform.Find("MainMenuPanel").gameObject;
			GameObject HighScorePanel = Canvas.transform.Find("HighScorePanel").gameObject;
			Button HighScoreButton = MainMenuPanel.transform.Find("HighScoreButton").gameObject.GetComponent<Button>();
			Button BackButton = HighScorePanel.transform.Find("BackButton").gameObject.GetComponent<Button>();
			HighScoreButton.onClick.Invoke();
			yield return null;
			Assert.IsFalse(MainMenuPanel.activeInHierarchy);
			Assert.IsTrue(HighScorePanel.activeInHierarchy);
			BackButton.onClick.Invoke();
			yield return null;
			Assert.IsFalse(HighScorePanel.activeInHierarchy);
			Assert.IsTrue(MainMenuPanel.activeInHierarchy);
		}

		[UnityTest]
		public IEnumerator _SettingsPanel_Navigation() {
			SceneManager.LoadScene(0);
			yield return null;
			GameObject Canvas = GameObject.Find("Canvas");
			GameObject MainMenuPanel = Canvas.transform.Find("MainMenuPanel").gameObject;
			GameObject SettingsPanel = Canvas.transform.Find("SettingsPanel").gameObject;
			Button SettingsButton = MainMenuPanel.transform.Find("SettingsButton").gameObject.GetComponent<Button>();
			Button BackButton = SettingsPanel.transform.Find("BackButton").gameObject.GetComponent<Button>();
			SettingsButton.onClick.Invoke();
			yield return null;
			Assert.IsFalse(MainMenuPanel.activeInHierarchy);
			Assert.IsTrue(SettingsPanel.activeInHierarchy);
			BackButton.onClick.Invoke();
			yield return null;
			Assert.IsFalse(SettingsPanel.activeInHierarchy);
			Assert.IsTrue(MainMenuPanel.activeInHierarchy);
		}

		[UnityTest]
		public IEnumerator _CreditsPanel_Navigation() {
			SceneManager.LoadScene(0);
			yield return null;
			GameObject Canvas = GameObject.Find("Canvas");
			GameObject MainMenuPanel = Canvas.transform.Find("MainMenuPanel").gameObject;
			GameObject CreditsPanel = Canvas.transform.Find("CreditsPanel").gameObject;
			Button CreditsButton = MainMenuPanel.transform.Find("CreditsButton").gameObject.GetComponent<Button>();
			Button BackButton = CreditsPanel.transform.Find("BackButton").gameObject.GetComponent<Button>();
			CreditsButton.onClick.Invoke();
			yield return null;
			Assert.IsFalse(MainMenuPanel.activeInHierarchy);
			Assert.IsTrue(CreditsPanel.activeInHierarchy);
			BackButton.onClick.Invoke();
			yield return null;
			Assert.IsFalse(CreditsPanel.activeInHierarchy);
			Assert.IsTrue(MainMenuPanel.activeInHierarchy);
		}

		[UnityTest]
		public IEnumerator _PlayButton_Functionality() {
			SceneManager.LoadScene(0);
			yield return null;
			GameObject Canvas = GameObject.Find("Canvas");
			GameObject MainMenuPanel = Canvas.transform.Find("MainMenuPanel").gameObject;
			Button PlayButton = MainMenuPanel.transform.Find("PlayButton").gameObject.GetComponent<Button>();
			PlayButton.onClick.Invoke();
			yield return null;
			Assert.AreEqual(SceneManager.GetActiveScene().name, "game");
		}
	}
}