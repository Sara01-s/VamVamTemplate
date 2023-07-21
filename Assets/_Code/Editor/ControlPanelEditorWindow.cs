using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEditor;
using System;

namespace VamVam.Editor {

    public class ControlPanelEditorWindow : EditorWindow {
        [MenuItem("VamVam/Control Panel")]
        public static void Open() {
            var window = CreateWindow<ControlPanelEditorWindow>("Control panel");
        }

        internal struct DesignFieldInfo {
            internal MemberInfo Info { get; }
            internal DesignFieldAttribute DesignField { get; }
            internal DesignFieldInfo(MemberInfo info, DesignFieldAttribute attribute) {
                Info = info;
                DesignField = attribute;
            }
        }

        private List<DesignFieldInfo> _designFields = new List<DesignFieldInfo>();

        private void OnEnable() {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies) {
                var types = assembly.GetTypes();

                foreach (var type in types) {
                    var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                    var members = type.GetMembers(flags);

                    foreach(var member in members) {
                        if (member.CustomAttributes.ToArray().Length > 0) {
                            var attribute = member.GetCustomAttribute<DesignFieldAttribute>();

                            if (attribute != null) {
                                _designFields.Add(new DesignFieldInfo(member, attribute));
                            }
                        }
                    }
                }
            }
        }

        private void OnGUI() {
            EditorGUILayout.LabelField("Design Properties", EditorStyles.boldLabel);

            foreach (var member in _designFields) {
                EditorGUILayout.LabelField($"{member.DesignField.DisplayName}");
            }
        }
    }
    
}