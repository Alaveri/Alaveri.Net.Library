namespace Alaveri.UnitTesting
{
    /// <summary>
    /// Generic test class for use in unit testing.
    /// </summary>
    [Serializable]
    public class GenericItem
    {
        /// <summary>
        /// The id of this item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A test string property.
        /// </summary>
        public string StringProperty { get; set; }

        /// <summary>
        /// A test Int32 property
        /// </summary>
        public int Int32Property { get; set; }

        /// <summary>
        /// A test single property.
        /// </summary>
        public float SingleProperty { get; set; }

        /// <summary>
        /// List of generic sub-items.
        /// </summary>
        public List<GenericSubItem> SubItems { get; } = new List<GenericSubItem>();
    }
}
