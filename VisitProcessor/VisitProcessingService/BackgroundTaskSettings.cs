﻿namespace VisitProcessor.VisitProcessingService
{
  
    public class BackgroundTaskSettings
    {
        public string ConnectionString { get; set; }

        public string EventBusConnection { get; set; }

        public string SubscriptionClientName { get; set; }
    }
    
}