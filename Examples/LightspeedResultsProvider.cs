/*
Copyright (c) 2010 Stephen P Ward and Joseph E Feser

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Mindscape.LightSpeed;
using IntelliBox.Examples;

namespace Examples {

    public class LightspeedResultsProvider : IIntelliboxResultsProvider {

        private LightSpeedContext<NorthwindLightSpeedModelUnitOfWork> _unitOfWork;

        private NorthwindLightSpeedModelUnitOfWork UnitOfWork {
            get {
                if (_unitOfWork == null) {
                    _unitOfWork = GetNewContext();
                }
                return _unitOfWork.CreateUnitOfWork();
            }
        }

        public static LightSpeedContext<NorthwindLightSpeedModelUnitOfWork> GetNewContext() {
            var connString = "Data Source=Northwind.sdf";

            LightSpeedContext<NorthwindLightSpeedModelUnitOfWork> lsCtx = new LightSpeedContext<NorthwindLightSpeedModelUnitOfWork>();
            lsCtx.ConnectionString = connString;
            lsCtx.DataProvider = DataProvider.SqlServerCE;
            lsCtx.IdentityMethod = IdentityMethod.IdentityColumn;
            lsCtx.QuoteIdentifiers = true;
            //lsCtx.VerboseLogging = true;
            //lsCtx.Logger = new Mindscape.LightSpeed.Logging.TraceLogger();
            return lsCtx;
        }

        public IEnumerable<object> DoSearch(string searchTerm, int maxResults, object extraInfo) {
            //using (var uow = UnitOfWork) {
                //return UnitOfWork.Products.Where(p => p.ProductName
                //    .StartsWith(searchTerm)).Take(maxResults).Cast<object>();
                return UnitOfWork.Products.Where(p => p.ProductName
                  .StartsWith(searchTerm)).Cast<object>();
            //}
        }
    }
}
