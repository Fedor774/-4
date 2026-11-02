using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PR_3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Создаем переменную контекста модели Entity Framework
        ModelEF.ModelEF model = new ModelEF.ModelEF();

        // Событие загрузки формы
        private void MainForm_Load(object sender, EventArgs e)
        {
            StartLoadData();
        }

        // Событие клика на кнопку удаления в BindingNavigator
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        // Событие клика на кнопку сохранения в BindingNavigator
        private void usersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        // Событие клика на кнопку добавления нового элемента в BindingNavigator
        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            role_NameComboBox.SelectedIndex = 0;
        }

        // Метод для первоначальной загрузки данных в Источники данных
        private void StartLoadData()
        {
            model.Users.Load();
            role_NameComboBox.DataSource = model.Roles.ToList();
            usersBindingSource.DataSource = model.Users.Local.ToBindingList();
        }

        // Метод для синхронизации изменений с моделью данных и базой данных
        private void SaveData()
        {
            try
            {
                Validate();
                usersBindingSource.EndEdit();
                usersBindingSource.ResetBindings(true);
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                StartLoadData();
            }
        }
    }
}
