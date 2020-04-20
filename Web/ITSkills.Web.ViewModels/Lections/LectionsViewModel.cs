namespace ITSkills.Web.ViewModels.Lections
{
    using System.Text.RegularExpressions;

    using ITSkills.Data.Models;
    using ITSkills.Services.Mapping;

    public class LectionsViewModel : IMapFrom<Lection>
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public string IFrameSource
        {
            get
            {
                if (this.Url.Contains("youtube"))
                {
                    var regex = new Regex(@"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)(?<id>[a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);
                    var videoId = regex.Match(this.Url).Groups["id"];
                    return $"https://www.youtube.com/embed/{videoId}";
                }
                else
                {
                    return this.Url;
                }
            }
        }
    }
}
