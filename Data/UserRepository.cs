using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using UserListViewer.Configuration;
using UserListViewer.Models;

namespace UserListViewer.Data
{
    /// <summary>
    /// Repository implementation for user data access operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="databaseSettings">The database configuration settings.</param>
        public UserRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            _connectionString = databaseSettings.Value.ConnectionString 
                ?? throw new ArgumentNullException(nameof(databaseSettings), "Connection string cannot be null");
        }

        /// <inheritdoc/>
        public async Task<List<UserInfo>> GetAllUsersAsync()
        {
            var users = new List<UserInfo>();

            try
            {
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();

                const string query = "SELECT ID, Name, Surname, Email FROM Userinfo ORDER BY ID";
                
                await using var command = new MySqlCommand(query, connection);
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    users.Add(new UserInfo
                    {
                        ID = reader.GetInt32("ID"),
                        Name = reader.GetString("Name"),
                        Surname = reader.GetString("Surname"),
                        Email = reader.GetString("Email")
                    });
                }
            }
            catch (MySqlException ex)
            {
                throw new InvalidOperationException($"Database error while retrieving users: {ex.Message}", ex);
            }

            return users;
        }

        /// <inheritdoc/>
        public async Task<UserInfo?> GetUserByIdAsync(int id)
        {
            try
            {
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();

                const string query = "SELECT ID, Name, Surname, Email FROM Userinfo WHERE ID = @Id";
                
                await using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new UserInfo
                    {
                        ID = reader.GetInt32("ID"),
                        Name = reader.GetString("Name"),
                        Surname = reader.GetString("Surname"),
                        Email = reader.GetString("Email")
                    };
                }
            }
            catch (MySqlException ex)
            {
                throw new InvalidOperationException($"Database error while retrieving user {id}: {ex.Message}", ex);
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                await using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();
                return connection.State == System.Data.ConnectionState.Open;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
    }
}
