using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    [SerializeField]
    private GameObject _textHolder;

    [SerializeField]
    private GameObject _nextlevelButton;

    [SerializeField]
    private GameObject _canvas;

    [SerializeField]
    private GameObject _victory;

    [SerializeField]
    private GameObject _defeat;

    [SerializeField]
    private List<GameObject> _stars;

    [SerializeField]
    private int _StarTimer;

    [SerializeField]
    private int _DefeatTimer;

    [SerializeField]
    private Text _textSpace;

    private int _points;
    private bool _Finished;
    private bool _withinTimer = true;
    private bool _shotInnocent;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame(int scene)
    {
        StartLevel();
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        StartLevel();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void NextGame(int scene)
    {
        StartLevel();
        SceneManager.LoadScene(scene);
    }

    public void VictoryGame()
    {
        if (!_Finished)
        {
            Time.timeScale = 0.2f;
            Cursor.lockState = CursorLockMode.None;
            _Finished = true;
            _victory.SetActive(true);
            _canvas.SetActive(true);
            StartCoroutine(Stars());
            _nextlevelButton.SetActive(true);
            if (_withinTimer)
            {
                AddText("You shot the target within " + _StarTimer + " seconds +1 Star");
            }
            if (!_shotInnocent)
            {
                AddText("Clean kill did not shoot any innocents +1 Star");
            }
            AddText("Mission Succses +1 star");
        }
    }

    public void DefeatGame()
    {
        if (!_Finished)
        {
            if (!_shotInnocent)
            {
                _shotInnocent = true;
                _points--;
                AddText("You have hit a innocent no star");
                StartCoroutine(DefeatTimer());
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                _Finished = true;
                AddText("You have shot another innocent you lost");
                _defeat.SetActive(true);
                _canvas.SetActive(true);
                _points = 0;
            }
        }
    }

    private void StartLevel()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        _withinTimer = true;
        _shotInnocent = false;
        _Finished = false;
        _points = _stars.Count;
        _nextlevelButton.SetActive(false);
        _defeat.SetActive(false);
        _victory.SetActive(false);
        _canvas.SetActive(false);
        for (int i = 0; i < _stars.Count; i++)
        {
            _stars[i].SetActive(false);
        }
    }

    private void AddText(string text)
    {
        Text textSpace = Instantiate(_textSpace);
        textSpace.gameObject.transform.SetParent(_textHolder.gameObject.transform);
        textSpace.text = text;
    }

    private IEnumerator Stars()
    {
        for (int i = 0; i < _points; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _stars[i].SetActive(true);
        }
    }

    private IEnumerator StarTimer()
    {
        yield return new WaitForSeconds(_StarTimer);
        if (!_Finished)
        {
            _withinTimer = false;
            AddText("Did not shot the target within " + _StarTimer + " seconds no Star");
            _points--;
        }
    }

    private IEnumerator DefeatTimer()
    {
        yield return new WaitForSeconds(_DefeatTimer);
        if (!_Finished)
        {
            Cursor.lockState = CursorLockMode.None;
            _Finished = true;
            AddText("Did not shoot the target within " + _DefeatTimer + " seconds after being spotted");
            AddText("You have failed the mission no stars");
            _defeat.SetActive(true);
            _canvas.SetActive(true);
            _points = 0;
        }
    }
}
