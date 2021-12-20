using MyEvernote.Auxiliary;
using MyEvernote.Model;
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
                return newNotebook = newNotebook ?? new RelayCommand(x => NewNotebookButton());
            }
        }

        private ICommand newNote;

        public ICommand NewNoteCommand
        {
            get
            {
                return newNote = newNote ?? new RelayCommand(x => NewNoteButton());
            }
        }

        void NewNotebookButton()
        {

        }

        void NewNoteButton()
        {

        }

    }
}
