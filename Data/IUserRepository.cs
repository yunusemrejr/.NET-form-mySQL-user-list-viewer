using UserListViewer.Models;

namespace UserListViewer.Data
{
    /// <summary>
    /// Interface for user data access operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves all users from the database asynchronously.
        /// </summary>
        /// <returns>A list of all users.</returns>
        Task<List<UserInfo>> GetAllUsersAsync();

        /// <summary>
        /// Retrieves a user by their ID asynchronously.
        /// </summary>
        /// <param name="id">The user's unique identifier.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        Task<UserInfo?> GetUserByIdAsync(int id);

        /// <summary>
        /// Tests the database connection.
        /// </summary>
        /// <returns>True if connection is successful; otherwise, false.</returns>
        Task<bool> TestConnectionAsync();
    }
}
