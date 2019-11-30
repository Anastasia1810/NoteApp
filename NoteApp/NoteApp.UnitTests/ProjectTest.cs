using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NoteApp.UnitTests
{
    [TestFixture]
    class ProjectTest
    {
        [Test(Description = "Проверка добавления заметки в Project")]
        public void TestAddNoteToProject()
        {
            var note = new Note();
            note.Title = "Title";
            note.Text = "Text";
            note.DateOfCreation = DateTime.Now;
            note.DateOfChange = DateTime.Now;
            note.CategoryNote = CategoryNote.Different;

            var project = new Project();
            project.Note.Add(note);
            var actual = project.Note;
            Assert.AreEqual(project.Note, actual, "");
        }

        [Test(Description = "Проверка списка заметок в Project")]
        public void TestListNoteToProject()
        {
            var note = new List<Note>
            {
                new Note {Title = "Title 1"},
                new Note {Title = "Title 2"},
                new Note {Title = "Title 3"}
            };

            var project = new Project(note);
            Assert.AreEqual(project.Note, note, "");
        }
    }
}
