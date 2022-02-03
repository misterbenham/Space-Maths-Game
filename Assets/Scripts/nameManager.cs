using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nameManager : MonoBehaviour
{
    public static string name;
    public InputField nameInput;
    public Text exclamText;
    public Text exclamText2;
    void Awake()
    {
        exclamText.enabled = false;
        exclamText2.enabled = false;
        Login();
    }

    //Login of the player to Playfab to be able to communicate with the playfab server
    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }
    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create !");
        if (result.InfoResultPayload.PlayerProfile.DisplayName != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            Debug.Log(name);
            nameInput.text = name;
        }
        else
        {
            Debug.Log("Player data not complete, must play one time");
        }
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating accunt !");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SubmiteNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text,
        };
        if (nameInput.text != name)
        {
            if (nameInput.text.Length < 3 || nameInput.text.Length > 11)
            {
                exclamText.enabled = true;
            }
            else
            {
                PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnErrorNameUpdte);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void OnErrorNameUpdte(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        exclamText2.enabled = true;
    }
    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Update display name!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
