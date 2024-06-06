﻿// <auto-generated />
using System;
using DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAO.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObject.Models.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountID"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CitizenCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<bool?>("IsLoginWithGmail")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtpToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PackageStatus")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("AccountID");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BusinessObject.Models.BillPayment", b =>
                {
                    b.Property<int>("BillPaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BillPaymentID"));

                    b.Property<double?>("BillAmount")
                        .HasColumnType("float");

                    b.Property<int?>("BillPaymentStatus")
                        .HasColumnType("int");

                    b.Property<int?>("BillType")
                        .HasColumnType("int");

                    b.Property<int>("ContractID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Month")
                        .HasColumnType("int");

                    b.Property<double?>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("BillPaymentID");

                    b.HasIndex("ContractID");

                    b.ToTable("BillPayment");
                });

            modelBuilder.Entity("BusinessObject.Models.BillPaymentDetail", b =>
                {
                    b.Property<int>("BillPaymentDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BillPaymentDetailID"));

                    b.Property<int?>("BillPaymentID")
                        .HasColumnType("int");

                    b.Property<double?>("NewNumberService")
                        .HasColumnType("float");

                    b.Property<double?>("OldNumberService")
                        .HasColumnType("float");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("RoomServiceID")
                        .HasColumnType("int");

                    b.Property<double?>("ServiceTotalAmount")
                        .HasColumnType("float");

                    b.HasKey("BillPaymentDetailID");

                    b.HasIndex("BillPaymentID");

                    b.HasIndex("RoomServiceID");

                    b.ToTable("BillPaymentDetail");
                });

            modelBuilder.Entity("BusinessObject.Models.Complain", b =>
                {
                    b.Property<int>("ComplainID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComplainID"));

                    b.Property<int?>("AccountID")
                        .HasColumnType("int");

                    b.Property<string>("ComplainText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateComplain")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RoomID")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("ComplainID");

                    b.HasIndex("AccountID");

                    b.HasIndex("RoomID");

                    b.ToTable("Complain");
                });

            modelBuilder.Entity("BusinessObject.Models.Contract", b =>
                {
                    b.Property<int>("ContractID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContractID"));

                    b.Property<string>("ContractTerm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateSign")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<double?>("DepositFee")
                        .HasColumnType("float");

                    b.Property<int?>("OwnerAccountID")
                        .HasColumnType("int");

                    b.Property<double?>("RoomFee")
                        .HasColumnType("float");

                    b.Property<int?>("RoomID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("StudentAccountID")
                        .HasColumnType("int");

                    b.HasKey("ContractID");

                    b.HasIndex("OwnerAccountID");

                    b.HasIndex("RoomID");

                    b.HasIndex("StudentAccountID");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("BusinessObject.Models.ContractMember", b =>
                {
                    b.Property<int>("ContractMemberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContractMemberID"));

                    b.Property<string>("CitizenCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ContractID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContractMemberID");

                    b.HasIndex("ContractID");

                    b.ToTable("ContractMember");
                });

            modelBuilder.Entity("BusinessObject.Models.Hostel", b =>
                {
                    b.Property<int>("HostelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HostelID"));

                    b.Property<int?>("AccountID")
                        .HasColumnType("int");

                    b.Property<string>("HostelAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HostelDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HostelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("HostelID");

                    b.HasIndex("AccountID");

                    b.ToTable("Hostel");
                });

            modelBuilder.Entity("BusinessObject.Models.HostelImage", b =>
                {
                    b.Property<int>("HostelImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HostelImageID"));

                    b.Property<int>("HostelID")
                        .HasColumnType("int");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HostelImageID");

                    b.HasIndex("HostelID");

                    b.ToTable("HostelImages");
                });

            modelBuilder.Entity("BusinessObject.Models.MemberShip", b =>
                {
                    b.Property<int>("MemberShipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberShipID"));

                    b.Property<int?>("CapacityHostel")
                        .HasColumnType("int");

                    b.Property<double?>("MemberShipFee")
                        .HasColumnType("float");

                    b.Property<string>("MemberShipName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Month")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("MemberShipID");

                    b.ToTable("Membership");
                });

            modelBuilder.Entity("BusinessObject.Models.MemberShipRegisterTransaction", b =>
                {
                    b.Property<int>("MemberShipTransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberShipTransactionID"));

                    b.Property<int?>("AccountID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateExpire")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateRegister")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MemberShipID")
                        .HasColumnType("int");

                    b.Property<double>("PackageFee")
                        .HasColumnType("float");

                    b.HasKey("MemberShipTransactionID");

                    b.HasIndex("AccountID");

                    b.HasIndex("MemberShipID");

                    b.ToTable("MembershipsRegisterTransaction");
                });

            modelBuilder.Entity("BusinessObject.Models.Notice", b =>
                {
                    b.Property<int>("NoticeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoticeID"));

                    b.Property<int?>("AccountID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateNotice")
                        .HasColumnType("datetime2");

                    b.Property<int>("NoticeAccountAccountID")
                        .HasColumnType("int");

                    b.Property<string>("NoticeText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NoticeID");

                    b.HasIndex("AccountID");

                    b.HasIndex("NoticeAccountAccountID");

                    b.ToTable("Notice");
                });

            modelBuilder.Entity("BusinessObject.Models.Room", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomID"));

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HostelID")
                        .HasColumnType("int");

                    b.Property<double?>("Lenght")
                        .HasColumnType("float");

                    b.Property<double?>("RoomFee")
                        .HasColumnType("float");

                    b.Property<string>("RoomName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double?>("Width")
                        .HasColumnType("float");

                    b.HasKey("RoomID");

                    b.HasIndex("HostelID");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("BusinessObject.Models.RoomAppointment", b =>
                {
                    b.Property<int>("ViewRoomAppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ViewRoomAppointmentId"));

                    b.Property<DateTime>("AppointmentTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("ViewerId")
                        .HasColumnType("int");

                    b.HasKey("ViewRoomAppointmentId");

                    b.HasIndex("RoomId");

                    b.HasIndex("ViewerId");

                    b.ToTable("RoomAppointments");
                });

            modelBuilder.Entity("BusinessObject.Models.RoomImage", b =>
                {
                    b.Property<int>("RoomImgID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomImgID"));

                    b.Property<int?>("RoomID")
                        .HasColumnType("int");

                    b.Property<string>("RoomUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomImgID");

                    b.HasIndex("RoomID");

                    b.ToTable("RoomsImage");
                });

            modelBuilder.Entity("BusinessObject.Models.RoomService", b =>
                {
                    b.Property<int>("RoomServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomServiceId"));

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TypeServiceId")
                        .HasColumnType("int");

                    b.HasKey("RoomServiceId");

                    b.HasIndex("RoomId");

                    b.HasIndex("TypeServiceId");

                    b.ToTable("RoomService");
                });

            modelBuilder.Entity("BusinessObject.Models.TypeService", b =>
                {
                    b.Property<int>("TypeServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeServiceID"));

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeServiceID");

                    b.ToTable("TypeService");
                });

            modelBuilder.Entity("BusinessObject.Models.BillPayment", b =>
                {
                    b.HasOne("BusinessObject.Models.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("BusinessObject.Models.BillPaymentDetail", b =>
                {
                    b.HasOne("BusinessObject.Models.BillPayment", "BillPayment")
                        .WithMany("Details")
                        .HasForeignKey("BillPaymentID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusinessObject.Models.RoomService", "RoomService")
                        .WithMany("BillPaymentDetails")
                        .HasForeignKey("RoomServiceID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("BillPayment");

                    b.Navigation("RoomService");
                });

            modelBuilder.Entity("BusinessObject.Models.Complain", b =>
                {
                    b.HasOne("BusinessObject.Models.Account", "ComplainAccount")
                        .WithMany("AccountComplain")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusinessObject.Models.Room", "Room")
                        .WithMany("Complains")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ComplainAccount");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BusinessObject.Models.Contract", b =>
                {
                    b.HasOne("BusinessObject.Models.Account", "OwnerAccount")
                        .WithMany("OwnerContract")
                        .HasForeignKey("OwnerAccountID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusinessObject.Models.Room", "Room")
                        .WithMany("RoomContract")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusinessObject.Models.Account", "StudentLeadAccount")
                        .WithMany("StudentContract")
                        .HasForeignKey("StudentAccountID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("OwnerAccount");

                    b.Navigation("Room");

                    b.Navigation("StudentLeadAccount");
                });

            modelBuilder.Entity("BusinessObject.Models.ContractMember", b =>
                {
                    b.HasOne("BusinessObject.Models.Contract", "Contract")
                        .WithMany("Members")
                        .HasForeignKey("ContractID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("BusinessObject.Models.Hostel", b =>
                {
                    b.HasOne("BusinessObject.Models.Account", "OwnerAccount")
                        .WithMany("Hostels")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("OwnerAccount");
                });

            modelBuilder.Entity("BusinessObject.Models.HostelImage", b =>
                {
                    b.HasOne("BusinessObject.Models.Hostel", "Hostel")
                        .WithMany("Images")
                        .HasForeignKey("HostelID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Hostel");
                });

            modelBuilder.Entity("BusinessObject.Models.MemberShipRegisterTransaction", b =>
                {
                    b.HasOne("BusinessObject.Models.Account", "OwnerAccount")
                        .WithMany("Memberships")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusinessObject.Models.MemberShip", "MemberShip")
                        .WithMany("MemberShipRegisterTransactions")
                        .HasForeignKey("MemberShipID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("MemberShip");

                    b.Navigation("OwnerAccount");
                });

            modelBuilder.Entity("BusinessObject.Models.Notice", b =>
                {
                    b.HasOne("BusinessObject.Models.Account", "ReceiveAccount")
                        .WithMany("AccountNoticeReceive")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusinessObject.Models.Account", "NoticeAccount")
                        .WithMany("AccountNotice")
                        .HasForeignKey("NoticeAccountAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NoticeAccount");

                    b.Navigation("ReceiveAccount");
                });

            modelBuilder.Entity("BusinessObject.Models.Room", b =>
                {
                    b.HasOne("BusinessObject.Models.Hostel", "Hostel")
                        .WithMany("Rooms")
                        .HasForeignKey("HostelID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Hostel");
                });

            modelBuilder.Entity("BusinessObject.Models.RoomAppointment", b =>
                {
                    b.HasOne("BusinessObject.Models.Room", "Room")
                        .WithMany("RoomAppointments")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessObject.Models.Account", "Viewer")
                        .WithMany("Appointments")
                        .HasForeignKey("ViewerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("Viewer");
                });

            modelBuilder.Entity("BusinessObject.Models.RoomImage", b =>
                {
                    b.HasOne("BusinessObject.Models.Room", "Room")
                        .WithMany("RoomImages")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BusinessObject.Models.RoomService", b =>
                {
                    b.HasOne("BusinessObject.Models.Room", "Room")
                        .WithMany("RoomServices")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessObject.Models.TypeService", "TypeService")
                        .WithMany("RoomServices")
                        .HasForeignKey("TypeServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("TypeService");
                });

            modelBuilder.Entity("BusinessObject.Models.Account", b =>
                {
                    b.Navigation("AccountComplain");

                    b.Navigation("AccountNotice");

                    b.Navigation("AccountNoticeReceive");

                    b.Navigation("Appointments");

                    b.Navigation("Hostels");

                    b.Navigation("Memberships");

                    b.Navigation("OwnerContract");

                    b.Navigation("StudentContract");
                });

            modelBuilder.Entity("BusinessObject.Models.BillPayment", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("BusinessObject.Models.Contract", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("BusinessObject.Models.Hostel", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("BusinessObject.Models.MemberShip", b =>
                {
                    b.Navigation("MemberShipRegisterTransactions");
                });

            modelBuilder.Entity("BusinessObject.Models.Room", b =>
                {
                    b.Navigation("Complains");

                    b.Navigation("RoomAppointments");

                    b.Navigation("RoomContract");

                    b.Navigation("RoomImages");

                    b.Navigation("RoomServices");
                });

            modelBuilder.Entity("BusinessObject.Models.RoomService", b =>
                {
                    b.Navigation("BillPaymentDetails");
                });

            modelBuilder.Entity("BusinessObject.Models.TypeService", b =>
                {
                    b.Navigation("RoomServices");
                });
#pragma warning restore 612, 618
        }
    }
}
