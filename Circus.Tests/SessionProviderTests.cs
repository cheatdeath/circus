﻿using System;

namespace Circus.Tests
{
    public class SessionProviderTests
    {
        public SessionProviderTests()
        {
            Creation();
        }

        public void Creation()
        {
            var tod = DateTime.UtcNow.TimeOfDay;

            var ot = tod + TimeSpan.FromSeconds(15);
            var ct = ot - TimeSpan.FromSeconds(60);
            //var ts = new TradingSession(ot, ct);
            //ts.Changed += (sender, e) => Console.WriteLine("state=" + e);
        }
    }
}