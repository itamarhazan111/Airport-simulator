
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;

namespace Simulator_New.Modules
{

    public class TaskManager
    {
        //    private ConcurrentDictionary<Guid, Task> _tasks = new ConcurrentDictionary<Guid, Task>();

        Task LandingTask { get; set; }
        Task DepratureTask { get; set; }
        private CancellationTokenSource LandingCts { get; set; }  // Define needed?
        private CancellationTokenSource DepartureCts { get; set; }  // Define needed?


        public async Task CreateNewTask(int timeInterverlMiliSec, bool landing)
        {
            if (landing)
            {
                // Cancel the existing LandingTask if it's running
                bool tryRemove = await EnsureTaskIsCompleteAndRemove(landing);
                if (!tryRemove)
                {
                    Console.WriteLine("Delete was not successful");
                    return;
                }
                else
                {
                    Console.WriteLine("Delete was Successful");
                }
                LandingCts = new CancellationTokenSource(); // Initialize it here
                LandingTask = Task.Run(async () =>
                {
                    using (var client = new HttpClient())
                    {
                        int i = 1;
                        int timeUntilChek = 1;
                        // Sending request to the other server.
                        while (true)
                        {

                            try
                            {
                                LandingCts?.Token.ThrowIfCancellationRequested();
                                client.GetAsync($"http://localhost:7072/api/Flights/land/f{i++}").Wait();
                                Console.WriteLine($"sent Land interval was {timeInterverlMiliSec}");
                                await Task.Delay(timeInterverlMiliSec);

                            }
                            catch (TaskCanceledException)
                            {
                                Console.WriteLine("The Landing task was canceled intentionally.");
                                return; // Exit from the loop and task
                            }
                            catch (OperationCanceledException)
                            {
                                Console.WriteLine("The Landing task was canceled intentionally.");
                                return; // Exit from the loop and task
                            }
                            catch (Exception x)
                            {
                                Console.WriteLine($"Server is sleeping, exception: {x.Message}");
                                await Task.Delay(timeUntilChek++ * 1000);
                            }
                        }
                    }
                }, LandingCts.Token);

            }
            else
            {
                // Cancel the existing LandingTask if it's running
                bool tryRemove = await DeleteTask(landing);
                if (!tryRemove)
                {
                    Console.WriteLine("Delete was not suscseful");
                    return;
                }
                else
                {
                    Console.WriteLine("Delete was Sucsseful");
                }

                DepartureCts = new CancellationTokenSource(); // Initialize it here

                DepratureTask = Task.Run(async () =>
                            {
                                using (var client = new HttpClient())
                                {
                                    int i = 1;
                                    int timeUntilChek = 1;
                                    // Sending request to the other server.
                                    while (true)
                                    {
                                        try
                                        {
                                            DepartureCts?.Token.ThrowIfCancellationRequested();
                                            client.GetAsync($"http://localhost:7072/api/Flights/departure/f{i++}").Wait();
                                            Console.WriteLine($"sent Deprature interval was {timeInterverlMiliSec}");
                                            await Task.Delay(timeInterverlMiliSec);
                                        }
                                        catch (TaskCanceledException)
                                        {
                                            Console.WriteLine("The Landing task was canceled intentionally.");
                                            return; // Exit from the loop and task
                                        }
                                        catch (OperationCanceledException)
                                        {
                                            Console.WriteLine("The Landing task was canceled intentionally.");
                                            return;
                                        }
                                        catch (Exception x)
                                        {
                                            Console.WriteLine($"Server is sleeping, exception: {x.Message}");
                                            await Task.Delay(timeUntilChek++ * 1000);
                                        }
                                    }
                                }
                            }, DepartureCts.Token);
            }
        }


        private async Task<bool> EnsureTaskIsCompleteAndRemove(bool isLanding)
        {
            Task taskToCheck;
            CancellationTokenSource cts;

            if (isLanding)
            {
                taskToCheck = LandingTask;
                cts = LandingCts;
            }
            else
            {
                taskToCheck = DepratureTask;
                cts = DepartureCts;
            }

            if (taskToCheck != null && !taskToCheck.IsCompleted && !taskToCheck.IsFaulted && !taskToCheck.IsCanceled)
            {
                cts?.Cancel();
                try
                {
                    await taskToCheck; // Wait for the task to finish
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine($"The task (landing?={isLanding}) was canceled intentionally.");
                    // Expected if the task is cancelled
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"an exeption occured in the shutdown of the task \n {ex.Message}");
                }
            }

            cts?.Dispose();
            return true;
        }

        public async Task<bool> DeleteTask(bool deleteLanding)
        {
            return await EnsureTaskIsCompleteAndRemove(deleteLanding);
        }
    }
}
