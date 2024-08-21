using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.UX;
using UnityEngine;

public class NameManager : MonoBehaviour
{
    [SerializeField] private string playerPrefsName = "NameField";

    [SerializeField] private MRTKTMPInputField inputField;
    [SerializeField] private GameObject lobbyScreen;

    public static string Name;

    // Start is called before the first frame update
    private void Start()
    {
        if (!PlayerPrefs.HasKey(playerPrefsName)) return;
        if (PlayerPrefs.GetString(playerPrefsName).Length <= 1) return;

        inputField.text = PlayerPrefs.GetString(playerPrefsName);

    }

    public void OnNextButton()
    {
        if (inputField.text.Length <= 1) return;
        PlayerPrefs.SetString(playerPrefsName, inputField.text);
        Debug.Log($"Name of the client/server: {inputField.text}");
        Name = inputField.text;
        lobbyScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
