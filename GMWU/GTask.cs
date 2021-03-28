namespace GMWU
{
    public class GTask
    {
        public string Arguments { get; set; }
        public string FileName { get; set; }
        public string FolderLocation { get; set; }
        public string TaskName { get; set; }
        public string TaskNotes { get; set; }

        // Creating a new Garry's Mod workshop task that is empty
        public GTask()
        {

        }

        // Returns the information about the task for the queue (either with or without notes if any were entered)
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(TaskNotes) ? TaskName : TaskName + " - " + TaskNotes;
        }
    }
}
