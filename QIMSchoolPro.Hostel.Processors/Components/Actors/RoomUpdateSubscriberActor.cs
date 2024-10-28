using Akka.Event;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QIMSchoolPro.Hostel.Processors.Components.Messages;
using QIMSchoolPro.Hostel.Processors.Helpers;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Components.Actors
{


    public class RoomUpdateSubscriberActor : BaseActor
    {
        private readonly ILogger<RoomUpdateSubscriberActor> _logger; 
        private readonly IHubContext<BookingHub> _hubContext;
        private readonly IConnectionMultiplexer _redis;
        public RoomUpdateSubscriberActor( IHubContext<BookingHub> hubContext, IConnectionMultiplexer redis, ILogger<RoomUpdateSubscriberActor> logger)
        {


            _hubContext = hubContext;
            _redis = redis;
            _logger = logger;
            HandleMessages();
            
        }


        private void HandleMessages()
        {
            ReceiveAsync<BackgroundMessage>(OnStartBackgroundWorkerManager);
        }

        private async Task OnStartBackgroundWorkerManager(BackgroundMessage arg)
        {

            while (true)
            {
                await ExecuteAsync();
            }
        }

        private bool _isSubscribed = false;

        protected async Task ExecuteAsync()
        {
            if (!_isSubscribed)
            {
                var subscriber = _redis.GetSubscriber();
                await subscriber.SubscribeAsync("bed_bookings", async (channel, message) =>
                {
                    var bookingInfo = JsonConvert.DeserializeObject<dynamic>(message);
                    int roomId = bookingInfo.RoomId;
                    int bedId = bookingInfo.BedId;

                    try
                    {
                        await _hubContext.Clients.All.SendAsync("BedBooked", roomId, bedId);
                        // Log after the message has been sent
                        Console.WriteLine("Message sent successfully.");
                    }
                    catch (Exception ex)
                    {
                        // Log any exception that occurs
                        Console.WriteLine($"Error sending message: {ex.Message}");
                    }
                });

                _isSubscribed = true;
            }
        }


    }

}
