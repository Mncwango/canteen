using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Canteen.Models
{
    public class MongoContext<T> : IDisposable where T : class
    {
        public String connectionString = "mongodb://localhost"; //its best to keep it in the config file not here...
        public String DataBaseName = "Canteen2";
        private string DocumentName = "";
        public MongoDatabase Database;

        //Initialize all the necessary objects to connect to MongoDb
        public MongoContext(string documentName)
        {
            this.DocumentName = documentName;
            var client = new MongoClient(connectionString);
            var server = client.GetServer();

            Database = server.GetDatabase(DataBaseName);
        }

        /// <summary>
        /// use this to get the collection of the document
        /// </summary>
        public MongoCollection<T> context
        {
            get
            {
                var Context = Database.GetCollection<T>(this.DocumentName);

                return Context;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MongoContext() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}