using MyEvernote.Auxiliary;
using MyEvernote.Model;
using MyEvernote.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace MyEvernote.ViewModel
{
    public class NotesViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        private Notebook selectedNotebook;

        private ICommand speechCommand;

        public ICommand SpeechCommand
        {
            get
            {
                return speechCommand ?? (speechCommand = new RelayCommand(x => { }));
            }
        }

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                OnPropertyChanged(nameof(SelectedNotebook));
                GetNotes();
            }
        }

        private ICommand newNotebookCommand;

        public ICommand NewNotebookCommand
        {
            get
            {
                return newNotebookCommand ?? (newNotebookCommand =
                    new RelayCommand(
                        _ => AddNotebookAction(_))
                    );
            }
        }

        private ICommand newNoteCommand;

        public ICommand NewNoteCommand
        {
            get
            {
                return newNoteCommand ?? (newNoteCommand = new RelayCommand(
                    (x) => AddNoteAction(x),
                    (x) => x is Notebook)
                    );
            }
        }

        private readonly Action<object> AddNotebookAction = delegate (object parameter)
        {
            Notebook localNotebook = new Notebook()
            {
                Name = "New Notebook",
            };

            _ = DataBaseHelper.Insert(localNotebook);
        };

        private readonly Action<object> AddNoteAction = delegate (object parameter)
        {
            if (parameter is Notebook localNotebook)
            {
                Note newNote = new Note()
                {
                    NotebookId = localNotebook.Id,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Title = "New Note"
                };

                _ = DataBaseHelper.Insert(newNote);
            }
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public NotesViewModel ()
        {
            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            GetNotebooks();
        }

        private void GetNotebooks()
        {
            var localNotebooks = DataBaseHelper.Read<Notebook>();
            Notebooks.Clear();

            foreach (Notebook notebook in localNotebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private void GetNotes()
        {
            if(SelectedNotebook is object)
            {
                var localNotes = DataBaseHelper.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();
                Notes.Clear();

                foreach (Note note in localNotes)
                {
                    Notes.Add(note);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}