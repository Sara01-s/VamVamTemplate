using System;

namespace VamVam.Editor {

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DesignFieldAttribute : Attribute {

        public string DisplayName;

        public DesignFieldAttribute(string displayName) {
            DisplayName = displayName;
        }
        

    }
}
