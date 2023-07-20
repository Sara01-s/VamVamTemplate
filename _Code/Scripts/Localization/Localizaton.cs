using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization;
using UnityEngine;

namespace VamVam.Scripts.Localization {

    internal sealed class Localization : MonoBehaviour {

        [SerializeField] private StringTable _stringTable;

        public static void UpdateStringTable() {
            var sheet = Resources.Load<StringTable>("Localization/Tables/ST_UI_TEXT");
        }

        private void OnEnable() {
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        }

        private void OnDisable() {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }

        private void Start() {
            LoadLocale(LocalizationSettings.SelectedLocale);
        }

        public void SetLanguage(string language) {
            var locale = LocalizationSettings.AvailableLocales.GetLocale(language);

            if (locale != null)
                LocalizationSettings.SelectedLocale = locale;
            else 
                Debug.LogWarningFormat("Could not find Locale for language '{0}'", language);
        }

        private void OnLocaleChanged(Locale locale) {
            LoadLocale(locale);
        }

        private void LoadLocale(Locale locale) {
            LocalizationSettings.SelectedLocale = locale;
            Debug.Log($"Loaded Locale '{locale.ToString()}'");
        }

        public string GetLocalizedText(string key) {
            return _stringTable.GetEntry(key).GetLocalizedString().ToString();
        }
    } 
}

