using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PoETrademasterAPI.Database;

public partial class PoEtrademasterContext : DbContext
{
    public PoEtrademasterContext()
    {
    }

    public PoEtrademasterContext(DbContextOptions<PoEtrademasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Affix> Affixes { get; set; }

    public virtual DbSet<AffixGroup> AffixGroups { get; set; }

    public virtual DbSet<AffixOrigin> AffixOrigins { get; set; }

    public virtual DbSet<AffixRAffixGroup> AffixRAffixGroups { get; set; }

    public virtual DbSet<AffixRTag> AffixRTags { get; set; }

    public virtual DbSet<Base> Bases { get; set; }

    public virtual DbSet<BaseAffix> BaseAffixes { get; set; }

    public virtual DbSet<BaseAffixTier> BaseAffixTiers { get; set; }

    public virtual DbSet<BaseGroup> BaseGroups { get; set; }

    public virtual DbSet<ConfigFlag> ConfigFlags { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemProperty> ItemProperties { get; set; }

    public virtual DbSet<ItemPropertyType> ItemPropertyTypes { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=desktop-t6oildn;Database=PoETrademaster;Trusted_Connection=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Affix>(entity =>
        {
            entity.HasKey(e => e.AffixId).HasName("PK__Affix__F7B2DBE725AAB24E");

            entity.ToTable("Affix");

            entity.Property(e => e.AffixName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ElevatedAffix)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.AffixOrigin).WithMany(p => p.Affixes)
                .HasForeignKey(d => d.AffixOriginId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Affix_AffixOrigin");
        });

        modelBuilder.Entity<AffixGroup>(entity =>
        {
            entity.ToTable("AffixGroup");

            entity.Property(e => e.AffixGroupDisplayId)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.AffixGroupName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AffixOrigin>(entity =>
        {
            entity.HasKey(e => e.AffixOriginId).HasName("PK__AffixOri__D15C2C76546F9853");

            entity.ToTable("AffixOrigin");

            entity.Property(e => e.AffixOriginName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AffixRAffixGroup>(entity =>
        {
            entity.ToTable("Affix_r_AffixGroup");

            entity.Property(e => e.AffixRAffixGroupId).HasColumnName("Affix_r_AffixGroupId");

            entity.HasOne(d => d.AffixGroup).WithMany(p => p.AffixRAffixGroups)
                .HasForeignKey(d => d.AffixGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Affix_r_AffixGroup_AffixGroup");

            entity.HasOne(d => d.Affix).WithMany(p => p.AffixRAffixGroups)
                .HasForeignKey(d => d.AffixId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Affix_r_AffixGroup_Affix");
        });

        modelBuilder.Entity<AffixRTag>(entity =>
        {
            entity.HasKey(e => e.AffixRTagId).HasName("PK__Affix_r___A5F37BEBAE5C6432");

            entity.ToTable("Affix_r_Tag");

            entity.Property(e => e.AffixRTagId).HasColumnName("Affix_r_TagId");

            entity.HasOne(d => d.Affix).WithMany(p => p.AffixRTags)
                .HasForeignKey(d => d.AffixId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Affix_r_Tag_Affix");

            entity.HasOne(d => d.Tag).WithMany(p => p.AffixRTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Affix_r_Tag_Tag");
        });

        modelBuilder.Entity<Base>(entity =>
        {
            entity.HasKey(e => e.BaseId).HasName("PK__Base__E7970876B1E02E20");

            entity.ToTable("Base");

            entity.Property(e => e.BaseName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.BaseGroup).WithMany(p => p.Bases)
                .HasForeignKey(d => d.BaseGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Base_BaseGroup");

            entity.HasOne(d => d.ParentBase).WithMany(p => p.InverseParentBase)
                .HasForeignKey(d => d.ParentBaseId)
                .HasConstraintName("FK_Base_Base");
        });

        modelBuilder.Entity<BaseAffix>(entity =>
        {
            entity.HasKey(e => e.BaseAffixId).HasName("PK__BaseAffi__A58C6D734583B428");

            entity.ToTable("BaseAffix");

            entity.HasOne(d => d.Affix).WithMany(p => p.BaseAffixes)
                .HasForeignKey(d => d.AffixId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BaseAffix_Affix");

            entity.HasOne(d => d.Base).WithMany(p => p.BaseAffixes)
                .HasForeignKey(d => d.BaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BaseAffix_Base");
        });

        modelBuilder.Entity<BaseAffixTier>(entity =>
        {
            entity.ToTable("BaseAffixTier");

            entity.Property(e => e.IlvlRequirement).HasColumnName("ILvlRequirement");
            entity.Property(e => e.Stat1EndValue).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Stat1StartValue).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Stat2EndValue).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Stat2StartValue).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Stat3EndValue).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Stat3StartValue).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Stat4EndValue).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Stat4StartValue).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.BaseAffix).WithMany(p => p.BaseAffixTiers)
                .HasForeignKey(d => d.BaseAffixId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BaseAffixTier_BaseAffix");
        });

        modelBuilder.Entity<BaseGroup>(entity =>
        {
            entity.HasKey(e => e.BaseGroupId).HasName("PK__BaseGrou__17A4250FC47B31AC");

            entity.ToTable("BaseGroup");

            entity.Property(e => e.BaseGroupName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ConfigFlag>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Flag)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__727E838BA2ECEE14");

            entity.ToTable("Item");

            entity.Property(e => e.ImgLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Base).WithMany(p => p.Items)
                .HasForeignKey(d => d.BaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Base");
        });

        modelBuilder.Entity<ItemProperty>(entity =>
        {
            entity.HasKey(e => e.ItemPropertyId).HasName("PK__ItemProp__2E4BFEC27A4933F1");

            entity.ToTable("ItemProperty");

            entity.Property(e => e.Property)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.Item).WithMany(p => p.ItemProperties)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemProperty_Item");

            entity.HasOne(d => d.ItemPropertyType).WithMany(p => p.ItemProperties)
                .HasForeignKey(d => d.ItemPropertyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemProperty_ItemPropertyType");
        });

        modelBuilder.Entity<ItemPropertyType>(entity =>
        {
            entity.HasKey(e => e.ItemPropertyTypeId).HasName("PK__ItemProp__17DD2034F8EBB0EC");

            entity.ToTable("ItemPropertyType");

            entity.Property(e => e.ItemPropertyTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");

            entity.Property(e => e.TagName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
