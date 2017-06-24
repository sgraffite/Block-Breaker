using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory _inventoryInstance;

    private const int MaxToolCount = 4;
    private int previousToolIndex;
    private int currentToolIndex;
    public Tool[] tools = null;

    public static int[] toolPrefabIndexes = new int[] { 0, 1, 2, 3 };
    public Tool[] toolPrefabs;

    void Awake()
    {
        //if (_inventoryInstance != null || _inventoryInstance != this)
        if (_inventoryInstance != null)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate inventory, destroying!");
            return;
        }

        _inventoryInstance = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }

    void Start () {
        previousToolIndex = 0;
        currentToolIndex = 0;

        //InstantiateTools();
    }

    public void InstantiateTools()
    {
        tools = new Tool[MaxToolCount];
        for (var i = 0; i < MaxToolCount; i++)
        {
            var toolPrefabIndex = toolPrefabIndexes[i];
            tools[i] = Instantiate(toolPrefabs[toolPrefabIndex], this.transform.position, Quaternion.identity) as Tool;
            tools[i].gameObject.SetActive(false);
        }
        Debug.Log("Initialized! " + tools.Length);
    }

    public Tool GetCurrentTool()
    {
        Debug.Log("currentToolIndex " + currentToolIndex);
        return tools[currentToolIndex];
    }

    public void CollectTool(Tool newTool)
    {
        var toolIndex = System.Array.FindIndex(tools, x => x.type == newTool.type);
        var toolPrefabIndex = System.Array.FindIndex(toolPrefabs, x => x.type == newTool.type && x.power == newTool.power);
        toolPrefabIndexes[toolIndex] = toolPrefabIndex;

        Destroy(newTool);
        tools[toolIndex] = Instantiate(toolPrefabs[toolPrefabIndex], this.transform.position, Quaternion.identity) as Tool;
        tools[toolIndex].gameObject.SetActive(false);
    }

    public void SwapTool(bool positive)
    {
        previousToolIndex = currentToolIndex;
        currentToolIndex = currentToolIndex + (positive ? 1 : -1);
        if (currentToolIndex < 0)
        {
            currentToolIndex = toolPrefabIndexes.Length - 1;
        }
        else if (currentToolIndex >= toolPrefabIndexes.Length)
        {
            currentToolIndex = 0;
        }

        EquipTool();
    }

    public void SelectTool(int index)
    {
        previousToolIndex = currentToolIndex;
        currentToolIndex = index;
        EquipTool();
    }

    private void EquipTool()
    {
        var velocity = tools[previousToolIndex].GetComponent<Rigidbody2D>().velocity;
        var position = tools[previousToolIndex].transform.position;
        tools[previousToolIndex].gameObject.SetActive(false);

        tools[currentToolIndex].gameObject.SetActive(true);
        tools[currentToolIndex].GetComponent<Rigidbody2D>().velocity = velocity;
        tools[currentToolIndex].transform.position = position;
    }
}
