 using System;
  
 public static class Epoch
 {
     public static int Current()
     {
         DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
         int currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
  
         return currentEpochTime;
     }
  
     public static string GetTimerString(int timeLeft)
     {
         int minutes = timeLeft / 60;
         int seconds = timeLeft - (minutes * 60);
  
         string timer = (minutes < 10  ? "0" : "") + minutes + " : " + (seconds < 10 ? "0" : "") + seconds;
         
         return timer;
     }
 }
