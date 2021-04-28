using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class UiManager : MonoBehaviour
{

    public Slider playerHealthBar;
    public Text playerHealthText;
    public HealthManager playerHealthManager;
    public Text arrowsLeft;
    public PlayerController arrows;
    public Text playerLevelText;
    public Slider playerExpBar;
    public CharacterStats playerStats;
    public Image playerAvatar;


    public GameObject inventoryPanel, menuPanel;
    public Button inventoryButton;
    //public Text inventoryText;
    private WeaponManager weaponManager;
    //private ItemsManager itemsManager;



    private void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        inventoryPanel.SetActive(false);
        menuPanel.SetActive(false);
        //itemsManager = FindObjectOfType<ItemsManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        playerHealthBar.maxValue = playerHealthManager.maxHealth;
        playerHealthBar.value = playerHealthManager.Health;

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Hp: ").Append(playerHealthManager.Health).Append(" / ").Append(playerHealthManager.maxHealth);

        playerHealthText.text = stringBuilder.ToString();
        arrowsLeft.text = arrows.arrows +"";

        playerLevelText.text = "Level: " + playerStats.level;
        if (playerStats.level >= playerStats.expToLevelUp.Length)
        {
            playerExpBar.enabled = false;
            return;
        }
        playerExpBar.maxValue = playerStats.expToLevelUp[playerStats.level];
        playerExpBar.minValue = playerStats.expToLevelUp[playerStats.level - 1];
        playerExpBar.value = playerStats.exp;
    }

    public void ToggleInventory()
    {
        //Time.timeScale = 0;
        //inventoryText.text = "";
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        menuPanel.SetActive(!menuPanel.activeInHierarchy);
        if (inventoryPanel.activeInHierarchy)
        {
            foreach (Transform t in inventoryPanel.transform)
            {
                Destroy(t.gameObject);
            }
            FillInventory();
        }
    }

    public void FillInventory()
    {
        List<GameObject> weapons = weaponManager.GetAllWeapons();
        int i = 0;

        foreach (GameObject w in weapons)
        {
            AddItemToInventory(w, InventoryButton.ItemType.WEAPON, i);
            i++;
        }

        /*i = 0;
        List<GameObject> keyItems = itemsManager.GetQuestItems();
        foreach (GameObject item in keyItems)
        {
            AddItemToInventory(item, InventoryButton.ItemType.SPECIAL_ITEMS, i);
            i++;
        }*/
    }

    private void AddItemToInventory(GameObject item, InventoryButton.ItemType type, int pos)
    {
        Button tempB = Instantiate(inventoryButton, inventoryPanel.transform);
        tempB.GetComponent<InventoryButton>().type = type;
        tempB.GetComponent<InventoryButton>().itemIdx = pos;
        tempB.onClick.AddListener(() => tempB.GetComponent<InventoryButton>().ActivateButton());
        tempB.image.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }




    public void ShowOnly(int type)
    {
        foreach (Transform t in inventoryPanel.transform)
        {
            t.gameObject.SetActive((int)t.GetComponent<InventoryButton>().type == type);
        }
    }

    public void ShowAll()
    {
        foreach (Transform t in inventoryPanel.transform)
        {
            t.gameObject.SetActive(true);
        }
    }

    public void ChangeAvatarImage(Sprite sprite)
    {
        playerAvatar.sprite = sprite;
    }
}
