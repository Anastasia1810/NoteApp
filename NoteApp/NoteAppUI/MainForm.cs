using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NoteApp;

namespace NoteAppUI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Поле для хранения пути файла.
        /// </summary>
        private readonly string _fileName = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\NoteApp.json";

        /// <summary>
        /// Объект класса
        /// </summary>
        private Project _project = new Project();

        /// <summary>
        /// Поле для показываемых заметок.
        /// </summary>
        private static List<Note> _showNotes;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            AddCategoryBox();
        }

        /// <summary>
        /// Добавляем категории.
        /// </summary>
        private void AddCategoryBox()
        {
            CategoryComboBox.Items.Add("All");
            CategoryComboBox.Items.AddRange(Enum.GetNames(typeof(CategoryNote)));
            CategoryComboBox.SelectedItem = 0;
        }

        /// <summary>
        /// Загрузка данных из файла при запуске приложения.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            _project = ProjectManager.LoadFromFile(_fileName);
            CategoryComboBox.Text = _project.CurrentCategory;
            ShowListBoxNote();
            
            if (_project.CurrentNote != null)
            {
                TitleLabel.Text = _project.CurrentNote.Title;
                TextRichTextBox1.Text = _project.CurrentNote.Text;
                CategoryLabel.Text = _project.CurrentNote.CategoryNote.ToString();
                CreatedDateTime.Value = _project.CurrentNote.DateOfCreation;
                ModifiedDateTime.Value = _project.CurrentNote.DateOfChange;
            }
        }

        
        /// <summary>
        /// Сохранение данных в файл при закрытии.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProjectManager.SaveToFile(_project, _fileName);
        }

        /// <summary>
        /// Кнопка о программе в меню Help
        /// </summary>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.Show();
        }

        /// <summary>
        /// Кнопка закрытия в меню File
        /// </summary>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Метод для добавления новой записи.
        /// </summary>
        public void AddNote()
        {
            var addForm = new NoteForm();
            addForm.Note = new Note();


            var dialogResult = addForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var newNote = addForm.Note;

                _project.Note.Add(newNote);
                NotesListBox.Items.Add(newNote.Title);
            }

            ShowListBoxNote();

            ProjectManager.SaveToFile(_project,_fileName);
        }

        /// <summary>
        /// Метод для редактирования существующей записи.
        /// </summary>
        public void EditNote()
        {
            
            //Получаем текущую выбранную заметку
            if (NotesListBox.SelectedItem == null)
            {
                MessageBox.Show("Заметка не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var index = NotesListBox.SelectedIndex;
    
            var addAndEditForm = new NoteForm(); //Создаем форму
            var note = _showNotes[NotesListBox.SelectedIndex];
            addAndEditForm.Note = note; //Передаем форме данные
            var dialogResult = addAndEditForm.ShowDialog(); //Отображаем форму для редактирования
            if (dialogResult == DialogResult.OK)
            {
                NotesListBox.Items[index] = addAndEditForm.Note.Title;

                TitleLabel.Update();
                NotesListBox.Update();

                ShowListBoxNote();

                ProjectManager.SaveToFile(_project, _fileName);
            }

        }

        /// <summary>
        /// Метод для удаления записи.
        /// </summary>
        public void RemoveNote()
        {
            var index = NotesListBox.SelectedIndex;
            if (NotesListBox.SelectedItem == null)
            {
                MessageBox.Show("Заметка не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = new System.Windows.Forms.DialogResult();
            result = MessageBox.Show(
                @"Do you really want to remove this note: " + NotesListBox.Items[NotesListBox.SelectedIndex]
                + "?", "Remove note",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                ClearAll();
                _project.Note.Remove(_showNotes[NotesListBox.SelectedIndex]);
                NotesListBox.Items.Remove(_showNotes[NotesListBox.SelectedIndex]);
                _project.CurrentNote = null;
                
                ShowListBoxNote();
                ProjectManager.SaveToFile(_project, _fileName);
            }
        }


        /// <summary>
        /// Очищает все поля.
        /// </summary>
        public void ClearAll()
        {
            TitleLabel.Text = "";
            CategoryLabel.Text = "";
            CreatedDateTime.Value = DateTime.Now;
            ModifiedDateTime.Value = DateTime.Now;
            TextRichTextBox1.Text = "";
        }
        /// <summary>
        /// Кнопка удаления в меню Edit
        /// </summary>
        private void DeleteNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveNote();
        }

        /// <summary>
        /// Кнопка удаления заметки
        /// </summary>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            RemoveNote();
        }

        /// <summary>
        /// Кнопка добавления в меню Edit
        /// </summary>
        private void AddNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNote();
        }

        /// <summary>
        /// Кнопка редактирования в меню Edit
        /// </summary>
        private void EditNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditNote();   
        }

        /// <summary>
        /// Кнопка добавления заметки 
        /// </summary>
        private void AddButton_Click(object sender, EventArgs e)
        {
            AddNote();
        }

        /// <summary>
        /// Кнопка редактирования заметки
        /// </summary>
        private void EditButton_Click(object sender, EventArgs e)
        {
            EditNote();
        }

        /// <summary>
        /// Вывод текста из ListBox'a в TextBox.
        /// </summary>
        private void NotesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NotesListBox.SelectedIndex == -1)
            {
                TitleLabel.Text = "";
                CategoryLabel.Text = "";
                CreatedDateTime.Value = DateTime.Now;
                ModifiedDateTime.Value = DateTime.Now;
                TextRichTextBox1.Text = "";
                _project.CurrentNote = null;
            }
            else
            {
                var note = _showNotes[NotesListBox.SelectedIndex];
               

                TitleLabel.Text = note.Title;
                TextRichTextBox1.Text = note.Text;
                CategoryLabel.Text = note.CategoryNote.ToString();
                CreatedDateTime.Value = note.DateOfCreation; ;
                ModifiedDateTime.Value = note.DateOfChange;
                _project.CurrentNote = _showNotes[NotesListBox.SelectedIndex];
            }

            ProjectManager.SaveToFile(_project, _fileName);

        }
        /// <summary>
        /// Вывод заметок по категории
        /// </summary>
        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowListBoxNote();
        }

        /// <summary>
        /// Вывод заметок в листбокс
        /// </summary>
        public void ShowListBoxNote()
        {
            NotesListBox.Items.Clear();
            

            if (_project.Note.Count <= 0)
                return;

            if (CategoryComboBox.SelectedItem.ToString() != "All")
            {
                _showNotes = _project.SortedNotesCategory(StringToNoteCategory
                    (CategoryComboBox.SelectedItem.ToString()));
                _project.CurrentCategory = CategoryComboBox.SelectedItem.ToString();
            }
            else
            {
                _showNotes = _project.SortedNotes();
                _project.CurrentCategory = "All";
            }
            foreach (Note t in _showNotes)
            {
                NotesListBox.Items.Add(t.Title);
            }

            ProjectManager.SaveToFile(_project, _fileName);

        }

        /// <summary>
        /// Перевод текста категории в саму категорию
        /// </summary>
        private CategoryNote StringToNoteCategory(string textCategory)
        {
            switch (textCategory)
            {
                case "Different":
                    return CategoryNote.Different;

                case "Documents":
                    return CategoryNote.Documents;

                case "Finance":
                    return CategoryNote.Finance;

                case "HeathAndSport":
                    return CategoryNote.HeathAndSport;

                case "Home":
                    return CategoryNote.Home;

                case "People":
                    return CategoryNote.People;

                case "Work":
                    return CategoryNote.Work;

                default:
                    return CategoryNote.Different;
            }
        }

        /// <summary>
        /// Обработчик событий для кнопки Delete, по удалению заметки.
        /// </summary>
        private void RemoveNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                RemoveNote();
            }
        }

      
    }
}
