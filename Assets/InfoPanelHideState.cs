using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class InfoPanelHideState
    {
        public HashSet<string> HiddenItems { get; set; } = new();

        public string CurrentItem { get; set; } = "";
        
        public InfoPanelHideState(bool load = true)
        {
            if (load)
                LoadFromPlayerPrefs();
        }
        
        public bool IsHidden(string name)
        {
            return HiddenItems.Contains(name);
        }
        
        public void Hide(string name)
        {
            HiddenItems.Add(name);
            SaveToPlayerPrefs();
        }

        public void HideCurrent()
        {
            if (CurrentItem == "")
                return;
            
            Hide(CurrentItem);
        }
        
        private void LoadFromPlayerPrefs()
        {
            var hiddenItems = PlayerPrefs.GetString("HiddenItems", "");
            if (hiddenItems.Length < 1)
            {
                return;
            }
            
            var items = hiddenItems.Split(';');
            foreach (var item in items)
            {
                HiddenItems.Add(item);
            }
        }
        
        public void SaveToPlayerPrefs()
        {
            PlayerPrefs.SetString("HiddenItems", string.Join(";", HiddenItems));
        }

        public void ClearAndSaveToPlayerPrefs()
        {
            HiddenItems.Clear();
            SaveToPlayerPrefs();
        }
    }
}