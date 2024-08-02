using System.Data.Entity;

public class ArchivalContext : DbContext
{
    //Secondary Tables
    public DbSet<AuditHistoryArchival> AuditHistoryArchivals { get; set; }
    public DbSet<BarCodeResultArchival> BarCodeResultArchivals { get; set; }
    public DbSet<RegBarCodeArchival> RegBarCodeArchivals { get; set; }
    public DbSet<RegistrationInfoArchival> RegistrationInfoArchivals { get; set; }
    public DbSet<AutoApprovalLogArchival> AutoApprovalLogArchivals { get; set; }

    public ArchivalContext() : base("name=ArchivalContext")
    {
        //if it is set then EF will not create DB if does not exist 
        Database.SetInitializer<ArchivalContext>(null);
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Secondary Tables
        modelBuilder.Entity<AuditHistoryArchival>().ToTable("AuditHistory");
        modelBuilder.Entity<BarCodeResultArchival>().ToTable("REG_BarCodeResult");
        modelBuilder.Entity<RegBarCodeArchival>().ToTable("TRF_Reg_BarCode");
        modelBuilder.Entity<RegistrationInfoArchival>().ToTable("TRF_RegistrationInfo");
        modelBuilder.Entity<AutoApprovalLogArchival>().ToTable("TrnAutoApprovalLog");
    }
}
