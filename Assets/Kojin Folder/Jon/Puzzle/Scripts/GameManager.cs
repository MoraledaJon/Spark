using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private List<Transform> piecePrefab;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private bool shuffling = false;
    private bool iscreated = false;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI timerText;
    [Header("タイマー時間")]
    public float timeRemaining = 10f;

    public bool timerIsRunning = false;

    public ParticleSystem winEffect;

    private bool playEffect = false;

    int stageCount = 0;

    public List<Sprite> imageList;

    public Image canvasImage;
    PreloadManager pmanager;

    #region Puzzle

    // Create the game setup with size x size pieces.
    private void CreateGamePieces(float gapThickness, int number)
    {
        // This is the width of each tile.
        float width = 1 / (float)size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab[number], gameTransform);
                pieces.Add(piece);
                // Pieces will be in a game board going from -1 to +1.
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                  +1 - (2 * width * row) - width,
                                                  0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";
                // We want an empty space in the bottom right.
                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    // We want to map the UV coordinates appropriately, they are 0->1.
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    // UV coord order: (0, 1), (1, 1), (0, 0), (1, 0)
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
                    // Assign our new UVs to the mesh.
                    mesh.uv = uv;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("PreloadManager") != null)
        { 
            pmanager = GameObject.Find("PreloadManager").GetComponent<PreloadManager>();
            StartCoroutine(CoFadeIn());
        }
        else
        {
            CreatePuzzle(0);
            iscreated = true;
        }
    }

    IEnumerator CoFadeIn()
    {
        Image FadeOut = GameObject.Find("FadeOut").GetComponent<Image>();
        Color tmpc = FadeOut.color;
        tmpc.a = 1;
        FadeOut.color = tmpc;
        while (FadeOut.color.a > 0)
        {
            tmpc.a -= 0.005f;
            FadeOut.color = tmpc;
            yield return 0;
        }

        CreatePuzzle(0);
        iscreated = true;
    }
    IEnumerator CoWin()
    {
        iscreated = false;
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < gameTransform.childCount; i++)
        {
            Destroy(gameTransform.GetChild(i).gameObject);
        }
        Image FadeOut = GameObject.Find("FadeOut").GetComponent<Image>();
        Color tmpc = FadeOut.color;
        while (FadeOut.color.a < 1)
        {
            tmpc.a += 0.005f;
            FadeOut.color = tmpc;
            yield return 0;
        }
        PreloadManager manager = GameObject.Find("PreloadManager").GetComponent<PreloadManager>();
        SceneManager.LoadScene(18);
    }
    public void CreatePuzzle(int number)
    {
        pieces = new List<Transform>();
        size = 2;
        CreateGamePieces(0.01f, number);

        shuffling = true;
        StartCoroutine(WaitShuffle(0.01f));

        questionText.text = "パズルを解けてください";
        timerIsRunning = true;

        canvasImage.sprite = imageList[stageCount];
    }

    // Update is called once per frame
    void Update()
    {
        if (!iscreated)
            return;
        // Check for completion.
        if (!shuffling && CheckCompletion())
        {
              
            if (stageCount < 2)
            {
                timerIsRunning = false;
                //if(!playEffect)
                //{
                //    winEffect.Play();
                //    playEffect = true;
                //}


                for (int i = 0; i < gameTransform.childCount; i++)
                {
                    Destroy(gameTransform.GetChild(i).gameObject);
                }
                stageCount++;
                Debug.Log(stageCount.ToString());
                CreatePuzzle(stageCount);
            }      
            else if(iscreated)
               StartCoroutine(CoWin());

        }

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                if (GameObject.Find("PreloadManager") != null)
                    pmanager.Talk(1);
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }

        // On click send out ray to see if we click a piece.
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                // Go through the list, the index tells us the position.
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        // Check each direction to see if valid move.
                        // We break out on success so we don't carry on and swap back again.
                        if (SwapIfValid(i, -size, size)) { break; }
                        if (SwapIfValid(i, +size, size)) { break; }
                        if (SwapIfValid(i, -1, 0)) { break; }
                        if (SwapIfValid(i, +1, size - 1)) { break; }
                    }
                }
            }
        }
    }

    // colCheck is used to stop horizontal moves wrapping.
    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            // Swap them in game state.
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            // Swap their transforms.
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            // Update empty location.
            emptyLocation = i;
            return true;
        }
        return false;
    }

    // We name the pieces in order so we can use this to check completion.
    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }
        if (GameObject.Find("PreloadManager") != null)
            pmanager.Talk(0);
        timeRemaining = 60;
        return true;     
    }

    private IEnumerator WaitShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    // Brute force shuffling.
    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            // Pick a random location.
            int rnd = Random.Range(0, size * size);
            // Only thing we forbid is undoing the last move.
            if (rnd == last) { continue; }
            last = emptyLocation;
            // Try surrounding spaces looking for valid move.
            if (SwapIfValid(rnd, -size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(rnd, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(rnd, +1, size - 1))
            {
                count++;
            }
        }
    }

    #endregion

    #region Timer

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    #endregion


}

