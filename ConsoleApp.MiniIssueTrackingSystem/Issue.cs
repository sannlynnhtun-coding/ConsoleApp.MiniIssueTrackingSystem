namespace ConsoleApp.MiniIssueTrackingSystem;

public class Issue
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } // Open, In Progress, Resolved, Closed
    public string AssignedTo { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? FixedDate { get; set; } // Nullable for unfixed issues

    public Issue(int id, string title, string description, string createdBy)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = "Open";
        CreatedBy = createdBy;
        CreatedDate = DateTime.Now;
    }

    public void AssignTo(string user)
    {
        AssignedTo = user;
    }

    public void FixIssue()
    {
        Status = "Resolved";
        FixedDate = DateTime.Now;
    }
}