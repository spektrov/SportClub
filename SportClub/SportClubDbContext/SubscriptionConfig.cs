using System.Data.Entity.ModelConfiguration;
using SportClub.Model;


namespace SportClub.SportClubDbContext
{
    class SubscriptionConfig : EntityTypeConfiguration<Subscription>
    {
        public SubscriptionConfig()
        {
            HasKey(subscription => subscription.SubscriptionId);
            HasRequired(subscription => subscription.Client).WithMany(client => client.Subscriptions).HasForeignKey(s => s.ClientId).WillCascadeOnDelete(true);
            HasRequired(subscription => subscription.Tariff).WithMany(tariff => tariff.Subscriptions).HasForeignKey(s => s.TariffId).WillCascadeOnDelete(true);
            Property(subscription => subscription.VisitLeft).IsRequired();
            Property(subscription => subscription.PersonalTrainingLeft).IsRequired();
            Property(subscription => subscription.GroupTrainingLeft).IsRequired();
            Property(subscription => subscription.BuyDate).HasColumnType("datetime2").IsRequired();
            Property(subscription => subscription.ValidityDate).HasColumnType("datetime2").IsRequired();
            Property(subscription => subscription.IsNotified).IsRequired();

            ToTable("Subscriptions");
        }
    }
}
