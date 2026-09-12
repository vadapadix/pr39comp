using System.Collections.Generic;
using PostService.Models;

namespace PostService.Services;

public interface IPostingService
{
    Posting Create(Posting newPosting);
    Posting? Update(Posting posting);
    List<Posting> GetAll();
    Posting? Find(int postingId);
    int Delete(int postingId);
}
