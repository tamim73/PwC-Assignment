using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Stories
{
    public class DeleteStoryResponse: BaseResponse
    {
        public int StoryId { get; set; }
    }
}
