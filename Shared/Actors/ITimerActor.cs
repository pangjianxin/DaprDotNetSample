using Dapr.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Actors;
public interface ITimerActor : IActor
{
    Task StartTimerAsync(string name, string text);
    Task StopTimerAsync(string name);
    Task DoAnotherThing(string message);
}