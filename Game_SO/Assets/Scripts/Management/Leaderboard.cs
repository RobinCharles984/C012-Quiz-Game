using UnityEngine;
using TMPro;

// NOTE: Make sure to include the following namespace wherever you want to access Leaderboard Creator methods
using Dan.Main;
using UnityEngine.SocialPlatforms.Impl;

namespace LeaderboardCreatorDemo
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _entryRank;
        [SerializeField] private TMP_Text[] _entryScore;
        [SerializeField] private TMP_Text[] _entryName;
        [SerializeField] private TMP_InputField _usernameInputField;

        // Make changes to this section according to how you're storing the player's score:
        // ------------------------------------------------------------

        private int Score = GameManager.instance.score;
        // ------------------------------------------------------------

        private void Start()
        {
            LoadEntries();
        }

        private void LoadEntries()
        {
            // Q: How do I reference my own leaderboard?
            // A: Leaderboards.<NameOfTheLeaderboard>

            Leaderboards.SoLeaderBoard.GetEntries(entries =>
            {
                foreach (var t in _entryRank)
                    t.text = "";
                var length = Mathf.Min(_entryRank.Length, entries.Length);
                for (int i = 0; i < length; i++)
                    _entryRank[i].text = $"{entries[i].Rank}";

                foreach (var t in _entryScore)
                    t.text = "";
                length = Mathf.Min(_entryScore.Length, entries.Length);
                for (int i = 0; i < length; i++)
                    _entryScore[i].text = $"{entries[i].Score}";

                foreach (var t in _entryName)
                    t.text = "";
                length = Mathf.Min(_entryName.Length, entries.Length);
                for (int i = 0; i < length; i++)
                    _entryName[i].text = $"{entries[i].Username}";
            });
        }

        public void UploadEntry()
        {
            Leaderboards.SoLeaderBoard.UploadNewEntry(_usernameInputField.text, Score, isSuccessful =>
            {
                if (isSuccessful)
                    LoadEntries();
            });
        }
    }
}