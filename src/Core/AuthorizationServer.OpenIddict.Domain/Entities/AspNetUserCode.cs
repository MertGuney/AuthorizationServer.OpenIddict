﻿namespace AuthorizationServer.OpenIddict.Domain.Entities;

public class AspNetUserCode : BaseAuditableEntity
{
    public string Value { get; set; }
    public CodeTypeEnum Type { get; set; }
    public DateTime ExpireTime { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public AspNetUserCode() { }

    public AspNetUserCode(string value, Guid userId, CodeTypeEnum type, int second = 200)
    {
        Type = type;
        Value = value;
        UserId = userId;
        ExpireTime = DateTime.UtcNow.AddSeconds(second);
    }
}
