using UnityEngine;

namespace Assets
{
    public class GamePointState
    {
        private int _points = 0;

        public int Points
        {
            get => _points;
            set
            {
                _points = value;
                SaveToPlayerPrefs();
            }
        }

        public GamePointState()
        {
            LoadFromPlayerPrefs();
        }

        public void LoadFromPlayerPrefs()
        {
            _points = PlayerPrefs.GetInt("points", 0);
        }

        public void SaveToPlayerPrefs()
        {
            PlayerPrefs.SetInt("points", _points);
        }
    }
}