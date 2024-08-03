using FirestoreMaui.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirestoreMaui.Services
{
    public class FirestoreService
    {
        private FirestoreDb db;
        private async Task SetupFirestore()
        {
            if (db == null)
            {
                var stream = await FileSystem.OpenAppPackageFileAsync("admin-sdk.json");
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();

                db = new FirestoreDbBuilder
                {
                    ProjectId = "sample-project-14660",

                    ConverterRegistry = new ConverterRegistry
                    {
                        //new DateTimeToTimestampConverter(),
                    },
                    JsonCredentials = contents
                }.Build();
            }
        }
        public async Task InsertSampleModel(SampleModel sample)
        {
            await SetupFirestore();
            await db.Collection("SampleModels").AddAsync(sample);
        }
        public async Task<List<SampleModel>> GetSampleModels()
        {
            await SetupFirestore();
            var data = await db
                            .Collection("SampleModels")
                            .GetSnapshotAsync();
            var sampleModels = data.Documents
                .Select(doc =>
                {
                    var sampleModel = doc.ConvertTo<SampleModel>();
                    sampleModel.Id = doc.Id; // FirebaseId hinzufügen
                    return sampleModel;
                })
                .ToList();
            return sampleModels;
        }

    }
}
