namespace KingPriceDemo.Persistence.Data.Configuration
{
    public partial class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable(nameof(User));

            entity.HasKey(x => x.Id);

            entity.Property(x => x.FullName)
                .HasMaxLength(128);

            entity.Property(x => x.Surname)
                .HasMaxLength(64);

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(x => x.CellphoneNumber)
                .HasMaxLength(16);

            OnConfigurePartial(entity);
        }
        partial void OnConfigurePartial(EntityTypeBuilder<User> entity);
    }
}
