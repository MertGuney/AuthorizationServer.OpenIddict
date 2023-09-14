namespace AuthorizationServer.OpenIddict.Persistence.Configurations;

public class AspNetUserCodeConfiguration : IEntityTypeConfiguration<AspNetUserCode>
{
    public void Configure(EntityTypeBuilder<AspNetUserCode> builder)
    {
        builder.Property(x => x.Value).HasMaxLength(6).IsRequired();
        builder.Property(x => x.Type)
            .HasConversion(c => c.ToString(),
                p => (CodeTypeEnum)Enum.Parse(typeof(CodeTypeEnum), p))
            .HasMaxLength(16);

        builder.Property(x => x.CreatedBy).HasMaxLength(64);
        builder.Property(x => x.UpdatedBy).HasMaxLength(64);
    }
}
