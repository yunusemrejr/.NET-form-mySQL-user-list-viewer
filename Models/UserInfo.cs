namespace UserListViewer.Models
{
    /// <summary>
    /// Represents a user entity from the database.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Gets or sets the user's unique identifier.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's surname.
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets the user's full name.
        /// </summary>
        public string FullName => $"{Name} {Surname}";
    }
}
