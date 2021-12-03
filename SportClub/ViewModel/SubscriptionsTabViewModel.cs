using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;
using System.Collections.Generic;

namespace SportClub.ViewModel
{
    class SubscriptionsTabViewModel : ViewModelBase
    {
        public SportClubContext Context { get; }
        public Subscription SubscriptionInfo { get; set; } = new Subscription() { BuyDate = System.DateTime.Now };
        public Subscription SelectedSubscription { get; set; }

        public SubscriptionsTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.Subscriptions.Load();
            Context.Subscriptions.Include(t => t.Client);
            Context.Subscriptions.Include(t => t.Tariff);
            Context.Tariffs.Load();
        }

        private RelayCommand _addSubscriptionCommand;
        private RelayCommand _updateSubscriptionCommand;
        private RelayCommand _deleteSubscriptionCommand;
        private RelayCommand _subscriptionGridSelectionChangedCommand;

        public ICommand AddSubscriptionCommand =>
            _addSubscriptionCommand ??
            (_addSubscriptionCommand = new RelayCommand(
                () =>
                {
                    Context.Subscriptions.Add(new Subscription
                    {
                        Client = SubscriptionInfo.Client,
                        Tariff = SubscriptionInfo.Tariff,
                        VisitLeft = SubscriptionInfo.Tariff.VisitNumber,
                        PersonalTrainingLeft = SubscriptionInfo.Tariff.PersonalTraining,
                        GroupTrainingLeft = SubscriptionInfo.Tariff.GroupTraining,
                        BuyDate = SubscriptionInfo.BuyDate,
                        ValidityDate = SubscriptionInfo.BuyDate.AddDays(SubscriptionInfo.Tariff.Duration),
                        IsNotified = false
                    });
                    Context.SaveChanges();
                },
                () =>
                {
                    if (Equals(SubscriptionInfo.Client, null)
                        || Equals(SubscriptionInfo.Tariff, null)
                        || Equals(SubscriptionInfo.BuyDate, null)
                        )
                    {
                        return false;
                    }
                    if (!OtherVerifies())
                    {
                        return false;
                    }

                    return true;
                }));

        public ICommand UpdateSubscriptionCommand =>
            _updateSubscriptionCommand ??
            (_updateSubscriptionCommand = new RelayCommand(
                () =>
                {
                    SelectedSubscription.Client = SubscriptionInfo.Client;
                    SelectedSubscription.Tariff = SubscriptionInfo.Tariff;
                    SelectedSubscription.VisitLeft = SubscriptionInfo.Tariff.VisitNumber;
                    SelectedSubscription.PersonalTrainingLeft = SubscriptionInfo.Tariff.PersonalTraining;
                    SelectedSubscription.GroupTrainingLeft = SubscriptionInfo.Tariff.GroupTraining;
                    SelectedSubscription.BuyDate = SubscriptionInfo.BuyDate;
                    SelectedSubscription.ValidityDate = SubscriptionInfo.BuyDate.AddDays(SubscriptionInfo.Tariff.Duration);
                    SelectedSubscription.IsNotified = false;

                    Context.SaveChanges();
                },
                () =>
                {
                    if (SelectedSubscription == null) return false;
                    if (Equals(SubscriptionInfo.Client, null)
                        || Equals(SubscriptionInfo.Tariff, null)
                        || Equals(SubscriptionInfo.BuyDate, null)
                        )
                    {
                        return false;
                    }
                    if (!OtherVerifies() && (SelectedSubscription?.ClientId != SubscriptionInfo.Client.ClientId))
                    {
                        return false;
                    }

                    return true;
                }));

        public ICommand DeleteSubscriptionCommand =>
           _deleteSubscriptionCommand ??
           (_deleteSubscriptionCommand = new RelayCommand(
               () =>
               {
                   Context.Subscriptions.Remove(SelectedSubscription);
                   Context.SaveChanges();
               },
               () => SelectedSubscription != null));

        public ICommand SubscriptionGridSelectionChangedCommand =>
           _subscriptionGridSelectionChangedCommand ??
           (_subscriptionGridSelectionChangedCommand = new RelayCommand(
               () =>
               {
                   SubscriptionInfo.Client = SelectedSubscription.Client;
                   SubscriptionInfo.Tariff = SelectedSubscription.Tariff;
                   SubscriptionInfo.BuyDate = SelectedSubscription.BuyDate;
               },
               () => SelectedSubscription != null));


        public bool OtherVerifies()
        {
            var query1 = $"SELECT * FROM Subscriptions WHERE ClientId = {SubscriptionInfo.Client.ClientId}";

            var subscriptions = Context.Subscriptions.SqlQuery(query1).ToListAsync();

            return subscriptions.Result.Count < 1;
        }
    } 
}
