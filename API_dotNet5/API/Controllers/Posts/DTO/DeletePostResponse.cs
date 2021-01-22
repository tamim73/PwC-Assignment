using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Posts
{
    public class DeletePostResponse: BaseResponse
    {
        public int PostId { get; set; }
    }
}
