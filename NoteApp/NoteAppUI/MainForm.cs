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
            CategoryComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Загрузка данных из файла при запуске приложения.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            _project = ProjectManager.LoadFromFile(_fileName);
            UpdateNotesListBox(_project);
            if (_project.Note.Count >= 1)
            {
                NotesListBox.SetSelected(0, true);
            }
        }

        /// <summary>
        /// Обновление списка заметок.
        /// </summary>
        private void UpdateNotesListBox(Project project)
        {
            NotesListBox.Items.Clear();
            foreach (var note in project.Note)
            {
                NotesListBox.Items.Add(note.Title);
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

            ProjectManager.SaveToFile(_project,_fileName);
        }

        /// <summary>
        /// Метод для редактирования существующей записи.
        /// </summary>
        public void EditNote()
        {
            //Получаем текущую выбранную дату
            var index = NotesListBox.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            var note = _project.Note[index];
            var addAndEditForm = new NoteForm(); //Создаем форму
            addAndEditForm.Note = note; //Передаем форме данные
            var dialogResult = addAndEditForm.ShowDialog(); //Отображаем форму для редактирования
            if (dialogResult == DialogResult.OK)
            {
                var updatedNote = addAndEditForm.Note; //Забираем измененные данные
                //Осталось удалить старые данные по выбранному индексу
                // и заменить их на обновленные
                NotesListBox.Items.RemoveAt(index);
                _project.Note.RemoveAt(index);
                _project.Note.Insert(index, updatedNote);
                var time = updatedNote.DateOfChange.ToLongTimeString();
                var title = updatedNote.Title;
                NotesListBox.Items.Insert(index, title);
            }

            ProjectManager.SaveToFile(_project, _fileName);
        }

        /// <summary>
        /// Метод для удаления записи.
        /// </summary>
        public void RemoveNote()
        {
            var index = NotesListBox.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            var result = new System.Windows.Forms.DialogResult();
            result = MessageBox.Show(
                @"Do you really want to remove this note: " +
                _project.Note[index].Title + " ?", "Remove note",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                NotesListBox.Items.RemoveAt(index);
                _project.Note.RemoveAt(index);
                ProjectManager.SaveToFile(_project, _fileName);
                if (_project.Note.Count >= 1)
                {
                    NotesListBox.SetSelected(0, true);
                }

            }
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
            }
            else
            {
                TitleLabel.Text = _project.Note[NotesListBox.SelectedIndices[0]].Title;
                TextRichTextBox1.Text = _project.Note[NotesListBox.SelectedIndices[0]].Text;
                CategoryLabel.Text = _project.Note[NotesListBox.SelectedIndices[0]].CategoryNote.ToString();
                CreatedDateTime.Value = _project.Note[NotesListBox.SelectedIndices[0]].DateOfCreation; ;
                ModifiedDateTime.Value = _project.Note[NotesListBox.SelectedIndices[0]].DateOfChange;
            }
           
        }

           

    }
}
