namespace Alaveri.UnitTesting
{
    /// <summary>
    /// A generic test sub-item for unit testing.
    /// </summary>
    [Serializable]
    public class GenericSubItem
    {
        /// <summary>
        /// The ID of this test item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of this test item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the GenericSubItem class using the specified id and name.
        /// </summary>
        /// <param name="id">The ID of this item.</param>
        /// <param name="name">The name of this item.</param>
        public GenericSubItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the GenericSubItem class..
        /// </summary>
        public GenericSubItem()
        {
        }
    }
}
