using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace signalr.Models
{
    public partial class dotnetinternalContext : DbContext
    {
        public dotnetinternalContext()
        {
        }

        public dotnetinternalContext(DbContextOptions<dotnetinternalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConversationInfo> ConversationInfos { get; set; } = null!;
        public virtual DbSet<MessageInfo> MessageInfos { get; set; } = null!;
        public virtual DbSet<UserInfo> UserInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=PC0420\\MSSQL2019;Database=dotnetinternal;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConversationInfo>(entity =>
            {
                entity.HasKey(e => e.Convid)
                    .HasName("PK__Conversa__0F9C3C5DEE26EA7A");

                entity.ToTable("ConversationInfo");

                entity.Property(e => e.Convid).HasColumnName("convid");

                entity.Property(e => e.Userfrst).HasColumnName("userfrst");

                entity.Property(e => e.Usersecond).HasColumnName("usersecond");

                entity.HasOne(d => d.UserfrstNavigation)
                    .WithMany(p => p.ConversationInfoUserfrstNavigations)
                    .HasForeignKey(d => d.Userfrst)
                    .HasConstraintName("FK__Conversat__userf__286302EC");

                entity.HasOne(d => d.UsersecondNavigation)
                    .WithMany(p => p.ConversationInfoUsersecondNavigations)
                    .HasForeignKey(d => d.Usersecond)
                    .HasConstraintName("FK__Conversat__users__29572725");
            });

            modelBuilder.Entity<MessageInfo>(entity =>
            {
                entity.HasKey(e => e.Messageid)
                    .HasName("PK__MessageI__4807CDDB177D1A16");

                entity.ToTable("MessageInfo");

                entity.Property(e => e.Messageid).HasColumnName("messageid");

                entity.Property(e => e.Conid).HasColumnName("conid");

                entity.Property(e => e.Messagecontent)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("messagecontent");

                entity.Property(e => e.Texter).HasColumnName("texter");

                entity.HasOne(d => d.Con)
                    .WithMany(p => p.MessageInfos)
                    .HasForeignKey(d => d.Conid)
                    .HasConstraintName("FK__MessageIn__conid__2C3393D0");

                entity.HasOne(d => d.TexterNavigation)
                    .WithMany(p => p.MessageInfos)
                    .HasForeignKey(d => d.Texter)
                    .HasConstraintName("FK__MessageIn__texte__2D27B809");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserInfo__1788CC4C81D6F7AF");

                entity.ToTable("UserInfo");

                entity.Property(e => e.Password)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
