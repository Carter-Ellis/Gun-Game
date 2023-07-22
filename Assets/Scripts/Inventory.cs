using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool isActive = false;
    public GameObject invenSlotPrefab;
    public List<Item> items = new List<Item>();
    private GameObject inventory;
    private List<Image> icons = new List<Image>();
    private CanvasGroup canvas;
    private Player player;
    public int itemHeld = -1;
    public List<int> itemIds = new List<int>();
    int invSpace = 16;
    int invCount = 0;
    void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        canvas = inventory.GetComponent<CanvasGroup>();
        canvas.alpha = 0f;
        SetSize(invSpace);
        player = gameObject.GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isActive)
            {
                isActive = false;
                canvas.alpha = 0f;
                canvas.blocksRaycasts = false;
            }
            else
            {
                isActive = true;
                canvas.alpha = 1f;
                canvas.blocksRaycasts = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && itemHeld != -1)
        {
            if (player.equipped)
            {
                items[itemHeld].gameObject.SetActive(false);
                player.equipped = false;
            }
            else
            {
                items[itemHeld].gameObject.SetActive(true);
                player.equipped = true;
            }

        }
    }

    public void Clicked(string id)
    {
        int parsedId = int.Parse(id);
        if (player.equipped && parsedId < invCount && itemHeld != parsedId)
        {
            items[itemHeld].gameObject.SetActive(false);
            items[parsedId].gameObject.SetActive(true);
            itemHeld = parsedId;
        }
        else if (!player.equipped && parsedId < invCount)
        {
            player.equipped = true;
            items[parsedId].gameObject.SetActive(true);
            itemHeld = parsedId;
        }
        
    }

    public void Add(Item item)
    {
        itemIds.Add(item.id);
        if (items.Count < invSpace)
        {
            items.Add(item);
            icons[invCount].sprite = item.sprite;
            icons[invCount].enabled = true;
            invCount++;
        }
        if (!player.equipped)
        {
            itemHeld = invCount - 1;
        }
    }

    private void SetSize(int size)
    { 
        foreach (Image img in icons) 
        {
            Destroy(img.gameObject); 
        }
        icons.Clear();
        GridLayoutGroup glg = inventory.GetComponentInChildren<GridLayoutGroup>();

        for (int i = 0; i < size; i++)
        {
            GameObject newIcon = Instantiate(invenSlotPrefab, glg.transform);
            Image[] children = newIcon.GetComponentsInChildren<Image>();
            newIcon.transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text = i.ToString();
            string id = newIcon.transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text;
            newIcon.transform.GetChild(0).GetComponent<Button>().onClick.AddListener( () => { Clicked(id); });
            foreach (Image child in children)
            {
                if (child.tag == "Icon")
                {
                    icons.Add(child);
                    break;
                }
            }
        }

    }

}
