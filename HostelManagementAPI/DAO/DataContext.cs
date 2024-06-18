using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAO
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("server=khang.systems; database=HostelManagement; uid=sa;pwd=server123@; TrustServerCertificate=True");
        //    }
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString()).EnableSensitiveDataLogging();
            }
        }


        private static string GetConnectionString()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var strConn = config["ConnectionStrings:SqlCloud"];

            return strConn;
        }


        public DbSet<Account> Account { get; set; }
        public DbSet<BillPayment> BillPayment { get; set; }
        public DbSet<BillPaymentDetail> BillPaymentDetail { get; set; }
        public DbSet<Complain> Complain { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<ContractMember> ContractMember { get; set; }
        public DbSet<Hostel> Hostel { get; set; }
        public DbSet<MemberShip> Membership { get; set; }
        public DbSet<MemberShipRegisterTransaction> MembershipsRegisterTransaction { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomImage> RoomsImage { get; set; }
        public DbSet<HostelImage> HostelImages { get; set; }
        public DbSet<TypeService> TypeService { get; set; }
        public DbSet<RoomService> RoomService { get; set; }
        public DbSet<RoomAppointment> RoomAppointments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasKey(a => a.AccountID);

            modelBuilder.Entity<Account>()
                .Property(a => a.AccountID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<BillPayment>()
               .HasKey(billpayment => billpayment.BillPaymentID);

            modelBuilder.Entity<BillPayment>()
                .Property(billpayment => billpayment.BillPaymentID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<BillPaymentDetail>()
               .HasKey(billPaymentDetail => billPaymentDetail.BillPaymentDetailID);

            modelBuilder.Entity<BillPaymentDetail>()
                .Property(billPaymentDetail => billPaymentDetail.BillPaymentDetailID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<Complain>()
              .HasKey(complain => complain.ComplainID);

            modelBuilder.Entity<Complain>()
                .Property(complain => complain.ComplainID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<Contract>()
              .HasKey(contract => contract.ContractID);

            modelBuilder.Entity<Contract>()
                .Property(contract => contract.ContractID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();


            modelBuilder.Entity<ContractMember>()
              .HasKey(contractmember => contractmember.ContractMemberID);

            modelBuilder.Entity<ContractMember>()
                .Property(contractmember => contractmember.ContractMemberID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<Hostel>()
              .HasKey(hostel => hostel.HostelID);

            modelBuilder.Entity<Hostel>()
                .Property(hostel => hostel.HostelID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<MemberShip>()
              .HasKey(membership => membership.MemberShipID);

            modelBuilder.Entity<MemberShip>()
                .Property(membership => membership.MemberShipID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<MemberShipRegisterTransaction>()
              .HasKey(memberShipRegisterTransaction => memberShipRegisterTransaction.MemberShipTransactionID);

            modelBuilder.Entity<MemberShipRegisterTransaction>()
                .Property(memberShipRegisterTransaction => memberShipRegisterTransaction.MemberShipTransactionID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<Notification>()
             .HasKey(notice => notice.NotificationID);

            modelBuilder.Entity<Notification>()
                .Property(notice => notice.NotificationID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<Room>()
             .HasKey(room => room.RoomID);

            modelBuilder.Entity<Room>()
                .Property(room => room.RoomID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<RoomImage>()
             .HasKey(roomImg => roomImg.RoomImgID);

            modelBuilder.Entity<RoomImage>()
                .Property(roomImg => roomImg.RoomImgID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<HostelImage>()
             .HasKey(HostelImage => HostelImage.HostelImageID);

            modelBuilder.Entity<HostelImage>()
                .Property(HostelImage => HostelImage.HostelImageID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<RoomService>()
                .HasKey(rs => rs.RoomServiceId);

            modelBuilder.Entity<RoomService>()
                .Property(rs => rs.RoomServiceId)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<TypeService>()
             .HasKey(typeservice => typeservice.TypeServiceID);

            modelBuilder.Entity<TypeService>()
                .Property(typeservice => typeservice.TypeServiceID)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            modelBuilder.Entity<RoomAppointment>()
             .HasKey(RoomAppointment => RoomAppointment.ViewRoomAppointmentId);

            modelBuilder.Entity<RoomAppointment>()
                .Property(RoomAppointment => RoomAppointment.ViewRoomAppointmentId)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();


            //one account have many hostel
            modelBuilder.Entity<Hostel>()
                .HasOne(hostel => hostel.OwnerAccount)
                .WithMany(a => a.Hostels)
                .HasForeignKey(hostel => hostel.AccountID)
                .OnDelete(DeleteBehavior.Restrict);

            // one account have many membershipregister
            modelBuilder.Entity<MemberShipRegisterTransaction>()
                .HasOne(membership => membership.OwnerAccount)
                .WithMany(account => account.Memberships)
                .HasForeignKey(membership => membership.AccountID)
                .OnDelete(DeleteBehavior.Restrict);

            // one account have many notice sent
            modelBuilder.Entity<Notification>()
               .HasOne(notice => notice.AccountNotice)
               .WithMany(account => account.AccountNotice)
               .HasForeignKey(notice => notice.AccountNoticeId)
               .OnDelete(DeleteBehavior.Restrict);

            // one account have many notice receive
            modelBuilder.Entity<Notification>()
               .HasOne(notice => notice.ReceiveAccount)
               .WithMany(account => account.AccountNoticeReceive)
               .HasForeignKey(notice => notice.ReceiveAccountId)
               .OnDelete(DeleteBehavior.Restrict);

            // one account have many contract owner
            modelBuilder.Entity<Contract>()
               .HasOne(contract => contract.OwnerAccount)
               .WithMany(account => account.OwnerContract)
               .HasForeignKey(contract => contract.OwnerAccountID)
               .OnDelete(DeleteBehavior.Restrict);

            // one account have many contract student
            modelBuilder.Entity<Contract>()
               .HasOne(contract => contract.StudentLeadAccount)
               .WithMany(account => account.StudentContract)
               .HasForeignKey(contract => contract.StudentAccountID)
               .OnDelete(DeleteBehavior.Restrict);

            //one account have many complain
            modelBuilder.Entity<Complain>()
                .HasOne(complain => complain.ComplainAccount)
                .WithMany(account => account.AccountComplain)
                .HasForeignKey(complain => complain.AccountID)
                .OnDelete(DeleteBehavior.Restrict);

            //one bill have many bill detail
            modelBuilder.Entity<BillPaymentDetail>()
               .HasOne(billdetail => billdetail.BillPayment)
               .WithMany(bill => bill.Details)
               .HasForeignKey(billdetail => billdetail.BillPaymentID)
               .OnDelete(DeleteBehavior.Restrict);

            //one contract have many member contract
            modelBuilder.Entity<ContractMember>()
               .HasOne(contractmember => contractmember.Contract)
               .WithMany(contract => contract.Members)
               .HasForeignKey(contractmember => contractmember.ContractID)
               .OnDelete(DeleteBehavior.Restrict);


            // one hostel have many room
            modelBuilder.Entity<Room>()
                .HasOne(room => room.Hostel)
                .WithMany(hostel => hostel.Rooms)
                .HasForeignKey(room => room.HostelID)
                .OnDelete(DeleteBehavior.Restrict);

            // one membership have many membershipregister
            modelBuilder.Entity<MemberShipRegisterTransaction>()
                .HasOne(membershipsRegisterTransaction => membershipsRegisterTransaction.MemberShip)
                .WithMany(member => member.MemberShipRegisterTransactions)
                .HasForeignKey(membershipsRegisterTransaction => membershipsRegisterTransaction.MemberShipID)
                .OnDelete(DeleteBehavior.Restrict);

            //one room have many complain
            modelBuilder.Entity<Complain>()
                .HasOne(complain => complain.Room)
                .WithMany(room => room.Complains)
                .HasForeignKey(complain => complain.RoomID)
                .OnDelete(DeleteBehavior.Restrict);

            //one room have many img
            modelBuilder.Entity<RoomImage>()
                .HasOne(roomImg => roomImg.Room)
                .WithMany(room => room.RoomImages)
                .HasForeignKey(roomImg => roomImg.RoomID)
                .OnDelete(DeleteBehavior.Restrict);

            //one room have many img
            modelBuilder.Entity<HostelImage>()
                .HasOne(img => img.Hostel)
                .WithMany(Hostel => Hostel.Images)
                .HasForeignKey(img => img.HostelID)
                .OnDelete(DeleteBehavior.Restrict);

            //one room have many contract
            modelBuilder.Entity<Contract>()
                .HasOne(contract => contract.Room)
                .WithMany(room => room.RoomContract)
                .HasForeignKey(roomImg => roomImg.RoomID)
                .OnDelete(DeleteBehavior.Restrict);

            //one service have many bill detail
            modelBuilder.Entity<BillPaymentDetail>()
                .HasOne(billdetail => billdetail.RoomService)
                .WithMany(service => service.BillPaymentDetails)
                .HasForeignKey(billdetail => billdetail.RoomServiceID)
                .OnDelete(DeleteBehavior.Restrict);

            //one type service have many service
            modelBuilder.Entity<RoomService>()
                .HasOne(service => service.TypeService)
                .WithMany(typeservice => typeservice.RoomServices)
                .HasForeignKey(service => service.TypeServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomService>()
                .HasOne(rs => rs.Room)
                .WithMany(r => r.RoomServices)
                .HasForeignKey(rs => rs.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomAppointment>()
                .HasOne(RoomAppointment => RoomAppointment.Room)
                .WithMany(Room => Room.RoomAppointments)
                .HasForeignKey(RoomAppointment => RoomAppointment.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomAppointment>()
                 .HasOne(RoomAppointment => RoomAppointment.Viewer)
                 .WithMany(a => a.Appointments)
                 .HasForeignKey(RoomAppointment => RoomAppointment.ViewerId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BillPayment>()
                .HasOne(BillPayment => BillPayment.AccountPay)
                .WithMany(a => a.BillPaymentPays)
                .HasForeignKey(BillPayment => BillPayment.AccountPayId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BillPayment>()
                .HasOne(BillPayment => BillPayment.AccountReceive)
                .WithMany(a => a.BillPaymentReceives)
                .HasForeignKey(BillPayment => BillPayment.AccountReceiveId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
