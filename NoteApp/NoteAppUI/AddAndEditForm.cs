using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoteApp;

namespace NoteAppUI
{
    public partial class AddAndEditForm : Form
    {
        /// <summary>
        /// Поле для передачи данных.
        /// </summary>
        private Note _note;

        /// <summary>
        /// Определение поля _note.
        /// </summary>
        public Note Note {
            get

            {
                return _note;
            }
            set
            {
                _note = value;
                if (value != null)
                {
                    TitleTextBox.Text = value.Title;
                    NoteRichTextBox.Text = value.Text;
                    CategoryComboBox.SelectedItem = value.CategoryNote;
                    CreatedDateTimePicker.Value = value.DateOfCreation;
                    ModifiedDateTimePicker.Value = value.DateOfChange;
                }
                else
                {
                    TitleTextBox.Text= "Без названия";
                    NoteRichTextBox.Text= "";
                    CategoryComboBox.SelectedItem = "Different";
                    CreatedDateTimePicker.Value = DateTime.Now;
                    ModifiedDateTimePicker.Value = DateTime.Now;
                }
               
                
            }
            }
        

        public AddAndEditForm()
        {
            InitializeComponent();
            AddCategoryBox();

        }
       
        /// <summary>
        /// Метод для добавления категорий
        /// </summary>
        private void AddCategoryBox()
        {
            CategoryComboBox.Items.Add(CategoryNote.Work);
            CategoryComboBox.Items.Add(CategoryNote.Home);
            CategoryComboBox.Items.Add(CategoryNote.HeathAndSport);
            CategoryComboBox.Items.Add(CategoryNote.People);
            CategoryComboBox.Items.Add(CategoryNote.Documents);
            CategoryComboBox.Items.Add(CategoryNote.Finance);
            CategoryComboBox.Items.Add(CategoryNote.Different);
        }

        /// <summary>
        /// Работа кнопки ОК
        /// </summary>
        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
            _note.CategoryNote = (CategoryNote)CategoryComboBox.SelectedItem;
            _note.Text = NoteRichTextBox.Text;
            _note.Title = TitleTextBox.Text;
            _note.DateOfChange = DateTime.Now;
        }

        /// <summary>
        /// Работа кнопки Cancel
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Проверка на корректность ввода названия заметки
        /// </summary>
        private void TitleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (TitleTextBox.Text.Length > 50)
            {
                TitleTextBox.ForeColor = Color.Red;
            }
            else
            {
                TitleTextBox.ForeColor = Color.Black;
            }
        }
    }
}
