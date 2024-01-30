namespace VVT {

    public static class StringExtensions {

        /// <summary>
        /// Returns a cut string after the target char value.
        /// </summary>
        /// <param name="value">The string to cut.</param>
        /// <param name="target">String will be cut after target character</param>
        /// <param name="inclusive">Should the cut include the target character?</param>
        /// <returns>Cut string</returns>
        public static string CutAfter(this string value, char target, bool inclusive = true) {
            int index = inclusive ? value.IndexOf(target) : value.IndexOf(target) + 1;
            return (index >= 0) ? value[.. index] : string.Empty;
        }

        /// <summary>
        /// Returns a cut string before the target char value.
        /// </summary>
        /// <param name="value">The string to cut.</param>
        /// <param name="target">String will be cut before target character</param>
        /// <param name="exclusive">Should the cut exclude the target character?</param>
        /// <returns>Cut string</returns>
        public static string CutBefore(this string value, char target, bool exclusive = true) {
            int index = exclusive ? value.IndexOf(target) + 1 : value.IndexOf(target);
            return (index >= 0) ? value[index ..] : string.Empty;
        }

        /// <summary>
        /// Returns a cut string between two characters.
        /// </summary>
        /// <param name="value">The string to cut.</param>
        /// <param name="start">Where the cut's starts.</param>
        /// <param name="end">Where the cut's ends.</param>
        /// <param name="inclusive">Should the cut include start and end?</param>
        /// <returns>Cut string.</returns>
        public static string CutBetween(this string value, char start, char end, bool inclusive = true) {
            int startIndex = value.IndexOf(start);
            int endIndex   = value.IndexOf(end);

            if (startIndex >= 0 && endIndex > startIndex) {
                int startIndexToUse = inclusive ? startIndex + 1 : startIndex;
                int endIndexToUse   = inclusive ? endIndex : endIndex + 1;

                return value[startIndexToUse .. endIndexToUse];
            }

            return string.Empty;
        }

    }
}
