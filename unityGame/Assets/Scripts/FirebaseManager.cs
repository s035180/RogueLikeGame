using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Linq;
using System;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FirebaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    public FirebaseFirestore db;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    //User Data variables
    [Header("UserData")]
    public TMP_InputField usernameField;
    public TMP_InputField scoreField;
    public TMP_InputField killsField;
    public TMP_InputField deathsField;
    public GameObject scoreElement;
    public Transform scoreboardContent;
    public Achievements achiv;
    public GameObject scoreElementAchievements;
    public Transform achivementsContent;

    string curTime;
    public List<Achievements> achivs = new List<Achievements>();

    private bool _update = true;


    void Awake()
    {
        curTime = DateTime.Now.ToString();
        curTime = curTime.Replace(@"/", "-");

        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
                Debug.Log("Awake");
                //StartCoroutine(UpdateScore(StaticData.Score));
                Debug.Log("AwakeEnd");
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
    void Update()
    {
        if (StaticData.Score > 0 && _update)
        {
            _update = false;
            Debug.Log("Update");
            StartCoroutine(UpdateScore(StaticData.Score));
            StartCoroutine(UpdateKills(StaticData.Kills));
            StartCoroutine(UpdateDeaths(StaticData.Deaths));
            StartCoroutine(UpdateName(StaticData.username));
            StartCoroutine(UpdateMaxUserData());
            Debug.Log("UpdateEnd");
        }
    }
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        //Setting reference to FIREBASE FIRESTORE
        db = FirebaseFirestore.DefaultInstance;
        

    }
    public void ClearLoginFeilds()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
    }
    public void ClearRegisterFeilds()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";
    }
    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }
    //Function for the sign out button
    public void SignOutButton()
    {
        auth.SignOut();
        UIManager.instance.LoginScreen();
        ClearRegisterFeilds();
        ClearLoginFeilds();
    }
    public void ScoreboardButton()
    {
        StartCoroutine(LoadScoreboardData());
    }

    public void UserDataButton()
    {
        StartCoroutine(LoadUserData());
    }
    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
            StaticData.username = User.DisplayName.ToString();

            StaticData.UserID = User.UserId;
            Debug.Log(User.UserId);

            yield return new WaitForSeconds(1);
            AchivsAsync();
            //usernameField.text = User.DisplayName;
            UIManager.instance.showMain();
            confirmLoginText.text = "";
            ClearLoginFeilds();
            ClearRegisterFeilds();
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }

    private IEnumerator UpdateMaxUserData()
    {
        Debug.Log(StaticData.UserID);
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("achv").Child(StaticData.UserID).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result;

            if (snapshot.Exists)
            {

                if (Convert.ToInt32(snapshot.Child("kills").Value) < StaticData.Kills)
                {
                    var DBTaskUpdKills = DBreference.Child("achv").Child(StaticData.UserID).Child("kills").SetValueAsync(StaticData.Kills);

                    yield return new WaitUntil(predicate: () => DBTaskUpdKills.IsCompleted);
                }

                if (Convert.ToInt32(snapshot.Child("score").Value) < StaticData.Score)
                {
                    var DBTaskUpdScore = DBreference.Child("achv").Child(StaticData.UserID).Child("score").SetValueAsync(StaticData.Score);

                    yield return new WaitUntil(predicate: () => DBTaskUpdScore.IsCompleted);
                }

                if (StaticData.Deaths != 0)
                {
                    var DBTaskUpdDeaths = DBreference.Child("achv").Child(StaticData.UserID).Child("deaths").SetValueAsync(StaticData.Deaths + Convert.ToInt32(snapshot.Child("deaths").Value));

                    yield return new WaitUntil(predicate: () => DBTaskUpdDeaths.IsCompleted);
                }
            }
            else
            {
                var DBTaskUpdKills = DBreference.Child("achv").Child(StaticData.UserID).Child("kills").SetValueAsync(StaticData.Kills);

                yield return new WaitUntil(predicate: () => DBTaskUpdKills.IsCompleted);

                var DBTaskUpdScore = DBreference.Child("achv").Child(StaticData.UserID).Child("score").SetValueAsync(StaticData.Score);

                yield return new WaitUntil(predicate: () => DBTaskUpdScore.IsCompleted);

                var DBTaskUpdDeaths = DBreference.Child("achv").Child(StaticData.UserID).Child("deaths").SetValueAsync(StaticData.Deaths);

                yield return new WaitUntil(predicate: () => DBTaskUpdDeaths.IsCompleted);

                var DBTaskUpdUsername = DBreference.Child("achv").Child(StaticData.UserID).Child("username").SetValueAsync(StaticData.username);

                yield return new WaitUntil(predicate: () => DBTaskUpdUsername.IsCompleted);
            }
        }

        StaticData.Score = 0;
        StaticData.Deaths = 0;
        StaticData.Kills = 0;
        _update = true;
    }

    private IEnumerator UpdateScore(int _score)
    {

        //Set the currently logged in user xp
        var DBTask = DBreference.Child("users").Child(curTime).Child("score").SetValueAsync(_score);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Score is now updated
        }
    }

    private IEnumerator UpdateKills(int _kills)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(curTime).Child("kills").SetValueAsync(_kills);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Kills are now updated
        }
    }

    private IEnumerator UpdateDeaths(int _deaths)
    {
        //Set the currently logged in user deaths
        var DBTask = DBreference.Child("users").Child(curTime).Child("deaths").SetValueAsync(_deaths);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Deaths are now updated
        }
    }
    private IEnumerator UpdateName(string _name)
    {
        Debug.Log(_name + "CHEEEEEEEEEEEEEECHK");
        //Set the currently logged in user deaths
        var DBTask = DBreference.Child("users").Child(curTime).Child("username").SetValueAsync(_name);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Deaths are now updated
        }
    }
    private IEnumerator LoadScoreboardData()
    {
        Debug.Log("LoadScoreboardData");
        //Get all the users data ordered by score amount
        var DBTask = DBreference.Child("users").OrderByChild("score").GetValueAsync();


        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);


        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in scoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                string kills = childSnapshot.Child("kills").Value.ToString();
                string deaths = childSnapshot.Child("deaths").Value.ToString();
                string score = childSnapshot.Child("score").Value.ToString();

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, kills, deaths, score);
                Debug.Log("LoadScoreboardData Complete");
            }

            //Go to scoareboard screen
        }
    }

    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("achv").Child(StaticData.UserID).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            scoreField.text = "No Data";
            killsField.text = "No Data";
            deathsField.text = "No Data";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            usernameField.text = snapshot.Child("username").Value.ToString();
            scoreField.text = snapshot.Child("score").Value.ToString();
            killsField.text = snapshot.Child("kills").Value.ToString();
            deathsField.text = snapshot.Child("deaths").Value.ToString();
        }
    }

    //new method that stores everything from database to ACHIEVEMENT CLASS, adding everything to ac LIST
    public async void AchivsAsync()
    {
        foreach (Transform child in scoreboardContent.transform)
        {
            Destroy(child.gameObject);
        }
        Firebase.Firestore.Query capitalQuery = db.Collection("Achievements");
        QuerySnapshot capitalQuerySnapshot = await capitalQuery.GetSnapshotAsync();
        foreach (DocumentSnapshot documentSnapshot in capitalQuerySnapshot.Documents)
        {
            string achievementName = $"{documentSnapshot.GetValue<string>("Achievement Name")}";
            string achievementType = $"{documentSnapshot.GetValue<string>("Complete Task")}";
            string value = ($"{documentSnapshot.GetValue<int>("Value")}").ToString();
            string unlocked = false.ToString();
            achiv = gameObject.AddComponent<Achievements>();
            achiv.NewAchivElement(achievementName, achievementType, Convert.ToInt32(value), Convert.ToBoolean(unlocked));
            StaticData.achivsSet = achiv;
            
        }
        AchievementsShow();
        

    }

    public void AchievementsShow()
    {
        foreach (Transform child in achivementsContent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in StaticData.achivsGet)
            {
                GameObject scoreboardElement = Instantiate(scoreElementAchievements, achivementsContent);
                
                 StartCoroutine(LoadUserStats());



                if (StaticData.snapy != null)
                {
                    if (Convert.ToInt32(StaticData.snapy.Child(item.achievementType).Value) >= item.value)
                    {
                        scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(item.achievementName, item.achievementType, item.value.ToString(), "true");
                    }
                    else
                    {
                        scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(item.achievementName, item.achievementType, item.value.ToString(), "false");
                    }
                }
                else
                {

                    scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(item.achievementName, item.achievementType, item.value.ToString(), item.unlocked.ToString());
                }
            
            }
        
    }
    private IEnumerator LoadUserStats()
    {
        Debug.Log("Enterting the tunnel");
        //Get the currently logged in user data
        var DBTask = DBreference.Child("achv").Child(StaticData.UserID).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            Debug.Log("Exiting the tunnel from first if");


        }
        else if (DBTask.Result.Value == null)
        {
            Debug.Log("Exiting the tunnel from second else if");


        }
        else
        {
            //Data has been retrieved
            Debug.Log("Exiting the tunnel from third else");
            StaticData.snapy = DBTask.Result;

            

/*            usernameField.text = snapshot.Child("username").Value.ToString();
            scoreField.text = snapshot.Child("score").Value.ToString();
            killsField.text = snapshot.Child("kills").Value.ToString();
            deathsField.text = snapshot.Child("deaths").Value.ToString();*/
        }
    }
    


}
