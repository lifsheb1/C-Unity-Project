using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TMPro;
using TrashMetadata;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class InventoryController : MonoBehaviour
    {
        public TMP_Text overallScoreText;
        public TMP_Text roundScoreText;
        public int overallScore;
        public int roundScore;

        public GameObject inventoryGuiPanel;

        public GameObject metaInfoPanel;
        public Toggle metaDoNotShowToggle;
        public TMP_Text metaInfoName, metaInfoDegrades, metaInfoYesNo, metaInfoText;
        private string currentMetaName;
        
        public delegate void OnInventoryItemAdded(string name, int count);
        public event OnInventoryItemAdded OnInventoryItemAddedEvent;

        protected readonly Dictionary<string, int> _inventory = new();

        public readonly Dictionary<string, ITrashMetadata> _trashTypes = new()
        {
            {"BottleOfWater", new PlasticWaterBottle()},
            {"Cereal_2", new CerealBox()},
            {"PlasticCup", new PlasticCup()},
            {"Battery", new Battery()},
            {"GlassBottle", new GlassBottle()},
            {"ToiletPaper", new ToiletPaper()}
        };

        private readonly Dictionary<string, GameObject> _inventoryGuiItems = new();

        public ReadOnlyDictionary<string, ITrashMetadata> TrashTypes => new(_trashTypes);
        public ReadOnlyDictionary<string, int> Inventory => new(_inventory);

        private void Start()
        {
            foreach (Transform child in inventoryGuiPanel.transform)
            {
                _inventoryGuiItems.Add(child.name.Replace("Item", ""), child.gameObject);
                child.gameObject.SetActive(false);
            }

            // default: hide inventory
            inventoryGuiPanel.SetActive(false);

            if(PlayerPrefs.HasKey("ScoreOverall")){
                overallScore  = PlayerPrefs.GetInt("ScoreOverall");
            }
            else{
                overallScore = 0;
            }
            overallScoreText.text = "Overall Score: " + overallScore;
        }

        public static string CleanObjectName(string name)
        {
            return name.Replace("Variant", "").Replace("(Clone)", "").Trim();
        }

        public void AddTrash(string trashName)
        {
            if (!_trashTypes.ContainsKey(trashName))
            {
                return;
            }

            if (_inventory.ContainsKey(trashName))
                _inventory[trashName]++;
            else
                _inventory.Add(trashName, 1);
            
            // update GUI info
            if (!GameManager.Instance.infoPanelHideState.IsHidden(trashName))
                ShowMetadataGui(TrashTypes[trashName]);
            
            UpdateGui();
        }

        private string ToYesNo(bool value)
        {
            return value ? "YES" : "No";
        }
        
        public void ShowMetadataGui(ITrashMetadata meta)
        {
            GameManager.Instance.Freeze();
            Cursor.visible = true;

            
            metaInfoPanel.SetActive(false);
            metaInfoName.text = meta.Name;
            metaInfoDegrades.text = "Degrades in " + meta.DegradeTime;
            metaInfoYesNo.text = $"Recyclable: {ToYesNo(meta.Recyclable)}\n" +
                "Compostable: " + ToYesNo(meta.Compostable) + "\n" +
                "Biodegradable: " + ToYesNo(meta.IsBiodegradable) + "\n" +
                "Reusable: " + ToYesNo(meta.IsReusable);
            metaInfoText.text = meta.Description;
            metaDoNotShowToggle.isOn = false; // default state is off
            metaInfoPanel.SetActive(true);
            
            // re-find the key from the meta object (oops?) 
            GameManager.Instance.infoPanelHideState.CurrentItem = _trashTypes.First(x => x.Value == meta).Key;
        }

        public void UpdateGui()
        {
            foreach (var (trashName, count) in Inventory) Debug.Log($"{trashName}: {count}");

            roundScoreText.text = "Round Score: " + roundScore;
            overallScoreText.text = "Overall Score: " + overallScore;
            PlayerPrefs.SetInt("ScoreOverall", overallScore);
           

            // hide all items first
            foreach (var item in _inventoryGuiItems.Values) item.SetActive(false);

            var i = 0;
            foreach (var (trashName, count) in Inventory)
            {
                if (count < 1) continue;

                var item = _inventoryGuiItems[trashName];
                item.GetComponentInChildren<TMP_Text>().text = count.ToString();
                item.transform.localPosition = new Vector3(170f * i - 490f, 75f, 0);
                item.SetActive(true);
                i++;
            }

            inventoryGuiPanel.SetActive(i > 0);
        }

        private void RemoveTrashByRecyclable(bool recyclable)
        {
            var toRemove = new List<string>();
            // first one only
            foreach (var (trashName, count) in Inventory)
            {
                if (!TrashTypes.ContainsKey(trashName)) break;
                var trashMeta = TrashTypes[trashName];
                if (trashMeta.Recyclable != recyclable) break;
                toRemove.Add(trashName);
                roundScore += trashMeta.PointValue * count;
                overallScore += trashMeta.PointValue * count;
                GameManager.Instance.gamePointState.Points += trashMeta.PointValue * count;
                OnInventoryItemAddedEvent?.Invoke(trashName, count);
                break;
            }

            foreach (var trashName in toRemove) _inventory.Remove(trashName);

            UpdateGui();
        } 
        
        public void EmptyTrash()
        {
            RemoveTrashByRecyclable(false);
        }

        public void EmptyRecyclables()
        {
            RemoveTrashByRecyclable(true);
        }
    }
}