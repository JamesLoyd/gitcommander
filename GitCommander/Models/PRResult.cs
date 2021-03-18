namespace GitCommander.Models
{
    public class PRResult
    {
        public string Result { get; set; }
        public int Index { get; set; }
        public string PRNumber { get; set; }
        public string Branch {get;set;}
        public string Status {get;set;}
    }

    public class Config
    {
        public string Repo {get;set;}
        public string Location {get;set;}
    }
}