namespace csharp_editor.Dialogs {

    public enum ProjectLoadAction { SaveAll, Close, Add, Abort }

    /// <summary>
    /// Shown when the user tries to open a project while one is already loaded.
    /// Lets the user choose how to handle the currently open maps.
    /// </summary>
    public partial class ProjectLoadConflictDialog : Form {

        public ProjectLoadAction SelectedAction { get; private set; } = ProjectLoadAction.Abort;

        public ProjectLoadConflictDialog(string projectName) {
            InitializeComponent();
            labelMessage.Text =
                $"A project is already loaded: \"{projectName}\"\n\n" +
                "Loading a new project will overwrite it. What would you like to do with the currently open maps?";

            btnAbort.Click   += (_, _) => { SelectedAction = ProjectLoadAction.Abort;   Close(); };
            btnAdd.Click     += (_, _) => { SelectedAction = ProjectLoadAction.Add;     Close(); };
            btnClose.Click   += (_, _) => { SelectedAction = ProjectLoadAction.Close;   Close(); };
            btnSaveAll.Click += (_, _) => { SelectedAction = ProjectLoadAction.SaveAll; Close(); };
        }
    }
}
