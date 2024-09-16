using Business;
using Entities;
using Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WpfCrud
{
    public partial class MainWindow : Window
    {
        private readonly UserBusiness _userService;
        private readonly AreaBusiness _areaService;
        private ObservableCollection<Usuarios> _allUsers; 
        private ObservableCollection<Areas> _areas;
        public ObservableCollection<Usuarios> PagedUsers { get; set; }
        private Usuarios _selectedUser = null; 

        private int _currentPage = 1;
        private int _itemsPerPage = 10;

        public string PageInfo => $"Página {_currentPage} de {(_allUsers.Count + _itemsPerPage - 1) / _itemsPerPage}";

        public MainWindow()
        {
            InitializeComponent();

            var unitOfWork = new UnitOfWork();
            _userService = new UserBusiness(unitOfWork);
            _areaService = new AreaBusiness(unitOfWork);

            _allUsers = new ObservableCollection<Usuarios>();
            _areas = new ObservableCollection<Areas>();
            PagedUsers = new ObservableCollection<Usuarios>();

            DataContext = this;

            LoadUsers();
            LoadAreas();
        }

        private void LoadUsers()
        {
            try
            {
                var users = _userService.ListaUsuario().ToList();
                _allUsers = new ObservableCollection<Usuarios>(users);

                UpdatePagedUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los usuarios: {ex.Message}");
            }
        }
        private void LoadAreas()
        {
            try
            {
                var areas = _areaService.ListaAreas().ToList();
                _areas = new ObservableCollection<Areas>(areas);
                AreaComboBox.ItemsSource = _areas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las áreas: {ex.Message}");
            }
        }
        private void AssignArea_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedUser != null && AreaComboBox.SelectedValue != null)
            {
                int selectedAreaId = (int)AreaComboBox.SelectedValue;
                bool result = _userService.AsignarArea(_selectedUser.IdUsuario, selectedAreaId);

                if (result)
                {
                    MessageBox.Show("Área asignada con éxito.");
                }
                else
                {
                    MessageBox.Show("El usuario ya cuenta con una Área asignada");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario y un área para realizar la asignación.");
            }
        }

        private void UpdatePagedUsers()
        {
            PagedUsers.Clear();

            var pagedUsers = _allUsers.Skip((_currentPage - 1) * _itemsPerPage)
                                      .Take(_itemsPerPage);

            foreach (var user in pagedUsers)
            {
                PagedUsers.Add(user);
            }

            UsersDataGrid.ItemsSource = PagedUsers;
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserFirstNameTextBox.Text) ||
        string.IsNullOrWhiteSpace(UserLastNameTextBox.Text) ||
        string.IsNullOrWhiteSpace(UserRoleTextBox.Text) ||
        string.IsNullOrWhiteSpace(UserEmailTextBox.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.");
                return;
            }

            var usuario = new Usuarios
            {
                IdUsuario = 0,
                Nombres = UserFirstNameTextBox.Text,
                Apellidos = UserLastNameTextBox.Text,
                Cargo = UserRoleTextBox.Text,
                Correo = UserEmailTextBox.Text
            };

            bool result = _userService.CreateUser(usuario);

            if (result)
            {
                _allUsers.Add(usuario);
                UpdatePagedUsers();
                MessageBox.Show("Usuario creado con éxito.");
                ClearFields();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al crear el usuario.");
            }
        }

        private void UsersDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem != null)
            {
                _selectedUser = (Usuarios)UsersDataGrid.SelectedItem;

                UserFirstNameTextBox.Text = _selectedUser.Nombres;
                UserLastNameTextBox.Text = _selectedUser.Apellidos;
                UserRoleTextBox.Text = _selectedUser.Cargo;
                UserEmailTextBox.Text = _selectedUser.Correo;

                UpdateButton.IsEnabled = true;
            }
        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedUser != null)
            {
                _selectedUser.Nombres = UserFirstNameTextBox.Text;
                _selectedUser.Apellidos = UserLastNameTextBox.Text;
                _selectedUser.Cargo = UserRoleTextBox.Text;
                _selectedUser.Correo = UserEmailTextBox.Text;

                bool result = _userService.UpdateUser(_selectedUser);

                if (result)
                {
                    MessageBox.Show("Usuario actualizado con éxito.");
                    UpdatePagedUsers();
                    ClearFields();
                    UpdateButton.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al actualizar el usuario.");
                }
            }
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdatePagedUsers();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if ((_currentPage * _itemsPerPage) < _allUsers.Count)
            {
                _currentPage++;
                UpdatePagedUsers();
            }
        }

        private void ClearFields()
        {
            UserFirstNameTextBox.Clear();
            UserLastNameTextBox.Clear();
            UserRoleTextBox.Clear();
            UserEmailTextBox.Clear();
            _selectedUser = null;
        }
    }
}
