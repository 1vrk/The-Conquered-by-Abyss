using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class UserManager : MonoBehaviour
{
    [SerializeField] private DatabaseRecordsManager database_records_manager;
    private DatabaseManager database_manager;
    public TMP_InputField input_login;
    public TMP_InputField input_password;
    public TMP_InputField input_password_again;
    public TMP_Text error_case;
    public static string login;
    private string password;
    private string check_pass;

    void Start()
    {
        database_manager = GetComponent<DatabaseManager>();
        input_login.characterLimit = 20;
        input_password.characterLimit = 20;
        input_password_again.characterLimit = 20;
    }

    public void Registration()
    {
        login = input_login.text;
        password = input_password.text;
        check_pass = input_password_again.text;

        if (!IsLoginValid(login))
        {
            error_case.text = "Логин должен содержать от 8 до 20 символов, одну заглавную букву и одну цифру";
            return;
        }
        if (!IsPasswordValid(password))
        {
            error_case.text = "Пароль должен содержать от 8 до 20 символов, одну заглавную букву и одну цифру";
            return;
        }

        if (password != check_pass)
        {
            error_case.text = "Пароли не совпадают";
            return;
        }

        if (database_manager.UserExists(login))
        {
            error_case.text = "Логин уже существует";
            return;
        }


        database_manager.InsertUser(login, password);
        database_records_manager.AddPlayerRecords(login);

        LogIn();

    }

    public void LogIn()
    {
        login = input_login.text;
        password = input_password.text;

        bool isValid = database_manager.ValidateUser(login, password);
        if (isValid)
        {
            //try
            //{
            //    database_records_manager.AddPlayerRecords(login);
            //}
            //catch (Exception)
            //{
            //}
          
            SceneManager.LoadScene(1);
        }
        else
        {
            error_case.text = "Неверный логин или пароль";
        }
    }

    private bool IsPasswordValid(string password)
    {
        return password.Length >= 8 && password.Length <= 20 && Regex.IsMatch(password, @"[A-Z]") && Regex.IsMatch(password, @"\d");
    }

    private bool IsLoginValid(string login)
    {
        return login.Length >= 8 && login.Length <= 20 && Regex.IsMatch(login, @"[A-Z]") && Regex.IsMatch(login, @"\d");
    }
}