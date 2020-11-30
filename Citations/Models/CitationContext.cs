using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Citations.Models
{
    public partial class CitationContext : DbContext
    {
        public CitationContext()
        {
        }

        public CitationContext(DbContextOptions<CitationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleAuthore> ArticleAuthores { get; set; }
        public virtual DbSet<ArticleIssue> ArticleIssues { get; set; }
        public virtual DbSet<ArticlesKeyword> ArticlesKeywords { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthorResearchField> AuthorResearchFields { get; set; }
        public virtual DbSet<AuthorsPositionInstitution> AuthorsPositionInstitutions { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<KeyWord> KeyWords { get; set; }
        public virtual DbSet<Magazine> Magazines { get; set; }
        public virtual DbSet<MagazineIssue> MagazineIssues { get; set; }
        public virtual DbSet<MagazineResearchField> MagazineResearchFields { get; set; }
        public virtual DbSet<PositionJob> PositionJobs { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<ResearchField> ResearchFields { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=citations;Username=citation_user;Password=citation_user");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("articles", "core");

                entity.Property(e => e.Articleid).HasColumnName("articleid");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Articletittle)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("articletittle");

                entity.Property(e => e.NumberOfCitations).HasColumnName("number_of_citations");

                entity.Property(e => e.NumberOfReferences).HasColumnName("number_of_references");

                entity.Property(e => e.ScannedArticlePdf)
                    .HasMaxLength(512)
                    .HasColumnName("scanned_article_pdf");

                entity.Property(e => e.ScannedBriefQuote)
                    .HasMaxLength(512)
                    .HasColumnName("scanned_brief_quote");
            });

            modelBuilder.Entity<ArticleAuthore>(entity =>
            {
                entity.HasKey(e => new { e.Authorid, e.Articleid })
                    .HasName("article_authores_pkey");

                entity.ToTable("article_authores", "core");

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.Articleid).HasColumnName("articleid");

                entity.Property(e => e.MainAuthor).HasColumnName("main_author");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleAuthores)
                    .HasForeignKey(d => d.Articleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("article_authores_articleid_fkey");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.ArticleAuthores)
                    .HasForeignKey(d => d.Authorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("article_authores_authorid_fkey");
            });

            modelBuilder.Entity<ArticleIssue>(entity =>
            {
                entity.HasKey(e => new { e.Articleid, e.MagazineIssueId })
                    .HasName("article_issue_pkey");

                entity.ToTable("article_issue", "core");

                entity.Property(e => e.Articleid).HasColumnName("articleid");

                entity.Property(e => e.MagazineIssueId).HasColumnName("magazine_issue_id");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleIssues)
                    .HasForeignKey(d => d.Articleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("article_issue_articleid_fkey");

                entity.HasOne(d => d.MagazineIssue)
                    .WithMany(p => p.ArticleIssues)
                    .HasForeignKey(d => d.MagazineIssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("article_issue_magazine_issue_id_fkey");
            });

            modelBuilder.Entity<ArticlesKeyword>(entity =>
            {
                entity.HasKey(e => new { e.Articleid, e.KeyWordid })
                    .HasName("articles_keywords_pkey");

                entity.ToTable("articles_keywords", "core");

                entity.Property(e => e.Articleid).HasColumnName("articleid");

                entity.Property(e => e.KeyWordid).HasColumnName("key_wordid");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticlesKeywords)
                    .HasForeignKey(d => d.Articleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("articles_keywords_articleid_fkey");

                entity.HasOne(d => d.KeyWord)
                    .WithMany(p => p.ArticlesKeywords)
                    .HasForeignKey(d => d.KeyWordid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("articles_keywords_key_wordid_fkey");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("authors", "core");

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.AuthorBio)
                    .HasMaxLength(512)
                    .HasColumnName("author_bio");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.PointerH).HasColumnName("pointer_h");

                entity.Property(e => e.ScannedAuthorimage)
                    .HasMaxLength(512)
                    .HasColumnName("scanned_authorimage");
            });

            modelBuilder.Entity<AuthorResearchField>(entity =>
            {
                entity.HasKey(e => new { e.Authorid, e.Fieldid })
                    .HasName("author_research_fields_pkey");

                entity.ToTable("author_research_fields", "core");

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.Fieldid).HasColumnName("fieldid");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorResearchFields)
                    .HasForeignKey(d => d.Authorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("author_research_fields_authorid_fkey");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.AuthorResearchFields)
                    .HasForeignKey(d => d.Fieldid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("author_research_fields_fieldid_fkey");
            });

            modelBuilder.Entity<AuthorsPositionInstitution>(entity =>
            {
                entity.ToTable("authors_position_institution", "core");

                entity.Property(e => e.AuthorsPositionInstitutionId)
                    .HasColumnName("authors_position_institution_id")
                    .HasDefaultValueSql("nextval('core.authors_position_institution_authors_position_institution_i_seq'::regclass)");

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.Institutionid).HasColumnName("institutionid");

                entity.Property(e => e.MainIntitute).HasColumnName("main_intitute");

                entity.Property(e => e.PositionJobid).HasColumnName("position_jobid");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.AuthorsPositionInstitutions)
                    .HasForeignKey(d => d.Authorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authors_position_institution_authorid_fkey");

                entity.HasOne(d => d.Institution)
                    .WithMany(p => p.AuthorsPositionInstitutions)
                    .HasForeignKey(d => d.Institutionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authors_position_institution_institutionid_fkey");

                entity.HasOne(d => d.PositionJob)
                    .WithMany(p => p.AuthorsPositionInstitutions)
                    .HasForeignKey(d => d.PositionJobid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authors_position_institution_position_jobid_fkey");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country", "core");

                entity.HasIndex(e => e.Name, "country_name_key")
                    .IsUnique();

                entity.Property(e => e.Countryid).HasColumnName("countryid");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Institution>(entity =>
            {
                entity.ToTable("institution", "core");

                entity.HasIndex(e => e.Name, "institution_name_key")
                    .IsUnique();

                entity.Property(e => e.Institutionid).HasColumnName("institutionid");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Country).HasColumnName("country");

                entity.Property(e => e.ImpactFactor).HasColumnName("impact_factor");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.NumberOfCitations).HasColumnName("number_of_citations");

                entity.Property(e => e.PointerH).HasColumnName("pointer_h");

                entity.Property(e => e.ScannedCoverImage)
                    .HasMaxLength(512)
                    .HasColumnName("scanned_cover_image");

                entity.Property(e => e.ScannedLogoImage)
                    .HasMaxLength(512)
                    .HasColumnName("scanned_logo_image");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Institutions)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("institution_country_fkey");
            });

            modelBuilder.Entity<KeyWord>(entity =>
            {
                entity.ToTable("key_words", "core");

                entity.Property(e => e.KeyWordid).HasColumnName("key_wordid");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.KeyWord1)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("key_word");
            });

            modelBuilder.Entity<Magazine>(entity =>
            {
                entity.ToTable("magazines", "core");

                entity.HasIndex(e => e.Isbn, "magazines_isbn_key")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "magazines_name_key")
                    .IsUnique();

                entity.Property(e => e.Magazineid).HasColumnName("magazineid");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.AppropriateValue).HasColumnName("appropriate_value");

                entity.Property(e => e.ImmediateCoefficient).HasColumnName("immediate_coefficient");

                entity.Property(e => e.ImpactFactor).HasColumnName("impact_factor");

                entity.Property(e => e.Institutionid).HasColumnName("institutionid");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("isbn");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.NumberOfCitations).HasColumnName("number_of_citations");

                entity.HasOne(d => d.Institution)
                    .WithMany(p => p.Magazines)
                    .HasForeignKey(d => d.Institutionid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("magazines_institutionid_fkey");
            });

            modelBuilder.Entity<MagazineIssue>(entity =>
            {
                entity.HasKey(e => e.Issueid)
                    .HasName("magazine_issue_pkey");

                entity.ToTable("magazine_issue", "core");

                entity.Property(e => e.Issueid).HasColumnName("issueid");

                entity.Property(e => e.DateOfPublication)
                    .HasColumnType("date")
                    .HasColumnName("date_of_publication");

                entity.Property(e => e.Issuenumber).HasColumnName("issuenumber");

                entity.Property(e => e.Magazineid).HasColumnName("magazineid");

                entity.Property(e => e.Publisherid).HasColumnName("publisherid");

                entity.HasOne(d => d.Magazine)
                    .WithMany(p => p.MagazineIssues)
                    .HasForeignKey(d => d.Magazineid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("magazine_issue_magazineid_fkey");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.MagazineIssues)
                    .HasForeignKey(d => d.Publisherid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("magazine_issue_publisherid_fkey");
            });

            modelBuilder.Entity<MagazineResearchField>(entity =>
            {
                entity.HasKey(e => new { e.Magazineid, e.Fieldid })
                    .HasName("magazine_research_fields_pkey");

                entity.ToTable("magazine_research_fields", "core");

                entity.Property(e => e.Magazineid).HasColumnName("magazineid");

                entity.Property(e => e.Fieldid).HasColumnName("fieldid");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.MagazineResearchFields)
                    .HasForeignKey(d => d.Fieldid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("magazine_research_fields_fieldid_fkey");

                entity.HasOne(d => d.Magazine)
                    .WithMany(p => p.MagazineResearchFields)
                    .HasForeignKey(d => d.Magazineid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("magazine_research_fields_magazineid_fkey");
            });

            modelBuilder.Entity<PositionJob>(entity =>
            {
                entity.ToTable("position_job", "core");

                entity.Property(e => e.PositionJobid).HasColumnName("position_jobid");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.PositionJob1)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("position_job");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("publishers", "core");

                entity.Property(e => e.Publisherid).HasColumnName("publisherid");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("address");

                entity.Property(e => e.Country).HasColumnName("country");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Publishers)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("publishers_country_fkey");
            });

            modelBuilder.Entity<ResearchField>(entity =>
            {
                entity.HasKey(e => e.Fieldid)
                    .HasName("research_fields_pkey");

                entity.ToTable("research_fields", "core");

                entity.Property(e => e.Fieldid).HasColumnName("fieldid");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
