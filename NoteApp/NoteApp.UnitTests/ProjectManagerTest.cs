using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NoteApp.UnitTests
{
    [TestFixture]
    class ProjectManagerTest
    {
        
        Project _project = new Project();

        [Test(Description = "Позитивный тест сериализации")]
        public void TestSaveToFileProjectManager()
        {
            //Setup
            var note1 = new Note();
            note1.Title = "Заметка 1";
            note1.Text = "Текст 1";
            note1.DateOfCreation = DateTime.Parse("2019-11-30T14:18:40.1755416+07:00");
            note1.DateOfChange = DateTime.Parse("2019-11-30T14:18:49.6611773+07:00");
            note1.CategoryNote = CategoryNote.Different;

            var note2 = new Note();
            note2.Title = "Заметка 2";
            note2.Text = "Тескт 2";
            note2.DateOfCreation = DateTime.Parse("2019-11-30T14:18:51.1594008+07:00");
            note2.DateOfChange = DateTime.Parse("2019-11-30T14:19:02.04922+07:00");
            note2.CategoryNote = CategoryNote.Work;

            var note3 = new Note();
            note3.Title = "Заметка 3";
            note3.Text = "Текст 3";
            note3.DateOfCreation = DateTime.Parse("2019-11-30T14:19:03.1424418+07:00");
            note3.DateOfChange = DateTime.Parse("2019-11-30T14:19:14.0438695+07:00");
            note3.CategoryNote = CategoryNote.HeathAndSport;

            string name = (@"\expectedProject.json");
            string path = Assembly.GetExecutingAssembly().Location;
            string path1 = Path.GetDirectoryName(path);
            string _fileName = path1 + name;

            //Arrange
            ProjectManager.SaveToFile(_project, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"actualProject.json");
            
            //Assert
            string expected = File.ReadAllText(_fileName);
            string actual = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"actualProject.json");
            Assert.AreEqual(expected, actual, "Метод не производит сериализацию");

        }

        [Test(Description = "Позитивный тест десериализации")]
        public void TestLoadFromFileProjectManager()
        {
            //Setup
            var note1 = new Note();
            note1.Title = "Заметка 1";
            note1.Text = "Текст 1";
            note1.DateOfCreation = DateTime.Parse("2019-11-30T14:18:40.1755416+07:00");
            note1.DateOfChange = DateTime.Parse("2019-11-30T14:18:49.6611773+07:00");
            note1.CategoryNote = CategoryNote.Different;

            var note2 = new Note();
            note2.Title = "Заметка 2";
            note2.Text = "Тескт 2";
            note2.DateOfCreation = DateTime.Parse("2019-11-30T14:18:51.1594008+07:00");
            note2.DateOfChange = DateTime.Parse("2019-11-30T14:19:02.04922+07:00");
            note2.CategoryNote = CategoryNote.Work;

            var note3 = new Note();
            note3.Title = "Заметка 3";
            note3.Text = "Текст 3";
            note3.DateOfCreation = DateTime.Parse("2019-11-30T14:19:03.1424418+07:00");
            note3.DateOfChange = DateTime.Parse("2019-11-30T14:19:14.0438695+07:00");
            note3.CategoryNote = CategoryNote.HeathAndSport;

            _project.Note.Add(note1);
            _project.Note.Add(note2);
            _project.Note.Add(note3);

            string name = (@"\expectedProject.json");
            string path = Assembly.GetExecutingAssembly().Location;
            string path1 = Path.GetDirectoryName(path);
            string _fileName = path1 + name;

            //Arrange
            Project _loadProject = ProjectManager.LoadFromFile(_fileName);

            //Assert
            var expected = true;
            var actual = false;

            for (var numberNote = 0; numberNote < 2; numberNote++)
            {
                if (
                    _loadProject.Note[numberNote].Title == _project.Note[numberNote].Title &&
                    _loadProject.Note[numberNote].Text == _project.Note[numberNote].Text &&
                    _loadProject.Note[numberNote].CategoryNote == _project.Note[numberNote].CategoryNote &&
                    _loadProject.Note[numberNote].DateOfCreation == _project.Note[numberNote].DateOfCreation &&
                    _loadProject.Note[numberNote].DateOfChange == _project.Note[numberNote].DateOfChange 
                   )
                {
                    actual = true;
                }
            }
            Assert.AreEqual(expected, actual, "Метод не производит дисериализацию");
        }
    }
}
