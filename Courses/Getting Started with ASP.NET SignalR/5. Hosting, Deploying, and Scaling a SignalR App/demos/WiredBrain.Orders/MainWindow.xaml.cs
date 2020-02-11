using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.AspNet.SignalR.Client;

namespace WiredBrain.Orders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            var orders = new ObservableCollection<UpdateInfo>();
            OrderList.ItemsSource = orders;

            var queryStrings = new Dictionary<string, string>
            {
                { "group", "allUpdates" }
            };
            var hubConnection = new HubConnection("http://localhost:55774", queryStrings);
            var hubProxy = hubConnection.CreateHubProxy("CoffeeHub");

            hubProxy.On<UpdateInfo>("ReceiveOrderUpdate", updateObject =>
            {
                var order = orders.SingleOrDefault(c => c.OrderId == updateObject.OrderId);
                if (order != null)
                    Dispatcher.Invoke(() => orders.Remove(order));

                if (!updateObject.Finished)
                    Dispatcher.Invoke(() => orders.Add(updateObject));
            });

            await hubConnection.Start();
        }
    }
}
