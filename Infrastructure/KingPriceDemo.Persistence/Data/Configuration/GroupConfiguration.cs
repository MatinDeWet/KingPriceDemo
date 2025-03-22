namespace KingPriceDemo.Persistence.Data.Configuration
{
    public partial class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> entity)
        {
            entity.ToTable(nameof(Group));

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);

            entity.Property(x => x.InviteToken)
                .IsRequired()
                .HasMaxLength(20);

            OnConfigurePartial(entity);
        }
        partial void OnConfigurePartial(EntityTypeBuilder<Group> entity);
    }
}
