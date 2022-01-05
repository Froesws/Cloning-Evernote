using MyEvernote.Auxiliary;
using MyEvernote.Model;
using MyEvernote.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyEvernote.ViewModel
{
    public class NotesViewModel
    {
        private ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                //TODO: 
            }
        }

        public ObservableCollection<Note> Notes { get; set; }
        private ICommand newNotebook;

        public ICommand NewNotebookCommand
        {
            get
            {
                return newNotebook ??(newNotebook = new RelayCommand(x => NewNotebookButton()));
            }
        }

        private ICommand newNote;

        public ICommand NewNoteCommand
        {
            get
            {
                return newNote ?? (newNote = new RelayCommand((object parameter) => NewNoteButton(parameter),
                    (object parameter) => (CanExecute(parameter))));
            }
        }

        private void NewNotebookButton()
        {
            Notebook localNotebook = new Notebook()
            {
                Name = "New Notebook",
            };

            DataBaseHelper.Insert(localNotebook);
        }

        private readonly Predicate<object> CanExecute = delegate (object parameter)
        {
            bool returnValue = false;
            if (parameter is Notebook localNotebook)
            {
                returnValue = true;
            }
           return returnValue;

        };
        private void NewNoteButton(object parameter)
        {
            Notebook localNotebook = parameter as Notebook;

            Note newNote = new Note()
            {
                NotebookId = localNotebook.Id,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                Title = "New Note"
            };

            DataBaseHelper.Insert(newNote);
        }

    }
}