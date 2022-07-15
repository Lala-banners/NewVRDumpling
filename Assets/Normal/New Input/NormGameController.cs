using System.Collections;
using UnityEngine;
using TMPro;
using Normal.Realtime.Examples;

public class NormGameController : MonoBehaviour
{
      [SerializeField] float playTime = 50;

    [Header("GameObject Binding")]
    [SerializeField] CoinSpawner spawner;
    [SerializeField] NormPlayerPC player;

    // UI
	[Header("UI Binding")]
    [SerializeField] GameObject gameView;
    [SerializeField] GameObject resultView;
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text coinText;
    [SerializeField] TMP_Text resultCoinText;

    // Internal Data
    protected int coin;
    protected float time;
    protected bool isPlaying = false;
    protected bool waitForNewCoin = false;

    // Start is called before the first frame update
    void Start()
    {
        SetupPlayer();
        StartGame();
    }

	void SetupPlayer()
	{
        player.onCollectCoin = () =>
        {
			if(isPlaying)
			{
                AddCoin();
			}
        };
	}

    // Update is called once per frame
    void Update()
    {
		if(isPlaying)
		{
            HandlePlayLogic();
        }
    }

	void HandlePlayLogic()
	{
        time -= Time.deltaTime;
		if(time < 0)
		{
            EndGame();
            return;
		}

        CheckAndRespawnCoin();
        UpdateTimeValue();
	}

	void Reset()
	{
        coin = 0;
		time = playTime + 0.3f;
        waitForNewCoin = false;
    }

	public void StartGame()
	{
        Reset();
        ShowGameView();
        spawner.ClearCoins();
        spawner.SpawnCoins();

        isPlaying = true;
	}

    public void EndGame()
    {
        isPlaying = false;
        ShowResultView();
    }

	#region Coin Logic

	void CheckAndRespawnCoin()
	{
		//Debug.Log("Coin Count=" + spawner.GetCoinCount());
		if (spawner.GetCoinCount() == 0 && waitForNewCoin == false)
		{
			waitForNewCoin = true;
            
            StartCoroutine(LateSpawnCoins());
		}
	}

    IEnumerator LateSpawnCoins()
	{
        yield return new WaitForSeconds(1.0f);

		//
        if (isPlaying != false && spawner.GetCoinCount() == 0)
        {
            spawner.SpawnCoins();
            waitForNewCoin = false;	// Reset the flag
        }
    }

	public void AddCoin()
	{
        coin++;
        UpdateCoinValue();
	}

	#endregion

	#region UI Logic

	public void ShowGameView()
	{
        UpdateTimeValue();
        UpdateCoinValue();
        gameView.SetActive(true);
        resultView.SetActive(false);
	}

    public void ShowResultView()
    {
        resultCoinText.text = coin.ToString();

        gameView.SetActive(false);
        resultView.SetActive(true);
    }

    void UpdateTimeValue()
	{
        timeText.text = time.ToString("00");
	}

	void UpdateCoinValue()
	{
        coinText.text = coin.ToString();
    }

	#endregion

}
