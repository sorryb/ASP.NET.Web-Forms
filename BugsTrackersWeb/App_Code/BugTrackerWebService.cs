using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BugsTrackers.BusinessLogicLayer;
using System.Xml.Serialization;


namespace BugsTrackers.Services
{
/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[Serializable()]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class BugTrackerWebService : System.Web.Services.WebService {

    public BugTrackerWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Save a issue.
    /// </summary>
    /// <param name="issueId"></param>
    /// <param name="projectId"></param>
    /// <param name="title"></param>
    /// <param name="user"></param>
    /// <param name="img"></param>
    /// <returns>issueId</returns>
    [WebMethod]
    public string SaveIssue(int issueId,int projectId,string title,string user,byte[] img) {
    
    int category = 2;  // bug
    int milestone = 4 ;//usor
    int priority= 5;   //normal
    int status = 5;    //deschis
    int assigned = 2;  // sorin 
    int owner = 2 ;    //sorin


        Issue newIssue = new Issue( issueId, 
                                    projectId, 
                                    title, 
                                    category, 
                                    milestone, 
                                    priority, 
                                    status , 
                                    assigned , 
                                    owner, 
                                    user);

        if (!newIssue.Save())
        {
            return "0";
        }
        issueId = newIssue.Id;

        SaveAttachement(issueId, user, img);

        return issueId.ToString();
            
    }

    protected void SaveAttachement(int issueId, string userName, byte[] fileBytes)
    {

        // Get file name
        string fileName = issueId.ToString();
        string contentType = "JPG";

        // Create new Issue Attachment
        IssueAttachment newAttachment = new IssueAttachment(issueId, userName, fileName, contentType, fileBytes);
        newAttachment.Save();
        
    }

    [XmlRoot("KeyValuePair")]
    public abstract class ProjectInfoBase
    {
        public string Name = string.Empty;
        public string Key = string.Empty;
    }
    public class ProjectInfo : ProjectInfoBase
    {
        public ProjectInfo()
        { }
    }

    [WebMethod]
    [XmlInclude(typeof(ProjectInfo))]
    public ProjectInfoBase[] GetProjectByMemberUsername(string userName)
    {
        ProjectCollection projectsCollection = Project.GetProjectByMemberUsername(userName);
        ProjectInfoBase[] List = new ProjectInfoBase[projectsCollection.Count];
        int i = 0;
        foreach (Project project in projectsCollection)
        {
            ProjectInfo projectInfo = new ProjectInfo();
            projectInfo.Key = project.Id.ToString();
            projectInfo.Name = project.Name;

            List[i] = projectInfo;
            i++;
        }

        return List;
    }


    
}
}
