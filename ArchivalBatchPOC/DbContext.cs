using System.Data.Entity;

public class LIMSDevContext : DbContext
{
    //New Table
    public DbSet<ArchivalSchedule> ArchivalSchedules { get; set; }

    //Primary Tables
    public DbSet<AuditHistory> AuditHistorys { get; set; }
    public DbSet<BarCodeResult> BarCodeResults { get; set; }
    public DbSet<RegBarCode> RegBarCodes { get; set; }
    public DbSet<RegistrationInfo> RegistrationInfos { get; set; }
    public DbSet<AutoApprovalLog> AutoApprovalLogs { get; set; }

    public LIMSDevContext() : base("name=LIMSDevContext")
    {
        //if it is set then EF will not create DB if does not exist 
        Database.SetInitializer<LIMSDevContext>(null);
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //New Table
        modelBuilder.Entity<ArchivalSchedule>().ToTable("Archival_Schedule");

        //Primary Tables
        modelBuilder.Entity<AuditHistory>().ToTable("AuditHistory");
        modelBuilder.Entity<BarCodeResult>().ToTable("REG_BarCodeResult");
        modelBuilder.Entity<RegBarCode>().ToTable("TRF_Reg_BarCodes");
        modelBuilder.Entity<RegistrationInfo>().ToTable("TRF_RegistrationInfos");
        modelBuilder.Entity<AutoApprovalLog>().ToTable("TrnAutoApprovalLog");

    }
}
