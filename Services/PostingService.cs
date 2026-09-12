using System;
using System.Collections.Generic;
using System.Linq;
using PostService.Models;

namespace PostService.Services;

public class PostingService : IPostingService
{
    private static readonly List<Posting> _postings = new()
    {
        new Posting
        {
            Id = 1,
            From = "Alice",
            To = "Bob",
            Content = "Books",
            DeliveryType = DeliveryType.Courier,
            Weight = 2.5f,
            Width = 30,
            Height = 20,
            Depth = 10,
            Value = 50.0f,
            Price = 117.5f,
            CreatedAt = DateTime.UtcNow
        }
    };

    public Posting Create(Posting newPosting)
    {
        var maxId = 1;
        if (_postings.Count > 0)
        {
            maxId = _postings.Max(p => p.Id) + 1;
        }
        newPosting.Id = maxId;

        newPosting.CreatedAt = DateTime.UtcNow;

        float baseRate = newPosting.DeliveryType switch
        {
            DeliveryType.Department => 40f,
            DeliveryType.Courier => 80f,
            DeliveryType.ExpressCourier => 120f,
            _ => 40f
        };

        float perKgRate = newPosting.DeliveryType switch
        {
            DeliveryType.Department => 10f,
            DeliveryType.Courier => 15f,
            DeliveryType.ExpressCourier => 24f,
            _ => 10f
        };

        newPosting.Price = baseRate + (newPosting.Weight * perKgRate);

        _postings.Add(newPosting);
        return newPosting;
    }

    public List<Posting> GetAll()
    {
        return _postings;
    }

    public Posting? Find(int postingId)
    {
        return _postings.FirstOrDefault(p => p.Id == postingId);
    }

    public Posting? Update(Posting posting)
    {
        var existing = _postings.FirstOrDefault(p => p.Id == posting.Id);
        if (existing is null)
        {
            return null;
        }

        existing.From = posting.From;
        existing.To = posting.To;
        existing.Content = posting.Content;
        existing.DeliveryType = posting.DeliveryType;
        existing.Weight = posting.Weight;
        existing.Width = posting.Width;
        existing.Height = posting.Height;
        existing.Depth = posting.Depth;
        existing.Value = posting.Value;
        existing.Price = posting.Price;

        return existing;
    }

    public int Delete(int postingId)
    {
        var posting = _postings.FirstOrDefault(p => p.Id == postingId);
        if (posting is null)
        {
            return 0;
        }

        _postings.Remove(posting);
        return 1;
    }
}
