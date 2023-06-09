﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UpdatePrice.Models.Entities.p01
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ExchangeDBEntities : DbContext
    {
        public ExchangeDBEntities()
            : base("name=ExchangeDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<PaymentService> PaymentServices { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AccessLevel> AccessLevels { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<DetailsTicket> DetailsTickets { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<ErrorMessage> ErrorMessages { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<LogForSignalr> LogForSignalrs { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PostHistory> PostHistories { get; set; }
        public virtual DbSet<RefralCommision> RefralCommisions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SellVoucherWithoutAuthentication> SellVoucherWithoutAuthentications { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<TblState> TblStates { get; set; }
        public virtual DbSet<Tether> Tethers { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<UserBankCard> UserBankCards { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSm> UserSms { get; set; }
        public virtual DbSet<UserVerifyToken> UserVerifyTokens { get; set; }
        public virtual DbSet<UserWallet> UserWallets { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<WithdrawDocument> WithdrawDocuments { get; set; }
        public virtual DbSet<Withdraw> Withdraws { get; set; }
        public virtual DbSet<PriceHistory> PriceHistories { get; set; }
    }
}
