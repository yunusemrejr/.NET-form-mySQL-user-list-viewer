using System.Data;
using UserListViewer.Data;
using UserListViewer.Models;

namespace UserListViewer
{
    /// <summary>
    /// Main form for displaying and navigating user information.
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly IUserRepository _userRepository;
        private List<UserInfo> _users = new();
        private int _currentIndex = 0;
        private BindingSource _bindingSource = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository for data access.</param>
        public Form1(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            InitializeComponent();
        }

        /// <summary>
        /// Handles the form load event and initializes data.
        /// </summary>
        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                ShowError("Failed to load users", ex);
            }
        }

        /// <summary>
        /// Loads all users from the database asynchronously.
        /// </summary>
        private async Task LoadUsersAsync()
        {
            try
            {
                // Show loading indicator
                Cursor = Cursors.WaitCursor;

                // Test connection first
                if (!await _userRepository.TestConnectionAsync())
                {
                    MessageBox.Show(
                        "Unable to connect to the database. Please check your connection settings.",
                        "Connection Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Load users
                _users = await _userRepository.GetAllUsersAsync();

                if (_users.Count == 0)
                {
                    MessageBox.Show(
                        "No users found in the database.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // Setup data binding
                SetupDataBinding();
                
                // Navigate to first record
                NavigateToRecord(0);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets up data binding for the form controls.
        /// </summary>
        private void SetupDataBinding()
        {
            // Clear existing bindings
            textBox3.DataBindings.Clear();
            textBox4.DataBindings.Clear();
            textBox5.DataBindings.Clear();
            textBox6.DataBindings.Clear();
            comboBox1.Items.Clear();

            // Setup binding source
            _bindingSource.DataSource = _users;

            // Bind controls to data source
            textBox3.DataBindings.Add("Text", _bindingSource, nameof(UserInfo.Name), false, DataSourceUpdateMode.Never);
            textBox4.DataBindings.Add("Text", _bindingSource, nameof(UserInfo.Surname), false, DataSourceUpdateMode.Never);
            textBox5.DataBindings.Add("Text", _bindingSource, nameof(UserInfo.Email), false, DataSourceUpdateMode.Never);
            textBox6.DataBindings.Add("Text", _bindingSource, nameof(UserInfo.ID), false, DataSourceUpdateMode.Never);

            // Setup DataGridView
            dataGridView1.DataSource = _bindingSource;

            // Populate ComboBox
            foreach (var user in _users)
            {
                comboBox1.Items.Add(user.ID);
            }
        }

        /// <summary>
        /// Navigates to a specific record by index.
        /// </summary>
        /// <param name="index">The zero-based index of the record.</param>
        private void NavigateToRecord(int index)
        {
            if (_users.Count == 0) return;

            // Ensure index is within bounds
            _currentIndex = Math.Max(0, Math.Min(index, _users.Count - 1));
            
            // Update binding source position
            _bindingSource.Position = _currentIndex;

            // Update position display
            UpdatePositionDisplay();
        }

        /// <summary>
        /// Updates the position display textbox.
        /// </summary>
        private void UpdatePositionDisplay()
        {
            if (_users.Count > 0)
            {
                textBox2.Text = $"{_currentIndex + 1}/{_users.Count}";
            }
            else
            {
                textBox2.Text = "0/0";
            }
        }

        /// <summary>
        /// Shows an error message to the user.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="exception">The exception that occurred.</param>
        private void ShowError(string message, Exception exception)
        {
            MessageBox.Show(
                $"{message}\n\nDetails: {exception.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #region Navigation Button Handlers

        /// <summary>
        /// Navigates to the first record.
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            NavigateToRecord(0);
        }

        /// <summary>
        /// Navigates to the previous record.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            NavigateToRecord(_currentIndex - 1);
        }

        /// <summary>
        /// Navigates to the next record.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            NavigateToRecord(_currentIndex + 1);
        }

        /// <summary>
        /// Navigates to the last record.
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            NavigateToRecord(_users.Count - 1);
        }

        /// <summary>
        /// Navigates to the previous record (alternative button).
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            NavigateToRecord(_currentIndex - 1);
        }

        /// <summary>
        /// Navigates to the next record (alternative button).
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            NavigateToRecord(_currentIndex + 1);
        }

        #endregion

        #region ComboBox Handler

        /// <summary>
        /// Handles the ComboBox selection change event.
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && int.TryParse(comboBox1.SelectedItem.ToString(), out int selectedId))
            {
                var index = _users.FindIndex(u => u.ID == selectedId);
                if (index >= 0)
                {
                    NavigateToRecord(index);
                }
            }
        }

        #endregion

        #region Unused Event Handlers (Kept for Designer compatibility)

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void tabPage1_Click(object sender, EventArgs e) { }
        private void tabPage2_Click(object sender, EventArgs e) { }
        private void tabPage3_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox7_TextChanged(object sender, EventArgs e) { }
        private void textBox8_TextChanged(object sender, EventArgs e) { }
        private void textBox9_TextChanged(object sender, EventArgs e) { }

        #endregion
    }
}
