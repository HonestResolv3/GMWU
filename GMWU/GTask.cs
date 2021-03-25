namespace GMWU
{
    public class GTask
    {
        // Fields for each Task that is created by the user
        // Not all of them will get used, three at most will
        public ulong AddonID { get; set; }
        public string AddonInputLocation { get; set; }
        public string AddonOutputLocation { get; set; }
        public string GMadFileLocation { get; set; }
        public string GMAInputLocation { get; set; }
        public string GMAOutputLocation { get; set; }
        public string GMPublishLocation { get; set; }
        public string IconLocation { get; set; }
        public string TaskName { get; set; }
        public string TaskNotes { get; set; }

        // Creating a new Garry's Mod workshop task that is empty, values will get added and changed as you create and edit tasks
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
