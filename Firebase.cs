using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace The_Missing_Pretzels
{
	class Firebase
	{
		public Firebase()
		{
			Debug.WriteLine("Instantiated new Firebase object");
		}

		public static string Get(string uri)
		{
			Task<string> getTask = Task.Run(async () =>
			{
				HttpClient httpClient = new HttpClient();

				Uri requestUri = new Uri(uri);

				HttpResponseMessage httpResponse = new HttpResponseMessage();
				string httpResponseBody = "";

				try
				{
					httpResponse = await httpClient.GetAsync(requestUri);
					httpResponse.EnsureSuccessStatusCode();
					httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
				}
				catch (Exception ex)
				{
					httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
				}

				return httpResponseBody;
			});

			return getTask.Result;
		}

		public static string Post(string uri, string data)
		{
			Task<string> postTask = Task.Run(async () =>
			{
				HttpClient httpClient = new HttpClient();

				Uri requestUri = new Uri(uri);

				HttpStringContent content = new HttpStringContent(data, UnicodeEncoding.Utf8, "application/json");

				string httpResponseBody = "";

				try
				{
					HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri, content);
					httpResponseMessage.EnsureSuccessStatusCode();
					httpResponseBody = await httpResponseMessage.Content.ReadAsStringAsync();
				}
				catch (Exception ex)
				{
					httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
				}

				return httpResponseBody;
			});

			return postTask.Result;
		}

		public static string Delete(string uri)
		{
			Task<string> deleteTask = Task.Run(async () =>
			{
				HttpClient httpClient = new HttpClient();

				Uri requestUri = new Uri(uri);

				HttpResponseMessage httpResponse = new HttpResponseMessage();
				string httpResponseBody = "";

				try
				{
					httpResponse = await httpClient.DeleteAsync(requestUri);
					httpResponse.EnsureSuccessStatusCode();
					httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
				}
				catch (Exception ex)
				{
					httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
				}

				return httpResponseBody;
			});

			return deleteTask.Result;
		}
	}
}