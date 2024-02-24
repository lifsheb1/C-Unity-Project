using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets;
using TMPro;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private InventoryController inventoryController;

    public TMP_Text questInfoText;
    public GameObject winPanel;
    
    private static System.Random random = new();

    private Dictionary<string, int> _goals = new();
    private Dictionary<string, int> _current = new();

    public int RequestedObjectCount { get; set; } = 330;
    
    // Start is called before the first frame update
    void Start()
    {
        inventoryController = GameObject.FindWithTag("Player") // we should be in the player but whatever
            .GetComponent<InventoryController>();
        inventoryController.OnInventoryItemAddedEvent += OnInventoryItemAdded;
        StartCoroutine(CreateGoalCoroutine());
    }
    
    private IEnumerator CreateGoalCoroutine()
    {
        yield return new WaitForSeconds(0.5f); // give some time to propagate the trash count
        CreateGoals();
    }

    private void OnInventoryItemAdded(string trashType, int count)
    {
        if (!_goals.ContainsKey(trashType)) return;
        
        if (!_current.ContainsKey(trashType))
            _current.Add(trashType, count);
        else
            _current[trashType] += count;
        
        // check if all goals are complete
        var completed = true;
        foreach (var goal in _goals)
        {
            if (!_current.ContainsKey(goal.Key))
            {
                completed = false;
                break;
            }

            if (_current[goal.Key] < goal.Value)
            {
                completed = false;
                break;
            }
        }

        if (completed)
        {
            GameManager.Instance.Freeze();
            // show win panel
            winPanel.SetActive(true);
        }
        
        UpdateGui();
    }

    public void CreateGoals()
    {
        _goals.Clear();
        var randomTrashTypes = inventoryController.TrashTypes.Keys
            .OrderBy(x => random.Next())
            .Take(3)
            .ToList();
        
        Debug.Log($"Creating goals for #{RequestedObjectCount}");
        
        var min = 1;
        var max = 10;
        switch (RequestedObjectCount)
        {
            case > 900:
                min = 8;
                max = 28;
                break;
            case > 400:
                min = 5;
                max = 15;
                break;
        }
        
        foreach (var trashType in randomTrashTypes)
        {
            _goals.Add(trashType, random.Next(min, max));
        }
        UpdateGui();
    }

    public void UpdateGui()
    {
        var text = new StringBuilder("Goals:\n");
        foreach (var goal in _goals)
        {
            var hasMeta = inventoryController.TrashTypes.ContainsKey(goal.Key);
            var name = hasMeta ? inventoryController.TrashTypes[goal.Key].Name : goal.Key;
            var current = _current.ContainsKey(goal.Key) ? _current[goal.Key] : 0;
            text.AppendLine($"- {name}: {current}/{goal.Value}");
        }
        questInfoText.text = text.ToString();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
