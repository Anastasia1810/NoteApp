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
    public partial class WindowForm : Form
    {
        private List<Note> _notes = new List<Note>();
        public WindowForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void EditButton_Click(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Note newNote = new Note();
            newNote.Title = "1";
            newNote.CategoryNote = CategoryNote.Different;
            newNote.Text = "123";

            _notes.Add(newNote);
            NotesListBox.Items.Add(newNote.Title);

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

        }

        private void deleteNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAndEditForm addForm = new AddAndEditForm();
            addForm.Show();
        }

        private void editNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAndEditForm editForm = new AddAndEditForm();
            editForm.Show();
        }
    }
}
