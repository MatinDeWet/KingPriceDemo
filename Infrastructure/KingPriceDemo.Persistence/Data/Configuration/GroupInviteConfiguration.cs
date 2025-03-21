namespace KingPriceDemo.Persistence.Data.Configuration
{
    public partial class GroupInviteConfiguration : IEntityTypeConfiguration<GroupInvite>
    {
        public void Configure(EntityTypeBuilder<GroupInvite> entity)
        {
            entity.ToTable(nameof(GroupInvite));

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(16);

            entity.HasOne(x => x.Group)
                .WithMany(x => x.Invites)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.AcceptedByUser)
                .WithMany(x => x.AcceptedInvites)
                .HasForeignKey(x => x.AcceptedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            OnConfigurePartial(entity);
        }
        partial void OnConfigurePartial(EntityTypeBuilder<GroupInvite> entity);
    }
}
