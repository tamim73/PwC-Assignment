using Lib.Entites;
using Lib.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Stories
{
    public class SearchStoriesResponse: SearchResponse<StoriesListModel>
    {

    }

    public class StoriesListModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
