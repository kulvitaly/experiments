using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlobStorage
{
	class Program
	{
		// TODO: add your connection string here
		private static readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=vkupsstorage;AccountKey=5HQfhj46Ir42O9BYLRr/cqz6plZULrkCB0VL4PSUZrBPadj8c8W4n7eHVt13SxsiCwb/j3bb2jRUdgB7N3wRDg==;EndpointSuffix=core.windows.net";

		private static readonly string _blobContainerName = "authors";
		private static readonly string _blobName = "thomas.html";

		static async Task Main(string[] args)
		{
			try
			{
				var blobClient = await CreateContainerAndUploadBlobAsync();

				await SetBlobPropertiesAsync(blobClient);

				await GetBlobPropertiesAsync(blobClient);

				await SetBlobMetagataAsync(blobClient);

				await GetBlobMetadataAsync(blobClient);

				await ListContainersWithTheirBlobsAsync();

				await DowloadBlobAsync();

				Console.WriteLine();
				Console.WriteLine($"Press ENTER to delete blob container '{_blobContainerName}'");
				Console.ReadLine();

				await DeleteContainerAsync();
			}
			catch (RequestFailedException ex)
			{
				Console.WriteLine($"Error: {ex.ErrorCode}. " + ex);
			}
		}

		private static async Task GetBlobMetadataAsync(BlobClient blobClient)
		{
			Console.WriteLine($"6. Get blob metadata");

			var blobProperties = await blobClient.GetPropertiesAsync();

			foreach (var metadata in blobProperties.Value.Metadata)
			{
				Console.WriteLine($"    - {metadata.Key} : {metadata.Value}");
			}
		}

		private static async Task SetBlobMetagataAsync(BlobClient blobClient)
		{
			Console.WriteLine($"5. Set blob metadata");

			var metadata = new Dictionary<string, string>
			{
				["category"] = "author profile",
				["fullname"] = "VKU"
			};

			await blobClient.SetMetadataAsync(metadata);
		}

		private static async Task GetBlobPropertiesAsync(BlobClient blobClient)
		{
			Console.WriteLine($"4. Get blob properties");

			var properties = await blobClient.GetPropertiesAsync();

			Console.WriteLine($"   - Content type: {properties.Value.ContentType}");
			Console.WriteLine($"   - Blob type: {properties.Value.BlobType}");
			Console.WriteLine($"   - Created on: {properties.Value.CreatedOn}");
			Console.WriteLine($"   - Last modified: {properties.Value.LastModified}");
		}

		private static async Task SetBlobPropertiesAsync(BlobClient blobClient)
		{
			Console.WriteLine($"3. Set blob properties");

			var blobProperties = await blobClient.GetPropertiesAsync();

			BlobHttpHeaders headers = new()
			{
				ContentType = "text/html",
				ContentLanguage = "en-us",

				CacheControl = blobProperties.Value.CacheControl,
				ContentDisposition = blobProperties.Value.ContentDisposition,
				ContentEncoding = blobProperties.Value.ContentEncoding,
				ContentHash = blobProperties.Value.ContentHash
			};

			await blobClient.SetHttpHeadersAsync(headers);
		}

		private static async Task DowloadBlobAsync()
		{
			var localFileName = "downloaded.html";

			Console.WriteLine($"4. Downloading blob '{_blobName}' to local file '{localFileName}'");

			BlobClient blobClient = new(_connectionString, _blobContainerName, _blobName);

			bool exists = await blobClient.ExistsAsync();

			if (exists)
			{
				var blobDownloadInfo = await blobClient.DownloadContentAsync();

				using var fileStream = File.OpenWrite(localFileName);
				await blobDownloadInfo.Value.Content.ToStream().CopyToAsync(fileStream);
			}
		}

		private static async Task DeleteContainerAsync()
		{
			Console.WriteLine($"5. Deleting blob container '{_blobContainerName}'");

			var blobContainerClient = new BlobContainerClient(_connectionString, _blobContainerName);

			await blobContainerClient.DeleteIfExistsAsync();
		}

		private static async Task ListContainersWithTheirBlobsAsync()
		{
			BlobServiceClient blobServiceClient = new(_connectionString);

			Console.WriteLine($"3. Listing container and blobs of {blobServiceClient.AccountName} account");

			await foreach (var blobContainerItem in blobServiceClient.GetBlobContainersAsync())
			{
				Console.WriteLine($"	> {blobContainerItem.Name}");

				var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerItem.Name);

				await foreach (var blobItem in blobContainerClient.GetBlobsAsync())
				{
					Console.WriteLine($"		- {blobItem.Name}");
				}
			}
		}

		private static async Task<BlobClient> CreateContainerAndUploadBlobAsync()
		{
			// 1. Create the Blob Container
			BlobServiceClient blobServiceClient = new(_connectionString);

			var blobContainerClient = blobServiceClient.GetBlobContainerClient(_blobContainerName);

			Console.WriteLine($"1. Creating blob container '{_blobContainerName}'");

			await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

			// 2. Upload blob
			var blobClient = blobContainerClient.GetBlobClient(_blobName);
			Console.WriteLine($"2. Upload blob '{blobClient.Name}'");
			Console.WriteLine($"	> {blobClient.Uri}");

			using var fileStream = File.OpenRead("fileToUpload.html");
			await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = "text/html" });

			return blobClient;
		}
	}
}
