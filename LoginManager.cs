using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using Models;
using Proyecto26;
using System.Collections.Generic;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviour
{
    public string baseUrl = "https://appsoftspace.azurewebsites.net/Login/user";
    public InputField usernameInputField;
    public InputField passwordInputField;

    private void Start()
    {
        Login();
    }
    void Login()
    {
        string username = "user1";
        string password = "password1";
        var jsonBody = new UserLogin
        {
            username = username,
            password = password
        };
       

        // Create request
        RestClient.Post("https://appsoftspace.azurewebsites.net/Login/user", jsonBody).Then(response =>
        {
            if (response.StatusCode == 200)
            {
                SceneManager.LoadScene("Logged", LoadSceneMode.Single);
            }
            else
            {
                Debug.LogError("Login failed. Status code: " + response.StatusCode);
               
            }
        }).Catch(error =>
        {
            Debug.LogError("Error: " + error.Message);
            
        });



    }
}
