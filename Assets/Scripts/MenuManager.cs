using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; set; }

    public GameObject menuCanvas;

    public bool isMenuOpen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isMenuOpen)
        {
            menuCanvas.SetActive(true);
            isMenuOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;


        } else if(Input.GetKeyDown(KeyCode.Escape) && isMenuOpen)
        {
            menuCanvas.SetActive(false);
            isMenuOpen = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void CloseMenu()
    {
        menuCanvas.SetActive(false);
        isMenuOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
