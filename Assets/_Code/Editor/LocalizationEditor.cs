using UnityEditor.Localization.Plugins.Google;
using UnityEditor;

namespace VamVam.Editor {
    
    internal sealed class LocalizationEditor {
        private const string SpreadSheetID = "1R56tGoEMf60AkELfKBfr763a7dz4G2VoLkIjkt0zDFQ";

        [MenuItem("VamVam/Localization/Open UI Sheet")]
        public static void OpenUISheet() {
            GoogleSheets.OpenSheetInBrowser(SpreadSheetID, 0);
        }
    }
}