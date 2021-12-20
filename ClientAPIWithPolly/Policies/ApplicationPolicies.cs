using System;
using System.Net;
using System.Net.Http;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace ClientAPIWithPolly.Policies
{
    public static class ApplicationPolicies
    {
        public static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy.HandleResult<HttpResponseMessage>(msg => msg.StatusCode == HttpStatusCode.InternalServerError)
                         .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                          onRetry: (msg, timeSpan) =>
                          {
                              Console.ForegroundColor = ConsoleColor.Red;
                              Console.Out.WriteLineAsync($"Erro: {msg.Result.ReasonPhrase}. Nova tentativa em {timeSpan.Seconds} segundos");
                              Console.ForegroundColor = ConsoleColor.White;
                          });
        }

        public static AsyncCircuitBreakerPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return Policy.HandleResult<HttpResponseMessage>(msg => msg.StatusCode == HttpStatusCode.InternalServerError)
                         .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30),
                            onBreak: (_, _) =>
                            {
                                ShowCircuitState("Open (onBreak)", ConsoleColor.Red);
                            },
                            onReset: () =>
                            {
                                ShowCircuitState("Closed (onReset)", ConsoleColor.Green);
                            },
                            onHalfOpen: () =>
                            {
                                ShowCircuitState("Half Open (onHalfOpen)", ConsoleColor.Yellow);
                            });
        }

        private static void ShowCircuitState(
           string descStatus, ConsoleColor backgroundColor)
        {
            var previousBackgroundColor = Console.BackgroundColor;
            var previousForegroundColor = Console.ForegroundColor;

            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Out.WriteLine($" ***** Estado do Circuito: {descStatus} **** ");

            Console.BackgroundColor = previousBackgroundColor;
            Console.ForegroundColor = previousForegroundColor;
        }

    }
}
