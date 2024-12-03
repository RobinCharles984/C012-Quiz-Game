using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;
public class AuthManager : MonoBehaviour
{
    //Variables
    [HideInInspector] public bool connected = false;
    [HideInInspector] public bool error = false;
    public static AuthManager instance;

    [SerializeField] private AsyncLoader asyncLoader;

    public event Action<PlayerProfile> OnSignedIn;
    public event Action<PlayerProfile> OnAvatarUpdate;

    private PlayerInfo playerInfo;

    private PlayerProfile playerProfile;
    //public PlayerProfile PlayerProfile => playerProfile;

    [Serializable]
    public struct PlayerProfile
    {
        public PlayerInfo playerInfo;
        public string Name;
    }

    async void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        await UnityServices.InitializeAsync();
        PlayerAccountService.Instance.SignedIn += SignedIn;
    }

    // Update is called once per frame
    async void loginAnonimous()
    {
        await signInAnonymous();
    }

    public void SignInAnonimous()
    {
        loginAnonimous();
    }

    public async void SignedIn()
    {
        try
        {
            var accessToken = PlayerAccountService.Instance.AccessToken;
            await SignInWithUnityAsync(accessToken);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public async void SignInUnity()
    {
        await InitSignIn();
    }

    public async Task InitSignIn()
    {
        await PlayerAccountService.Instance.StartSignInAsync();
    }

    private async Task SignInWithUnityAsync(string accessToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
            Debug.Log("SignIn is successful.");

            playerInfo = AuthenticationService.Instance.PlayerInfo;

            var name = await AuthenticationService.Instance.GetPlayerNameAsync();

            playerProfile.playerInfo = playerInfo;
            playerProfile.Name = name;

            OnSignedIn?.Invoke(playerProfile);
            connected = true;
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
            connected = false;
            error = true;
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            connected = false;
            error = true;
        }
    }

    private void OnDestroy()
    {
        PlayerAccountService.Instance.SignedIn -= SignedIn;
    }


//Connecting as anonymous
    public async Task signInAnonymous()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            connected = true;
            print("Connect anonymously!");
        }
        catch (System.Exception)
        {
            connected = false;
            error = true;
            print("Could not connect!");
        }
    }
}
