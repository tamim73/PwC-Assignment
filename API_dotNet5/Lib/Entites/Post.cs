using System.ComponentModel.DataAnnotations;

namespace Lib.Entites
{
    public class Post: BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public bool IsTopic { get; set; }

        // the below relation allow us to have a post being a topic for a new story
        // and an added post for an existence story at the same time 

        /// <summary>
        /// If the post is a topic for a story (new story)
        /// </summary>
        public int? TopicForStoryId { get; set; }
        public Story TopicForStory { get; set; }

        /// <summary>
        /// If the post is a story post (added content to an existence story)
        /// </summary>
        public int? PostForStoryId { get; set; }
        public Story PostForStory { get; set; }

    }
}
